namespace Bluetooth_rSAP
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Connect_BTN = new Button();
            Custom_APDU_Command_BTN = new Button();
            Custom_APDU_Command_TB = new TextBox();
            Communication_TB = new TextBox();
            Clear_BTN = new Button();
            Disconnect_BTN = new Button();
            Get_ICCID_BTN = new Button();
            ICCID_TB = new TextBox();
            Get_Contacts_BTN = new Button();
            Contacts_LV = new ListView();
            Get_Contacts_PB = new ProgressBar();
            Phone_Name_TB = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // Connect_BTN
            // 
            Connect_BTN.Location = new Point(251, 26);
            Connect_BTN.Name = "Connect_BTN";
            Connect_BTN.Size = new Size(75, 23);
            Connect_BTN.TabIndex = 0;
            Connect_BTN.Text = "Connect";
            Connect_BTN.UseVisualStyleBackColor = true;
            Connect_BTN.Click += button1_Click;
            // 
            // Custom_APDU_Command_BTN
            // 
            Custom_APDU_Command_BTN.Enabled = false;
            Custom_APDU_Command_BTN.Location = new Point(346, 99);
            Custom_APDU_Command_BTN.Name = "Custom_APDU_Command_BTN";
            Custom_APDU_Command_BTN.Size = new Size(170, 23);
            Custom_APDU_Command_BTN.TabIndex = 2;
            Custom_APDU_Command_BTN.Text = "Custom APDU CMD";
            Custom_APDU_Command_BTN.UseVisualStyleBackColor = true;
            Custom_APDU_Command_BTN.Click += button3_Click;
            // 
            // Custom_APDU_Command_TB
            // 
            Custom_APDU_Command_TB.Enabled = false;
            Custom_APDU_Command_TB.Location = new Point(522, 99);
            Custom_APDU_Command_TB.Name = "Custom_APDU_Command_TB";
            Custom_APDU_Command_TB.Size = new Size(266, 23);
            Custom_APDU_Command_TB.TabIndex = 4;
            // 
            // Communication_TB
            // 
            Communication_TB.Location = new Point(12, 312);
            Communication_TB.Multiline = true;
            Communication_TB.Name = "Communication_TB";
            Communication_TB.ReadOnly = true;
            Communication_TB.ScrollBars = ScrollBars.Vertical;
            Communication_TB.Size = new Size(776, 327);
            Communication_TB.TabIndex = 5;
            // 
            // Clear_BTN
            // 
            Clear_BTN.Location = new Point(618, 283);
            Clear_BTN.Name = "Clear_BTN";
            Clear_BTN.Size = new Size(170, 23);
            Clear_BTN.TabIndex = 6;
            Clear_BTN.Text = "CLEAR";
            Clear_BTN.UseVisualStyleBackColor = true;
            Clear_BTN.Click += button2_Click_1;
            // 
            // Disconnect_BTN
            // 
            Disconnect_BTN.Enabled = false;
            Disconnect_BTN.Location = new Point(493, 27);
            Disconnect_BTN.Name = "Disconnect_BTN";
            Disconnect_BTN.Size = new Size(75, 23);
            Disconnect_BTN.TabIndex = 7;
            Disconnect_BTN.Text = "Disconnect";
            Disconnect_BTN.UseVisualStyleBackColor = true;
            Disconnect_BTN.Click += button4_Click;
            // 
            // Get_ICCID_BTN
            // 
            Get_ICCID_BTN.Enabled = false;
            Get_ICCID_BTN.Location = new Point(346, 128);
            Get_ICCID_BTN.Name = "Get_ICCID_BTN";
            Get_ICCID_BTN.Size = new Size(170, 23);
            Get_ICCID_BTN.TabIndex = 8;
            Get_ICCID_BTN.Text = "GET ICCID";
            Get_ICCID_BTN.UseVisualStyleBackColor = true;
            Get_ICCID_BTN.Click += button5_Click;
            // 
            // ICCID_TB
            // 
            ICCID_TB.Enabled = false;
            ICCID_TB.Location = new Point(522, 128);
            ICCID_TB.Name = "ICCID_TB";
            ICCID_TB.Size = new Size(266, 23);
            ICCID_TB.TabIndex = 9;
            // 
            // Get_Contacts_BTN
            // 
            Get_Contacts_BTN.Enabled = false;
            Get_Contacts_BTN.Location = new Point(12, 99);
            Get_Contacts_BTN.Name = "Get_Contacts_BTN";
            Get_Contacts_BTN.Size = new Size(300, 23);
            Get_Contacts_BTN.TabIndex = 12;
            Get_Contacts_BTN.Text = "GET CONTACTS";
            Get_Contacts_BTN.UseVisualStyleBackColor = true;
            Get_Contacts_BTN.Click += button7_Click;
            // 
            // Contacts_LV
            // 
            Contacts_LV.Enabled = false;
            Contacts_LV.Location = new Point(12, 157);
            Contacts_LV.Name = "Contacts_LV";
            Contacts_LV.Size = new Size(301, 149);
            Contacts_LV.TabIndex = 13;
            Contacts_LV.UseCompatibleStateImageBehavior = false;
            // 
            // Get_Contacts_PB
            // 
            Get_Contacts_PB.Location = new Point(12, 128);
            Get_Contacts_PB.Name = "Get_Contacts_PB";
            Get_Contacts_PB.Size = new Size(300, 23);
            Get_Contacts_PB.TabIndex = 14;
            // 
            // Phone_Name_TB
            // 
            Phone_Name_TB.Location = new Point(332, 27);
            Phone_Name_TB.Name = "Phone_Name_TB";
            Phone_Name_TB.Size = new Size(155, 23);
            Phone_Name_TB.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(336, 9);
            label1.Name = "label1";
            label1.Size = new Size(151, 15);
            label1.TabIndex = 16;
            label1.Text = "Part of Paired Device Name";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 651);
            Controls.Add(label1);
            Controls.Add(Phone_Name_TB);
            Controls.Add(Get_Contacts_PB);
            Controls.Add(Contacts_LV);
            Controls.Add(Get_Contacts_BTN);
            Controls.Add(ICCID_TB);
            Controls.Add(Get_ICCID_BTN);
            Controls.Add(Disconnect_BTN);
            Controls.Add(Clear_BTN);
            Controls.Add(Communication_TB);
            Controls.Add(Custom_APDU_Command_TB);
            Controls.Add(Custom_APDU_Command_BTN);
            Controls.Add(Connect_BTN);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Main";
            ShowIcon = false;
            Text = "rSAP Example";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Connect_BTN;
        private Button Custom_APDU_Command_BTN;
        private TextBox Custom_APDU_Command_TB;
        private TextBox Communication_TB;
        private Button Clear_BTN;
        private Button Disconnect_BTN;
        private Button Get_ICCID_BTN;
        private TextBox ICCID_TB;
        private Button Get_Contacts_BTN;
        private ListView Contacts_LV;
        private ProgressBar Get_Contacts_PB;
        private TextBox Phone_Name_TB;
        private Label label1;
    }
}
