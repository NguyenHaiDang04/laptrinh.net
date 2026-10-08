using System;

class BaiC
{
    static bool IsPrime(int n)
    {
        if (n < 2)
            return false;

        for (int i = 2; i < n; i++)
        {
            if (n % i == 0)
                return false;
        }

        return true;
    }

    static bool IsPerfectNumber(int n)
    {
        int tong = 0;

        for (int i = 1; i < n; i++)
        {
            if (n % i == 0)
                tong += i;
        }

        return tong == n;
    }

    public static void Chay()
    {
        Console.Write("Nhap N: ");
        int n = int.Parse(Console.ReadLine()!);

        if (IsPrime(n))
            Console.WriteLine($"{n} la so nguyen to");
        else
            Console.WriteLine($"{n} khong la so nguyen to");

        if (IsPerfectNumber(n))
            Console.WriteLine($"{n} la so hoan hao");
        else
            Console.WriteLine($"{n} khong la so hoan hao");

        Console.Write("Day Fibonacci: ");

        int a = 0, b = 1;

        for (int i = 0; i < n; i++)
        {
            Console.Write(a + " ");

            int c = a + b;
            a = b;
            b = c;
        }

        Console.WriteLine();
    }
}