using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Model
{
    public class Transacciones
    {
        public string Guid { get; set; }
        public string Placa { get; set; }
        public string Clase { get; set; }
        public DateTime? FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int? Tiempo { get; set; }
        public int? Valor { get; set; }
    }
}
