namespace ProtocolTest
{
	partial class FormMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSend = new System.Windows.Forms.Button();
			this.txtConsole = new System.Windows.Forms.TextBox();
			this.btnOpenCOM = new System.Windows.Forms.Button();
			this.txtCOM = new System.Windows.Forms.TextBox();
			this.lblCOM = new System.Windows.Forms.Label();
			this.lblGraph0 = new System.Windows.Forms.Label();
			this.lblGraph1 = new System.Windows.Forms.Label();
			this.lblGraph3 = new System.Windows.Forms.Label();
			this.lblGraph2 = new System.Windows.Forms.Label();
			this.lblGraph7 = new System.Windows.Forms.Label();
			this.lblGraph6 = new System.Windows.Forms.Label();
			this.lblGraph5 = new System.Windows.Forms.Label();
			this.lblGraph4 = new System.Windows.Forms.Label();
			this.lblGraph15 = new System.Windows.Forms.Label();
			this.lblGraph14 = new System.Windows.Forms.Label();
			this.lblGraph13 = new System.Windows.Forms.Label();
			this.lblGraph12 = new System.Windows.Forms.Label();
			this.lblGraph11 = new System.Windows.Forms.Label();
			this.lblGraph10 = new System.Windows.Forms.Label();
			this.lblGraph9 = new System.Windows.Forms.Label();
			this.lblGraph8 = new System.Windows.Forms.Label();
			this.lblGraphBg = new System.Windows.Forms.Label();
			this.chkEnableGraph = new System.Windows.Forms.CheckBox();
			this.cbCommand = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnSend
			// 
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(12, 12);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(142, 28);
			this.btnSend.TabIndex = 5;
			this.btnSend.Text = "Send Command";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// txtConsole
			// 
			this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtConsole.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtConsole.Location = new System.Drawing.Point(310, 72);
			this.txtConsole.Multiline = true;
			this.txtConsole.Name = "txtConsole";
			this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtConsole.Size = new System.Drawing.Size(362, 287);
			this.txtConsole.TabIndex = 6;
			this.txtConsole.WordWrap = false;
			// 
			// btnOpenCOM
			// 
			this.btnOpenCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpenCOM.Location = new System.Drawing.Point(568, 12);
			this.btnOpenCOM.Name = "btnOpenCOM";
			this.btnOpenCOM.Size = new System.Drawing.Size(104, 28);
			this.btnOpenCOM.TabIndex = 2;
			this.btnOpenCOM.Text = "Open COM";
			this.btnOpenCOM.UseVisualStyleBackColor = true;
			this.btnOpenCOM.Click += new System.EventHandler(this.btnOpenCOM_Click);
			// 
			// txtCOM
			// 
			this.txtCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCOM.Location = new System.Drawing.Point(505, 17);
			this.txtCOM.Name = "txtCOM";
			this.txtCOM.Size = new System.Drawing.Size(57, 20);
			this.txtCOM.TabIndex = 0;
			this.txtCOM.Text = "7";
			// 
			// lblCOM
			// 
			this.lblCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCOM.AutoSize = true;
			this.lblCOM.Location = new System.Drawing.Point(468, 20);
			this.lblCOM.Name = "lblCOM";
			this.lblCOM.Size = new System.Drawing.Size(31, 13);
			this.lblCOM.TabIndex = 1;
			this.lblCOM.Text = "COM";
			// 
			// lblGraph0
			// 
			this.lblGraph0.BackColor = System.Drawing.Color.Red;
			this.lblGraph0.Location = new System.Drawing.Point(12, 78);
			this.lblGraph0.Name = "lblGraph0";
			this.lblGraph0.Size = new System.Drawing.Size(16, 255);
			this.lblGraph0.TabIndex = 7;
			// 
			// lblGraph1
			// 
			this.lblGraph1.BackColor = System.Drawing.Color.Red;
			this.lblGraph1.Location = new System.Drawing.Point(30, 78);
			this.lblGraph1.Name = "lblGraph1";
			this.lblGraph1.Size = new System.Drawing.Size(16, 255);
			this.lblGraph1.TabIndex = 8;
			// 
			// lblGraph3
			// 
			this.lblGraph3.BackColor = System.Drawing.Color.Red;
			this.lblGraph3.Location = new System.Drawing.Point(66, 78);
			this.lblGraph3.Name = "lblGraph3";
			this.lblGraph3.Size = new System.Drawing.Size(16, 255);
			this.lblGraph3.TabIndex = 10;
			// 
			// lblGraph2
			// 
			this.lblGraph2.BackColor = System.Drawing.Color.Red;
			this.lblGraph2.Location = new System.Drawing.Point(48, 78);
			this.lblGraph2.Name = "lblGraph2";
			this.lblGraph2.Size = new System.Drawing.Size(16, 255);
			this.lblGraph2.TabIndex = 9;
			// 
			// lblGraph7
			// 
			this.lblGraph7.BackColor = System.Drawing.Color.Red;
			this.lblGraph7.Location = new System.Drawing.Point(138, 78);
			this.lblGraph7.Name = "lblGraph7";
			this.lblGraph7.Size = new System.Drawing.Size(16, 255);
			this.lblGraph7.TabIndex = 14;
			// 
			// lblGraph6
			// 
			this.lblGraph6.BackColor = System.Drawing.Color.Red;
			this.lblGraph6.Location = new System.Drawing.Point(120, 78);
			this.lblGraph6.Name = "lblGraph6";
			this.lblGraph6.Size = new System.Drawing.Size(16, 255);
			this.lblGraph6.TabIndex = 13;
			// 
			// lblGraph5
			// 
			this.lblGraph5.BackColor = System.Drawing.Color.Red;
			this.lblGraph5.Location = new System.Drawing.Point(102, 78);
			this.lblGraph5.Name = "lblGraph5";
			this.lblGraph5.Size = new System.Drawing.Size(16, 255);
			this.lblGraph5.TabIndex = 12;
			// 
			// lblGraph4
			// 
			this.lblGraph4.BackColor = System.Drawing.Color.Red;
			this.lblGraph4.Location = new System.Drawing.Point(84, 78);
			this.lblGraph4.Name = "lblGraph4";
			this.lblGraph4.Size = new System.Drawing.Size(16, 255);
			this.lblGraph4.TabIndex = 11;
			// 
			// lblGraph15
			// 
			this.lblGraph15.BackColor = System.Drawing.Color.Red;
			this.lblGraph15.Location = new System.Drawing.Point(282, 78);
			this.lblGraph15.Name = "lblGraph15";
			this.lblGraph15.Size = new System.Drawing.Size(16, 255);
			this.lblGraph15.TabIndex = 22;
			// 
			// lblGraph14
			// 
			this.lblGraph14.BackColor = System.Drawing.Color.Red;
			this.lblGraph14.Location = new System.Drawing.Point(264, 78);
			this.lblGraph14.Name = "lblGraph14";
			this.lblGraph14.Size = new System.Drawing.Size(16, 255);
			this.lblGraph14.TabIndex = 21;
			// 
			// lblGraph13
			// 
			this.lblGraph13.BackColor = System.Drawing.Color.Red;
			this.lblGraph13.Location = new System.Drawing.Point(246, 78);
			this.lblGraph13.Name = "lblGraph13";
			this.lblGraph13.Size = new System.Drawing.Size(16, 255);
			this.lblGraph13.TabIndex = 20;
			// 
			// lblGraph12
			// 
			this.lblGraph12.BackColor = System.Drawing.Color.Red;
			this.lblGraph12.Location = new System.Drawing.Point(228, 78);
			this.lblGraph12.Name = "lblGraph12";
			this.lblGraph12.Size = new System.Drawing.Size(16, 255);
			this.lblGraph12.TabIndex = 19;
			// 
			// lblGraph11
			// 
			this.lblGraph11.BackColor = System.Drawing.Color.Red;
			this.lblGraph11.Location = new System.Drawing.Point(210, 78);
			this.lblGraph11.Name = "lblGraph11";
			this.lblGraph11.Size = new System.Drawing.Size(16, 255);
			this.lblGraph11.TabIndex = 18;
			// 
			// lblGraph10
			// 
			this.lblGraph10.BackColor = System.Drawing.Color.Red;
			this.lblGraph10.Location = new System.Drawing.Point(192, 78);
			this.lblGraph10.Name = "lblGraph10";
			this.lblGraph10.Size = new System.Drawing.Size(16, 255);
			this.lblGraph10.TabIndex = 17;
			// 
			// lblGraph9
			// 
			this.lblGraph9.BackColor = System.Drawing.Color.Red;
			this.lblGraph9.Location = new System.Drawing.Point(174, 78);
			this.lblGraph9.Name = "lblGraph9";
			this.lblGraph9.Size = new System.Drawing.Size(16, 255);
			this.lblGraph9.TabIndex = 16;
			// 
			// lblGraph8
			// 
			this.lblGraph8.BackColor = System.Drawing.Color.Red;
			this.lblGraph8.Location = new System.Drawing.Point(156, 78);
			this.lblGraph8.Name = "lblGraph8";
			this.lblGraph8.Size = new System.Drawing.Size(16, 255);
			this.lblGraph8.TabIndex = 15;
			// 
			// lblGraphBg
			// 
			this.lblGraphBg.BackColor = System.Drawing.Color.Black;
			this.lblGraphBg.Location = new System.Drawing.Point(4, 72);
			this.lblGraphBg.Name = "lblGraphBg";
			this.lblGraphBg.Size = new System.Drawing.Size(300, 267);
			this.lblGraphBg.TabIndex = 23;
			// 
			// chkEnableGraph
			// 
			this.chkEnableGraph.AutoSize = true;
			this.chkEnableGraph.Location = new System.Drawing.Point(7, 345);
			this.chkEnableGraph.Name = "chkEnableGraph";
			this.chkEnableGraph.Size = new System.Drawing.Size(120, 17);
			this.chkEnableGraph.TabIndex = 24;
			this.chkEnableGraph.Text = "Enable Visualization";
			this.chkEnableGraph.UseVisualStyleBackColor = true;
			this.chkEnableGraph.CheckedChanged += new System.EventHandler(this.chkEnableGraph_CheckedChanged);
			// 
			// cbCommand
			// 
			this.cbCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCommand.Enabled = false;
			this.cbCommand.FormattingEnabled = true;
			this.cbCommand.Items.AddRange(new object[] {
            "Start Bin Transmission",
            "Stop Bin Transmission",
            "Previous",
            "Play/Pause",
            "Next",
            "Play",
            "Pause",
            "Increase Volume",
            "Decrease Volume"});
			this.cbCommand.Location = new System.Drawing.Point(159, 16);
			this.cbCommand.Name = "cbCommand";
			this.cbCommand.Size = new System.Drawing.Size(177, 21);
			this.cbCommand.TabIndex = 25;
			// 
			// FormMain
			// 
			this.AcceptButton = this.btnOpenCOM;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 371);
			this.Controls.Add(this.cbCommand);
			this.Controls.Add(this.chkEnableGraph);
			this.Controls.Add(this.lblGraph15);
			this.Controls.Add(this.lblGraph14);
			this.Controls.Add(this.lblGraph13);
			this.Controls.Add(this.lblGraph12);
			this.Controls.Add(this.lblGraph11);
			this.Controls.Add(this.lblGraph10);
			this.Controls.Add(this.lblGraph9);
			this.Controls.Add(this.lblGraph8);
			this.Controls.Add(this.lblGraph7);
			this.Controls.Add(this.lblGraph6);
			this.Controls.Add(this.lblGraph5);
			this.Controls.Add(this.lblGraph4);
			this.Controls.Add(this.lblGraph3);
			this.Controls.Add(this.lblGraph2);
			this.Controls.Add(this.lblGraph1);
			this.Controls.Add(this.lblGraph0);
			this.Controls.Add(this.lblCOM);
			this.Controls.Add(this.txtCOM);
			this.Controls.Add(this.btnOpenCOM);
			this.Controls.Add(this.txtConsole);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.lblGraphBg);
			this.MinimumSize = new System.Drawing.Size(700, 410);
			this.Name = "FormMain";
			this.Text = "Protocol Test";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox txtConsole;
		private System.Windows.Forms.Button btnOpenCOM;
		private System.Windows.Forms.TextBox txtCOM;
		private System.Windows.Forms.Label lblCOM;
		private System.Windows.Forms.Label lblGraph0;
		private System.Windows.Forms.Label lblGraph1;
		private System.Windows.Forms.Label lblGraph3;
		private System.Windows.Forms.Label lblGraph2;
		private System.Windows.Forms.Label lblGraph7;
		private System.Windows.Forms.Label lblGraph6;
		private System.Windows.Forms.Label lblGraph5;
		private System.Windows.Forms.Label lblGraph4;
		private System.Windows.Forms.Label lblGraph15;
		private System.Windows.Forms.Label lblGraph14;
		private System.Windows.Forms.Label lblGraph13;
		private System.Windows.Forms.Label lblGraph12;
		private System.Windows.Forms.Label lblGraph11;
		private System.Windows.Forms.Label lblGraph10;
		private System.Windows.Forms.Label lblGraph9;
		private System.Windows.Forms.Label lblGraph8;
		private System.Windows.Forms.Label lblGraphBg;
		private System.Windows.Forms.CheckBox chkEnableGraph;
		private System.Windows.Forms.ComboBox cbCommand;
	}
}

