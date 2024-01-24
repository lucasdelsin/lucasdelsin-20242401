using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using TesteRodonaves.Data;
using TesteRodonaves.Models;

namespace TesteRodonaves.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColaboradoresController : Controller
    {
        private readonly RodonavesDbContext dbContext;

        public ColaboradoresController(RodonavesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetColaboradores()
        {
            var colabQuery = dbContext.Colaboradores.Select(c => new { c.Id, c.Nome, Unidade = new { c.Unidade.Id, c.Unidade.Nome }});

            return Ok(await colabQuery.ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetColaboradorPorId([FromRoute] int id)
        {
            var unidade = await dbContext.Unidades.FindAsync(id);

            if (unidade == null)
            {
                return NotFound();
            }

            return Ok(unidade);
        }

        [HttpPost]
        public async Task<IActionResult> AddColaborador(AddColaboradorRequest addColaboradorRequest)
        {
            var colaborador = new Colaborador()
            {
                Nome = addColaboradorRequest.Nome,
                Unidade_IdFK = addColaboradorRequest.Unidade_IdFK,
                Usuario_IdFK = addColaboradorRequest.Usuario_IdFK,
            };

            await dbContext.Colaboradores.AddAsync(colaborador);
            await dbContext.SaveChangesAsync();

            return Ok(colaborador);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateColaborador([FromRoute] int id, UpdateColaboradorRequest updateColaboradorRequest)
        {
            var colaborador = await dbContext.Colaboradores.FindAsync(id);

            if (colaborador != null)
            {
                colaborador.Nome = updateColaboradorRequest.Nome;
                colaborador.Unidade_IdFK = updateColaboradorRequest.Unidade_IdFK;

                await dbContext.SaveChangesAsync();

                return Ok(colaborador);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteColaborador([FromRoute] int id)
        {
            var colaborador = await dbContext.Colaboradores.FindAsync(id);

            if (colaborador != null)
            {
                dbContext.Remove(colaborador);
                await dbContext.SaveChangesAsync();
                return Ok(colaborador);
            }

            return NotFound();
        }

    }
}
