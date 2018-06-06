namespace ProyectoIA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.estudiantes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        registro = c.String(),
                        nombre = c.String(),
                        apellido = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.fotos",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        contenido = c.Binary(),
                        estudiante_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.estudiantes", t => t.estudiante_id)
                .Index(t => t.estudiante_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.fotos", "estudiante_id", "dbo.estudiantes");
            DropIndex("dbo.fotos", new[] { "estudiante_id" });
            DropTable("dbo.fotos");
            DropTable("dbo.estudiantes");
        }
    }
}
