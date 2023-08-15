namespace CloudPressGui.AzureService
{
    partial class register
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
            this.components = new System.ComponentModel.Container();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.usernameButton = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.returnLoginButton = new System.Windows.Forms.Button();
            this.rePasswordButton = new System.Windows.Forms.TextBox();
            this.passwordButton = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Re-password:";
            // 
            // usernameButton
            // 
            this.usernameButton.Location = new System.Drawing.Point(90, 15);
            this.usernameButton.Name = "usernameButton";
            this.usernameButton.Size = new System.Drawing.Size(100, 20);
            this.usernameButton.TabIndex = 3;
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(57, 126);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(59, 23);
            this.registerButton.TabIndex = 6;
            this.registerButton.Text = "Register";
            this.registerButton.UseVisualStyleBackColor = true;
            // 
            // returnLoginButton
            // 
            this.returnLoginButton.Location = new System.Drawing.Point(19, 155);
            this.returnLoginButton.Name = "returnLoginButton";
            this.returnLoginButton.Size = new System.Drawing.Size(138, 23);
            this.returnLoginButton.TabIndex = 7;
            this.returnLoginButton.Text = "Have account? Login here";
            this.returnLoginButton.UseVisualStyleBackColor = true;
            // 
            // rePasswordButton
            // 
            this.rePasswordButton.Location = new System.Drawing.Point(90, 67);
            this.rePasswordButton.Name = "rePasswordButton";
            this.rePasswordButton.Size = new System.Drawing.Size(100, 20);
            this.rePasswordButton.TabIndex = 8;
            // 
            // passwordButton
            // 
            this.passwordButton.Location = new System.Drawing.Point(90, 41);
            this.passwordButton.Name = "passwordButton";
            this.passwordButton.Size = new System.Drawing.Size(100, 20);
            this.passwordButton.TabIndex = 9;
            // 
            // register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 201);
            this.Controls.Add(this.passwordButton);
            this.Controls.Add(this.rePasswordButton);
            this.Controls.Add(this.returnLoginButton);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.usernameButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "register";
            this.Text = "register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox usernameButton;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button returnLoginButton;
        private System.Windows.Forms.TextBox rePasswordButton;
        private System.Windows.Forms.TextBox passwordButton;
    }
}