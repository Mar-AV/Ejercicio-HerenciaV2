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
    public sealed class AlumnosController
    {
        public BindingList<Alumno> Listar() => new BindingList<Alumno>(InMemoryDb.Alumnos);

        public void Crear(string nombre, string email)
        {
            if (string.IsNullOrWhiteSpace(nombre) || !email.Contains("@"))
            {
                throw new ArgumentException("Nombre requerido y email válido.");
            }
            InMemoryDb.Alumnos.Add(new Alumno { Nombre = nombre, Email = email });
        }
    }
}