using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tienda.Data;
using tienda.Models;

namespace tienda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly TiendaContext _context;

        public ProductoController(TiendaContext context)
        {
            _context = context;
        }

        // GET: api/Producto/Listar
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.Series) // Carga las series relacionadas (opcional, pero útil)
                .ToListAsync();
            return Ok(productos);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoById(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Series)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null)
            {
                return NotFound($"Producto con ID {id} no encontrado.");
            }

            return Ok(producto);
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            // Validación básica (opcional, pero recomendada)
            if (string.IsNullOrWhiteSpace(producto.Modelo) || string.IsNullOrWhiteSpace(producto.Tipo))
            {
                return BadRequest("Modelo y Tipo son obligatorios.");
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            // Devuelve 201 Created con Location header
            return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, producto);
        }

        // POST: api/Producto/ConSeries
        [HttpPost("ConSeries")]
        public async Task<ActionResult<Producto>> PostProductoConSeries([FromBody] ProductoConSeriesDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Modelo) || string.IsNullOrWhiteSpace(dto.Tipo))
                return BadRequest("Datos del producto inválidos.");

            var producto = new Producto
            {
                Modelo = dto.Modelo,
                Tipo = dto.Tipo,
                Precio = dto.Precio,
                Series = dto.NrosSerie?.Select(ns => new Serie { NroSerie = ns }).ToList() ?? new List<Serie>()
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, producto);
        }
    }

    // DTO para crear producto + series en una sola llamada
    public class ProductoConSeriesDto
    {
        public string? Modelo { get; set; }
        public string? Tipo { get; set; }
        public decimal Precio { get; set; }
        public List<string>? NrosSerie { get; set; } // Ej: ["SN001", "SN002", "SN003"]
    }
}