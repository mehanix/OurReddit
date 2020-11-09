namespace OurReddit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        CreationDate = c.String(),
                        CategoryId = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        SubjectId = c.String(),
                        CreationDate = c.String(),
                        Content = c.String(nullable: false),
                        edited = c.Boolean(nullable: false),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Description = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Messages", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Messages", new[] { "Subject_Id" });
            DropIndex("dbo.Subjects", new[] { "Category_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Subjects");
            DropTable("dbo.Categories");
        }
    }
}
