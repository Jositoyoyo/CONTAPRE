namespace Dimatica.ContaPre.OL.Migrations
{
    #region NameSpaces

    using System.Data.Entity.Migrations;

    #endregion

    public partial class AddObsoleteToUser : DbMigration
    {
        #region Public Methods

        public override void Up()
        {
            this.AddColumn("dbo.USUARIO", "Obsolete", c => c.Boolean(nullable: false));
            this.AddColumn("dbo.USUARIO", "ObsoleteBy", c => c.Int());
            this.AddColumn("dbo.USUARIO", "ObsoleteDate", c => c.DateTime());
        }

        public override void Down()
        {
            this.DropColumn("dbo.USUARIO", "ObsoleteDate");
            this.DropColumn("dbo.USUARIO", "ObsoleteBy");
            this.DropColumn("dbo.USUARIO", "Obsolete");
        }

        #endregion
    }
}