using AutoMapper;
using RallyDakar.API.Modelos;
using RallyDakar.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RallyDakar.API.AutoMapper
{
    public class PilotoProfile : Profile
    {
        public PilotoProfile()
        {
            CreateMap<Piloto, PilotoModelo>();
            CreateMap<PilotoModelo, Piloto>();
        }
    }
}
