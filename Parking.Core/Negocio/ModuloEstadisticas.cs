using Parking.Core.Model;
using Parking.Core.Repositorio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Negocio
{
    public class ModuloEstadisticas : Maestro
    {

        public ModuloEstadisticas() : base()
        {
        }

        /// <summary>
        /// Constructor con repositorio
        /// </summary>
        /// <param name="repositorio"></param>
        public ModuloEstadisticas(ParkingRepositorioLite repositorio) : base(repositorio)
        {

        }

        /// <summary>
        /// Consulta las ocupaciones existentes para un lote por dia especifico
        /// </summary>
        /// <param name="idLote"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<Ocupacion> GetResumenOcupacionesDiarias(int idLote, DateTime fecha)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IdLote", idLote));
            parameters.Add(new SqlParameter("@Fecha", fecha));

            return this.Repositorio.CallQuery<Ocupacion>("EXEC proc_getOcupaciones @IdLote, @Fecha", parameters.ToArray()).ToList();

        }

        public List<Transacciones> GetTransaccionesDiarias(int idLote, DateTime fecha)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@IdLote", idLote));
            parameters.Add(new SqlParameter("@Fecha", fecha));

            return this.Repositorio.CallQuery<Transacciones>("EXEC proc_getTransacciones @IdLote, @Fecha", parameters.ToArray()).ToList();
        }
    }
}
