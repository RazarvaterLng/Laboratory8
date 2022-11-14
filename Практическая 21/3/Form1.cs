using System;
using System.Linq;
using System.Windows.Forms;

namespace _3
{
    public partial class Form1 : Form
    {

        private char[] chars;

        public Form1()=>
            InitializeComponent();

        private void button1_Click(object sender, EventArgs e)
        {
            chars = textBox1.Text.ToCharArray().Reverse().ToArray();
            textBox1.Text = new string(chars);
        }      
    }
}
