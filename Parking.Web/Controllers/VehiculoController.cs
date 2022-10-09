using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Parking.Core.Dto;
using Parking.Core.Negocio;

namespace Parking.Web.Controllers
{
    public class VehiculoController : ApiController
    {

        private ModuloVehiculo modulo = null;

        public VehiculoController()
        {
            this.modulo = new ModuloVehiculo();
        }

        [HttpPost]
        [Route("api/Transaccion")]
        public IHttpActionResult RegistarTransaccion(VehiculoTransaccion vehiculoTransaccion)
        {
            try
            {
                Parking.Core.Model.ResumenTransaccion resumen = this.modulo.RegistrarTransaccion(vehiculoTransaccion);

                return Created($"Transaccion", resumen);
            }
            catch(Exception ex)
            {
                return  InternalServerError(ex);
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