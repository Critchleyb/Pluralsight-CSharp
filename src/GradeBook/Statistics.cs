using System;
using System.Collections.Generic;

namespace GradeBook
{
    public class Statistics
    {
        private List<double> _grades;
        public double Average;
        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (this.Average)
                {
                case var d when d >= 90.0:
                    return 'A'; 
                case var d when d >= 80.0:
                    return 'B';
                case var d when d >= 70.0:
                    return 'C';
                case var d when d >= 60.0:
                    return 'D';
                default:
                    return 'F';
                }
            }
        }

        public Statistics(List<double> grades)
        {
            this._grades = grades;
            this.Average = 0.0;
            this.High = double.MinValue;
            this.Low = double.MaxValue;
            CalculateParameters();
        }

        private void CalculateParameters()
        {
             foreach(var grade in _grades)
            {
                this.High = Math.Max(grade, this.High);
                this.Low = Math.Min(grade, this.Low);
                this.Average += grade;
            }

            this.Average /= _grades.Count;
        }
    }
}