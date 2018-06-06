namespace ProyectoIA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class preguntas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examen",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nombreExamen = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Pregunta",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pregunta = c.String(),
                        examen_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Examen", t => t.examen_id)
                .Index(t => t.examen_id);
            
            CreateTable(
                "dbo.Respuesta",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        respuesta = c.String(),
                        correcta = c.Boolean(nullable: false),
                        pregunta_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Pregunta", t => t.pregunta_id)
                .Index(t => t.pregunta_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Respuesta", "pregunta_id", "dbo.Pregunta");
            DropForeignKey("dbo.Pregunta", "examen_id", "dbo.Examen");
            DropIndex("dbo.Respuesta", new[] { "pregunta_id" });
            DropIndex("dbo.Pregunta", new[] { "examen_id" });
            DropTable("dbo.Respuesta");
            DropTable("dbo.Pregunta");
            DropTable("dbo.Examen");
        }
    }
}
