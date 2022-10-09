using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.Core.Dto
{
    public class EspacioDelimitado
    {
        public int IdEspacioDelimitado { get; set; }
        public string Coord1 { get; set; }
        public string Coord2 { get; set; }
        public string Coord3 { get; set; }
        public string Coord4 { get; set; }
        public bool? Habilitado { get; set; }
        public string Tipo { get; set; }
        public DateTime? Eliminado { get; set; }
        public int? IdLote { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? Indice { get; set; }
    }
}
