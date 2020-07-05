using System;

namespace Летняя_практика
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите P");
            int P = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите Q");
            int Q = int.Parse(Console.ReadLine());
            int n = P * Q;
            Console.WriteLine("n=" + n.ToString());
            int F = (P - 1) * (Q - 1);
            Console.WriteLine("F=" + F.ToString());
            int l = 0;
            for (int e = 2; e <= F; e++)
            {
                if (isSimple(e))
                {
                    int min;
                    if (e > F) min = F;
                    else min = e;
                    int i = min;
                    int c = 0;
                    while (i > 0 && c == 0)
                    {
                        if ((e % i == 0) && (F % i == 0))
                            c = i;
                        i--;
                    }
                    if (c > 1) ;
                    else
                    {
                        Console.Write(e.ToString() + " ");
                    }
                   
                }     
            }
            Console.WriteLine(" ");
            Console.WriteLine("Введите E из предложенных выше");
            double E = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите M");
            double M = double.Parse(Console.ReadLine());
            for (double D=0;D<=100;D++) 
            {
                double d=(D * E) % F;
                if (d == 1)
                {
                    Console.WriteLine("D=" + D.ToString());
                    double C = Math.Pow(M, E) % n;
                    Console.WriteLine("C=" + C.ToString());
                    double m = Math.Pow(C, D) % n;
                    Console.WriteLine("M'=" + m.ToString());
                    break;
                }
            } 
            Console.ReadKey();
        }
        private static bool isSimple(int N)
        {
            for (int e = 2; e <= (int)(N / 2); e++)
            {
                if (N % e == 0)
                    return false;
            }
            return true;
        } 
        
    }
}
