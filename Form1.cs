using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace WinF
{
    public partial class LittleCalculator : Form
    {
        private bool IsError = false;
        private Graphics g;
        private Graphic graph;
        public LittleCalculator()=>
            InitializeComponent();
        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        /// <param name="message"></param>
        private void ErrorMessage(string message)
        { 
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            IsError = true;
        }

        /// <summary>
        /// печать операции в конец строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintSymbol(object sender, EventArgs e)=>
            VrTextBox.Text += sender.GetType().GetProperty("Text").GetValue(sender).ToString();
        /// <summary>
        /// Вычисление выражения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintResult(object sender, EventArgs e)
        {
            trackBar2.Enabled = false;
                double Result = Parser.Calculate(VrTextBox.Text);
                if (!IsError && VrTextBox.Text.IndexOf("x")==-1)
                    VrTextBox.Text = Result.ToString().ToLower() == "не число" ? "Не определён" : Result.ToString().ToLower();
                IsError = false;
            trackBar2.Enabled = true;
        }  
        /// <summary>
        /// Очистка поля ввода выражения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearField(object sender, EventArgs e) =>
            VrTextBox.Text = string.Empty;
        /// <summary>
        /// Установка ивентов в Parser и инициализация Picturebox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LittleCalculator_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(trackBar2, "Для быстро растущих вверх функций ставьте шаг 0.007 и менее(например Tg, Ctg, степенные и т.д)");
            Parser.show += ErrorMessage;
            Parser.printGraphic += PrintGraphic;
            Graphic.Image = new Bitmap(Graphic.Width,Graphic.Height);
            g = Graphics.FromImage(Graphic.Image);
        }
        /// <summary>
        /// Отрисовка графика
        /// </summary>
        /// <param name="points"></param>
        /// <param name="startX"></param>
        /// <param name="Sh"></param>
        /// <param name="Xzoom"></param>
        /// <param name="Yzoom"></param>
        private void PrintGraphic(List<double> points, int startX, double Sh, int Xzoom, int Yzoom)
        {
            //Установка зума и создание кистей
            Xzoom = trackBar1.Value;
            Yzoom = trackBar1.Value;
            graph = new Graphic(points,startX,Sh);
            Pen pn = new Pen(Color.FromArgb(130,0,255,0));
            g.Clear(Color.Black);
            int tempX = 0,tempY = 0;
            Pen Gpn = new Pen(Color.FromArgb(20, 190, 190, 190));
            Pen Gpn2 = new Pen(Color.FromArgb(50, 190, 190, 190));

            //----Отрисовка сетки----//
            while (tempX < Graphic.Image.Width / 2)
            { 
                if((tempX / Xzoom) % 5 == 0)
                {
                    g.DrawLine(Gpn2, Graphic.Image.Width / 2 + tempX, 0, Graphic.Image.Width / 2 + tempX, Graphic.Image.Height);
                    g.DrawLine(Gpn2, Graphic.Image.Width / 2 - tempX, 0, Graphic.Image.Width / 2 - tempX, Graphic.Image.Height);
                }
                else
                {
                    g.DrawLine(Gpn, Graphic.Image.Width / 2 + tempX, 0, Graphic.Image.Width / 2 + tempX, Graphic.Image.Height);
                    g.DrawLine(Gpn, Graphic.Image.Width / 2 - tempX, 0, Graphic.Image.Width / 2 - tempX, Graphic.Image.Height);
                }
                tempX += Xzoom;
            }
            while (tempY < Graphic.Image.Height / 2)
            {
                if ((tempY / Yzoom) % 5 == 0)
                {
                    g.DrawLine(Gpn2, 0, Graphic.Image.Height / 2 + tempY, Graphic.Image.Width, Graphic.Image.Height / 2 + tempY);
                    g.DrawLine(Gpn2, 0, Graphic.Image.Height / 2 - tempY, Graphic.Image.Width, Graphic.Image.Height / 2 - tempY);
                }
                else
                {
                    g.DrawLine(Gpn, 0, Graphic.Image.Height / 2 + tempY, Graphic.Image.Width, Graphic.Image.Height / 2 + tempY);
                    g.DrawLine(Gpn, 0, Graphic.Image.Height / 2 - tempY, Graphic.Image.Width, Graphic.Image.Height / 2 - tempY);
                }
                tempY += Yzoom;
            }
            g.DrawLine(new Pen(Color.Yellow), Graphic.Image.Width / 2, 0, Graphic.Image.Width / 2, Graphic.Image.Height);
            g.DrawLine(new Pen(Color.Red), 0, Graphic.Image.Height / 2, Graphic.Image.Width, Graphic.Image.Height / 2);
            //----Отрисовка сетки----//

            //----Отрисовка графика
            double X = startX;
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i] - points[i + 1] < 200 && points[i + 1] - points[i] < 200)
                    g.DrawLine(pn, (float)(Graphic.Image.Width / 2 + X * Xzoom), (-(float)points[i] * Yzoom) + Graphic.Image.Height / 2, (float)(Graphic.Image.Width / 2 + (X + Sh) * Xzoom), (-(float)points[i + 1] * Yzoom) + Graphic.Image.Height / 2);
                X += Sh;
            }
            Graphic.Refresh();
        }     
        /// <summary>
        /// Изменение зума графика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"Зум графика: {trackBar1.Value - 9}x";
            if (graph != null)
                PrintGraphic(graph.points, graph.startX, graph.Sh, trackBar1.Value,trackBar1.Value);
        }
        /// <summary>
        /// Изменение точности графика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label3.Text = (trackBar2.Value / 1000.0).ToString() + "x";
            Properties.Settings.Default.accuracy = trackBar2.Value / 1000.0;
        }
    }
    /// <summary>
    /// Класс в объекте которого хранится график
    /// </summary>
    public class Graphic
    {
        public List<double> points;
        public int startX;
        public double Sh;
        public Graphic(List<double> points, int startX, double sh)
        {
            this.points = points;
            this.startX = startX;
            Sh = sh;
        }
    }
}
