using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Mvc;
using Parking.Core.Model;


namespace Parking.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);           
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += ParkingApplication_PostAuthenticateRequest;
            this.AuthenticateRequest += ParkingApplication_AuthenticateRequest;
            this.BeginRequest += ParkingApplication_BeginRequest;

            base.Init();
        }

        void ParkingApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);

        }

        void ParkingApplication_BeginRequest(object sender, EventArgs e)
        {
            //System.Web.HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            System.Diagnostics.Debug.WriteLine("Begin Request");
        }

        /// <summary>
        /// Autentica el request con el token de autorización jwt
        /// https://github.com/johnsheehan/jwt
        /// http://stackoverflow.com/questions/6407689/how-can-i-set-the-page-user-property-in-asp-net
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ParkingApplication_AuthenticateRequest(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Authenticate Request");

            //var token = this.Request.Headers["Authorization"];

            ////Si el usuario tiene un token
            //if (token != null)
            //{
            //    System.Diagnostics.Debug.WriteLine(token);

            //    try
            //    {
            //        Usuario usuario = Seguridad.DecodificarToken(token);
            //        UsuarioPrincipal usuarioPrincipal = new UsuarioPrincipal(usuario);
            //        Context.User = usuarioPrincipal;

            //        System.Diagnostics.Debug.WriteLine("Usuario autenticado " + usuario.IdUsuario);
            //    }
            //    catch (JWT.SignatureVerificationException)
            //    {
            //        throw new Exception("Authorization invalid token");
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("Authorization invalid token. " + ex.Message, ex);
            //    }
            //}
        }

        protected void ParkingApplication_Error(object sender, EventArgs e)
        {
            var ex = this.Server.GetLastError();
            System.Diagnostics.Debug.WriteLine("APPLICATION ERROR\r\n" + ex.ToString());
        }

    }
}
