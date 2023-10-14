using System;
using System.Collections.Generic;

namespace APIProductos.Models.Entities;

public partial class Categorias
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Productos2> Productos2 { get; set; } = new List<Productos2>();
}
