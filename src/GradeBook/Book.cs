using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject : object
    {
        public NamedObject(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        String Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public class InMemoryBook : Book
    {
        private List<double> _grades;
        readonly string _category;

        public InMemoryBook(string name) : base(name)
        {
            this._category = "Science";
            _grades = new List<double>();
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                _grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}: {grade}");
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                case 'F':
                    AddGrade(50);
                    break;
                default:
                    break;
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics(_grades);
           
            return result;
        }

    }

    public class DiskBook : Book
    {
        readonly string _category;

        public DiskBook(string name) : base(name)
        {
            this._category = "Science";
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                using(var writer = File.AppendText($"./{this.Name}.txt"))
                {
                    writer.WriteLine(grade);
                    if (GradeAdded != null)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}: {grade}");
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;
                case 'B':
                    AddGrade(80);
                    break;
                case 'C':
                    AddGrade(70);
                    break;
                case 'D':
                    AddGrade(60);
                    break;
                case 'F':
                    AddGrade(50);
                    break;
                default:
                    break;
            }
        }

        public override Statistics GetStatistics()
        {
            var grades = new List<double>();

            using(var reader = File.OpenText($"./{this.Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line!=null)
                {
                    var grade = double.Parse(line);
                    grades.Add(grade);
                    line = reader.ReadLine();
                }
            }
            
            var result = new Statistics(grades);

            return result;
        }

    }
}
