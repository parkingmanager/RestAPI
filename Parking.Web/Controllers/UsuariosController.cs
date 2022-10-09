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
using Parking.Core.Model;
using Parking.Core.Negocio;


namespace Parking.Web.Controllers
{
    [Authorize]
    public class UsuariosController : ApiController
    {
        
        private ModuloUsuarios modulo = null;

        public UsuariosController()
        {
            this.modulo = new ModuloUsuarios();
        }

        /// <summary>
        /// Obtiene un token de autenticación para un usuario existente 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <param name="contrasena"></param>
        /// <returns></returns>
        [Route("api/autenticar")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult PostAutenticacion(dynamic autenticacion)
        {
            try
            {
                string identificacion = autenticacion["identificacion"];
                string contrasena = autenticacion["contrasena"];

                return Ok(this.modulo.GetUsuarioAutenticacion(identificacion, contrasena));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        /// <summary>
        /// Consulta todos los usuarios del sistema 
        /// </summary>
        /// <returns></returns>
        [Route("api/usuarios")]
        public IHttpActionResult GetUsuarios()
        {
            try
            {
                return Ok(this.modulo.GetUsuarios());
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los usuarios", ex);
            }
        }

        [Route("api/usuario/{id}")]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {            
            Usuario usuario = this.modulo.GetUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Route("api/usuario/{id}")]    
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            try
            {
                this.modulo.ActualizarUsuario(id, usuario);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }      
        }

        [HttpPost()]
        [Route("api/usuario")]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            this.modulo.CrearUsuario(usuario);

            return Ok(usuario);
        }

        [Route("api/usuario/{id}")]
        [HttpDelete()]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            this.modulo.EliminarUsuario(id); 
            return Ok(id);
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