using System;
using System.Collections.Generic;

namespace APIProductos.Models.Entities;

public partial class Productos2
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public double? Precio { get; set; }

    public int IdCategoria { get; set; }

    public virtual Categorias IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Listacompras> Listacompras { get; set; } = new List<Listacompras>();
}
