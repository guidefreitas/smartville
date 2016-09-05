namespace Smartville.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;
    internal sealed class Configuration : DbMigrationsConfiguration<Smartville.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Smartville.Models.DatabaseContext context)
        {
            context.Countries.Add(new Country
            {
                Name = "Brasil",
                CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
                UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3))

            });

            context.SaveChanges();

            context.States.Add(new State
            {
                Name = "Santa Catarina",
                Code = "SC",
                Country = context.Countries.First(),
                CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
                UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3))
            });

            context.SaveChanges();

            context.Cities.Add(new City
            {
                Name = "Joinville",
                State = context.States.First(),
                CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
                UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3))
            });

            context.SaveChanges();

            var admin = new User
            {
                Name = "Administrador",
                Email = "admin@admin.com",
                Password = Crypto.HashPassword("admin")
            };

            admin.UserType = UserType.GlobalAdministrator;
            context.Users.Add(admin);
            context.SaveChanges();

            var user = new User
            {
                Name = "Administrador",
                Email = "admin@defesaciviljoinville.com.br",
                Password = Crypto.HashPassword("admin123")
            };

            user.UserType = UserType.InstituteAdministrator;
            context.Users.Add(user);
            context.SaveChanges();

            context.Institutes.Add(new Institute
            {
                Name = "Defesa Civil",
                City = context.Cities.First(),
                Administrator = user,
                CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
                UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3))
            });

            context.SaveChanges();

            
            context.Sensors.Add(new Sensor
            {
                Address = "Rua teste",
                City = context.Cities.First(),
                Latitude = 10.00,
                Longitude = 12.00,
                Name = "Sensor 1",
                SerialNumber = "1",
                TimeZone = -3,
                CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3)),
                UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3))

            });

            context.SaveChanges();
            /*
            for(int i = 0; i < 100; i++)
            {
                context.SensorStatuses.Add(new SensorStatus
                {
                    Sensor = context.Sensors.First(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    StatusType = SensorStatusType.TEMPERATURE,
                    Value = i.ToString()
                });

                context.SaveChanges();
            }
            */

            
        }
    }
}
