using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Models
{
    public sealed class CursoPresencial : Curso
    {
        public string Campus { get; }
        public string Salon { get; }
        public CursoPresencial(string t, string c, int cupo, decimal costo, Docente d, string campus, string salon)
            : base(t, c, cupo, costo, d) { Campus = campus; Salon = salon; }
        public override string GenerarAcceso() => $"Asista a {Campus}, salón {Salon}.";
    }
}