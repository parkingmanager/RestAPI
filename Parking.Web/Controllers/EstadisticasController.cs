using Parking.Core.Model;
using Parking.Core.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Parking.Core.Dto;

namespace Parking.Web.Controllers
{
    public class EstadisticasController : ApiController
    {

        private ModuloEstadisticas modulo = null;

        public EstadisticasController()
        {
            this.modulo = new ModuloEstadisticas();
        }

        [HttpGet]
        [Route("api/estadisticas/ocupaciones/{idLote}/{fecha}")]
        public IHttpActionResult GetOcupaciones(int idLote, string fecha)
        {
            try
            {
                DateTime fechaParsed = Convert.ToDateTime(fecha);

                List<Ocupacion> ocupaciones = this.modulo.GetResumenOcupacionesDiarias(idLote, fechaParsed);

                return Ok(ocupaciones);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/estadisticas/transacciones/{idLote}/{fecha}")]
        public IHttpActionResult GetTransacciones(int idLote, string fecha)
        {
            try
            {
                DateTime fechaParsed = Convert.ToDateTime(fecha);
                List<EstadisticaTransaccion> estadisticaTransacciones = new List<EstadisticaTransaccion>();


                List<Transacciones> ocupaciones = this.modulo.GetTransaccionesDiarias(idLote, fechaParsed);

                foreach (Transacciones t in ocupaciones)
                {
                    EstadisticaTransaccion et = new EstadisticaTransaccion()
                    {
                        Clase = t.Clase,
                        FechaEntrada = t.FechaEntrada.HasValue ? t.FechaEntrada.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                        FechaSalida = t.FechaSalida.HasValue ? t.FechaSalida.Value.ToString("dd/MM/yyyy HH:mm:ss") : "",
                        Guid = t.Guid,
                        Placa = t.Placa,
                        Tiempo = t.Tiempo,
                        Valor = t.Valor
                    };

                    estadisticaTransacciones.Add(et);
                }

                return Ok(estadisticaTransacciones);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                modulo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
