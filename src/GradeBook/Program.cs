﻿using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("Grade Book");
            // book.AddGrade(50.5);
            // book.AddGrade(1.1);
            // book.AddGrade(99.1);

            Console.WriteLine("Enter Grades to add to the book, or q to show stats and quit");
            while(true)
            {
                Console.WriteLine("Enter Value:");
                string input = Console.ReadLine();
                if(input == "q")
                {
                    break;
                }

                try
                {
                    book.AddGrade(double.Parse(input));
                    Console.WriteLine($"Grade: {input} added!");
                }
                catch(ArgumentException error)
                {
                    Console.WriteLine(error.Message);
                }
                catch(FormatException error)
                {
                    Console.WriteLine(error.Message);
                }
            };

            var stats = book.GetStatistics();
            Console.WriteLine($"Average Grade is {stats.Average:N1}");
            Console.WriteLine($"Average Letter Grade is {stats.Letter}");
        }
    }
}
