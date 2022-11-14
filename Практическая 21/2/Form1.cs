using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] pows = new int[11];
            pows[0] = 1;
            for (int i = 1; i < pows.Length; i++)
            {
                pows[i] = pows[i-1]*2;
                textBox1.Text += $"{pows[i]}\t";
            }
        }
    }
}
