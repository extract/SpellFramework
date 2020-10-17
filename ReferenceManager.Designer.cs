namespace SpellFramework
{
    partial class ReferenceManager
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
            this.fieldValueLabel = new System.Windows.Forms.Label();
            this.copyButton = new System.Windows.Forms.Button();
            this.changeRefButton = new System.Windows.Forms.Button();
            this.labelDefault = new System.Windows.Forms.Label();
            this.ReferenceText = new System.Windows.Forms.TextBox();
            this.copyRenameText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fieldValueLabel
            // 
            this.fieldValueLabel.AutoSize = true;
            this.fieldValueLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fieldValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldValueLabel.Location = new System.Drawing.Point(12, 9);
            this.fieldValueLabel.Name = "fieldValueLabel";
            this.fieldValueLabel.Size = new System.Drawing.Size(118, 42);
            this.fieldValueLabel.TabIndex = 0;
            this.fieldValueLabel.Text = "label1";
            this.fieldValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.fieldValueLabel.Click += new System.EventHandler(this.fieldValueLabel_Click);
            // 
            // copyButton
            // 
            this.copyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyButton.Location = new System.Drawing.Point(393, 142);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(137, 23);
            this.copyButton.TabIndex = 1;
            this.copyButton.Text = "Copy to own mod";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // changeRefButton
            // 
            this.changeRefButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.changeRefButton.Location = new System.Drawing.Point(393, 207);
            this.changeRefButton.Name = "changeRefButton";
            this.changeRefButton.Size = new System.Drawing.Size(137, 23);
            this.changeRefButton.TabIndex = 2;
            this.changeRefButton.Text = "Change reference";
            this.changeRefButton.UseVisualStyleBackColor = true;
            this.changeRefButton.Click += new System.EventHandler(this.changeRefButton_Click);
            // 
            // labelDefault
            // 
            this.labelDefault.AutoSize = true;
            this.labelDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelDefault.Location = new System.Drawing.Point(16, 67);
            this.labelDefault.Name = "labelDefault";
            this.labelDefault.Size = new System.Drawing.Size(374, 52);
            this.labelDefault.TabIndex = 3;
            this.labelDefault.Text = "This file is referenced from Default. \r\nTo be able to edit this file specifically" +
    ", you need to copy it.\r\nYou can also change the reference to an existing or new " +
    "file in the mod folder.\r\n\r\n";
            this.labelDefault.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReferenceText
            // 
            this.ReferenceText.Location = new System.Drawing.Point(110, 209);
            this.ReferenceText.Name = "ReferenceText";
            this.ReferenceText.Size = new System.Drawing.Size(277, 20);
            this.ReferenceText.TabIndex = 4;
            // 
            // copyRenameText
            // 
            this.copyRenameText.Location = new System.Drawing.Point(110, 144);
            this.copyRenameText.Name = "copyRenameText";
            this.copyRenameText.Size = new System.Drawing.Size(277, 20);
            this.copyRenameText.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Rename file";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Change reference";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSize = true;
            this.cancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.cancelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(0, 256);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(542, 35);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // ReferenceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(57)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(542, 291);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.copyRenameText);
            this.Controls.Add(this.ReferenceText);
            this.Controls.Add(this.labelDefault);
            this.Controls.Add(this.changeRefButton);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.fieldValueLabel);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.Name = "ReferenceManager";
            this.Text = "ReferenceManager";
            this.Load += new System.EventHandler(this.ReferenceManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fieldValueLabel;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button changeRefButton;
        private System.Windows.Forms.Label labelDefault;
        private System.Windows.Forms.TextBox ReferenceText;
        private System.Windows.Forms.TextBox copyRenameText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cancelButton;
    }
}