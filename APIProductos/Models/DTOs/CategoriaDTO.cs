using APIProductos.Repositories;
using System.ComponentModel.DataAnnotations;

namespace APIProductos.Models.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debes indicar el nombre de la categoria.")]
        [MaxLength(100, ErrorMessage = "El nombre de la categoria no debe exceder de 100 caracteres.")]
        [CategoriaNoRepetida]
        public string Nombre { get; set; } = null!;

        public List<ProductoDTO>?Productos = null;

    }


    public class CategoriaNoRepetida : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var repository = validationContext.GetService<CategoriasRepository>();
            if(repository!=null && value!=null)
            {
                if (repository.GetByName((string)value) == null)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("La categoria ya existe");
        }
    }
}
