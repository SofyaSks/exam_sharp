using System;
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

    internal class Program
    {
        static string file_name = "training_results.txt";

        public class speedException : ApplicationException
        {
            string message = "Во время посадки взлёт запрещён.";

            public override string Message
            {
                get { return message; }
            }
        }

        public class zeroException : ApplicationException
        {
            string message = "Самолёт разбился";

            public override string Message
            {
                get { return message; }
            }
        }
        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;

            FlightDelegate del = null;
            DispReg delreg = null;
            WriteLine("УПРАВЛЕНИЕ НА WASD (чтобы не применять изменения нажмите X)");

            try
            {
                using (FileStream fs = new FileStream(file_name, FileMode.Append))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                    {
                        sw.WriteLine(dt);
                        Airplane a = new Airplane();
                        string name1, name2;
                        Write("Введите имя первого диспетчера: ");
                        name1 = ReadLine();
                        Clear();
                        Write("Введите имя второго диспетчера: ");
                        name2 = ReadLine();
                        Clear();


                        Dispathcher d = new Dispathcher(name1);
                        Dispathcher d2 = new Dispathcher(name2);
                        a.change_speed();
                        d.recommendedHeight(a);


                        del += a.change_height;
                        del += a.change_speed;

                        delreg += d.impose_Fine;
                        delreg += d.check_fine;
                        delreg += d.recommendedHeight;

                        try
                        {
                            while (a.speed >= 50 && a.speed < 500)
                            {
                                foreach (FlightDelegate item in del.GetInvocationList())
                                {
                                    item();
                                }

                                foreach (DispReg item in delreg.GetInvocationList())
                                {
                                    item(a);
                                }
                                if (a.speed >= 500)
                                {
                                    d.change += a.halfWay;
                                    d.halfWay("Половина пути пройдена", ref d, ref d2, ref delreg);
                                    WriteLine("Можете приступить к снижению");
                                    sw.WriteLine("Можете приступить к снижению");
                                }
                                if (a.speed == 0 || a.height == 0)
                                {
                                    throw new zeroException();
                                }

                            }

                           


                            try
                            {
                                d2.change += a.endOfFlight;                               
                                int maxheight;
                                while (a.speed >= 50 && a.speed <= 1000 && a.height >= 0)
                                {
                                    d2.endOfFlight("Поздравляем с успешным завершением полёта!", a, ref d2, ref delreg, ref del);
                                    maxheight = a.height;
                                    foreach (FlightDelegate item in del.GetInvocationList())
                                    {
                                        item();
                                    }

                                    foreach (DispReg item in delreg.GetInvocationList())
                                    {
                                        item(a);
                                    }

                                    if (a.height > maxheight)
                                    {
                                        throw new speedException();
                                    }
                                }
                            }
                            catch (speedException spex)
                            {
                                WriteLine(spex.Message);
                            }
                            catch (Exception e)
                            {
                                WriteLine(e.Message);
                            }

                            
                        }
                        catch (zeroException ze)
                        {
                            WriteLine(ze);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }
    }
}
       


    

