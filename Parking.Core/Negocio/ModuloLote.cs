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
    public class ModuloLote : Maestro
    {

        /// <summary>
        /// Constructor vacio
        /// </summary>
        public ModuloLote() : base()
        {
        }

        /// <summary>
        /// Constructor con repositorio
        /// </summary>
        /// <param name="repositorio"></param>
        public ModuloLote(ParkingRepositorioLite repositorio) : base(repositorio)
        {

        }

        public Dto.Lote ConsultarLote(int Id)
        {

            this.Repositorio.LazyLoading = false;

            Model.Lote lote = this.Repositorio.GetAll<Model.Lote>("EspacioDelimitadoes").Where(l => l.IdLote == Id).FirstOrDefault();

            // Mapea el DTO
            Dto.Lote loteDto = new Dto.Lote()
            {
                IdLote = lote.IdLote,
                Direccion = lote.Direccion,
                Email = lote.Email,
                Identificador = lote.Identificador,
                Nombre = lote.Nombre,
                Token = lote.Token,
                EspaciosDelimitados = new List<Dto.EspacioDelimitado>(),
                Eliminado = lote.Eliminado,
                FechaCreacion = lote.FechaCreacion,
                FuenteVideo = lote.FuenteVideo,
                RutaModelo = lote.RutaModelo
            };

            foreach (Model.EspacioDelimitado ds in lote.EspacioDelimitadoes)
            {
                Dto.EspacioDelimitado dsDto = new Dto.EspacioDelimitado()
                {
                    Coord1 = ds.Coord1,
                    Coord2 = ds.Coord2,
                    Coord3 = ds.Coord3,
                    Coord4 = ds.Coord4,
                    Eliminado = ds.Eliminado,
                    FechaCreacion = ds.FechaCreacion,
                    Habilitado = ds.Habilitado,
                    Indice = ds.Indice,
                    Tipo = ds.Tipo,
                    IdEspacioDelimitado = ds.IdEspacioDelimitado,
                    IdLote = ds.IdLote
                };


                loteDto.EspaciosDelimitados.Add(dsDto);
            }

            return loteDto;
        }

        public List<Dto.Lote> ConsultarLotes()
        {
            this.Repositorio.LazyLoading = false;

            List<Model.Lote> lotes = this.Repositorio.GetAll<Model.Lote>().Where(l => l.Eliminado == null).ToList();

            List<Dto.Lote> lotesDto = new List<Dto.Lote>();

            foreach (Model.Lote l in lotes)
            {
                Dto.Lote loteDto = new Dto.Lote()
                {
                    IdLote = l.IdLote,
                    Direccion = l.Direccion,
                    Email = l.Email,
                    Identificador = l.Identificador,
                    Nombre = l.Nombre,
                    Token = l.Token,
                    Eliminado = l.Eliminado,
                    FechaCreacion = l.FechaCreacion,
                    FuenteVideo = l.FuenteVideo,
                    RutaModelo = l.RutaModelo
                };

                lotesDto.Add(loteDto);

            }

            return lotesDto;
        }

        public Dto.Lote GuardarLote(Dto.Lote lote)
        {

            //Crea un lote
            if (lote.IdLote == 0)
            {
                //Crea el DA del lote 
                Model.Lote loteNuevo = new Model.Lote();

                //Mapea EL DTO en el objeto DA
                loteNuevo.Email = lote.Email;
                loteNuevo.Identificador = lote.Identificador;
                loteNuevo.Nombre = lote.Nombre;
                loteNuevo.Token = lote.Token;
                loteNuevo.Direccion = lote.Direccion;
                loteNuevo.FechaCreacion = DateTime.Now;
                loteNuevo.FuenteVideo = lote.FuenteVideo;
                loteNuevo.RutaModelo = lote.RutaModelo;


                //Crea los espacios delimitados 
                foreach (Dto.EspacioDelimitado espacio in lote.EspaciosDelimitados)
                {
                    Model.EspacioDelimitado espacioNuevo = new Model.EspacioDelimitado();

                    espacioNuevo.Coord1 = espacio.Coord1;
                    espacioNuevo.Coord2 = espacio.Coord2;
                    espacioNuevo.Coord3 = espacio.Coord3;
                    espacioNuevo.Coord4 = espacio.Coord4;
                    espacioNuevo.Eliminado = espacio.Eliminado;
                    espacioNuevo.FechaCreacion = DateTime.Now;
                    espacioNuevo.Habilitado = espacio.Habilitado;
                    espacioNuevo.Indice = espacio.Indice;
                    espacioNuevo.Tipo = espacio.Tipo;

                    loteNuevo.EspacioDelimitadoes.Add(espacioNuevo);
                }

                try
                {
                    //Inserción del lote
                    this.Repositorio.Insert<Model.Lote>(loteNuevo);
                    this.Repositorio.Commit();

                    // Mapea el DTO
                    Dto.Lote loteDto = new Dto.Lote()
                    {
                        IdLote = loteNuevo.IdLote,
                        Direccion = loteNuevo.Direccion,
                        Email = loteNuevo.Email,
                        Identificador = loteNuevo.Identificador,
                        Nombre = loteNuevo.Nombre,
                        Token = loteNuevo.Token,
                        Eliminado = loteNuevo.Eliminado,
                        FechaCreacion = loteNuevo.FechaCreacion,
                        FuenteVideo = loteNuevo.FuenteVideo,
                        RutaModelo = loteNuevo.RutaModelo
                    };

                    return loteDto;

                }
                catch (Exception ex)
                {
                    throw new Exception("Error en la creación de lote", ex);
                }

            }

            //Actualiza uno existente
            else
            {
                // Consulta el lote completo
                this.Repositorio.LazyLoading = false;

                Model.Lote loteActualizar = this.Repositorio.GetAll<Model.Lote>("EspacioDelimitadoes").Where(l => l.IdLote == lote.IdLote).FirstOrDefault();

                if (loteActualizar != null)
                {
                    //Actualiza las propiedades
                    loteActualizar.Email = lote.Email;
                    loteActualizar.Identificador = lote.Identificador;
                    loteActualizar.Nombre = lote.Nombre;
                    loteActualizar.Token = lote.Token;
                    loteActualizar.Direccion = lote.Direccion;
                    loteActualizar.FechaCreacion = DateTime.Now;
                    loteActualizar.FuenteVideo = lote.FuenteVideo;
                    loteActualizar.RutaModelo = lote.RutaModelo;

                    foreach (Dto.EspacioDelimitado ed in lote.EspaciosDelimitados)
                    {
                        Model.EspacioDelimitado ds = loteActualizar.EspacioDelimitadoes.Where(e => e.IdEspacioDelimitado == ed.IdEspacioDelimitado && e.Indice == ed.Indice).FirstOrDefault();

                        // Actualiza el espacio
                        if (ds != null)
                        {
                            ds.Habilitado = ed.Habilitado;
                            ds.Tipo = ed.Tipo;

                        }
                    }


                    try
                    {

                        this.Repositorio.Commit();

                        // Mapea el DTO
                        Dto.Lote loteDto = new Dto.Lote()
                        {
                            IdLote = loteActualizar.IdLote,
                            Direccion = loteActualizar.Direccion,
                            Email = loteActualizar.Email,
                            Identificador = loteActualizar.Identificador,
                            Nombre = loteActualizar.Nombre,
                            Token = loteActualizar.Token,
                            Eliminado = loteActualizar.Eliminado,
                            FechaCreacion = loteActualizar.FechaCreacion,
                            FuenteVideo = loteActualizar.FuenteVideo,
                            RutaModelo = loteActualizar.RutaModelo
                        };

                        return loteDto;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error en la actualización del lote {lote.IdLote}", ex);
                    }

                }
                else
                    throw new Exception($"No se encontró el lote con Id {lote.IdLote}");


            }
        }

        public bool ActualizarTarifa(Dto.Tarifa tarifa)
        {
            try
            {
                Model.Tarifa nuevaTarifa = new Model.Tarifa()
                {
                    IdLote = tarifa.IdLote,
                    PrecioFijo = (decimal)tarifa.PrecioFijo,
                    PrecioFraccion = (decimal)tarifa.PrecioFraccion,
                    FraccionMinimaPrecioFijo = (decimal)tarifa.FraccionMinimaPrecioFijo,
                    FechaCreacion = DateTime.Now,
                    Activo = true
                };

                this.Repositorio.Insert<Model.Tarifa>(nuevaTarifa);

                this.Repositorio.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarLote(int Id)
        {
            Model.Lote lote = this.Repositorio.GetAll<Model.Lote>().Where(l => l.IdLote == Id).FirstOrDefault();

            if (lote != null)
            {
                lote.Eliminado = DateTime.Now;
                this.Repositorio.Commit();
            }
            else
                throw new Exception($"No existe un lote con Id: {Id}");

        }

        public void GuardarMonitoreos(List<Dto.Monitoreo> monitoreos)
        {

            foreach (Dto.Monitoreo m in monitoreos)
            {

                if (m.IdEspacioDelimitado != 0)
                {

                    if (!m.Fecha.HasValue)
                        m.Fecha = DateTime.Now;

                    Model.Monitoreo monitoreo = new Model.Monitoreo()
                    {
                        Estado = m.Estado,
                        Fecha = m.Fecha.Value,
                        IdEspacioDelimitado = m.IdEspacioDelimitado
                    };

                    this.Repositorio.Insert<Model.Monitoreo>(monitoreo);
                }
            } 

            this.Repositorio.Commit();

        }

    }
}
