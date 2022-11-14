using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5
{
    public partial class Form1 : Form
    {
        private int[,] array;
        private int[,] array2;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NextGen();
            PrintArrayInTextBox(array,textBox2);
        }
        private void NextGen()
        {
            Random rnd = new Random();
            array = new int[rnd.Next(3, 6), rnd.Next(3, 6)];
            array2 = new int[array.GetLength(0)-1, array.GetLength(1)-1];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = rnd.Next(0, 10);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int DeletedX = rnd.Next(array.GetLength(0) - 1), DeletedY = rnd.Next(array.GetLength(1) - 1);
            int tempX = 0, tempY;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (i == DeletedX) continue;

                tempY = 0;
                
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (j == DeletedY) continue;
                    array2[tempX,tempY] = array[i, j];
                    tempY++;
                }
                tempX++;
            }
            PrintArrayInTextBox(array2,textBox1);
            textBox1.AppendText($"{Environment.NewLine}НОМЕРА(не индексы){Environment.NewLine}Были удалены строка: {DeletedX + 1}{Environment.NewLine}Столбец: {DeletedY + 1}");
        }
    }
}
