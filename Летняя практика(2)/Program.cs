using System;

namespace Летняя_практика_2алг_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите g");
            int g =int.Parse(Console.ReadLine());
            Console.WriteLine("Введите p");
            int p = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите a");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите b");
            int b = int.Parse(Console.ReadLine());
            double A = Math.Pow(g, a) % p;
            double B = Math.Pow(g, b) % p;
            double Ka =Math.Pow(B,a)%p;
            double Kb = Math.Pow(A, b) % p;
            double K =Math.Pow(g,a*b)%p;
            Console.Write(Ka.ToString() + ";");
            Console.Write(Kb.ToString() + ";");
            Console.Write(K.ToString()+".");
            Console.ReadKey();
        }
    }
}
