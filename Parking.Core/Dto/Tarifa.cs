using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Dto
{
    public class Tarifa
    {
        public int IdLote { get; set; }
        public double PrecioFraccion { get; set; }
        public double PrecioFijo { get; set; }
        public double FraccionMinimaPrecioFijo { get; set; }
        public bool Activo { get; set; }
    }
}
