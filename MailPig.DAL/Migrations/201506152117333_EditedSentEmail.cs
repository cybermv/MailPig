namespace MailPig.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditedSentEmail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SentEmails", "RecipientId", "dbo.Subscribers");
            AddColumn("dbo.SentEmails", "Subscriber_Id", c => c.Int());
            CreateIndex("dbo.SentEmails", "Subscriber_Id");
            AddForeignKey("dbo.SentEmails", "Subscriber_Id", "dbo.Subscribers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.SentEmails", "Subscriber_Id", "dbo.Subscribers");
            DropIndex("dbo.SentEmails", new[] { "Subscriber_Id" });
            DropColumn("dbo.SentEmails", "Subscriber_Id");
            AddForeignKey("dbo.SentEmails", "RecipientId", "dbo.Subscribers", "Id", cascadeDelete: true);
        }
    }
}