using System;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Task4
{
    internal class Order
    {
        public Order(string input)
        {
            try
            {
                Name = new Regex(@"^\w+(?= - )").Match(input).Value;
                Price = Decimal.Parse(new Regex(@"(?<= - )(.*)(?= - )").Match(input).Value);
                Count = int.Parse(new Regex(@"(\d*)(?=шт)").Match(input).Value);
                Date = DateTime.Parse(new Regex(@"(?<=шт )(.*)").Match(input).Value);
            }
            catch
            {
                throw;
            }
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Count { get; private set; }
        public DateTime Date { get; private set; }

        public void Print(CultureInfo cultureInfo = null)
        {
            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.CurrentCulture;
            }
            Console.WriteLine($"{Name} - {Price.ToString("C", cultureInfo)} - {Count} - {Date.ToString(cultureInfo)}");
        }
    }
}
