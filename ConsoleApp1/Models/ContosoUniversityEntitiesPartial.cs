using System;
using System.Data.Entity;

namespace ConsoleApp1.Models
{
    public partial class ContosoUniversityEntities : DbContext
    {
        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries();

            foreach (var entry in entities)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(new { UpdatedOn = DateTime.Now });
                        break;
                    default:
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
