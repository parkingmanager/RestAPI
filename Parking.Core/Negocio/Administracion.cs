using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Core.Repositorio;
using Parking.Core.Model;

namespace Parking.Core.Negocio
{
    
    public class Administracion : Maestro
    {

        /// <summary>
        /// Constructor vacio
        /// </summary>
        public Administracion() : base()
        {
        }

        /// <summary>
        /// Constructor con repositorio
        /// </summary>
        /// <param name="repositorio"></param>
        public Administracion(ParkingRepositorio repositorio) : base(repositorio)
        {

        }

        /// <summary>
        /// Consulta todos los usuarios. 
        /// </summary>
        /// <returns></returns>
        public List<Model.Usuario> GetUsuarios()
        {
            return this.Repositorio.GetAll<Model.Usuario>().ToList();             
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveUsuario(Usuario usuario)
        {
            this.Repositorio.Insert(usuario);

            this.Repositorio.Commit();
        }


    }
}
