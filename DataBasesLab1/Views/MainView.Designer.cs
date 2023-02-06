namespace DataBasesLab1.Views
{
    partial class MainView
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
            this.btnEmployers = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogs = new System.Windows.Forms.Button();
            this.btnAgreements = new System.Windows.Forms.Button();
            this.btnEmployees = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEmployers
            // 
            this.btnEmployers.BackColor = System.Drawing.Color.Salmon;
            this.btnEmployers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEmployers.FlatAppearance.BorderSize = 0;
            this.btnEmployers.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEmployers.Location = new System.Drawing.Point(1, 101);
            this.btnEmployers.Name = "btnEmployers";
            this.btnEmployers.Size = new System.Drawing.Size(196, 66);
            this.btnEmployers.TabIndex = 1;
            this.btnEmployers.Text = "Employers";
            this.btnEmployers.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightCoral;
            this.panel1.Controls.Add(this.btnLogs);
            this.panel1.Controls.Add(this.btnAgreements);
            this.panel1.Controls.Add(this.btnEmployees);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnEmployers);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 464);
            this.panel1.TabIndex = 2;
            // 
            // btnLogs
            // 
            this.btnLogs.BackColor = System.Drawing.Color.Salmon;
            this.btnLogs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLogs.FlatAppearance.BorderSize = 0;
            this.btnLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLogs.Location = new System.Drawing.Point(50, 424);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(82, 37);
            this.btnLogs.TabIndex = 5;
            this.btnLogs.Text = "Logs";
            this.btnLogs.UseVisualStyleBackColor = false;
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // btnAgreements
            // 
            this.btnAgreements.BackColor = System.Drawing.Color.Salmon;
            this.btnAgreements.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAgreements.FlatAppearance.BorderSize = 0;
            this.btnAgreements.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAgreements.Location = new System.Drawing.Point(1, 300);
            this.btnAgreements.Name = "btnAgreements";
            this.btnAgreements.Size = new System.Drawing.Size(199, 66);
            this.btnAgreements.TabIndex = 4;
            this.btnAgreements.Text = "Agreements";
            this.btnAgreements.UseVisualStyleBackColor = false;
            // 
            // btnEmployees
            // 
            this.btnEmployees.BackColor = System.Drawing.Color.Salmon;
            this.btnEmployees.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEmployees.FlatAppearance.BorderSize = 0;
            this.btnEmployees.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnEmployees.Location = new System.Drawing.Point(2, 199);
            this.btnEmployees.Name = "btnEmployees";
            this.btnEmployees.Size = new System.Drawing.Size(196, 66);
            this.btnEmployees.TabIndex = 3;
            this.btnEmployees.Text = "Employees";
            this.btnEmployees.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 37);
            this.label1.TabIndex = 2;
            this.label1.Text = "The Bureau";
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MistyRose;
            this.ClientSize = new System.Drawing.Size(877, 464);
            this.Controls.Add(this.panel1);
            this.IsMdiContainer = true;
            this.Name = "MainView";
            this.Text = "MainView";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainView_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnEmployers;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEmployees;
        private System.Windows.Forms.Button btnAgreements;
        private System.Windows.Forms.Button btnLogs;
    }
}