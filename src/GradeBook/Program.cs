using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Grade Book");
            book.AddGrade(50.5);
            book.AddGrade(1.1);
            book.AddGrade(99.1);

            var stats = book.GetStatistics();
            Console.WriteLine($"Average Grade is {stats.Average:N1}");
        }
    }
}
