namespace ChessProject
{
    partial class ControlPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbThongBao = new System.Windows.Forms.Label();
            this.gbCommand = new System.Windows.Forms.GroupBox();
            this.btCommand = new System.Windows.Forms.Button();
            this.cbFuct = new System.Windows.Forms.ComboBox();
            this.btReset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTDKT = new System.Windows.Forms.Label();
            this.lblSBD = new System.Windows.Forms.Label();
            this.lblTDBD = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.gbCommand.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.lbThongBao);
            this.groupBox1.Location = new System.Drawing.Point(7, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(235, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông báo";
            // 
            // lbThongBao
            // 
            this.lbThongBao.AllowDrop = true;
            this.lbThongBao.AutoEllipsis = true;
            this.lbThongBao.AutoSize = true;
            this.lbThongBao.Location = new System.Drawing.Point(6, 18);
            this.lbThongBao.MaximumSize = new System.Drawing.Size(230, 30);
            this.lbThongBao.Name = "lbThongBao";
            this.lbThongBao.Size = new System.Drawing.Size(59, 13);
            this.lbThongBao.TabIndex = 0;
            this.lbThongBao.Text = "Thông báo";
            // 
            // gbCommand
            // 
            this.gbCommand.Controls.Add(this.btCommand);
            this.gbCommand.Controls.Add(this.cbFuct);
            this.gbCommand.Location = new System.Drawing.Point(7, 72);
            this.gbCommand.Name = "gbCommand";
            this.gbCommand.Size = new System.Drawing.Size(235, 105);
            this.gbCommand.TabIndex = 1;
            this.gbCommand.TabStop = false;
            this.gbCommand.Text = "Bản điều khiển";
            // 
            // btCommand
            // 
            this.btCommand.Location = new System.Drawing.Point(152, 21);
            this.btCommand.Name = "btCommand";
            this.btCommand.Size = new System.Drawing.Size(75, 23);
            this.btCommand.TabIndex = 1;
            this.btCommand.Text = "Bắt đầu";
            this.btCommand.UseVisualStyleBackColor = true;
            // 
            // cbFuct
            // 
            this.cbFuct.FormattingEnabled = true;
            this.cbFuct.Location = new System.Drawing.Point(10, 22);
            this.cbFuct.Name = "cbFuct";
            this.cbFuct.Size = new System.Drawing.Size(121, 21);
            this.cbFuct.TabIndex = 0;
            // 
            // btReset
            // 
            this.btReset.Location = new System.Drawing.Point(159, 305);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(75, 23);
            this.btReset.TabIndex = 2;
            this.btReset.Text = "Restart";
            this.btReset.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTDKT);
            this.groupBox2.Controls.Add(this.lblSBD);
            this.groupBox2.Controls.Add(this.lblTDBD);
            this.groupBox2.Location = new System.Drawing.Point(7, 183);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 97);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin";
            // 
            // lblTDKT
            // 
            this.lblTDKT.AutoSize = true;
            this.lblTDKT.Location = new System.Drawing.Point(7, 49);
            this.lblTDKT.Name = "lblTDKT";
            this.lblTDKT.Size = new System.Drawing.Size(87, 13);
            this.lblTDKT.TabIndex = 2;
            this.lblTDKT.Text = "Tọa độ kết thúc:";
            // 
            // lblSBD
            // 
            this.lblSBD.AutoSize = true;
            this.lblSBD.Location = new System.Drawing.Point(32, 73);
            this.lblSBD.Name = "lblSBD";
            this.lblSBD.Size = new System.Drawing.Size(62, 13);
            this.lblSBD.TabIndex = 1;
            this.lblSBD.Text = "Số bước đi:";
            // 
            // lblTDBD
            // 
            this.lblTDBD.AutoSize = true;
            this.lblTDBD.Location = new System.Drawing.Point(9, 25);
            this.lblTDBD.Name = "lblTDBD";
            this.lblTDBD.Size = new System.Drawing.Size(88, 13);
            this.lblTDBD.TabIndex = 0;
            this.lblTDBD.Text = "Tọa độ bắt đầu: ";
            // 
            // ControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.gbCommand);
            this.Controls.Add(this.groupBox1);
            this.Name = "ControlPanel";
            this.Size = new System.Drawing.Size(283, 464);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbCommand.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lbThongBao;
        public System.Windows.Forms.GroupBox gbCommand;
        public System.Windows.Forms.Button btCommand;
        public System.Windows.Forms.Button btReset;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Label lblTDKT;
        public System.Windows.Forms.Label lblSBD;
        public System.Windows.Forms.Label lblTDBD;
        public System.Windows.Forms.ComboBox cbFuct;
    }
}
