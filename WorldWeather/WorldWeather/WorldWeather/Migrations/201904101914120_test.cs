namespace WorldWeather.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Weathers", "Description");
            DropColumn("dbo.Weathers", "WeatherId");
            DropColumn("dbo.Weathers", "Date");
            DropColumn("dbo.Weathers", "WindType");
            DropColumn("dbo.Weathers", "WindDirection");
            DropColumn("dbo.Weathers", "WindSpeed");
            DropColumn("dbo.Weathers", "MaxTemperature");
            DropColumn("dbo.Weathers", "MinTemperature");
            DropColumn("dbo.Weathers", "Pressure");
            DropColumn("dbo.Weathers", "Humidity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Weathers", "Humidity", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "Pressure", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "MinTemperature", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "MaxTemperature", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "WindSpeed", c => c.Double(nullable: false));
            AddColumn("dbo.Weathers", "WindDirection", c => c.String());
            AddColumn("dbo.Weathers", "WindType", c => c.String());
            AddColumn("dbo.Weathers", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Weathers", "WeatherId", c => c.Int(nullable: false));
            AddColumn("dbo.Weathers", "Description", c => c.String());
        }
    }
}
