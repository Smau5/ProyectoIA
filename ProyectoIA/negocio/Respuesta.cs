using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyectoIA.negocio
{
    [Table("Respuesta")]
    public class Respuesta
    {
        [Key]
        public int id { get; set; }
        public string respuesta { get; set; }
        public Pregunta pregunta { get; set; }
        public bool correcta { get; set; }

        public Respuesta()
        {

        }
        public Respuesta(int id, string respuesta, Pregunta pregunta, bool correcta)
        {
            this.id = id;
            this.respuesta = respuesta;
            this.pregunta = pregunta;
            this.correcta = correcta;
        }
    }
}
