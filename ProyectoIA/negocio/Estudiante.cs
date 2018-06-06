using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyectoIA.negocio
{
    [Table("estudiantes")]
    public class Estudiante
    {
        [Key]
        public int id { get; set; }
        public string registro { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public ICollection<Foto> fotos { get; set; }

        public Estudiante()
        {

        }
        public Estudiante(int id,string registro, string nombre, string apellido)
        {
            this.id = id;
            this.registro = registro;
            this.nombre = nombre;
            this.apellido = apellido;
        }



    }
}
