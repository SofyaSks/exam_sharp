﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;
using System.Windows.Forms;
using System.IO;

namespace exam_sharp
{ 
    public delegate void FlightDelegate();

    public delegate void changeDisp(string str);
    public class Airplane 
    {
        public class maxspeedException : ApplicationException
        {
            string message = "Превышена максимальная скорость.";

            public override string Message
            {
                get { return message; }
            }
        }

            public Airplane()
        {
            speed = 0;
            height = 0;
        }

        public void change_speed()
        {
            WriteLine("Измените скорость ");
            ConsoleKeyInfo key = ReadKey(true);
            try {
                using (FileStream fs = new FileStream("speed_change.txt", FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                    {
                        DateTime dt = DateTime.Now;
                        sw.WriteLine(dt);
                        switch (key.Key)
                        {
                            case ConsoleKey.D:
                                if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                                {
                                    Clear();
                                    speed += 150;
                                    WriteLine($"+ 150 скорость \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                    WriteLine(($"              \t\t\t\t\t\t\t\t\t\t Текущая высота   - {height}"));
                                    sw.WriteLine($"+ 150 скорость \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                    WriteLine(speed);
                                }
                                else
                                {
                                    Clear();
                                    speed += 50;
                                    WriteLine($"+ 50 скорость \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                    WriteLine(($"              \t\t\t\t\t\t\t\t\t\t Текущая высота   - {height}"));
                                    sw.WriteLine($"+ 50 скорость \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");

                                }
                                break;
                            case ConsoleKey.A:
                                if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                                {
                                    Clear();
                                    speed -= 150;
                                    WriteLine($"- 150 скорость \t\t\t\t\t\t\t\t\t\tТекущая скорость - {speed}");
                                    WriteLine(($"               \t\t\t\t\t\t\t\t\t\tТекущая высота   - {height}"));
                                    sw.WriteLine($"- 150 скорость \t\t\t\t\t\t\t\t\t\tТекущая скорость - {speed}");

                                }
                                else
                                {
                                    Clear();
                                    speed -= 50;
                                    WriteLine($"- 50 скорость \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                    WriteLine(($"              \t\t\t\t\t\t\t\t\t\t Текущая высота   - {height}"));
                                    sw.WriteLine($"- 50 скорость \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");

                                }
                                break;
                            default: break;

                        }

                        if (speed > 500)
                        {
                            throw new maxspeedException();
                        }
                    }
                }
            }
            catch(maxspeedException mse)
            {
                WriteLine(mse.Message);
            }

            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        public void change_height()
        {
            WriteLine("Измените высоту ");
            ConsoleKeyInfo key = ReadKey(true);

            using (FileStream fs = new FileStream("height_change.txt", FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    DateTime dt = DateTime.Now;
                    sw.WriteLine(dt);
                    switch (key.Key)
                    {
                        case ConsoleKey.W:
                            if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                            {
                                Clear();
                                height += 500;
                                WriteLine(($"+ 500 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}"));
                                WriteLine($"              \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                sw.WriteLine(($"+ 500 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}"));

                            }
                            else
                            {
                                Clear();
                                height += 250;
                                WriteLine($"+ 250 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}");
                                WriteLine($"              \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                sw.WriteLine($"+ 250 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}");
                            }
                            break;
                        case ConsoleKey.S:
                            if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                            {
                                Clear();
                                height -= 500;
                                WriteLine($"- 500 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}");
                                WriteLine($"              \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                sw.WriteLine($"- 500 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}");

                            }
                            else
                            {
                                Clear();
                                height -= 250;
                                WriteLine($"- 250 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}");
                                WriteLine($"              \t\t\t\t\t\t\t\t\t\t Текущая скорость - {speed}");
                                sw.WriteLine($"- 250 высота \t\t\t\t\t\t\t\t\t\t Текущая высота - {height}");
                            }
                            break;
                        default: break;
                    }
                }
            }

        }


        public void halfWay(string str)
        {
            WriteLine(str);
        }


        public int speed { get; set; }
        public int height { get; set; }



    }
}
