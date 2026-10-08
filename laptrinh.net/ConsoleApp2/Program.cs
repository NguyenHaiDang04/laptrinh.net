using System;

class Program
{
    static void Main()
    {
        int chon;

        do
        {
            Console.Clear();

            Console.WriteLine("===== MENU =====");
            Console.WriteLine("1. Bai A - Calculator");
            Console.WriteLine("2. Bai B - Phuong trinh bac 2");
            Console.WriteLine("3. Bai C - So nguyen to & Fibonacci");
            Console.WriteLine("0. Thoat");
            Console.WriteLine("================");

            Console.Write("Nhap lua chon: ");
            chon = int.Parse(Console.ReadLine());

            Console.Clear();

            switch (chon)
            {
                case 1:
                    BaiA.Chay();
                    break;

                case 2:
                    BaiB.Chay();
                    break;

                case 3:
                    BaiC.Chay();
                    break;

                case 0:
                    Console.WriteLine("Thoat chuong trinh!");
                    break;

                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }

            if (chon != 0)
            {
                Console.WriteLine("\nNhan phim bat ky de quay lai...");
                Console.ReadKey();
            }

        } while (chon != 0);
    }
}