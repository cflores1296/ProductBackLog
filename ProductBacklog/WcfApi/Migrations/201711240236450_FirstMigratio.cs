namespace WcfApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigratio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbUsers", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbUsers", "Comment");
        }
    }
}
