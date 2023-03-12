using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueMVC3.Models
{
    public class Estoque
    {
        public int EstoqueId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public IEnumerable<ItemEstoque> ItensEstoque { get; set; } = new List<ItemEstoque>();      
    }
}