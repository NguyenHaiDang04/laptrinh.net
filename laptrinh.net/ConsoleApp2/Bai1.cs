using System;

class BaiA
{
    public static void Chay()
    {
        Console.Write("Nhap a: ");
        double a = double.Parse(Console.ReadLine()!);

        Console.Write("Nhap b: ");
        double b = double.Parse(Console.ReadLine()!);

        Console.Write("Nhap phep toan (+,-,*,/,%): ");
        char op = char.Parse(Console.ReadLine()!);

        if ((op == '/' || op == '%') && b == 0)
        {
            Console.WriteLine("Loi: Khong the chia cho 0!");
            return;
        }

        double kq = op switch
        {
            '+' => a + b,
            '-' => a - b,
            '*' => a * b,
            '/' => a / b,
            '%' => a % b,
            _ => 0
        };

        Console.WriteLine($"Ket qua = {kq:F2}");
    }
}