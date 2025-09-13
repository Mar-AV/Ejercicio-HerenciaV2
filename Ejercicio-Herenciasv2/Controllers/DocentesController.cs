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
    public sealed class DocentesController
    {
        public BindingList<Docente> Listar() => new BindingList<Docente>(InMemoryDb.Docentes);

        public void Crear(string nombre, string especialidad)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(especialidad))
            {
                throw new ArgumentException("Nombre y especialidad requeridos.");
            }
            InMemoryDb.Docentes.Add(new Docente { Nombre = nombre, Especialidad = especialidad });
        }
    }
}