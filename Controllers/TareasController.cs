using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasMVC_NetCore.Entidades;
using TareasMVC_NetCore.Interfaces;

namespace TareasMVC_NetCore.Controllers
{
    [Route("api/tareas")]
    public class TareasController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IServicioUsuarios _servicioUsuarios;

        public TareasController(ApplicationDbContext context, IServicioUsuarios servicioUsuarios)
        {
            _context = context;
            _servicioUsuarios = servicioUsuarios;
        }

        [HttpGet]
        public async Task<List<Tarea>> Get()
        {
            return await _context.Tareas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> Post([FromBody] string titulo)
        {
            var usuarioId = _servicioUsuarios.ObtenerUsuarioId();

            var existenTareas = await _context.Tareas.AnyAsync(t => t.UsuarioCreacionId == usuarioId);

            var ordenMayor = 0;
            if (existenTareas) 
            {
                ordenMayor = await _context.Tareas.Where(t => t.UsuarioCreacionId == usuarioId)
                                                    .Select(t => t.Orden)
                                                    .MaxAsync();
            }

            var tarea = new Tarea
            {
                Titulo = titulo,
                UsuarioCreacionId = usuarioId,
                FechaCreacion = DateTime.UtcNow,
                Orden = ordenMayor + 1
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return tarea;

        }

    }
}
