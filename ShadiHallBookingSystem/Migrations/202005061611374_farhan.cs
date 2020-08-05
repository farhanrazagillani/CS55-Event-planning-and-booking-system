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
                "dbo.Users",
                c => new
                    {
                        U_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email_ID = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.U_ID);
            
            AddColumn("dbo.Bookings", "Total", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "U_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Services", "sitting", c => c.String());
            CreateIndex("dbo.Bookings", "U_ID");
            AddForeignKey("dbo.Bookings", "U_ID", "dbo.Users", "U_ID", cascadeDelete: true);
            DropColumn("dbo.Services", "sitting500");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "sitting500", c => c.String());
            DropForeignKey("dbo.Bookings", "U_ID", "dbo.Users");
            DropIndex("dbo.Bookings", new[] { "U_ID" });
            DropColumn("dbo.Services", "sitting");
            DropColumn("dbo.Bookings", "U_ID");
            DropColumn("dbo.Bookings", "Total");
            DropTable("dbo.Users");
            DropTable("dbo.Admins");
        }
    }
}
