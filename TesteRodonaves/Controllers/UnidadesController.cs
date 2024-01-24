using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteRodonaves.Data;
using TesteRodonaves.Models;

namespace TesteRodonaves.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadesController : Controller
    {
        private readonly RodonavesDbContext dbContext;

        public UnidadesController(RodonavesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUnidades()
        {
            return Ok(await dbContext.Unidades.Select(u => new { u.Id, u.Cod, u.Nome, u.Status,
                Colaboradores = u.Colaboradores.Select(c => new { c.Id, c.Nome })
            }).ToListAsync());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUnidadePorId([FromRoute] int id)
        {
            var unidade = await dbContext.Unidades.FindAsync(id);

            if (unidade == null)
            {
                return NotFound();
            }

            return Ok(unidade);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnidade(AddUnidadeRequest addUnidadeRequest)
        {
            var unidade = await dbContext.Unidades.FindAsync(addUnidadeRequest.Cod);

            if ( unidade != null)
            {
                return BadRequest("Código já cadastrado!");
            }

            unidade = new Unidade();

            unidade.Cod = addUnidadeRequest.Cod;
            unidade.Nome = addUnidadeRequest.Nome;
            unidade.Status = addUnidadeRequest.Status;

            await dbContext.Unidades.AddAsync(unidade);
            await dbContext.SaveChangesAsync();

            return Ok(unidade);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUnidade([FromRoute] int id, UpdateStatusUnidadeRequest updateStatusUnidadeRequest)
        {
            var unidade = await dbContext.Unidades.FindAsync(id);

            if (unidade != null)
            {
                unidade.Status = updateStatusUnidadeRequest.Status;

                await dbContext.SaveChangesAsync();

                return Ok(unidade);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> VincularColabUnidade([FromRoute] int id, VincularColabUnidadeRequest vincularColabUnidadeRequest)
        {

            var unidade = await dbContext.Unidades.Select(u => new { u.Id, u.Colaboradores, u.Status } ).Where(un => un.Id == id).SingleAsync();

            if (unidade != null)
            {
                if(!unidade.Status)
                {
                    return BadRequest("Unidade desativada!");
                }

                var colab = await dbContext.Colaboradores.FindAsync(vincularColabUnidadeRequest.Id_Colaborador);
                if (colab == null)
                {
                    return BadRequest("Colaborador não encontrado!");
                }

                bool colab_existe = unidade.Colaboradores.Any(c => c.Id == colab.Id);
                if (!colab_existe)
                {
                    unidade.Colaboradores.Add(colab);
                    await dbContext.SaveChangesAsync();
                    return Ok(unidade);
                }

                return BadRequest("Colaborador já vinculado!");
            }

            return NotFound();
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DesvincularColabUnidade([FromRoute] int id, VincularColabUnidadeRequest vincularColabUnidadeRequest)
        {

            var unidade = await dbContext.Unidades.FindAsync(id);

            if (unidade != null)
            {
                if (!unidade.Status)
                {
                    return BadRequest("Unidade desativada!");
                }

                var colab = await dbContext.Colaboradores.FindAsync(vincularColabUnidadeRequest.Id_Colaborador);
                if (colab == null)
                {
                    return BadRequest("Colaborador não encontrado!");
                }

                unidade.Colaboradores.Remove(colab);

                await dbContext.SaveChangesAsync();
                return Ok(unidade);
            }

            return NotFound();
        }

    }
}
