namespace MailPig.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailReads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SentEmailId = c.Int(nullable: false),
                        ReadDateTime = c.DateTime(nullable: false),
                        IpAddress = c.String(maxLength: 39),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SentEmails", t => t.SentEmailId, cascadeDelete: true)
                .Index(t => t.SentEmailId);

            CreateTable(
                "dbo.SentEmails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipientId = c.Int(nullable: false),
                        EmailId = c.Int(nullable: false),
                        DateSent = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Emails", t => t.EmailId, cascadeDelete: true)
                .ForeignKey("dbo.Subscribers", t => t.RecipientId, cascadeDelete: true)
                .Index(t => t.RecipientId)
                .Index(t => t.EmailId);

            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(maxLength: 250),
                        Body = c.String(storeType: "ntext"),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Surname = c.String(maxLength: 50),
                        Email = c.String(maxLength: 250),
                        SomeText = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.GroupSubscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        SubscriberId = c.Int(nullable: false),
                        DateJoined = c.DateTime(nullable: false),
                        DateLeft = c.DateTime(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Subscribers", t => t.SubscriberId, cascadeDelete: true)
                .Index(t => t.GroupId)
                .Index(t => t.SubscriberId);

            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SentEmails", "RecipientId", "dbo.Subscribers");
            DropForeignKey("dbo.GroupSubscriptions", "SubscriberId", "dbo.Subscribers");
            DropForeignKey("dbo.GroupSubscriptions", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.EmailReads", "SentEmailId", "dbo.SentEmails");
            DropForeignKey("dbo.SentEmails", "EmailId", "dbo.Emails");
            DropIndex("dbo.GroupSubscriptions", new[] { "SubscriberId" });
            DropIndex("dbo.GroupSubscriptions", new[] { "GroupId" });
            DropIndex("dbo.SentEmails", new[] { "EmailId" });
            DropIndex("dbo.SentEmails", new[] { "RecipientId" });
            DropIndex("dbo.EmailReads", new[] { "SentEmailId" });
            DropTable("dbo.Groups");
            DropTable("dbo.GroupSubscriptions");
            DropTable("dbo.Subscribers");
            DropTable("dbo.Emails");
            DropTable("dbo.SentEmails");
            DropTable("dbo.EmailReads");
        }
    }
}