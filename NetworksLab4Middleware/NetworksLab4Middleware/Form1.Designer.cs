namespace NetworksLab4Middleware
{
    partial class Form1
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
            this.server1IPLabel = new System.Windows.Forms.Label();
            this.server1IPTextbox = new System.Windows.Forms.TextBox();
            this.deviceLabel = new System.Windows.Forms.Label();
            this.deviceListTextbox = new System.Windows.Forms.RichTextBox();
            this.deviceDropDownLabel = new System.Windows.Forms.Label();
            this.deviceDropDown = new System.Windows.Forms.ComboBox();
            this.testDataLabel = new System.Windows.Forms.Label();
            this.testDataTextbox = new System.Windows.Forms.RichTextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.finishButton = new System.Windows.Forms.Button();
            this.deviceQueryButton = new System.Windows.Forms.Button();
            this.myIPLabel = new System.Windows.Forms.Label();
            this.myIPTextbox = new System.Windows.Forms.TextBox();
            this.server2IPTextbox = new System.Windows.Forms.TextBox();
            this.server2IPLabel = new System.Windows.Forms.Label();
            this.paceTextbox = new System.Windows.Forms.TextBox();
            this.paceLabel = new System.Windows.Forms.Label();
            this.msgCountLabel = new System.Windows.Forms.Label();
            this.msgCountTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // server1IPLabel
            // 
            this.server1IPLabel.AutoSize = true;
            this.server1IPLabel.Location = new System.Drawing.Point(13, 37);
            this.server1IPLabel.Name = "server1IPLabel";
            this.server1IPLabel.Size = new System.Drawing.Size(101, 13);
            this.server1IPLabel.TabIndex = 0;
            this.server1IPLabel.Text = "Server 1 IP Address";
            // 
            // server1IPTextbox
            // 
            this.server1IPTextbox.Location = new System.Drawing.Point(13, 53);
            this.server1IPTextbox.Name = "server1IPTextbox";
            this.server1IPTextbox.Size = new System.Drawing.Size(142, 20);
            this.server1IPTextbox.TabIndex = 2;
            // 
            // deviceLabel
            // 
            this.deviceLabel.AutoSize = true;
            this.deviceLabel.Location = new System.Drawing.Point(13, 115);
            this.deviceLabel.Name = "deviceLabel";
            this.deviceLabel.Size = new System.Drawing.Size(60, 13);
            this.deviceLabel.TabIndex = 2;
            this.deviceLabel.Text = "Device List";
            // 
            // deviceListTextbox
            // 
            this.deviceListTextbox.Location = new System.Drawing.Point(13, 131);
            this.deviceListTextbox.Name = "deviceListTextbox";
            this.deviceListTextbox.Size = new System.Drawing.Size(250, 91);
            this.deviceListTextbox.TabIndex = 7;
            this.deviceListTextbox.Text = "";
            // 
            // deviceDropDownLabel
            // 
            this.deviceDropDownLabel.AutoSize = true;
            this.deviceDropDownLabel.Location = new System.Drawing.Point(170, 76);
            this.deviceDropDownLabel.Name = "deviceDropDownLabel";
            this.deviceDropDownLabel.Size = new System.Drawing.Size(93, 13);
            this.deviceDropDownLabel.TabIndex = 4;
            this.deviceDropDownLabel.Text = "Select Nic Device";
            // 
            // deviceDropDown
            // 
            this.deviceDropDown.FormattingEnabled = true;
            this.deviceDropDown.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.deviceDropDown.Location = new System.Drawing.Point(223, 92);
            this.deviceDropDown.Name = "deviceDropDown";
            this.deviceDropDown.Size = new System.Drawing.Size(40, 21);
            this.deviceDropDown.TabIndex = 1;
            // 
            // testDataLabel
            // 
            this.testDataLabel.AutoSize = true;
            this.testDataLabel.Location = new System.Drawing.Point(13, 225);
            this.testDataLabel.Name = "testDataLabel";
            this.testDataLabel.Size = new System.Drawing.Size(75, 13);
            this.testDataLabel.TabIndex = 6;
            this.testDataLabel.Text = "Test Data Box";
            // 
            // testDataTextbox
            // 
            this.testDataTextbox.Location = new System.Drawing.Point(13, 241);
            this.testDataTextbox.Name = "testDataTextbox";
            this.testDataTextbox.Size = new System.Drawing.Size(250, 120);
            this.testDataTextbox.TabIndex = 8;
            this.testDataTextbox.Text = "";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(107, 1);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // finishButton
            // 
            this.finishButton.Location = new System.Drawing.Point(188, 1);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(75, 23);
            this.finishButton.TabIndex = 6;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // deviceQueryButton
            // 
            this.deviceQueryButton.Location = new System.Drawing.Point(13, 1);
            this.deviceQueryButton.Name = "deviceQueryButton";
            this.deviceQueryButton.Size = new System.Drawing.Size(88, 23);
            this.deviceQueryButton.TabIndex = 0;
            this.deviceQueryButton.Text = "Device Query";
            this.deviceQueryButton.UseVisualStyleBackColor = true;
            this.deviceQueryButton.Click += new System.EventHandler(this.deviceQueryButton_Click);
            // 
            // myIPLabel
            // 
            this.myIPLabel.AutoSize = true;
            this.myIPLabel.Location = new System.Drawing.Point(163, 37);
            this.myIPLabel.Name = "myIPLabel";
            this.myIPLabel.Size = new System.Drawing.Size(34, 13);
            this.myIPLabel.TabIndex = 11;
            this.myIPLabel.Text = "My IP";
            // 
            // myIPTextbox
            // 
            this.myIPTextbox.Location = new System.Drawing.Point(163, 53);
            this.myIPTextbox.Name = "myIPTextbox";
            this.myIPTextbox.Size = new System.Drawing.Size(100, 20);
            this.myIPTextbox.TabIndex = 4;
            // 
            // server2IPTextbox
            // 
            this.server2IPTextbox.Location = new System.Drawing.Point(13, 92);
            this.server2IPTextbox.Name = "server2IPTextbox";
            this.server2IPTextbox.Size = new System.Drawing.Size(142, 20);
            this.server2IPTextbox.TabIndex = 3;
            // 
            // server2IPLabel
            // 
            this.server2IPLabel.AutoSize = true;
            this.server2IPLabel.Location = new System.Drawing.Point(13, 76);
            this.server2IPLabel.Name = "server2IPLabel";
            this.server2IPLabel.Size = new System.Drawing.Size(104, 13);
            this.server2IPLabel.TabIndex = 13;
            this.server2IPLabel.Text = "Server  2 IP Address";
            // 
            // paceTextbox
            // 
            this.paceTextbox.Location = new System.Drawing.Point(270, 53);
            this.paceTextbox.Name = "paceTextbox";
            this.paceTextbox.Size = new System.Drawing.Size(81, 20);
            this.paceTextbox.TabIndex = 14;
            // 
            // paceLabel
            // 
            this.paceLabel.AutoSize = true;
            this.paceLabel.Location = new System.Drawing.Point(270, 37);
            this.paceLabel.Name = "paceLabel";
            this.paceLabel.Size = new System.Drawing.Size(32, 13);
            this.paceLabel.TabIndex = 15;
            this.paceLabel.Text = "Pace";
            // 
            // msgCountLabel
            // 
            this.msgCountLabel.AutoSize = true;
            this.msgCountLabel.Location = new System.Drawing.Point(270, 76);
            this.msgCountLabel.Name = "msgCountLabel";
            this.msgCountLabel.Size = new System.Drawing.Size(58, 13);
            this.msgCountLabel.TabIndex = 16;
            this.msgCountLabel.Text = "Msg Count";
            // 
            // msgCountTextbox
            // 
            this.msgCountTextbox.Location = new System.Drawing.Point(270, 92);
            this.msgCountTextbox.Name = "msgCountTextbox";
            this.msgCountTextbox.Size = new System.Drawing.Size(81, 20);
            this.msgCountTextbox.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 369);
            this.Controls.Add(this.msgCountTextbox);
            this.Controls.Add(this.msgCountLabel);
            this.Controls.Add(this.paceLabel);
            this.Controls.Add(this.paceTextbox);
            this.Controls.Add(this.server2IPTextbox);
            this.Controls.Add(this.server2IPLabel);
            this.Controls.Add(this.myIPTextbox);
            this.Controls.Add(this.myIPLabel);
            this.Controls.Add(this.deviceQueryButton);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.testDataTextbox);
            this.Controls.Add(this.testDataLabel);
            this.Controls.Add(this.deviceDropDown);
            this.Controls.Add(this.deviceDropDownLabel);
            this.Controls.Add(this.deviceListTextbox);
            this.Controls.Add(this.deviceLabel);
            this.Controls.Add(this.server1IPTextbox);
            this.Controls.Add(this.server1IPLabel);
            this.Name = "Form1";
            this.Text = "Middleware";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label server1IPLabel;
        private System.Windows.Forms.TextBox server1IPTextbox;
        private System.Windows.Forms.Label deviceLabel;
        private System.Windows.Forms.RichTextBox deviceListTextbox;
        private System.Windows.Forms.Label deviceDropDownLabel;
        private System.Windows.Forms.ComboBox deviceDropDown;
        private System.Windows.Forms.Label testDataLabel;
        private System.Windows.Forms.RichTextBox testDataTextbox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Button deviceQueryButton;
        private System.Windows.Forms.Label myIPLabel;
        private System.Windows.Forms.TextBox myIPTextbox;
        private System.Windows.Forms.TextBox server2IPTextbox;
        private System.Windows.Forms.Label server2IPLabel;
        private System.Windows.Forms.TextBox paceTextbox;
        private System.Windows.Forms.Label paceLabel;
        private System.Windows.Forms.Label msgCountLabel;
        private System.Windows.Forms.TextBox msgCountTextbox;
    }
}

