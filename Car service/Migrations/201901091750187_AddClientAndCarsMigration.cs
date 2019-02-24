namespace Car_service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientAndCarsMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Variant = c.String(),
                        Type = c.String(),
                        Year = c.String(),
                        RegistrationPlate = c.String(),
                        VIN = c.String(),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        MiddleName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "ClientId", "dbo.Clients");
            DropIndex("dbo.Cars", new[] { "ClientId" });
            DropTable("dbo.Clients");
            DropTable("dbo.Cars");
        }
    }
}
