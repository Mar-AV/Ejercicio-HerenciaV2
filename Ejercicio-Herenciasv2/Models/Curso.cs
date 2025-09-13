using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Models
{
    public abstract class Curso
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Titulo { get; private set; }
        public string Categoria { get; private set; }
        public int CupoMaximo { get; private set; }        // 0 = sin límite
        public decimal Costo { get; private set; }         // 0 = gratuito
        public Docente Docente { get; private set; }
        public List<Sesion> Sesiones { get; } = new();
        protected Curso(string t, string cat, int cupo, decimal costo, Docente d)
        { Titulo = t; Categoria = cat; CupoMaximo = Math.Max(0, cupo); Costo = Math.Max(0, costo); Docente = d; }
        public abstract string GenerarAcceso();            // Polimorfismo
        public int CupoDisponible(int confirmados) => CupoMaximo == 0 ? int.MaxValue : Math.Max(0, CupoMaximo - confirmados);
        public string Modalidad => GetType().Name.Replace("Curso", "");
        public DateTime? ProximaSesion => Sesiones.Where(s => s.Inicio > DateTime.Now).MinBy(s => s.Inicio)?.Inicio ?? null;

        public object Nombre { get; internal set; }
    }
}