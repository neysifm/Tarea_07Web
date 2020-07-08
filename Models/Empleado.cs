using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tarea_07Web.Repositorios.Base;

namespace Tarea_07Web.Models
{
    [Table("Empleados")]
    public class Empleado : ICamposControl
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Código")]
        public int Codigo { get; set; }
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
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Salario Mensual")]
        public double Salariomensual { get; set; }
        [Display(Name = "Departamento al que pertenece")]
        public string Departamento { get; set; }
        [Display(Name = "Contacto de emergencia")]
        public string Contactoemergencia { get; set; }
        [Display(Name = "AFP a la que pertenece")]
        public string AFP { get; set; }
        [Display(Name = "ARS a la que pertenece")]
        public string ARS { get; set; }
        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public bool Inactivo { get; set; }
    }
}
