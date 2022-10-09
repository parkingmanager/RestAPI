using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Dto
{
    public class EstadisticaTransaccion
    {
        public string Guid { get; set; }
        public string Placa { get; set; }
        public string Clase { get; set; }
        public string FechaEntrada { get; set; }
        public string FechaSalida { get; set; }
        public int? Tiempo { get; set; }
        public int? Valor { get; set; }
    }
}
