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
            this.components = new System.ComponentModel.Container();
            this.HelloWorld = new System.Windows.Forms.Button();
            this.Board = new System.Windows.Forms.DataGridView();
            this.boardDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Board)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // HelloWorld
            // 
            this.HelloWorld.Location = new System.Drawing.Point(2, 3);
            this.HelloWorld.Name = "HelloWorld";
            this.HelloWorld.Size = new System.Drawing.Size(184, 83);
            this.HelloWorld.TabIndex = 0;
            this.HelloWorld.Text = "Say Hello";
            this.HelloWorld.UseVisualStyleBackColor = true;
            this.HelloWorld.Click += new System.EventHandler(this.button1_Click);
            // 
            // Board
            // 
            this.Board.AutoGenerateColumns = false;
            this.Board.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Board.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.Board.DataSource = this.boardDataBindingSource;
            this.Board.Location = new System.Drawing.Point(2, 101);
            this.Board.Name = "Board";
            this.Board.RowTemplate.Height = 33;
            this.Board.Size = new System.Drawing.Size(444, 545);
            this.Board.TabIndex = 1;
            this.Board.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // boardDataBindingSource
            // 
            this.boardDataBindingSource.DataSource = typeof(winformSample.BoardData);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // BombsweeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 649);
            this.Controls.Add(this.Board);
            this.Controls.Add(this.HelloWorld);
            this.Name = "BombsweeperForm";
            this.Text = "BombSweeper";
            this.Load += new System.EventHandler(this.BombsweeperForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Board)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HelloWorld;
        private System.Windows.Forms.DataGridView Board;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewButtonColumn Column2;
        private System.Windows.Forms.DataGridViewButtonColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column4;
        private System.Windows.Forms.BindingSource boardDataBindingSource;
    }
}

