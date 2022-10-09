using Parking.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Parking.Core.Utilidades
{
    public class Seguridad
    {
        static string secretKey = System.Configuration.ConfigurationManager.AppSettings["SecretKey"];

        /// <summary>
        /// Genera una clave encriptada en base 64
        /// </summary>
        /// <param name="_clave"></param>
        /// <returns></returns>
        public static string Encriptar(string _clave)
        {
            byte[] saltBytes = Encoding.ASCII.GetBytes("EstacionamietoCK1");
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes("EstacionamietoCK1%", saltBytes);
            RijndaelManaged rijAlg = new RijndaelManaged();
            rijAlg.Key = key.GetBytes(rijAlg.KeySize / 8);
            rijAlg.IV = key.GetBytes(rijAlg.BlockSize / 8);

            byte[] encrypted;

            // Create a decrytor to perform the stream transform.
            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for encryption. 
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(_clave);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            // Return the encrypted bytes from the memory stream. 
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Crea un token jwt para un usuario
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public static string CrearToken(Usuario u)
        {          
            var payload = new Dictionary<string, object>() { { "IdUsuario", u.IdUsuario }, { "Identificacion", u.Identificacion }, {"FechaVencimiento", DateTime.Now.AddMinutes(30).ToString("dd-MM-yyyy HH:mm:ss") } };

            return JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
        }

        /// <summary>
        /// Decodifica el token y devuelve la identidad principal para el estudiante
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Usuario DecodificarToken(string token)
        {
            IDictionary<string, object> payload = JWT.JsonWebToken.DecodeToObject(token, secretKey) as IDictionary<string, object>;
            int idUsuario = Convert.ToInt32(payload["IdUsuario"]);
            DateTime fechaVencimiento = Convert.ToDateTime(payload["FechaVencimiento"]);

            if (DateTime.Now > fechaVencimiento)
                throw new Exception("La sesión expiró");

            Usuario usuario = null;

            using (var modulo = new Parking.Core.Negocio.ModuloUsuarios())
            {
                usuario = modulo.GetUsuario(idUsuario);

                if (usuario == null)
                    throw new Exception("Token inválido, el usuario no está activo o no existe. IdUsuario: " + idUsuario);


            }

            return usuario;
        }
    }
}