using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Models
{
    public sealed class Alumno
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Nombre { get; set; } = "";
        public string Email { get; set; } = "";
    }
}