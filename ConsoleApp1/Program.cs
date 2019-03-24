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

            db.Database.Log = (msg) => Console.WriteLine(msg);

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
    }
}
