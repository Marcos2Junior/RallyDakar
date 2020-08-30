namespace RallyDakar.API.Modelos
{
    public class PilotoModelo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public int EquipeId { get; set; }
        public string NomeCompleto => $"{Nome} {SobreNome}";
    }
}
