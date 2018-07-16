using System.Data.Entity.Migrations;
using GameManager.DbContext;

namespace GameManager.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GameContext>
    {
        #region Constructors

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        #endregion

        protected override void Seed(GameContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}