using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoIA.negocio
{
    [Table("fotos")]
    public class Foto
    {
        [Key]
        public int id { get; set; }
        public byte[] contenido { get; set; }

        public Estudiante estudiante { get; set; }

        public Foto()
        {

        }

        public Foto(int id, byte[] contenido, Estudiante estudiante)
        {
            this.id = id;
            this.contenido = contenido;
            this.estudiante = estudiante;
        }
    }
}
