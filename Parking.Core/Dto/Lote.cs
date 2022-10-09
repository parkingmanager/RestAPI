using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Dto
{
    public class Lote
    {
        public int IdLote { get; set; }
        public string Nombre { get; set; }       
        public string Email { get; set; }
        public string Identificador { get; set; }
        public string Token { get; set; }
        public string Direccion { get; set; }
        public DateTime? Eliminado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string FuenteVideo { get; set; }
        public string RutaModelo { get; set; }
        public List<EspacioDelimitado> EspaciosDelimitados { get; set; }

        public bool EsValido()
        {
            return true;
        }


    }
}
