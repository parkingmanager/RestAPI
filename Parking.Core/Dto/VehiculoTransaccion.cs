using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Dto
{
    public class VehiculoTransaccion
    {
        public int IdLote { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Linea { get; set; }
        public string Modelo { get; set; }
        public string Clase { get; set; }
        public string NumeroMotor { get; set; }
        public string Vin { get; set; }
        //public string TipoTransaccion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
