using System;
using System.Text.RegularExpressions;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            string login;
            string password;

            Console.WriteLine("Введите ваш логин");
            login = CheckInput(@"[a-zA-Z]+");
            Console.WriteLine("Введите ваш пароль");
            password = CheckInput(@"[\d\W]+");
            Console.WriteLine($"Логин: {login} Пароль: {password}");
        }
        private static string CheckInput(string pattern)
        {
            Regex regex = new Regex(pattern);
            string input;

            while (true)
            {
                input = Console.ReadLine();
                if (regex.IsMatch(input))
                {
                    return input;
                }
                Console.WriteLine("Попробуйте еще раз");
            }
        }
    }
}
