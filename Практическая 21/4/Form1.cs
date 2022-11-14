using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4
{
    public partial class Form1 : Form
    {
        private int[,] array;
        private int[,] rotatedArray;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //СОздание рандомного массива и его печать
            NextGen();
            PrintArrayInTextBox(array, textBox2);
        }
        private void NextGen()
        {
            Random rnd = new Random();
            array = new int[rnd.Next(3, 6), rnd.Next(3, 6)];
            rotatedArray = new int[array.GetLength(1), array.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = rnd.Next(0, 10);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Переворот массива
            for (int i = 0; i < rotatedArray.GetLength(0); i++)
                for (int j = 0; j < rotatedArray.GetLength(1); j++)
                    rotatedArray[i, j] = array[j, i];

            PrintArrayInTextBox(rotatedArray, textBox1);
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

        private void button2_Click(object sender, EventArgs e)
        {
            NextGen();
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            PrintArrayInTextBox(array, textBox2);
        }
    }
}
