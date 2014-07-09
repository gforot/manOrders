using System.Data.Linq.Mapping;

namespace GestioneOrdini.Cl
{
    [Table(Name = "Marche")]
    public class Marca
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }

        [Column(Name = "Nome")]
        public string Nome { get; set; }
    }
}
