using Microsoft.VisualBasic;
using System;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()=>
            InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            float sum = 0;
            float tmp;
            do
            {
                while (!float.TryParse(Interaction.InputBox($"Введите число для суммирования,(что-бы выйти введите 0), текущая сумма: {sum}"), out tmp))
                MessageBox.Show("Введите ЧИСЛО!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                sum += tmp;
            } while (tmp!=0);
            this.Close();
        }
    }
}
