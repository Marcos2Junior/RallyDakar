using System;
using System.Collections.Generic;
using System.Text;

namespace RallyDakar.Dominio.Entidades
{
    public class Equipe
    {
        public int Id { get; set; }
        public string CodigoIdentificador { get; set; }
        public string Nome { get; set; }
        public int TemporadaId { get; set; }
        public virtual Temporada Temporada { get; set; }
        public ICollection<Piloto> Pilotos { get; set; }
    }
}
