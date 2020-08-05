namespace ShadiHallBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class farhan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        A_ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.A_ID);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        B_ID = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(nullable: false, storeType: "date"),
                        ToDate = c.DateTime(nullable: false, storeType: "date"),
                        Total = c.Int(nullable: false),
                        Sh_ID = c.Int(nullable: false),
                        U_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.B_ID)
                .ForeignKey("dbo.Shifts", t => t.Sh_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.U_ID, cascadeDelete: true)
                .Index(t => t.Sh_ID)
                .Index(t => t.U_ID);
            
            CreateTable(
                "dbo.Shifts",
                c => new
                    {
                        Sh_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        S_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Sh_ID)
                .ForeignKey("dbo.ShaadiHalls", t => t.S_ID, cascadeDelete: true)
                .Index(t => t.S_ID);
            
            CreateTable(
                "dbo.ShaadiHalls",
                c => new
                    {
                        S_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Town = c.String(),
                        Address = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.S_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        U_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email_ID = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.U_ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        email = c.String(),
                        subject = c.String(),
                        message = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Se_ID = c.Int(nullable: false, identity: true),
                        Wifi = c.String(),
                        musicSystem = c.String(),
                        Decoration = c.String(),
                        speciaLights = c.String(),
                        Lights = c.String(),
                        Dj = c.String(),
                        bikeParking = c.String(),
                        carParking = c.String(),
                        AirCondition = c.String(),
                        socialMediaPages = c.String(),
                        Seggretion = c.String(),
                        Catering = c.String(),
                        Projector = c.String(),
                        stageDecoration = c.String(),
                        ladiesWaitress = c.String(),
                        peopleCapacity = c.String(),
                        Electricity = c.String(),
                        S_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Se_ID)
                .ForeignKey("dbo.ShaadiHalls", t => t.S_ID, cascadeDelete: true)
                .Index(t => t.S_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "S_ID", "dbo.ShaadiHalls");
            DropForeignKey("dbo.Bookings", "U_ID", "dbo.Users");
            DropForeignKey("dbo.Shifts", "S_ID", "dbo.ShaadiHalls");
            DropForeignKey("dbo.Bookings", "Sh_ID", "dbo.Shifts");
            DropIndex("dbo.Services", new[] { "S_ID" });
            DropIndex("dbo.Shifts", new[] { "S_ID" });
            DropIndex("dbo.Bookings", new[] { "U_ID" });
            DropIndex("dbo.Bookings", new[] { "Sh_ID" });
            DropTable("dbo.Services");
            DropTable("dbo.Messages");
            DropTable("dbo.Users");
            DropTable("dbo.ShaadiHalls");
            DropTable("dbo.Shifts");
            DropTable("dbo.Bookings");
            DropTable("dbo.Admins");
        }
    }
}
