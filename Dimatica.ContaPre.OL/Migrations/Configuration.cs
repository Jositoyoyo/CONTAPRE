namespace Dimatica.ContaPre.OL.Migrations
{
    #region NameSpaces

    using System.Data.Entity.Migrations;

    #endregion

    internal sealed class Configuration : DbMigrationsConfiguration<Dimatica.ContaPre.OL.Models.ContaPreModel>
    {
        #region Constructors and Desctructors

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        #endregion

        #region Private Methods

        protected override void Seed(Dimatica.ContaPre.OL.Models.ContaPreModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }

        #endregion
    }
}