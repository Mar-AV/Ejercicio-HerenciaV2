using CursosLibres.Data;
using CursosLibres.Enums;
using CursosLibres.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosLibres.Controllers
{
    public sealed class InscripcionesController
    {
        private static bool Overlaps(Sesion a, Sesion b)
        {
            DateTime fa = a.Inicio + a.Duracion;
            DateTime fb = b.Inicio + b.Duracion;
            return a.Inicio < fb && b.Inicio < fa;
        }

        public (Inscripcion insc, string msg) Inscribir(Alumno alumno, Curso curso, int maxActivos = 3)
        {
            int activos = InMemoryDb.Inscripciones.Count(i => i.Alumno.Id == alumno.Id &&
                (i.Estado == EstadoInscripcion.Confirmado || i.Estado == EstadoInscripcion.Preinscrito));

            if (activos >= maxActivos)
                return (null!, "Límite de cursos activos alcanzado.");

            bool choca = InMemoryDb.Inscripciones.Where(i => i.Alumno.Id == alumno.Id && i.Estado == EstadoInscripcion.Confirmado)
                .SelectMany(i => i.Curso.Sesiones).Any(s1 => curso.Sesiones.Any(s2 => Overlaps(s1, s2)));

            if (choca)
                return (null!, "Conflicto de horario con otro curso confirmado.");

            Inscripcion insc = new Inscripcion(alumno, curso);
            if (curso.CupoDisponible(InMemoryDb.Confirmados(curso.Id)) > 0)
            {
                insc.Confirmar();
            }
            else
            {
                insc.Espera();
            }

            InMemoryDb.Inscripciones.Add(insc);

            return (insc, insc.Estado == EstadoInscripcion.Confirmado ? $"Inscripción confirmada. {curso.GenerarAcceso()}" : "Sin cupo: lista de espera.");
        }

        public BindingList<Inscripcion> ListarPorCurso(Curso c)
        {
            return new(
                InMemoryDb.Inscripciones
                .Where(i => i.Curso.Id == c.Id)
                .ToList()
            );
        }
    }
}
