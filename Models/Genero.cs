namespace WIS_PI.Models
{
    public class Genero
    {
        public int GeneroId { get; set; }

        public string? NomeGenero { get; set; }
        public ICollection<Usuario>? Usuario { get; set; }
    }
}
