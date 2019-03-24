using ConsoleApp1.Models;
using System;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Data.Entity.Validation;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ContosoUniversityEntities())
            {
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

                //PracticeCRUD(db);

                //PracticeLazyLoading(db);

                //ManyToManyAddWithExceptionHandling(db);

                //AsNoTracking(db);

            }
        }

        private static void AsNoTracking(ContosoUniversityEntities db)
        {
            var data = db.Course.AsNoTracking();

            foreach (var item in data)
            {
                Console.WriteLine(item.Title);
            }
        }

        private static void ManyToManyAddWithExceptionHandling(ContosoUniversityEntities db)
        {
            var course = db.Course.Find(1);

            course.Instructors.Add(new Person()
            {
                FirstName = "Will",
                LastName = "Huang",
                HireDate = DateTime.Now
                //Discriminator = ""
            });

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        sb.AppendLine($"欄位 {ve.PropertyName} 發生錯誤: {ve.ErrorMessage}");
                    }
                }
                throw new Exception(sb.ToString(), ex);
            }
        }

        private static void PracticeLazyLoading(ContosoUniversityEntities db)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;

            var data = from p in db.Course.Include(p => p.Department)
                       select new
                       {
                           CourseTitle = p.Title,
                           DeptName = p.Department.Name
                       };

            foreach (var item in data)
            {
                Console.WriteLine(item.CourseTitle);
                Console.WriteLine(item.DeptName);
                Console.WriteLine();
            }
        }

        private static void PracticeCRUD(ContosoUniversityEntities db)
        {
            var dept = new Department()
            {
                Name = "酷奇資訊",
                Budget = 18000,
                StartDate = new DateTime(2019, 6, 1, 0, 0, 0),
                UpdatedOn = DateTime.Now
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

            new_dept.UpdatedOn = DateTime.Now;

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
