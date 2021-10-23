namespace techbWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrationDbCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaCategoryMaster",
                c => new
                    {
                        MediaCategoryId = c.Int(nullable: false, identity: true),
                        MediaCategoryName = c.String(),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.MediaCategoryId);
            
            CreateTable(
                "dbo.MediaFile",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        MediaGalleryId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                        FileDescription = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.MediaGallery", t => t.MediaGalleryId, cascadeDelete: true)
                .Index(t => t.MediaGalleryId);
            
            CreateTable(
                "dbo.MediaGallery",
                c => new
                    {
                        MediaGalleryId = c.Int(nullable: false, identity: true),
                        MediaType = c.Int(nullable: false),
                        Caption = c.String(),
                        Description = c.String(unicode: false, storeType: "text"),
                        Archive = c.Boolean(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        ArchiveDate = c.DateTime(nullable: false),
                        UserRole = c.String(),
                        MediaCategoryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MediaGalleryId)
                .ForeignKey("dbo.MediaCategoryMaster", t => t.MediaCategoryId, cascadeDelete: true)
                .Index(t => t.MediaCategoryId);
            
            CreateTable(
                "dbo.MenuItemMaster",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        SlugMenu = c.String(),
                        SortOrder = c.Int(nullable: false),
                        MenuName = c.String(),
                        HMenuName = c.String(),
                        PageTitle = c.String(),
                        PageHeading = c.String(),
                        HPageHeading = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        PositionType = c.Int(nullable: false),
                        Layout = c.Int(nullable: false),
                        ExternalLink = c.Boolean(nullable: false),
                        ExternalUrl = c.String(),
                        MenuArea = c.String(),
                        MenuUrlId = c.Int(),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.MenuUrlMaster", t => t.MenuUrlId)
                .Index(t => t.MenuUrlId);
            
            CreateTable(
                "dbo.MenuUrlMaster",
                c => new
                    {
                        MenuUrlId = c.Int(nullable: false, identity: true),
                        UrlPrefix = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        PageType = c.Int(nullable: false),
                        MenuLevel = c.Int(nullable: false),
                        UrlArea = c.String(),
                    })
                .PrimaryKey(t => t.MenuUrlId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItemMaster", "MenuUrlId", "dbo.MenuUrlMaster");
            DropForeignKey("dbo.MediaFile", "MediaGalleryId", "dbo.MediaGallery");
            DropForeignKey("dbo.MediaGallery", "MediaCategoryId", "dbo.MediaCategoryMaster");
            DropIndex("dbo.MenuItemMaster", new[] { "MenuUrlId" });
            DropIndex("dbo.MediaGallery", new[] { "MediaCategoryId" });
            DropIndex("dbo.MediaFile", new[] { "MediaGalleryId" });
            DropTable("dbo.MenuUrlMaster");
            DropTable("dbo.MenuItemMaster");
            DropTable("dbo.MediaGallery");
            DropTable("dbo.MediaFile");
            DropTable("dbo.MediaCategoryMaster");
        }
    }
}
