using System;
using System.Collections.Generic;

namespace APIProductos.Models.Entities;

public partial class Productos
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public double Precio { get; set; }

    public bool Eliminado { get; set; }

    public string Imagen { get; set; } = null!;
}
