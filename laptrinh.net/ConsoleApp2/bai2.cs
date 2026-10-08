using System;

class BaiB
{
    public static void Chay()
    {
        Console.Write("Nhap a: ");
        double a = double.Parse(Console.ReadLine()!);

        Console.Write("Nhap b: ");
        double b = double.Parse(Console.ReadLine()!);

        Console.Write("Nhap c: ");
        double c = double.Parse(Console.ReadLine()!);

        if (a == 0)
        {
            if (b == 0)
            {
                if (c == 0)
                    Console.WriteLine("Vo so nghiem");
                else
                    Console.WriteLine("Vo nghiem");
            }
            else
            {
                double x = -c / b;
                Console.WriteLine($"x = {x:F2}");
            }
        }
        else
        {
            double delta = b * b - 4 * a * c;

            if (delta > 0)
            {
                double x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b - Math.Sqrt(delta)) / (2 * a);

                Console.WriteLine($"x1 = {x1:F2}");
                Console.WriteLine($"x2 = {x2:F2}");
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                Console.WriteLine($"Nghiem kep x = {x:F2}");
            }
            else
            {
                Console.WriteLine("Vo nghiem");
            }
        }
    }
}