using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoIA.negocio;
namespace ProyectoIA.bdEntity
{
    public class IAContext : DbContext
    {
        public IAContext() : base()
        {

        }
        public DbSet<Estudiante> estudiantes { get; set; }
        public DbSet<Foto> fotos { get; set; }
        public DbSet<Examen> examenes { get; set; }
        public DbSet<Pregunta> preguntas { get; set; }
        public DbSet<Respuesta> respuestas { get; set; }

    }
}
