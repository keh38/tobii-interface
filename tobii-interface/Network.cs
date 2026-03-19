using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Serilog;

using KLib;
using KLib.IO;
using KLib.Net;
using Tobii.Research;

namespace tobii_interface
{
    internal class Network : IDisposable
    {
        private bool _disposed = false;
        private IPEndPoint EndPoint { get; set; }

        private CancellationTokenSource _serverCancellationToken = null;
        private MainForm _mainForm;

        private DiscoveryBeacon _discoveryBeacon;

        public Network(MainForm mainForm)
        {
            EndPoint = Discovery.FindNextAvailableEndPoint();
            _mainForm = mainForm;
        }

        public void Disconnect()
        {
            _discoveryBeacon?.Stop();
            _serverCancellationToken?.Cancel();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                Disconnect();                        // logical shutdown first
                _serverCancellationToken?.Dispose(); // then release the CTS handle
                _discoveryBeacon?.Dispose();            // if it's IDisposable
            }

            _disposed = true;
        }

        public void StartServer()
        {
            _discoveryBeacon = new DiscoveryBeacon(
                "TOBII.INTERFACE",
                EndPoint.Address.ToString(),
                EndPoint.Port);

            _discoveryBeacon.Start();

            _serverCancellationToken = new CancellationTokenSource();
            Task.Run(() =>
            {
                Listener(EndPoint, _serverCancellationToken.Token);
            }, _serverCancellationToken.Token);
        }

        private void Listener(IPEndPoint endpoint, CancellationToken ct)
        {
            var server = new KTcpListener();
            server.StartListener(endpoint);

            Debug.WriteLine($"TCP server started on {endpoint}");

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    if (server.Pending())
                    {
                        ProcessTCPMessage(server);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            server.CloseListener();
            Debug.WriteLine("TCP server stopped");
        }

        private void ProcessTCPMessage(KTcpListener server)
        {
            // convert from us to 100-ns ticks for consistency with HighPrecisionClock
            var receiveTime = 10 * EyeTrackingOperations.GetSystemTimeStamp();

            server.AcceptTcpClient();
            var request = server.ReadRequest();

            try
            {
                switch (request.Command)
                {
                    case "Record":
                        server.WriteResponse(TcpMessage.Ok());
                        var filename = request.GetPayload<string>();
                        _ = Task.Run(() => _mainForm.StartRecordingRemote(filename.Replace(Path.GetExtension(filename), ".tsr")));
                        break;
                    case "Stop":
                        server.WriteResponse(TcpMessage.Ok());
                        _mainForm.StopRecordingRemote();
                        break;
                    case "Ping":
                        server.WriteResponse(TcpMessage.Ok());
                        break;
                    case "Status":
                        server.WriteResponse(TcpMessage.Ok(new DataStreamStatusPayload()
                        {
                            Status = (int)_mainForm.Status
                        }));
                        break;
                    case "Sync":
                        var clockSyncData = new ClockSyncPayload()
                        {
                            T1 = receiveTime,
                            T2 = 10 * EyeTrackingOperations.GetSystemTimeStamp()
                        };
                        server.WriteResponse(TcpMessage.Ok(clockSyncData));
                        break;
                    case "GetLog":
                        var logFilePayload = new TextFilePayload()
                        {
                            Filename = Path.GetFileName(_mainForm.LogPath),
                            Content = Files.ReadAllTextShared(_mainForm.LogPath)
                        };
                        server.WriteResponse(TcpMessage.Ok(logFilePayload));
                        break;

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error processing command {Command}", request.Command);
                server.WriteResponse(TcpMessage.Error(ex.Message)); // if your protocol supports it
            }
            finally
            {
                server.CloseTcpClient();
            }
        }
    }
}
