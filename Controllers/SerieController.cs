using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tienda.Data;
using tienda.Models;

namespace tienda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SerieController : ControllerBase
    {
        private readonly TiendaContext _context;

        public SerieController(TiendaContext context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
        {
            var series = await _context.Series
                .Include(s => s.Producto)
                .ToListAsync();
            return Ok(series);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Serie>> GetSerieById(int id)
        {
            var serie = await _context.Series
                .Include(s => s.Producto)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (serie == null)
                return NotFound($"Serie con ID {id} no encontrada.");

            return Ok(serie);
        }

        [HttpPost]
        public async Task<ActionResult<Serie>> PostSerie(Serie serie)
        {

            var productoExiste = await _context.Productos.AnyAsync(p => p.Id == serie.ProductoId);
            if (!productoExiste)
                return BadRequest($"No existe un producto con ID {serie.ProductoId}.");

            _context.Series.Add(serie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSerieById), new { id = serie.Id }, serie);
        }
    }
}
