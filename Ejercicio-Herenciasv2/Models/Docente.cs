using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Models
{
    public sealed class Docente
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Nombre { get; set; } = "";
        public string Especialidad { get; set; } = "";
    }
}