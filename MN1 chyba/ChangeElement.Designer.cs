namespace MN1_chyba
{
    partial class ChangeElement
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
            this.label_Z = new System.Windows.Forms.Label();
            this.comboBox_Z_multi = new System.Windows.Forms.ComboBox();
            this.textBox_Z_Value = new System.Windows.Forms.TextBox();
            this.pictureBox_Z = new System.Windows.Forms.PictureBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.comboBox_elementType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Z)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Z
            // 
            this.label_Z.AutoSize = true;
            this.label_Z.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold);
            this.label_Z.Location = new System.Drawing.Point(119, 15);
            this.label_Z.Name = "label_Z";
            this.label_Z.Size = new System.Drawing.Size(42, 24);
            this.label_Z.TabIndex = 44;
            this.label_Z.Text = "Z11";
            // 
            // comboBox_Z_multi
            // 
            this.comboBox_Z_multi.BackColor = System.Drawing.Color.White;
            this.comboBox_Z_multi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Z_multi.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox_Z_multi.FormattingEnabled = true;
            this.comboBox_Z_multi.Items.AddRange(new object[] {
            "MV",
            "kV",
            "V",
            "mV",
            "uV",
            "nV",
            "pV"});
            this.comboBox_Z_multi.Location = new System.Drawing.Point(133, 202);
            this.comboBox_Z_multi.Name = "comboBox_Z_multi";
            this.comboBox_Z_multi.Size = new System.Drawing.Size(71, 28);
            this.comboBox_Z_multi.TabIndex = 42;
            this.comboBox_Z_multi.SelectedIndexChanged += new System.EventHandler(this.comboBox_Z_multi_SelectedIndexChanged);
            // 
            // textBox_Z_Value
            // 
            this.textBox_Z_Value.BackColor = System.Drawing.Color.White;
            this.textBox_Z_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Z_Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox_Z_Value.Location = new System.Drawing.Point(27, 201);
            this.textBox_Z_Value.Name = "textBox_Z_Value";
            this.textBox_Z_Value.Size = new System.Drawing.Size(101, 30);
            this.textBox_Z_Value.TabIndex = 41;
            this.textBox_Z_Value.Text = "5";
            // 
            // pictureBox_Z
            // 
            this.pictureBox_Z.Image = global::MN1_chyba.Properties.Resources.resistor_h;
            this.pictureBox_Z.Location = new System.Drawing.Point(27, 42);
            this.pictureBox_Z.Name = "pictureBox_Z";
            this.pictureBox_Z.Size = new System.Drawing.Size(232, 142);
            this.pictureBox_Z.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Z.TabIndex = 43;
            this.pictureBox_Z.TabStop = false;
            // 
            // button_ok
            // 
            this.button_ok.BackColor = System.Drawing.Color.YellowGreen;
            this.button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_ok.Location = new System.Drawing.Point(289, 194);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(111, 37);
            this.button_ok.TabIndex = 46;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.Coral;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_cancel.Location = new System.Drawing.Point(406, 206);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(90, 25);
            this.button_cancel.TabIndex = 47;
            this.button_cancel.Text = "ANULUJ";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // comboBox_elementType
            // 
            this.comboBox_elementType.BackColor = System.Drawing.Color.White;
            this.comboBox_elementType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_elementType.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBox_elementType.FormattingEnabled = true;
            this.comboBox_elementType.Items.AddRange(new object[] {
            "Rezystor",
            "Cewka",
            "Kondensator"});
            this.comboBox_elementType.Location = new System.Drawing.Point(308, 42);
            this.comboBox_elementType.Name = "comboBox_elementType";
            this.comboBox_elementType.Size = new System.Drawing.Size(188, 28);
            this.comboBox_elementType.TabIndex = 48;
            this.comboBox_elementType.SelectedIndexChanged += new System.EventHandler(this.comboBox_elementType_SelectedIndexChanged);
            // 
            // ChangeElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 240);
            this.Controls.Add(this.comboBox_elementType);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label_Z);
            this.Controls.Add(this.pictureBox_Z);
            this.Controls.Add(this.comboBox_Z_multi);
            this.Controls.Add(this.textBox_Z_Value);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeElement";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zmiana typu elementu";
            this.Load += new System.EventHandler(this.ChangeElement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Z)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Z;
        private System.Windows.Forms.PictureBox pictureBox_Z;
        private System.Windows.Forms.ComboBox comboBox_Z_multi;
        private System.Windows.Forms.TextBox textBox_Z_Value;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.ComboBox comboBox_elementType;
    }
}