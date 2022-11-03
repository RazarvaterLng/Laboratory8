using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Collections.Generic;
using System.Reflection;

namespace WinF
{
    static class Parser
    {
        //Делегат и событие для выдачи сообщений в форму
        public delegate void ErrorMessage(string Message);
        public static event ErrorMessage show;

        //Делегат и событие для отрисовки графика функции заданной пользователем
        public delegate void PrintGraphic(List<double> points,int startX,double Sh ,int x,int y);
        public static event PrintGraphic printGraphic;

        /// <summary>
        /// Подсчёт выражения указанного в виде строки
        /// </summary>
        /// <param name="Expression">Математическое выражение в формате строки</param>
        /// <returns>Вычисленное значение выражения либо Nan если выражение некорректно</returns>
        public static double Calculate(string Expression)
        {
            if (Expression.IndexOf("x") != -1)
            {
                CalculateGraphic(Expression);
                return double.NaN;
            }
            //Заменяем функции на вызов мат операций
            setFunctions(ref Expression);
            //Код динамически компилируемого приложения
            string sourcecode = @"using System;
                                namespace WinF
                                {
                                     public static class calculate
                                     {
                                        public static double Cot(double x)
                                        {
                                            return Math.Cos(x)/Math.Sin(x);
                                        }

                                        public static double Acot(double x)
                                        {
                                            return Math.PI / 2 - Math.Atan(x);
                                        }

                                        public static double calc()
                                        {
                                            return expression;
                                        }
                                     }
                                }";
            //Заменяем [expression] в коде на полученное ранее выражение
            sourcecode = sourcecode.Replace("expression", Expression); 
            try
            {
                //Создание provider(получаем доступ к компилятору)
                //Компиляция программы и получение её результатов(компиляции)
                CompilerResults results = new CSharpCodeProvider().CompileAssemblyFromSource(new CompilerParameters(), sourcecode);

                //получаем тип класса calculate(его поля методы и т.д)
                Type calc = results.CompiledAssembly.GetType("WinF.calculate");

                //Получаем нужный нам метод
                //Вызываем его с null параметрами и преобразуем object в строку, а строку в double
                return double.Parse(calc.GetMethod("calc").Invoke(null, new object[0]).ToString());
            }
            catch (Exception)
            {
                show.Invoke("Ошибка в выражении, проверьте правильность ввода");
                return double.NaN;
            }
        }
        /// <summary>
        /// Замена абревиатур на вызов методов
        /// </summary>
        /// <param name="Expression">Математическое выражение в виде строки</param>
        private static void setFunctions(ref string Expression)
        {
            //Заменяем операции,на вызов соответсвующих методов
            //(дальше можно сделать больше поддерживаемых операций)
            Expression = Expression.Replace("Asin", "Math.Asin");
            Expression = Expression.Replace("Acos", "Math.Acos");
            Expression = Expression.Replace("Atg", "Math.Atan");
            Expression = Expression.Replace("Actg", "Acot");
            Expression = Expression.Replace("Sin", "Math.Sin");
            Expression = Expression.Replace("Cos", "Math.Cos");
            Expression = Expression.Replace("Ctg", "Cot");
            Expression = Expression.Replace("Tg", "Math.Tan");
            Expression = Expression.Replace("Pow", "Math.Pow");
            Expression = Expression.Replace("Sqrt", "Math.Sqrt");
            Expression = Expression.Replace("Abs", "Math.Abs");
            Expression = Expression.Replace("Exp", "Math.Exp");
            Expression = Expression.Replace("Log", "Math.Log");
        }
        /// <summary>
        /// Подсчёт точек графика
        /// </summary>
        /// <param name="Expression">Математическое выражение в виде строки</param>
        private static void CalculateGraphic(string Expression)
        {
            //Заменяем функции на вызов мат операций
            setFunctions(ref Expression);
            //Код динамически компилируемого приложения
            string sourcecode = @"using System;
                                namespace WinF
                                {
                                     public static class calculate
                                     {
                                        public static double Cot(double x)
                                        {
                                            return Math.Cos(x)/Math.Sin(x);
                                        }

                                        public static double Acot(double x)
                                        {
                                            return Math.PI / 2 - Math.Atan(x);
                                        }

                                        public static double calc(double x)
                                        {
                                            return expression;
                                        }
                                     }
                                }";
            //Заменяем [expression] в коде на полученное ранее выражение
            sourcecode = sourcecode.Replace("expression", Expression);
            try
            {
                //Создание provider(получаем доступ к компилятору)
                //Компиляция программы и получение её результатов(компиляции)
                CompilerResults results = new CSharpCodeProvider().CompileAssemblyFromSource(new CompilerParameters(), sourcecode);

                //получаем тип класса calculate(его поля методы и т.д)
                Type calc = results.CompiledAssembly.GetType("WinF.calculate");

                MethodInfo met = calc.GetMethod("calc");

                List<double> points = new List<double>();
                int MinLength = -40, MaxLength = 40;
                double sh = Properties.Settings.Default.accuracy;
                for (double i = MinLength; i <MaxLength; i+=sh)
                {
                
                        //Получаем нужный нам метод
                        //Вызываем его с null параметрами и преобразуем object в строку, а строку в double
                        points.Add(double.Parse(met.Invoke(null, new object[1] {i}).ToString()));
                
                }
                printGraphic.Invoke(points, MinLength, sh, 2, 2);
            }
            catch (Exception)
            {
                show.Invoke("Ошибка в выражении, проверьте правильность ввода");
                return;
            }
        }
    }
}
