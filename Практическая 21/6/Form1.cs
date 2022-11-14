using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uint x, y;
            int startDigit;
            //Проверки значений
            if (!int.TryParse(textBox1.Text.Split('|')[0], out startDigit))
            {
                MessageBox.Show("Ошибочный ввод!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!uint.TryParse(textBox1.Text.Split('|')[1], out x))
            {
                MessageBox.Show("Ошибочный ввод!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!uint.TryParse(textBox1.Text.Split('|')[2], out y))
            {
                MessageBox.Show("Ошибочный ввод!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int[,] array = new int[x, y];
            int i = 0, j = 0;
            Rotation rotation = Rotation.right;
            //заполение массива
            while (true)
            {
                array[i, j] = startDigit;

                if ((i > 0 && j > 0 && i < array.GetLength(0)-1 && j < array.GetLength(1) -1 )&&array[i, j + 1] != 0 && array[i, j - 1] != 0 && array[i - 1, j] != 0 && array[i + 1, j] != 0)
                    break;

                if (rotation == Rotation.right && (j + 1 != array.GetLength(1) && array[i, j + 1] == 0))
                    j++;
                else if (rotation == Rotation.right)
                {
                    rotation = Rotation.down;
                    continue;
                }
                else if (rotation == Rotation.down && (i + 1 != array.GetLength(0) && array[i + 1, j] == 0))
                    i++;
                else if (rotation == Rotation.down)
                { 
                    rotation = Rotation.left;
                    continue;
                }
                else if (rotation == Rotation.left && (j != 0 && array[i, j - 1] == 0))
                    j--;
                else if (rotation == Rotation.left)
                { 
                    rotation = Rotation.up;
                    continue;
                }
                else if (rotation == Rotation.up && (i != 0 && array[i - 1, j] == 0))
                    i--;
                else if (rotation == Rotation.up)
                { 
                    rotation = Rotation.right;
                    continue;
                }

                startDigit++;
            }

            textBox2.Text = string.Empty;
            PrintArrayInTextBox(array, textBox2);
        }
        private void PrintArrayInTextBox(int[,] array, TextBox a)
        {
            //Печать массива произвольном TextBox'e
            string tempLine;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                tempLine = string.Empty;
                for (int j = 0; j < array.GetLength(1); j++)
                    tempLine += array[i, j] + "\t";

                a.AppendText(tempLine + Environment.NewLine);
            }
        }
        private enum Rotation
        {
            right,
            left,
            up,
            down
        }
    }
}
