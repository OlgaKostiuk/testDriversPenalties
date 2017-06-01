namespace DataFines.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsersBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FineId = c.Int(nullable: false),
                        UsersBrandId = c.Int(nullable: false),
                        State = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fines", t => t.FineId, cascadeDelete: true)
                .ForeignKey("dbo.UsersBrands", t => t.UsersBrandId, cascadeDelete: true)
                .Index(t => t.FineId)
                .Index(t => t.UsersBrandId);
            
            CreateTable(
                "dbo.Fines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        DriveLicense = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersBrands", "UserId", "dbo.Users");
            DropForeignKey("dbo.Histories", "UsersBrandId", "dbo.UsersBrands");
            DropForeignKey("dbo.Histories", "FineId", "dbo.Fines");
            DropForeignKey("dbo.UsersBrands", "BrandId", "dbo.Brands");
            DropIndex("dbo.Histories", new[] { "UsersBrandId" });
            DropIndex("dbo.Histories", new[] { "FineId" });
            DropIndex("dbo.UsersBrands", new[] { "BrandId" });
            DropIndex("dbo.UsersBrands", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Fines");
            DropTable("dbo.Histories");
            DropTable("dbo.UsersBrands");
            DropTable("dbo.Brands");
        }
    }
}
