using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Dto
{
    public class Monitoreo
    {
        public int IdMonitoreo { get; set; }
        public int IdEspacioDelimitado { get; set; }
        public bool Estado { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
