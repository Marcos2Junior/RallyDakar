﻿using Microsoft.EntityFrameworkCore.Internal;
using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RallyDakar.Dominio.Repositorios
{
    public class TelemetriaRepositorio : ITelemetriaRepositorio
    {
        private readonly RallyDbContexto _rallyDbContexto;

        public TelemetriaRepositorio(RallyDbContexto rallyDbContexto)
        {
            _rallyDbContexto = rallyDbContexto;
        }
        public void Adicionar(Telemetria telemetria)
        {
            _rallyDbContexto.Telemetria.Add(telemetria);
            _rallyDbContexto.SaveChanges();
        }

        public void Atualizar(Telemetria telemetria)
        {
            if (_rallyDbContexto.Entry(telemetria).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _rallyDbContexto.Attach(telemetria);
                _rallyDbContexto.Entry(telemetria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                _rallyDbContexto.Update(telemetria);
            }
            _rallyDbContexto.SaveChanges();
        }

        public void Deletar(Telemetria telemetria)
        {
            _rallyDbContexto.Remove(telemetria);
            _rallyDbContexto.SaveChanges();
        }

        public bool Existe(int telemetriaId)
        {
            return _rallyDbContexto.Telemetria.Any(x => x.Id == telemetriaId);
        }

        public Telemetria Obter(int telemetriaId)
        {
            return _rallyDbContexto.Telemetria.FirstOrDefault(x => x.Id == telemetriaId);
        }

        public IEnumerable<Telemetria> ObterTodos()
        {
            return _rallyDbContexto.Telemetria.ToList();
        }
    }
}
