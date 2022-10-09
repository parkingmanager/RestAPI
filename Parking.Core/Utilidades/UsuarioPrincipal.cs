using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Core.Model;


namespace Parking.Core.Utilidades
{
    /// <summary>
    /// Principal para la aplicación
    /// http://www.codeproject.com/Tips/574576/How-to-implement-a-custom-IPrincipal-in-ASP-NET-MV
    /// </summary>
    public class UsuarioPrincipal : System.Security.Principal.IPrincipal
    {
        public UsuarioPrincipal()
        {
            //this.Identity = new System.Security.Principal.GenericIdentity((idUsuario.ToString());
        }

        public UsuarioPrincipal(Usuario usuario)
        {
            this.Identity = new System.Security.Principal.GenericIdentity(usuario.IdUsuario.ToString());
            this.IdUsuario = usuario.IdUsuario;
            this.Nombres = usuario.Nombres;
            this.Apellidos = usuario.Apellidos;
            this.Email = usuario.Email;
            this.Identificacion = usuario.Identificacion;
        }

        public System.Security.Principal.IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated && !string.IsNullOrWhiteSpace(role);
        }

        public bool IsAuthenticated
        {
            get
            {
                return Identity != null && Identity.IsAuthenticated;
            }
        }

        public int IdUsuario
        {
            get;
            private set;
        }

        public string Identificacion
        {
            get;
            private set;
        }

        public string Nombres
        {
            get;
            private set;
        }

        public string Apellidos
        {
            get;
            private set;
        }

        public string Email
        {
            get;
            private set;
        }
    }
}
