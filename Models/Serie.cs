using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tienda.Models
{
    public class Serie
    {
        public int Id {get;set;}
        public string? NroSerie {get;set;}
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
    }
}