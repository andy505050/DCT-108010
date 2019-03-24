using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ContosoUniversityEntities();

            //db.Database.Log = (msg) => Console.WriteLine(msg);

            SelectCourseWithRelation(db);
            Console.WriteLine("--");
            SelectDepartmentWithRelation(db);
        }

        private static void SelectCourseByGitOrderByCredits(ContosoUniversityEntities db)
        {
            var c = from p in db.Course
                    where p.Title.StartsWith("Git")
                    orderby p.Credits descending
                    select new
                    {
                        p.Title
                    };

            foreach (var item in c)
            {
                Console.WriteLine(item.Title);
            }
        }

        private static void SelectCourseWithRelation(ContosoUniversityEntities db)
        {
            var c = from p in db.Course
                    select p;

            foreach (var item in c)
            {
                Console.WriteLine(item.Department.Name + "\t" + item.Title);
            }
        }

        private static void SelectDepartmentWithRelation(ContosoUniversityEntities db)
        {
            var dept = from p in db.Department
                    select p;

            foreach (var item in dept)
            {
                Console.WriteLine(item.Name);

                foreach (var c in item.Course)
                {
                    Console.WriteLine("\t" + c.Title);
                }
            }
        }
    }
}
