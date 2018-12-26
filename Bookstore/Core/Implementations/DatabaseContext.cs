using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookstore.Entities.Implementations;
using Bookstore.Entities.Interfaces;
using EntityFramework.DynamicFilters;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Bookstore.Core.Implementations
{
    public class DatabaseContext: IdentityDbContext<ApplicationUser, RoleIntPk, long,UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public string IsDeletedKey = "IsDeleted";

        public DatabaseContext() : base("BookstoreContext")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext,Configuration>());
            Configuration.ProxyCreationEnabled = false;
           
        }

        //Will be called from Owin Startup Class
        public static DatabaseContext Create()
        {
            return new DatabaseContext();
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

            modelBuilder.Filter(IsDeletedKey, (IAuditableEntity d) => d.IsDeleted, false);

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
