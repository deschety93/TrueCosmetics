namespace TrueCosmetics.BootstrapApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderModuleAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(defaultValueSql: "GETDATE()"),
                        Comment = c.String(),
                        UserAddress_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUserAddresses", t => t.UserAddress_Id, cascadeDelete: true)
                .Index(t => t.UserAddress_Id);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        DateChanged = c.DateTime(defaultValueSql: "GETDATE()"),
                        DeliveryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.ApplicationUserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Comment = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Products", "QuanityInStore", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserAddress_Id", "dbo.ApplicationUserAddresses");
            DropForeignKey("dbo.ApplicationUserAddresses", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderStatus", "OrderID", "dbo.Orders");
            DropIndex("dbo.ApplicationUserAddresses", new[] { "User_Id" });
            DropIndex("dbo.OrderStatus", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "UserAddress_Id" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropColumn("dbo.Products", "QuanityInStore");
            DropTable("dbo.ApplicationUserAddresses");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
