using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            //sort all student grades
            Students.Sort((s1, s2) =>
            s1.AverageGrade.CompareTo(s2.AverageGrade));

            //order by descending
            Students = Students.OrderByDescending(s => s.AverageGrade).ToList();

            //find position of this student's average
            int index = Students.FindIndex(s => s.AverageGrade == averageGrade);

            if (index > -1)
            {

                //index start at zero so let's use numbers from 1 position
                int studentPosition = index + 1;

                int percent20 = (int)Math.Round(0.2 * Students.Count);
                int percent40 = percent20 * 2;
                int percent60 = percent20 * 3;
                int percent80 = percent20 * 4;

                //check where student is in logic
                if (studentPosition > 0 && studentPosition <= percent20)
                {
                    return 'A';
                }
                else if (studentPosition > percent20 &&
                    studentPosition <= percent40)
                {
                    return 'B';
                }
                else if (studentPosition > percent40 &&
                    studentPosition <= percent60)
                {
                    return 'C';
                }
                else if (studentPosition > percent60 &&
                    studentPosition <= percent80)
                {
                    return 'D';
                }
            }


            double totalStudentAverage = Students.Sum(x => x.AverageGrade);



            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students " +
                    "with grades in order to properly calculate a student's overall " +
                    "grade.");

                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students " +
                    "with grades in order to properly calculate a student's overall " +
                    "grade.");

                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
