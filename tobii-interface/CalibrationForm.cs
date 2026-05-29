using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KLib.Net;

namespace tobii_interface
{
    internal partial class CalibrationForm : Form
    {
        Network _network;
        IPEndPoint _htsEndPoint;

        public CalibrationForm(Network network, IPEndPoint htsEndPoint)
        {
            _network = network;
            _htsEndPoint = htsEndPoint;

            InitializeComponent();
        }

        private void CalibrationForm_Shown(object sender, EventArgs e)
        {
            _network.OnStopCalibration += HandleStopCalibration;
            connectionLabel.Text = $"Connected to {_htsEndPoint.Address}:{_htsEndPoint.Port}";
            statusLabel.Text = "Initializing...";
        }

        private void CalibrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _network.OnStopCalibration -= HandleStopCalibration;
        }

        private void HandleStopCalibration()
        {
            KTcpClient.SendRequest(_htsEndPoint, TcpMessage.Request("Finish"));
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            HandleStopCalibration();
        }
    }
}
