namespace FlowChar
{
    partial class FormNodeProperty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNodeProperty));
            this.NodeProperty = new System.Windows.Forms.PropertyGrid();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtMutilText = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMutiText = new System.Windows.Forms.Button();
            this.btnSelectPic = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NodeProperty
            // 
            this.NodeProperty.Dock = System.Windows.Forms.DockStyle.Top;
            this.NodeProperty.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NodeProperty.LineColor = System.Drawing.SystemColors.ControlDark;
            this.NodeProperty.Location = new System.Drawing.Point(0, 0);
            this.NodeProperty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NodeProperty.Name = "NodeProperty";
            this.NodeProperty.Size = new System.Drawing.Size(770, 444);
            this.NodeProperty.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(480, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(322, 2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(150, 35);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtMutilText
            // 
            this.txtMutilText.AcceptsTab = true;
            this.txtMutilText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMutilText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMutilText.Location = new System.Drawing.Point(0, 444);
            this.txtMutilText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMutilText.Multiline = true;
            this.txtMutilText.Name = "txtMutilText";
            this.txtMutilText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMutilText.Size = new System.Drawing.Size(770, 69);
            this.txtMutilText.TabIndex = 1;
            this.txtMutilText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMutilText_KeyUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.panel1.Controls.Add(this.btnMutiText);
            this.panel1.Controls.Add(this.btnSelectPic);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 513);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(770, 38);
            this.panel1.TabIndex = 5;
            // 
            // btnMutiText
            // 
            this.btnMutiText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMutiText.Location = new System.Drawing.Point(164, 2);
            this.btnMutiText.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMutiText.Name = "btnMutiText";
            this.btnMutiText.Size = new System.Drawing.Size(150, 35);
            this.btnMutiText.TabIndex = 6;
            this.btnMutiText.Text = "Muti_text";
            this.btnMutiText.UseVisualStyleBackColor = true;
            this.btnMutiText.Click += new System.EventHandler(this.btnMutiText_Click);
            // 
            // btnSelectPic
            // 
            this.btnSelectPic.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectPic.Location = new System.Drawing.Point(6, 2);
            this.btnSelectPic.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectPic.Name = "btnSelectPic";
            this.btnSelectPic.Size = new System.Drawing.Size(150, 35);
            this.btnSelectPic.TabIndex = 5;
            this.btnSelectPic.Text = "Select Pic";
            this.btnSelectPic.UseVisualStyleBackColor = true;
            this.btnSelectPic.Click += new System.EventHandler(this.btnSelectPic_Click);
            // 
            // FormNodeProperty
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 551);
            this.Controls.Add(this.txtMutilText);
            this.Controls.Add(this.NodeProperty);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "FormNodeProperty";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NodeProperty";
            this.Load += new System.EventHandler(this.FormNodeProperty1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid NodeProperty;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtMutilText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectPic;
        private System.Windows.Forms.Button btnMutiText;
    }
}