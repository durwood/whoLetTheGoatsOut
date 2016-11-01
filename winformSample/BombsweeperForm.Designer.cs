namespace winformSample
{
    partial class BombsweeperForm
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
            this.HelloWorld = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // HelloWorld
            // 
            this.HelloWorld.Location = new System.Drawing.Point(12, 12);
            this.HelloWorld.Name = "HelloWorld";
            this.HelloWorld.Size = new System.Drawing.Size(184, 83);
            this.HelloWorld.TabIndex = 0;
            this.HelloWorld.Text = "Say Hello";
            this.HelloWorld.UseVisualStyleBackColor = true;
            this.HelloWorld.Click += new System.EventHandler(this.button1_Click);
            // 
            // BombsweeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 677);
            this.Controls.Add(this.HelloWorld);
            this.Name = "BombsweeperForm";
            this.Text = "BombSweeper";
            this.Load += new System.EventHandler(this.BombsweeperForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HelloWorld;
    }
}

