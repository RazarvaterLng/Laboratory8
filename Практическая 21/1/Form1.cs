using System;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()=>
            InitializeComponent();
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("Конструкторы класса: " + Environment.NewLine);
            Point point = new Point();
            textBox1.AppendText($"Без параметров: {point.ToString()}\n" + Environment.NewLine);
            point = new Point(5);
            textBox1.AppendText($"С одним параметром: {point.ToString()}\n" + Environment.NewLine);
            point = new Point(6, 10);
            textBox1.AppendText($"С двумя параметрами{point.ToString()}\n" + Environment.NewLine);
        }
    }
    public class Point
    {
        private int x;
        private int y;
        //конструктор без параметров
        public Point()
        {
            x = 0;
            y = 0;
        }
        //Конструктор для 1 поля
        public Point(int x)
        {
            this.x = x;
            y = 0;
        }
        //Конструктор для двух полей
        public Point(int x, int y) : this(x)=>
            this.y = y;

        //Метод для перевода в строку
        public override string ToString()=>$"x: {x}|y: {y}";
    }
}
