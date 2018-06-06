using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyectoIA.negocio
{
    [Table("Examen")]
    public class Examen
    {
        public int id { get; set; }
        public string nombreExamen { get; set; }

        public ICollection<Pregunta> preguntas { get; set; }
        public Examen()
        {

        }
    }
}
