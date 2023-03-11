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
    public class ProdutosController : Controller
    {
        private readonly ILogger<ProdutosController> _logger;
        private readonly AppDbContext _context;

        public ProdutosController(ILogger<ProdutosController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //route
        [Route("Index")]
        public IActionResult Index()
        {
            var produtos = _context.Produtos.ToList();
            return View(produtos);
        }

        //route
        [Route("Detalhes/{id}")]
        public IActionResult Detalhes(int id)
        {
            var produto = _context.Produtos.Find(id);
            return View(produto);
        }

        //route
        [Route("Cadastrar")]
        public IActionResult Cadastrar()
        {
            ViewData["Categorias"] = new List<string>()
            {
                "Alimentos",
                "Bebidas",
                "Limpeza",
                "Higiene",
            };

            return View();
        }

        [HttpPost("CadastrarT")]
        public IActionResult CadastrarT(Produto produto)
        {
            //verificar se o produto já existe
            var produtoExiste = _context.Produtos.Any(p => p.Nome.Equals(produto.Nome));
            if (produtoExiste)
            {
                ModelState.AddModelError("Nome", "Este produto já existe!");
                return View(produto);
            }

            else
            {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            var produto = _context.Produtos.Find(id);
            ViewData["Categorias"] = new List<string>()
            {
                "Alimentos",
                "Bebidas",
                "Limpeza",
                "Higiene",
            };
            return View(produto);
        }

        [HttpPost]
        public IActionResult EditarT(int id, Produto produto)
        {
     
                var produtoModificado = _context.Produtos.Find(id);
                produtoModificado.Nome = produto.Nome;
                produtoModificado.Descricao = produto.Descricao;
                produtoModificado.Preco = produto.Preco;
                produtoModificado.DataCadastro = produto.DataCadastro;
                produtoModificado.Categoria = produto.Categoria;

                _context.Produtos.Update(produtoModificado);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            [HttpPost ("Excluir/{id}")]
            public IActionResult Excluir(int id)
            {
                var produto = _context.Produtos.Find(id);
                _context.Produtos.Remove(produto);
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