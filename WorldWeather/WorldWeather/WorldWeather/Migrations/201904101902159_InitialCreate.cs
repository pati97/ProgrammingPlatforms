namespace WorldWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Description = c.String(),
                        WeatherId = c.Int(nullable: false),
                        IconID = c.String(),
                        Date = c.DateTime(nullable: false),
                        WindType = c.String(),
                        WindDirection = c.String(),
                        WindSpeed = c.Double(nullable: false),
                        Temperature = c.Double(nullable: false),
                        MaxTemperature = c.Double(nullable: false),
                        MinTemperature = c.Double(nullable: false),
                        Pressure = c.Double(nullable: false),
                        Humidity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Weathers");
        }
    }
}
