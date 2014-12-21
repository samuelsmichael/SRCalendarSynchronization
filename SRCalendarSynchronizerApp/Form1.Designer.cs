namespace SRCalendarSynchronizerApp {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSynchronizationPeriodicity = new System.Windows.Forms.TextBox();
            this.btnUpdateSynchronizationPeriodicity = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(219, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(280, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 1;
            // 
            // btnEnable
            // 
            this.btnEnable.Location = new System.Drawing.Point(36, 141);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(75, 23);
            this.btnEnable.TabIndex = 2;
            this.btnEnable.Text = "Enable";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Location = new System.Drawing.Point(373, 141);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(75, 23);
            this.btnDisable.TabIndex = 3;
            this.btnDisable.Text = "Disable";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Synchronization Periodicity (in minutes):";
            // 
            // tbSynchronizationPeriodicity
            // 
            this.tbSynchronizationPeriodicity.Location = new System.Drawing.Point(283, 56);
            this.tbSynchronizationPeriodicity.Name = "tbSynchronizationPeriodicity";
            this.tbSynchronizationPeriodicity.Size = new System.Drawing.Size(84, 20);
            this.tbSynchronizationPeriodicity.TabIndex = 5;
            // 
            // btnUpdateSynchronizationPeriodicity
            // 
            this.btnUpdateSynchronizationPeriodicity.Location = new System.Drawing.Point(373, 54);
            this.btnUpdateSynchronizationPeriodicity.Name = "btnUpdateSynchronizationPeriodicity";
            this.btnUpdateSynchronizationPeriodicity.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateSynchronizationPeriodicity.TabIndex = 6;
            this.btnUpdateSynchronizationPeriodicity.Text = "Update";
            this.btnUpdateSynchronizationPeriodicity.UseVisualStyleBackColor = true;
            this.btnUpdateSynchronizationPeriodicity.Click += new System.EventHandler(this.btnUpdateSynchronizationPeriodicity_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(36, 212);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 237);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnUpdateSynchronizationPeriodicity);
            this.Controls.Add(this.tbSynchronizationPeriodicity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.btnEnable);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Calendar Synchronization";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSynchronizationPeriodicity;
        private System.Windows.Forms.Button btnUpdateSynchronizationPeriodicity;
        private System.Windows.Forms.Label lblError;
    }
}

