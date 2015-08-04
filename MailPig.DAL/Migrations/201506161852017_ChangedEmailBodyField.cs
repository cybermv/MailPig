namespace MailPig.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedEmailBodyField : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Emails", "Body", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emails", "Body", c => c.String(storeType: "ntext"));
        }
    }
}
