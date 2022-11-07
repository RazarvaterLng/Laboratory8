using System;
using System.Windows.Forms;

namespace Tasks
{
    public partial class Form1 : Form
    {
        public Form1()=>
            InitializeComponent();

        private void button1_Click(object sender, EventArgs e)
        {
            int temp;
            if (!int.TryParse(textBox1.Text, out temp)) return;

            if (temp % 3 == 0 && temp % 7 == 0)
                MessageBox.Show("Это число делится на 3 и на 7","Успех");
            else
                MessageBox.Show("Это число не делится на 3 и на 7", "Провал");
        }
    }
}
