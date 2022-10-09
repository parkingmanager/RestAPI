using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parking.Core.Repositorio;
using Parking.Core.Dto;
using Parking.Core.Model;

namespace Parking.Core.Negocio
{
    public class ModuloVehiculo : Maestro
    {

        /// <summary>
        /// Constructor vacio
        /// </summary>
        public ModuloVehiculo() : base()
        {
        }

        /// <summary>
        /// Constructor con repositorio
        /// </summary>
        /// <param name="repositorio"></param>
        public ModuloVehiculo(ParkingRepositorioLite repositorio) : base(repositorio)
        {

        }

        /// <summary>
        /// Valida si la información mejoró para actualizarla 
        /// </summary>
        /// <param name="campo1">Dto</param>
        /// <param name="campo2">Almacenada</param>
        public string ValidarCampo(string campo1, string campo2)
        {
            if (string.IsNullOrWhiteSpace(campo2) && !string.IsNullOrWhiteSpace(campo1))
                campo2 = campo1;

            return campo2;
        }

        /// <summary>
        /// Registra una transacción y un vehiculo especifico asociado a la misma
        /// </summary>
        /// <param name="vtDto"></param>
        public Model.ResumenTransaccion RegistrarTransaccion(Dto.VehiculoTransaccion vtDto)
        {
            this.Repositorio.LazyLoading = false;

            Model.Vehiculo veh = this.Repositorio.GetAll<Vehiculo>().Where(v => v.Placa == vtDto.Placa).FirstOrDefault();

            Model.Lote lot = this.Repositorio.GetAll<Model.Lote>().Where(l => l.IdLote  == vtDto.IdLote).FirstOrDefault();

            //Verifica si la placa existe:
            if (veh == null)
            {
                //Crea el vehiculo a partir del DTO
                veh = new Vehiculo()
                {
                    Placa = vtDto.Placa,
                    Clase = vtDto.Clase,
                    Linea = vtDto.Linea,
                    Marca = vtDto.Marca,
                    Modelo = vtDto.Modelo,
                    NumeroMotor = vtDto.NumeroMotor,
                    Vin = vtDto.Vin,
                    FechaActualizacion = DateTime.Now,
                    FechaCreacion = DateTime.Now
                };

                //Inserta el vehiculo
                this.Repositorio.Insert<Vehiculo>(veh);                

            }
            //Actualiza los campos nuevos que vengan 
            else
            {
                veh.Placa = ValidarCampo(vtDto.Placa, veh.Placa);
                veh.Clase = ValidarCampo(vtDto.Clase, veh.Clase);
                veh.Linea = ValidarCampo(vtDto.Linea, veh.Linea);
                veh.Marca = ValidarCampo(vtDto.Marca, veh.Marca);
                veh.Modelo = ValidarCampo(vtDto.Modelo, veh.Modelo);
                veh.NumeroMotor = ValidarCampo(vtDto.NumeroMotor, veh.NumeroMotor);
                veh.Vin = ValidarCampo(vtDto.Vin, veh.Vin);
                veh.FechaActualizacion = DateTime.Now;
            }

            string tipoTransacccion;

            //Si existe una entrada no cerrada para el vehiculo, la transacción será una salida
            Model.VehiculoTransaccion ultimaTransaccion = this.Repositorio.GetAll<Model.VehiculoTransaccion>()
                .Where(_vt => _vt.Placa == veh.Placa).OrderByDescending(_vt => _vt.Fecha).FirstOrDefault();

            if (ultimaTransaccion != null)
            {
                if (ultimaTransaccion.TipoTransaccion == "Salida")
                    tipoTransacccion = "Entrada";
                else
                    tipoTransacccion = "Salida";
            }
            else
                tipoTransacccion = "Entrada";

            //Crea la transación 
            Model.VehiculoTransaccion vt = new Model.VehiculoTransaccion()
            {
                Placa = veh.Placa,
                Eliminado = null,
                Fecha = DateTime.Now,
                IdLote = vtDto.IdLote,
                TipoTransaccion = tipoTransacccion,
                Guid = tipoTransacccion == "Entrada" ? Convert.ToString(Guid.NewGuid()) : ultimaTransaccion.Guid
            };

            veh.VehiculoTransaccions.Add(vt);

            this.Repositorio.Commit();

            //Consulta ultima tarifa creada
            Model.Tarifa tarifa = this.Repositorio.GetAll<Model.Tarifa>().Where(_t => _t.IdLote == vtDto.IdLote).OrderByDescending(_t => _t.FechaCreacion).FirstOrDefault();

            if (tarifa == null)
            {
                tarifa = new Model.Tarifa()
                {
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    IdLote = vtDto.IdLote,
                    PrecioFraccion = 74,
                    PrecioFijo = 10000,
                    FraccionMinimaPrecioFijo = 240
                };

                this.Repositorio.Insert<Model.Tarifa>(tarifa);

                this.Repositorio.Commit();
            }

            Model.ResumenTransaccion resumen = new Model.ResumenTransaccion();

            if (tarifa != null)
            {              

                if (tipoTransacccion == "Entrada")
                {

                    resumen = new Model.ResumenTransaccion()
                    {
                        Placa = veh.Placa,
                        Guid = vt.Guid,
                        FechaEntrada = vt.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                        FechaSalida = null,
                        Tiempo = null,
                        TarifaFija = tarifa.PrecioFijo,
                        TarifaFraccion = tarifa.PrecioFraccion,
                        Valor = null,
                        FraccionMinimaPrecioFijo = tarifa.FraccionMinimaPrecioFijo,
                        DireccionLote = lot.Direccion,
                        NombreLote = lot.Nombre
                    };
                }
                else if (tipoTransacccion == "Salida")
                {

                    decimal minutos = (decimal)(vt.Fecha - ultimaTransaccion.Fecha).TotalMinutes;

                    resumen = new Model.ResumenTransaccion()
                    {
                        Placa = veh.Placa,
                        Guid = vt.Guid,
                        FechaEntrada = ultimaTransaccion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                        FechaSalida = vt.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                        Tiempo = Math.Truncate(Math.Ceiling(minutos)),
                        TarifaFija = tarifa.PrecioFijo,
                        TarifaFraccion = tarifa.PrecioFraccion,
                        Valor = minutos < tarifa.FraccionMinimaPrecioFijo ? Math.Truncate(Math.Ceiling(minutos * tarifa.PrecioFraccion)) : tarifa.PrecioFijo,
                        FraccionMinimaPrecioFijo = tarifa.FraccionMinimaPrecioFijo,
                        DireccionLote = lot.Direccion,
                        NombreLote = lot.Nombre
                    };

                    
                    //Aproximación a la cincuentena
                    int valor = Convert.ToInt32(resumen.Valor);

                    for (int i = 0; i <= 50; i++)
                    {
                        if (valor % 50 == 0)
                            break;

                        valor++;
                    }

                    resumen.Valor = valor;
                    vt.Valor = valor;
                    this.Repositorio.Commit();
                }

            }
            else             
                throw new Exception($"No existe tarifa asociada al lote {vtDto.IdLote}");            

            
            return resumen;
        }
    }
}
