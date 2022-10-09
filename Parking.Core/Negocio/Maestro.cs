using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Core.Model;
using Parking.Core.Repositorio;

namespace Parking.Core.Negocio
{
    public class Maestro : IDisposable
    {

        ParkingRepositorioLite repositorio;
        private bool dispose = false;

        public Maestro()
        {
            this.repositorio = null;
        }

        public Maestro(ParkingRepositorioLite repositorio)
        {
            this.repositorio = repositorio;
        }

        internal ParkingRepositorioLite Repositorio
        {
            get
            {
                if (this.repositorio == null)
                {
                    this.repositorio = new ParkingRepositorioLite();
                    dispose = true;
                }

                return repositorio;
            }
            set
            {
                this.repositorio = value;
            }
        }

        public virtual void Dispose()
        {
            if (dispose && this.repositorio != null)
                this.repositorio.Dispose();

            this.repositorio = null;           
        }
    }
}
