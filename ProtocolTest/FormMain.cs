using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

namespace ProtocolTest {
	public partial class FormMain : Form {
		private static readonly char[] Separator = new char[] { ' ' };

		private SerialPort Port;
		private Label[] GraphBars = new Label[16];
		private Action ActionDataReceived;
		private FPlayProtocol Protocol;

		public FormMain() {
			InitializeComponent();

			GraphBars[0] = lblGraph0;
			GraphBars[1] = lblGraph1;
			GraphBars[2] = lblGraph2;
			GraphBars[3] = lblGraph3;
			GraphBars[4] = lblGraph4;
			GraphBars[5] = lblGraph5;
			GraphBars[6] = lblGraph6;
			GraphBars[7] = lblGraph7;
			GraphBars[8] = lblGraph8;
			GraphBars[9] = lblGraph9;
			GraphBars[10] = lblGraph10;
			GraphBars[11] = lblGraph11;
			GraphBars[12] = lblGraph12;
			GraphBars[13] = lblGraph13;
			GraphBars[14] = lblGraph14;
			GraphBars[15] = lblGraph15;

			ActionDataReceived = new Action(DataReceived);
			Protocol = new FPlayProtocol();
		}

		protected override void OnFormClosing(FormClosingEventArgs e) {
			if (Port != null) {
				Port.Close();
				Port.Dispose();
			}
			base.OnFormClosing(e);
		}

		private void btnOpenCOM_Click(object sender, EventArgs e) {
			try {
				if (Port == null) {
					Protocol.Reset();
					txtConsole.Clear();

					Port = new SerialPort("COM" + txtCOM.Text.Trim()) {
						BaudRate = 115200,
						DataBits = 8,
						DiscardNull = false,
						DtrEnable = false,
						Handshake = Handshake.None,
						Parity = Parity.None,
						ReadBufferSize = 1024,
						ReadTimeout = 1000,
						ReceivedBytesThreshold = 1,
						RtsEnable = false,
						StopBits = StopBits.One,
						WriteBufferSize = 1024,
						WriteTimeout = 1000
					};
					Port.DataReceived += Port_DataReceived;
					Port.Open();
					AcceptButton = btnSend;
					btnSend.Enabled = true;
					cbCommand.Enabled = true;
					lblCOM.Enabled = false;
					txtCOM.Enabled = false;
					btnOpenCOM.Text = "Close COM";
				} else {
					Port.Close();
					Port.Dispose();
					Port = null;
					AcceptButton = btnOpenCOM;
					btnSend.Enabled = false;
					cbCommand.Enabled = false;
					lblCOM.Enabled = true;
					txtCOM.Enabled = true;
					btnOpenCOM.Text = "Open COM";
				}
			} catch (Exception ex) {
				Port = null;
				MessageBox.Show(ex.Message, "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSend_Click(object sender, EventArgs e) {
			switch (cbCommand.SelectedIndex) {
				case 0:
					Protocol.StartBinTransmission(Port);
					break;
				case 1:
					Protocol.StopBinTransmission(Port);
					break;
				case 2:
					Protocol.Previous(Port);
					break;
				case 3:
					Protocol.PlayPause(Port);
					break;
				case 4:
					Protocol.Next(Port);
					break;
				case 5:
					Protocol.Play(Port);
					break;
				case 6:
					Protocol.Pause(Port);
					break;
				case 7:
					Protocol.IncreaseVolume(Port);
					break;
				case 8:
					Protocol.DecreaseVolume(Port);
					break;
			}
		}

		private void chkEnableGraph_CheckedChanged(object sender, EventArgs e) {
			Protocol.GraphActive = chkEnableGraph.Checked;
		}

		private void DataReceived() {
			int i, min, receivedBins = Protocol.DataReceived(Port);
			if (receivedBins > 0) {
				min = Math.Min(16, receivedBins);
				for (i = 0; i < min; i++) {
					uint uh = Protocol.ReceiveBuffer[i];
					int h = (int)uh;
					GraphBars[i].Height = h;
					GraphBars[i].Top = 78 + (255 - h);
				}
				for (; i < 16; i++) {
					GraphBars[i].Height = 0;
					GraphBars[i].Top = 78 + (255 - 0);
				}
			} else if (receivedBins < 0) {
				txtConsole.Text = Protocol.Console.ToString();
			}
		}

		private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e) {
			Invoke(ActionDataReceived);
		}
	}
}
