using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tarea_07Web.Models
{
    public class Ejemplo
    {
        [Key]
        public int Id { get; set; }
        public string nombre { get; set; }
    }
}
