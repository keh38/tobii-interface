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
using KLib.Net;
using Tobii.Research;

namespace tobii_interface
{
    internal class Network
    {

        private IPEndPoint EndPoint { get; set; }

        private CancellationTokenSource _serverCancellationToken = null;
        private MainForm _mainForm;

        public Network(MainForm mainForm)
        {
            EndPoint = Discovery.FindNextAvailableEndPoint();
            _mainForm = mainForm;
        }

        public void Disconnect()
        {
            if (_serverCancellationToken != null)
            {
                _serverCancellationToken.Cancel();
            }
        }

        public void StartDiscoveryServer()
        {
            _serverCancellationToken = new CancellationTokenSource();
            Task.Run(() =>
            {
                Listener(EndPoint, _serverCancellationToken.Token);
            }, _serverCancellationToken.Token);

            Task.Run(() =>
            {
                MulticastReceiver("TOBII.INTERFACE", EndPoint, _serverCancellationToken.Token);
            }, _serverCancellationToken.Token);
        }

        private void Listener(IPEndPoint endpoint, CancellationToken ct)
        {
            var server = new KTcpListener();
            server.StartListener(endpoint);

            Debug.WriteLine($"TCP server started on {server.ListeningOn}");

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
            //            var receiveTime = HighPrecisionClock.UtcNowIn100nsTicks;

            // convert from us to 100-ns ticks for consistency with HighPrecisionClock
            var receiveTime = 10 * EyeTrackingOperations.GetSystemTimeStamp();

            server.AcceptTcpClient();

            string input = server.ReadString(acknowledge: false);

            var parts = input.Split(new char[] { ':' }, 2);
            string command = parts[0];
            string data = "";
            if (parts.Length > 1)
            {
                data = parts[1];
            }

            switch (command)
            {
                case "Record":
                    server.SendAcknowledgement();
                    _mainForm.StartRecordingRemote(data.Replace(Path.GetExtension(data), ".tsr"));
                    break;
                case "Stop":
                    server.SendAcknowledgement();
                    _mainForm.StopRecordingRemote();
                    break;
                case "Status":
                    Debug.WriteLine($"sending status {_mainForm.Status}");
                    server.WriteInt32ToOutputStream(_mainForm.Status);
                    break;
                case "Sync":
                    var byteArray = new byte[16];
//                    Buffer.BlockCopy(new long[] { receiveTime, HighPrecisionClock.UtcNowIn100nsTicks }, 0, byteArray, 0, 16);
                    Buffer.BlockCopy(new long[] { receiveTime, EyeTrackingOperations.GetSystemTimeStamp() }, 0, byteArray, 0, 16);
                    server.WriteByteArray(byteArray);
                    break;
            }

            server.CloseTcpClient();
        }

        private void MulticastReceiver(string name, IPEndPoint endpoint, CancellationToken ct)
        {
            var ipLocal = new IPEndPoint(endpoint.Address, 10000);

            var address = IPAddress.Parse("234.5.6.7");
            var ipEndPoint = new IPEndPoint(address, 10000);

            var udp = new UdpClient();
            udp.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udp.Client.Bind(ipLocal);
            udp.Client.ReceiveTimeout = 1000;

            udp.JoinMulticastGroup(address, endpoint.Address);
            Debug.WriteLine($"listening to multicast on {endpoint.Address}");

            var anyIP = new IPEndPoint(IPAddress.Any, 0);

            while (!ct.IsCancellationRequested)
            {
                try
                {
                    // receive bytes
                    var bytes = udp.Receive(ref anyIP);
                    var response = Encoding.Default.GetString(bytes);
 
                    if (response.Equals(name))
                    {
                        bytes = Encoding.UTF8.GetBytes(endpoint.Port.ToString());
                        udp.Send(bytes, bytes.Length, anyIP);
                    }
                }
                catch (Exception ex)
                {
                    //Debug.WriteLine(ex.Message);
                }
            }
        }

    }
}
