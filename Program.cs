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
    
    class Airplane // 1 объект
    {
        public Airplane()
        {
            speed = 0;
            height = 0;
        }
        public void change_speed()
        {            
            ConsoleKeyInfo key = ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.D:
                    if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                    {
                        WriteLine("+ 150 speed");
                        speed += 150;
                    }
                    else
                    {
                        WriteLine("+ 50 speed");
                        speed += 50;
                    }
                    break;
                case ConsoleKey.A:
                    if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                    {
                        WriteLine("- 150 speed");
                        speed -= 150;
                    }
                    else
                    {
                        WriteLine("- 50 speed");
                        speed -= 50;
                    }
                    break;

            }
        }

        public void change_height()
        {
            ConsoleKeyInfo key = ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.W:
                    if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                    {
                        WriteLine("+ 500 height");
                        height += 500;
                    }
                    else
                    {
                        WriteLine("+ 250 height");
                        height += 250;
                    }
                    break;
                case ConsoleKey.S:
                    if ((ConsoleModifiers.Shift & key.Modifiers) != 0)
                    {
                        WriteLine("- 500 height");
                        height -= 500;
                    }
                    else
                    {
                        WriteLine("- 250 height");
                        height -= 250;
                    }
                    break;

            }
        }

        public int speed { get; set; }
        public int height { get; set; }

        int path = 1000;
    }

    class Dispathcher : Airplane
    {
        public Dispathcher()
        {

        }
       public Dispathcher (string n)
        {
            name = n;
        }

        public void recommendedHeight(Airplane tmp)
        {
            Write($"Диспетчер {name} установил рекомендованную высоту: ");
            Random N = new Random();
            weather = 7 * tmp.speed - N.Next(-200, 200);
            WriteLine(weather);
        }

        public void impose_Fine(Airplane tmp)
        {
            if(tmp.height!=weather)
            {
                if(tmp.height > 300 && tmp.height < 600)
                {
                    WriteLine("fine + 25");
                    fine += 25;
                }
                if (tmp.height > 600 && tmp.height < 1000)
                {
                    WriteLine("fine + 50");
                    fine += 50;
                }
            }
        }

        public void flight(Airplane tmp)
        {
            recommendedHeight(tmp);
            tmp.change_height();
            impose_Fine(tmp);
            tmp.change_speed();
        }

        string name;
        int weather;
        int fine;     
    }

   /*class Flight : Dispathcher
    {
        public void flight(Airplane tmp)
        {
            recommendedHeight(tmp);
            tmp.change_height();
            impose_Fine(tmp);
            tmp.change_speed();
        }
    }*/
   
    internal class Program
    {
        public delegate void AirplaneDelegate();
        public event AirplaneDelegate AirplaneEvent;

        static void Main(string[] args)
        {
            AirplaneDelegate del = null;
            WriteLine("УПРАВЛЕНИЕ НА WASD");
            
            Airplane a = new Airplane();
            string name, name2;
            Write("Введите имя первого диспетчера: ");
            name = ReadLine();
            Write("Введите имя второго диспетчера: ");
            name2 = ReadLine();
            WriteLine("Нажмите D, чтобы начать полёт");

            Dispathcher d = new Dispathcher(name);
            a.change_speed();

          //  Flight f = new Flight();

            if (a.speed >= 50)
            {
                while (a.speed <= 500)
                {
                    d.flight(a);
                }  
            }

            
            
        }
    }
}
