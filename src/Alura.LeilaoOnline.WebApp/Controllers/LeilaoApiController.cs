using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alura.LeilaoOnline.WebApp.Models;
using Alura.LeilaoOnline.WebApp.Dados;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Alura.LeilaoOnline.WebApp.Controllers
{
    [ApiController]
    [Route("/api/leiloes")]
    public class LeilaoApiController : ControllerBase
    {
        AppDbContext _context;

        public LeilaoApiController()
        {
            _context = new AppDbContext();
        }

        private IEnumerable<Leilao> BuscarLeiloes()
        {
            return _context.Leiloes
                .Include(l => l.Categoria);
        }

        private Leilao BuscarLeilaoPorId(int id)
        {
            return _context.Leiloes.First(l => l.Id == id);
        }

        private void Incluir(Leilao leilao)
        {
            _context.Leiloes.Add(leilao);
            _context.SaveChanges();
        }

        private void Alterar(Leilao leilao)
        {
            _context.Leiloes.Update(leilao);
            _context.SaveChanges();
        }

        private void Remover(Leilao leilao)
        {
            _context.Leiloes.Remove(leilao);
            _context.SaveChanges();
        }

        [HttpGet]
        public IActionResult EndpointGetLeiloes()
        {
            var leiloes = BuscarLeiloes();
            return Ok(leiloes);
        }

        [HttpGet("{id}")]
        public IActionResult EndpointGetLeilaoById(int id)
        {
            var leilao = BuscarLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }
            return Ok(leilao);
        }

        [HttpPost]
        public IActionResult EndpointPostLeilao(Leilao leilao)
        {
            Incluir(leilao);
            return Ok(leilao);
        }



        [HttpPut]
        public IActionResult EndpointPutLeilao(Leilao leilao)
        {
            Alterar(leilao);
            return Ok(leilao);
        }



        [HttpDelete("{id}")]
        public IActionResult EndpointDeleteLeilao(int id)
        {
            var leilao = BuscarLeilaoPorId(id);
            if (leilao == null)
            {
                return NotFound();
            }
            Remover(leilao);
            return NoContent();
        }


    }
}
