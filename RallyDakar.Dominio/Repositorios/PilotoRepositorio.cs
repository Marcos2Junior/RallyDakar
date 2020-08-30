using RallyDakar.Dominio.DbContexto;
using RallyDakar.Dominio.Entidades;
using RallyDakar.Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RallyDakar.Dominio.Repositorios
{
    public class PilotoRepositorio : IPilotoRepositorio
    {
        private readonly RallyDbContexto _rallyDbContexto;

        public PilotoRepositorio(RallyDbContexto rallyDbContexto)
        {
            _rallyDbContexto = rallyDbContexto;
        }

        public void Adicionar(Piloto piloto)
        {
            _rallyDbContexto.Add(piloto);
            _rallyDbContexto.SaveChanges();
        }

        public void Atualizar(Piloto piloto)
        {
            if (_rallyDbContexto.Entry(piloto).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
            {
                _rallyDbContexto.Attach(piloto);
                _rallyDbContexto.Entry(piloto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                _rallyDbContexto.Update(piloto);
            }

            _rallyDbContexto.SaveChanges();
        }

        public void Deletar(Piloto piloto)
        {
            _rallyDbContexto.Remove(piloto);
            _rallyDbContexto.SaveChanges();
        }

        public bool Existe(int pilotoId)
        {
            return _rallyDbContexto.Pilotos.Any(x => x.Id == pilotoId);
        }

        public Piloto Obter(int Id)
        {
            return _rallyDbContexto.Pilotos.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Piloto> ObterTodos()
        {
            return _rallyDbContexto.Pilotos.ToList();
        }

        public IEnumerable<Piloto> ObterTodos(string nome)
        {
            return _rallyDbContexto.Pilotos.Where(x => x.Nome.Contains(nome)).ToList();
        }
    }
}
