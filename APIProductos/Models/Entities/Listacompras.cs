using System;
using System.Collections.Generic;

namespace APIProductos.Models.Entities;

public partial class Listacompras
{
    public int Id { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Productos2 IdProductoNavigation { get; set; } = null!;
}
