using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EstoqueMVC3.context;
using EstoqueMVC3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EstoqueMVC3.Controllers
{
    [Route("[controller]")]
    public class EstoqueController : Controller
    {
        private readonly ILogger<EstoqueController> _logger;
        private readonly AppDbContext _context;

        public EstoqueController(ILogger<EstoqueController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult Create()
        {
            var estoque = new Estoque();
            estoque.Nome = "Estoque 1";
            estoque.Descricao = "Estoque 1";
            _context.Estoques.Add(estoque);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var ItensEstoque = _context.ItensEstoque.ToList();
            return View(ItensEstoque);
        }

        //GET ItemEstoque/Adicionar
        [HttpGet("Adicionar")]
        public IActionResult Adicionar()
        {
            ViewData["Produtos"] = _context.Produtos.ToList();
            return View();
        }

        //POST ItemEstoque/Adicionar
        [HttpPost("Adicionar")]
        public IActionResult Adicionar(ItemEstoque itemEstoque)
        {
            //id do estoque será 1
            itemEstoque.EstoqueId = 1;
            itemEstoque.DataEntrada = DateTime.Now;
            itemEstoque.Nome = _context.Produtos.Find(itemEstoque.ProdutoId).Nome;
            //verifica se existe um ItemEstoque cuja mes e ano da validade sejam iguais
            //e o produto seja o mesmo
            var itemEstoqueExiste = _context.ItensEstoque.Any(i => i.ProdutoId == itemEstoque.ProdutoId
            && i.Validade.Month == itemEstoque.Validade.Month
            && i.Validade.Year == itemEstoque.Validade.Year);
            //se existir, atualiza a quantidade
            if (itemEstoqueExiste)
            {
                var itemEstoqueDb = _context.ItensEstoque.FirstOrDefault(i => i.ProdutoId == itemEstoque.ProdutoId
                && i.Validade.Month == itemEstoque.Validade.Month
                && i.Validade.Year == itemEstoque.Validade.Year);
                itemEstoqueDb.Quantidade += itemEstoque.Quantidade;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            //se não existir, adiciona um novo itemEstoque
            _context.ItensEstoque.Add(itemEstoque);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET itemEstoque/Detalhes/1
        [HttpGet("Detalhes/{id}")]
        public IActionResult Detalhes(int id)
        {
            var itemEstoque = _context.ItensEstoque.Find(id);
            //atribuir o produto ao itemEstoque
            itemEstoque.Produto = _context.Produtos.Find(itemEstoque.ProdutoId);
            return View(itemEstoque);
        }

        //GET itemEstoque/Editar/1
        [HttpGet("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            var itemEstoque = _context.ItensEstoque.Find(id);
            //atribuir o produto ao itemEstoque
            itemEstoque.Produto = _context.Produtos.Find(itemEstoque.ProdutoId);
            ViewData["Produtos"] = _context.Produtos.ToList();
            return View(itemEstoque);
        }

        //POST itemEstoque/Editar/1
        [HttpPost("Editar/{id}")]
        public IActionResult Editar(int id, ItemEstoque itemEstoque)
        {
            //atribuir o produto ao itemEstoque
            itemEstoque.Produto = _context.Produtos.Find(itemEstoque.ProdutoId);
            //atribuir o nome do produto ao itemEstoque
            itemEstoque.Nome = itemEstoque.Produto.Nome;       
            //atribuir a data de entrada ao itemEstoque
            itemEstoque.DataEntrada = DateTime.Now;
            _context.ItensEstoque.Update(itemEstoque);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET itemEstoque/Excluir/1
        [HttpGet("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var itemEstoque = _context.ItensEstoque.Find(id);
            //atribuir o produto ao itemEstoque
            itemEstoque.Produto = _context.Produtos.Find(itemEstoque.ProdutoId);
            return View(itemEstoque);
        }

        //POST itemEstoque/Excluir/1
        [HttpPost ("Excluir/{id}")]
        public IActionResult ExcluirT(int id)
        {
            var itemEstoque = _context.ItensEstoque.Find(id);
            _context.ItensEstoque.Remove(itemEstoque);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}