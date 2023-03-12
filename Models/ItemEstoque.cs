using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueMVC3.Models
{
    public class ItemEstoque
    {
        public int ItemEstoqueId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }      
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }
        public DateTime Validade { get; set; }
        public DateTime DataEntrada { get; set; }

        public int EstoqueId { get; set; }
        public Estoque Estoque { get; set; }



        
    }
}