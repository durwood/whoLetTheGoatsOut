﻿namespace bombsweeperWinform
{
    partial class MainForm
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
            this.BombCountLabel = new System.Windows.Forms.Label();
            this.ElapsedTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HiddenGoatCount = new System.Windows.Forms.Label();
            this.ElapsedTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BombCountLabel
            // 
            this.BombCountLabel.Location = new System.Drawing.Point(315, 33);
            this.BombCountLabel.Name = "BombCountLabel";
            this.BombCountLabel.Size = new System.Drawing.Size(288, 31);
            this.BombCountLabel.TabIndex = 3;
            this.BombCountLabel.Text = "Unmarked Goats Remaining:";
            this.BombCountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ElapsedTimeLabel
            // 
            this.ElapsedTimeLabel.Location = new System.Drawing.Point(315, 79);
            this.ElapsedTimeLabel.Name = "ElapsedTimeLabel";
            this.ElapsedTimeLabel.Size = new System.Drawing.Size(283, 31);
            this.ElapsedTimeLabel.TabIndex = 4;
            this.ElapsedTimeLabel.Text = "Elapsed Time:";
            this.ElapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 100);
            this.label1.TabIndex = 5;
            this.label1.Text = "Who Let the Goats Out?";
            // 
            // HiddenGoatCount
            // 
            this.HiddenGoatCount.Location = new System.Drawing.Point(603, 33);
            this.HiddenGoatCount.Name = "HiddenGoatCount";
            this.HiddenGoatCount.Size = new System.Drawing.Size(102, 31);
            this.HiddenGoatCount.TabIndex = 6;
            // 
            // ElapsedTime
            // 
            this.ElapsedTime.Location = new System.Drawing.Point(603, 79);
            this.ElapsedTime.Name = "ElapsedTime";
            this.ElapsedTime.Size = new System.Drawing.Size(102, 31);
            this.ElapsedTime.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 990);
            this.Controls.Add(this.ElapsedTime);
            this.Controls.Add(this.HiddenGoatCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ElapsedTimeLabel);
            this.Controls.Add(this.BombCountLabel);
            this.Name = "MainForm";
            this.Text = "BombSweeper";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label BombCountLabel;
        private System.Windows.Forms.Label ElapsedTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label HiddenGoatCount;
        private System.Windows.Forms.Label ElapsedTime;
    }
}

