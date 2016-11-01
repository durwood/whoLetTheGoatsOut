using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformSample
{
    public partial class BombsweeperForm : Form
    {
        public BombsweeperForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World!");
        }

        private void BombsweeperForm_Load(object sender, EventArgs e)
        {

        }
    }
}
