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
    //[Authorize]
    public class LoteController : ApiController
    {

        private ModuloLote modulo = null;

        public LoteController()
        {
            this.modulo = new ModuloLote();
        }

        [HttpPost]
        [Route("api/CrearLote")]
        public IHttpActionResult CrearLote(Lote lote)
        {
            try
            {
                Lote loteNuevo = this.modulo.GuardarLote(lote);

                return Created($"{loteNuevo.IdLote}", loteNuevo);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/ActualizarLote")]
        public IHttpActionResult ActualizarLote(Lote lote)
        {
            try
            {
                Lote loteActualizado = this.modulo.GuardarLote(lote);

                return Ok(loteActualizado);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/ConsultarLote/{id}")]
        public IHttpActionResult ConsultarLote(int id)
        {
            try
            {
                Lote lote = this.modulo.ConsultarLote(id);

                if (lote != null)
                    return Ok(lote);
                else
                    return StatusCode(HttpStatusCode.NotFound);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


        [HttpGet]
        [Route("api/ConsultarLotes")]
        public IHttpActionResult ConsultarLotes()
        {
            try
            {
                List<Lote> lote = this.modulo.ConsultarLotes();

                return Ok(lote);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


        [HttpDelete]
        [Route("api/EliminarLote/{id}")]
        public IHttpActionResult EliminarLote(int id)
        {
            try
            {
                this.modulo.EliminarLote(id);

                return StatusCode(HttpStatusCode.OK);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        [Route("api/ActualizarTarifa")]
        public IHttpActionResult ActualizarTarifa(Tarifa tarifa)
        {
            try
            {
                bool tarifaActualizada = this.modulo.ActualizarTarifa(tarifa);

                if (tarifaActualizada)
                    return StatusCode(HttpStatusCode.NoContent);
                else
                    return StatusCode(HttpStatusCode.BadRequest);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        [Route("api/GuardarMonitoreos")]
        public IHttpActionResult GuardarMonitoreos(List<Monitoreo> monitoreos)
        {
            try
            {
                this.modulo.GuardarMonitoreos(monitoreos);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return StatusCode(HttpStatusCode.InternalServerError);
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