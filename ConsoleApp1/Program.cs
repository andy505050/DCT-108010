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

            db.Database.Log = (msg) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(msg);
                Console.ResetColor();
            };

            //SelectCourseByGitOrderByCredits(db);

            //SelectCourseWithRelation(db);
            //Console.WriteLine("--");
            //SelectDepartmentWithRelation(db);

            PracticeCRUD(db);


        }

        private static void PracticeCRUD(ContosoUniversityEntities db)
        {
            var dept = new Department()
            {
                Name = "酷奇資訊",
                Budget = 18000,
                StartDate = new DateTime(2019, 6, 1, 0, 0, 0)
            };

            dept.Course.Add(new Course()
            {
                Title = "ASP.NET MVC 5 開發實戰",
                Credits = 1,
                Department = dept
            });

            dept.Course.Add(new Course()
            {
                Title = "Angular 7 開發實戰",
                Credits = 1,
                Department = dept
            });

            Console.WriteLine("------ 新增部門與課程資料 ------");
            db.Department.Add(dept);
            db.SaveChanges();

            Console.WriteLine("------ 取得新增後的 DepartmentID ------");

            Console.WriteLine("新部門 ID = " + dept.DepartmentID);

            Console.WriteLine("------ 重新查詢之前新增的部門資料 ------");

            var new_dept = db.Department.Find(dept.DepartmentID);

            Console.WriteLine("部門名稱: " + new_dept.Name);

            Console.WriteLine("------ 查詢該部門所有課程資料 ------");

            foreach (var course in new_dept.Course)
            {
                course.Credits = 5;
                Console.WriteLine("\t" + course.Title + "\t" + course.Credits);
            }

            Console.WriteLine("------ 更新該部門所有課程的 Credits 分數 ------");

            db.SaveChanges();

            db.Course.RemoveRange(new_dept.Course);
            db.Department.Remove(new_dept);

            Console.WriteLine("------ 移除課程資料與部門資料 ------");

            db.SaveChanges();
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
