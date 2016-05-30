namespace Smartville.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        State_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.State_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Country_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Institutes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Administrator_Id = c.Long(),
                        City_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Administrator_Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .Index(t => t.Administrator_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(),
                        AuthToken = c.String(maxLength: 100),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, name: "IX_User_Email")
                .Index(t => t.AuthToken, name: "IX_User_AuthToken");
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        SerialNumber = c.String(nullable: false),
                        Address = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        City_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .Index(t => t.City_Id);
            
            CreateTable(
                "dbo.SensorStatus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StatusType = c.Int(nullable: false),
                        Value = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Sensor_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sensors", t => t.Sensor_Id)
                .Index(t => t.Sensor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SensorStatus", "Sensor_Id", "dbo.Sensors");
            DropForeignKey("dbo.Sensors", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Institutes", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Institutes", "Administrator_Id", "dbo.Users");
            DropForeignKey("dbo.Cities", "State_Id", "dbo.States");
            DropForeignKey("dbo.States", "Country_Id", "dbo.Countries");
            DropIndex("dbo.SensorStatus", new[] { "Sensor_Id" });
            DropIndex("dbo.Sensors", new[] { "City_Id" });
            DropIndex("dbo.Users", "IX_User_AuthToken");
            DropIndex("dbo.Users", "IX_User_Email");
            DropIndex("dbo.Institutes", new[] { "City_Id" });
            DropIndex("dbo.Institutes", new[] { "Administrator_Id" });
            DropIndex("dbo.States", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "State_Id" });
            DropTable("dbo.SensorStatus");
            DropTable("dbo.Sensors");
            DropTable("dbo.Users");
            DropTable("dbo.Institutes");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
        }
    }
}
