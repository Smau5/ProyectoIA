using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyectoIA.negocio
{
    [Table("Pregunta")]
    public class Pregunta
    {
        [Key]
        public int id { get; set; }
        public string pregunta { get; set; }
        public Examen examen { get; set; }

        public ICollection<Respuesta> respuestas { get; set; }

        public Pregunta()
        {

        }
        public Pregunta(int id, string pregunta, Examen examen)
        {
            this.id = id;
            this.pregunta = pregunta;
            this.examen = examen;
        }
    }
}
