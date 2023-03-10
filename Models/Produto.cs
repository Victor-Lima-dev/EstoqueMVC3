using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueMVC3.Models
{
    public class Produto
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Categoria { get; set; }
        public IEnumerable<ItemEstoque> ItensEstoque { get; set; } = new List<ItemEstoque>();
        

    }
}