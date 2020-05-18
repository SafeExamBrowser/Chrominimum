namespace Chrominimum
{
	partial class VersionWindow
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
			this.LogoPictureBox = new System.Windows.Forms.PictureBox();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.BuildLabel = new System.Windows.Forms.Label();
			this.CopyrightLabel = new System.Windows.Forms.Label();
			this.CloseButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// LogoPictureBox
			// 
			this.LogoPictureBox.Location = new System.Drawing.Point(30, 63);
			this.LogoPictureBox.Name = "LogoPictureBox";
			this.LogoPictureBox.Size = new System.Drawing.Size(128, 128);
			this.LogoPictureBox.TabIndex = 0;
			this.LogoPictureBox.TabStop = false;
			// 
			// TitleLabel
			// 
			this.TitleLabel.AutoSize = true;
			this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TitleLabel.Location = new System.Drawing.Point(180, 88);
			this.TitleLabel.Name = "TitleLabel";
			this.TitleLabel.Size = new System.Drawing.Size(40, 17);
			this.TitleLabel.TabIndex = 1;
			this.TitleLabel.Text = "Title";
			// 
			// BuildLabel
			// 
			this.BuildLabel.AutoSize = true;
			this.BuildLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BuildLabel.Location = new System.Drawing.Point(180, 105);
			this.BuildLabel.Name = "BuildLabel";
			this.BuildLabel.Size = new System.Drawing.Size(30, 13);
			this.BuildLabel.TabIndex = 2;
			this.BuildLabel.Text = "Build";
			// 
			// CopyrightLabel
			// 
			this.CopyrightLabel.Location = new System.Drawing.Point(180, 137);
			this.CopyrightLabel.Name = "CopyrightLabel";
			this.CopyrightLabel.Size = new System.Drawing.Size(258, 104);
			this.CopyrightLabel.TabIndex = 3;
			this.CopyrightLabel.Text = "Copyright";
			// 
			// CloseButton
			// 
			this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
			this.CloseButton.FlatAppearance.BorderSize = 0;
			this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CloseButton.Location = new System.Drawing.Point(415, 12);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(23, 23);
			this.CloseButton.TabIndex = 4;
			this.CloseButton.Text = "X";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// VersionWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(450, 250);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.CopyrightLabel);
			this.Controls.Add(this.BuildLabel);
			this.Controls.Add(this.TitleLabel);
			this.Controls.Add(this.LogoPictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "VersionWindow";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "VersionWindow";
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox LogoPictureBox;
		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.Label BuildLabel;
		private System.Windows.Forms.Label CopyrightLabel;
		private System.Windows.Forms.Button CloseButton;
	}
}