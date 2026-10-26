
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public IndexModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = new List<Category>();

    public Category? SelectedCategory { get; set; }

    public async Task OnGetAsync(int? id)
    {
        Category = await _context.Category
            .Include(c => c.BookCategories!)
                .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Authors)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        if (id.HasValue)
        {
            SelectedCategory = Category
                .FirstOrDefault(c => c.ID == id.Value);
        }
    }
}
