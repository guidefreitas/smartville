using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smartville.Models
{
    public class DatabaseContext :  DbContext //IdentityDbContext<User>
    {
        public DatabaseContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DatabaseContext>(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorStatus> SensorStatuses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            */
            
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                    .HasIndex("IX_User_Email",     
                    e => e.Property(x => x.Email));

            modelBuilder.Entity<User>()
                        .HasIndex("IX_User_AuthToken",
                        e => e.Property(x => x.AuthToken));
            
            // Configure Asp Net Identity Tables
            //modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasMaxLength(500);
            //modelBuilder.Entity<User>().Property(u => u.Stamp).HasMaxLength(500);
            //modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasMaxLength(50);
            /*
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<UserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
            modelBuilder.Entity<UserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);
            */

            base.OnModelCreating(modelBuilder);
        }

    }
}
