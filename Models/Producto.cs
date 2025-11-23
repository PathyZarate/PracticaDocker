using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tienda.Models
{
    public class Producto
    {
        public int Id {get;set;}
        public string? Modelo {get;set;}
        public string? Tipo {get;set;}
        public decimal Precio {get;set;}
        public ICollection<Serie>? Series { get; set; } = new List<Serie>();
    }
}