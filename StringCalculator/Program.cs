using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a string of numbers");      
            string numbers = Console.ReadLine();
            Console.WriteLine("Press 'S' to calculate sum");
            if (Console.ReadKey(true).Key == ConsoleKey.S)
            {
                List<string> delimeter = GetDelimeter(numbers);
                int result = AddNumbers(numbers, delimeter);
                Console.WriteLine("Sum:" + result);
            }
           
           
        }

        public static int AddNumbers(string numbers, List<string> delimeterList)
        {
            Regex regex = new Regex(@"(?<!\d)-\d+");
            var result = regex.Matches(numbers);
            Error error = new Error();
            StringBuilder stringBuilder = new StringBuilder();
            if (result.Count() > 0)
            {
                error.Message = "negatives not allowed";
                for (int i = 0; i < result.Count(); i++)
                {
                    stringBuilder.Append(result[i].Value.ToString());
                    if (!(i == result.Count()-1))
                    {
                        stringBuilder.Append(",");
                    }
                    
                }
                error.Value = stringBuilder.ToString();
                throw new Exception(String.Format("{0} {1}", error.Message, error.Value));
            }

            Regex reg = new Regex(String.Format(@"[\{0}]+",String.Join(",", delimeterList)),RegexOptions.Compiled);
            string[] numArray = reg.Split(numbers);

            int total = 0;
            for (int i = 0; i < numArray.Length; i++)
            {
                total += Int32.Parse(numArray[i]);
            }

            return total;
        }

        public static List<string> GetDelimeter(string numbers)
        {
            Regex reg = new Regex(@"^(//)");
            var result = reg.Matches(numbers);

            Regex multipleDelimeter = new Regex(@"(,|\\n)");
            var resultDelimeter = multipleDelimeter.Matches(numbers);
            List<string> delimeter = new List<string>();
            if (result.Count() > 0)
            {
                int startindex = numbers.IndexOf(@"//");
                int endIndex = numbers.IndexOf(@"\n");
                delimeter.Add(numbers.Substring(startindex + 2, endIndex - startindex - 2));
            }
            else if (resultDelimeter.Count() > 0)
            {
                for (int i = 0; i < resultDelimeter.Count(); i++)
                {
                    delimeter.Add(resultDelimeter[i].Value.ToString());
                }
            }
            else
            {
                throw new Exception("Delimeter not supported");
            }

            return delimeter;
        }
    }
}
