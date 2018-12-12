namespace Bookstore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Isbn = c.String(),
                        PageCount = c.Int(nullable: false),
                        ThumbnailUrl = c.String(),
                        PublishedDate = c.DateTime(nullable: false),
                        LongDescription = c.String(),
                        Status = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookAuthor",
                c => new
                    {
                        BookRefId = c.Long(nullable: false),
                        AuthorRefId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookRefId, t.AuthorRefId })
                .ForeignKey("dbo.Books", t => t.BookRefId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.AuthorRefId, cascadeDelete: true)
                .Index(t => t.BookRefId)
                .Index(t => t.AuthorRefId);
            
            CreateTable(
                "dbo.BookCategory",
                c => new
                    {
                        BookRefId = c.Long(nullable: false),
                        CategoryRefId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookRefId, t.CategoryRefId })
                .ForeignKey("dbo.Books", t => t.BookRefId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryRefId, cascadeDelete: true)
                .Index(t => t.BookRefId)
                .Index(t => t.CategoryRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookCategory", "CategoryRefId", "dbo.Categories");
            DropForeignKey("dbo.BookCategory", "BookRefId", "dbo.Books");
            DropForeignKey("dbo.BookAuthor", "AuthorRefId", "dbo.Authors");
            DropForeignKey("dbo.BookAuthor", "BookRefId", "dbo.Books");
            DropIndex("dbo.BookCategory", new[] { "CategoryRefId" });
            DropIndex("dbo.BookCategory", new[] { "BookRefId" });
            DropIndex("dbo.BookAuthor", new[] { "AuthorRefId" });
            DropIndex("dbo.BookAuthor", new[] { "BookRefId" });
            DropTable("dbo.BookCategory");
            DropTable("dbo.BookAuthor");
            DropTable("dbo.Categories");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
