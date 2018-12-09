using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookstore.Entities.Implementations;
using Bookstore.Entities.Interfaces;

namespace Bookstore.Core
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        public DatabaseContext() : base("BookstoreContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasMany<Author>(b => b.Authors)
                        .WithMany(a => a.Books)
                        .Map(ab =>
                        {
                            ab.MapLeftKey("BookRefId");
                            ab.MapRightKey("AuthorRefId");
                            ab.ToTable("BookAuthor");
                        });

            modelBuilder.Entity<Book>()
                        .HasMany<Category>(b => b.Categories)
                        .WithMany(a => a.Books)
                        .Map(ab =>
                        {
                            ab.MapLeftKey("BookRefId");
                            ab.MapRightKey("CategoryRefId");
                            ab.ToTable("BookCategory");
                        });

            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSave()
        {
            SoftDelete();
            AddTimeStamps();
        }

        private void AddTimeStamps()
        {
            var modifiedEntries = ChangeTracker.Entries()
                                                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach(var entry in modifiedEntries)
            {
                DateTime now = DateTime.UtcNow;
                if (entry.Entity is IAuditableEntity entity)
                {
                    if(entry.State == EntityState.Added)
                    {
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedDate = now;
                }

            }
        }

        private void SoftDelete()
        {
            var deletedEntries = ChangeTracker.Entries()
                                              .Where(x => x.Entity is IAuditableEntity && x.State == EntityState.Deleted);

            foreach(var entry in deletedEntries)
            {
                if (entry.Entity is IAuditableEntity entity)
                {
                    Entry(entity).Property(x => x.IsDeleted).CurrentValue = true;
                    entry.State = EntityState.Modified;
                }
            }

        }
    }
}
