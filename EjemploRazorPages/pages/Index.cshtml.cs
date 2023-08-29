using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EjemploRazorPages.pages
{
    public class IndexModel : PageModel
    {
        public List<string> Tabla { get; set; } = new();

        public void OnGet(int tabla)
        {
            for (int i = 1; i <= 10; i++)
            {
                string o = $"{tabla}x{i}={tabla * i}";
                Tabla.Add(o);
            }                                        
        }
    }
}
