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
    public delegate void DispReg(Airplane tmp);
    class Dispathcher : Airplane
    {
       

        public event changeDisp change;
        public Dispathcher() { }
        public Dispathcher(string n)
        {
            name = n;
        }

        DateTime dt = DateTime.Now;
        public void recommendedHeight(Airplane tmp)
        {
            using (FileStream fs = new FileStream("recommended_height.txt", FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    DateTime dt = DateTime.Now;
                    sw.WriteLine(dt);
                    Write($"Диспетчер {name} установил рекомендованную высоту: ");
                    sw.Write($"Диспетчер {name} установил рекомендованную высоту: ");
                    Random N = new Random();
                    weather =7 * tmp.speed - N.Next(-200, 200);
                    WriteLine(weather);
                    sw.WriteLine(weather);
                }
            }
        }

        public void impose_Fine(Airplane tmp)
        {
            using (FileStream fs = new FileStream("impose_fine.txt", FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    DateTime dt = DateTime.Now;
                    sw.WriteLine(dt);
                    if ((weather - tmp.height) >= 100 && (weather - tmp.height) < 200)
                    {
                        Clear();
                        fine += 25;
                        WriteLine($"У ВАС ШТРАФ +25    \t\t\t\t\t\t\t\t\t Всего штрафных очков - {fine}");
                        WriteLine($"                     \t\t\t\t\t\t\t\t\t Текущая скорость - {tmp.speed}");
                        WriteLine(($"                    \t\t\t\t\t\t\t\t\t Текущая высота   - {tmp.height}"));
                        sw.WriteLine($"У ВАС ШТРАФ +25 \t\t\t\t\t\t\t\t Всего штрафных очков - {fine}");

                    }
                    if (weather - tmp.height >= 200)
                    {
                        Clear();
                        fine += 50;
                        WriteLine($"У ВАС ШТРАФ +50    \t\t\t\t\t\t\t\t\t Всего штрафных очков - {fine}");
                        WriteLine($"                     \t\t\t\t\t\t\t\t\t Текущая скорость - {tmp.speed}");
                        WriteLine(($"                    \t\t\t\t\t\t\t\t\t Текущая высота   - {tmp.height}"));
                        sw.WriteLine($"У ВАС ШТРАФ +50 \t\t\t\t\t\t\t\t Всего штрафных очков - {fine}");

                    }
                }
            }

        }

        public void check_fine(Airplane tmp)
        {
            if (fine >= 500)
            {
                Clear();
                WriteLine("Непригоден к полётам");

            }

        }


        public void halfWay(string str, ref Dispathcher d, ref Dispathcher d2, ref DispReg delreg)
        {

            if (change != null)
            {

                Clear();
                delreg -= d.impose_Fine;
                delreg -= d.recommendedHeight;
                delreg -= d.check_fine;

                delreg += d2.impose_Fine;
                delreg += d2.check_fine;
                delreg += d2.recommendedHeight;
                

                change($"{str}. Теперь вас ведёт диспетчер - {d2.name}");
            }


        }

        string name;
        int weather;
        int fine;
    }
}
