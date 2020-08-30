using System;
using System.Collections.Generic;

namespace RallyDakar.Dominio.Entidades
{
    public class Temporada
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataIncio { get; set; }
        public DateTime? DataFim { get; set; }
        public ICollection<Equipe> Equipes { get; set; }
    }
}
