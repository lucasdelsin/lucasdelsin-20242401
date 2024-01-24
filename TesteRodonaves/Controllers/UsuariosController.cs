using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TesteRodonaves.Data;
using TesteRodonaves.Models;

namespace TesteRodonaves.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly RodonavesDbContext dbContext;

        public UsuariosController(RodonavesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            return Ok(await dbContext.Usuarios.Select(u => new { u.Login, u.Status }).ToListAsync());
        }

        [HttpGet]
        [Route("{status:bool}")]
        public async Task<IActionResult> GetUsuariosPorStatus([FromRoute] bool status)
        {
            var usuario = await dbContext.Usuarios.Where(u => u.Status == status).ToListAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);


        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUsuarioPorId([FromRoute] int id)
        {
            var usuario = await dbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]  
        public async Task<IActionResult> AddUsuario(AddUsuarioRequest addUsuarioRequest)
        {
            var usuario = new Usuario()
            {
                Login = addUsuarioRequest.Login,
                Senha = addUsuarioRequest.Senha,
                Status = addUsuarioRequest.Status,
            };

            await dbContext.Usuarios.AddAsync(usuario);
            await dbContext.SaveChangesAsync();

            return Ok(usuario);

        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUsuario([FromRoute] int id, UpdateUsuarioRequest updateUsuarioRequest)
        {
            var usuario = await dbContext.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                usuario.Senha = updateUsuarioRequest.Senha;
                usuario.Status = updateUsuarioRequest.Status;

                await dbContext.SaveChangesAsync();

                return Ok(usuario);
            }

            return NotFound();
        }

    }
}
