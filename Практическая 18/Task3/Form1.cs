using System;
using System.Windows.Forms;

namespace Task3
{
    public partial class Form1 : Form
    {
        public Form1()=>
            InitializeComponent();
        private void SelectedIndexChange(object sender, EventArgs e)=>
            label3.Text = comboBox1.SelectedIndex == 0 ? "При делении числа на 5 в остатке выходит 2" : "При делении числа на 3 в остатке выходит 1";

        private void Count(object sender, EventArgs e)
        {
            uint Lng;
            if (!uint.TryParse(textBox2.Text, out Lng))
            {
                MessageBox.Show("Введите корректное количество чисел", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберете способ вычисления!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            string ResultText = string.Empty;
            long Result = 0;
            int counter = 1;
            if(comboBox1.SelectedIndex == 0)
            {
                while (counter <= Lng)
                {
                    ResultText += $"{counter * 5 - 3} + ";
                    Result += (counter * 5 - 3);
                    counter++;
                }
            }
            else
            {
                while (counter <= Lng)
                {
                    ResultText += $"{counter * 3 - 2} + ";
                    Result += (counter * 3 - 2);
                    counter++;
                }
            }
            ResultText = ResultText.Remove(ResultText.Length-3,3);
            textBox1.Text = ResultText + $" = {Result}";
        }
    }
}
