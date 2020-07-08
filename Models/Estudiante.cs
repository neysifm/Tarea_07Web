using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tarea_07Web.Repositorios.Base;

namespace Tarea_07Web.Models
{
    [Table("Estudiantes")]
    public class Estudiante : ICamposControl
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Matrícula")]
        public int Matricula { get; set; }
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
        [Display(Name = "Sexo")]
        public char Sexo { get; set; }
        [Display(Name = "Estado civil")]
        public string EstadoCivil { get; set; }
        [Display(Name = "Ocupación")]
        public string Ocupacion { get; set; }
        [Display(Name = "Tipo Sangre")]
        public string TipoSangre { get; set; }
        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }
        [Display(Name = "Religión")]
        public string Religion { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Nombre del padre")]
        public string NombrePadre { get; set; }
        [Display(Name = "Nombre de la madre")]
        public string NombreMadre { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Tipo de colegio")]
        public string TipoColegio { get; set; }
        [Display(Name = "Carrera")]
        public string Carrera { get; set; }
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public bool Inactivo { get; set; }
    }
}
