namespace MailPig.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class EditedMailPigUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OrganisationName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Oib", c => c.String(maxLength: 11));
        }

        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Oib", c => c.String());
            DropColumn("dbo.AspNetUsers", "OrganisationName");
        }
    }
}