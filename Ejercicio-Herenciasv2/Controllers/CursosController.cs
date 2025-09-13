using CursosLibres.Data;
using CursosLibres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Controllers
{
    public sealed class CursosController
    {
        public BindingList<Curso> Listar() => new(InMemoryDb.Cursos);
        public void CrearPresencial(string t, string cat, int cupo, decimal costo, Docente d, string campus, string salon)
        {
            InMemoryDb.Cursos.Add(new CursoPresencial(t, cat, cupo, costo, d, campus, salon));
        }

        public void CrearVirtual(string t, string cat, int cupo, decimal costo, Docente d, string plataforma, string url)
        {
            InMemoryDb.Cursos.Add(new CursoVirtual(t, cat, cupo, costo, d, plataforma, new Uri(url)));
        }
        public void CrearHibrido(string t, string cat, int cupo, decimal costo, Docente d, string campus, string salon, string url)
        {
            InMemoryDb.Cursos.Add(new CursoHibrido(t, cat, cupo, costo, d, campus, salon, new Uri(url)));
        }
        public void AgregarSesion(Curso c, DateTime inicio, TimeSpan dur)
        {
            c.Sesiones.Add(new Sesion(inicio, dur));
        }

        public List<Curso> ListarPorDocente(Docente docente)
        {
            return InMemoryDb.Cursos.Where(c => c.Docente.Id == docente.Id).ToList();
        }

        internal static List<Curso> ObtenerCursosActuales()
        {
            throw new NotImplementedException();
        }
    }
}