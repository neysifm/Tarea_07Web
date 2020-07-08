using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tarea_07Web.Repositorios.Base
{
    public interface ICamposControl
    {
        DateTime Creado { get; set; }
        DateTime Modificado { get; set; }
        bool Inactivo { get; set; }
    }
}
