using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public IndexModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Book = await _context.Book
            
        .Include(b => b.Publisher)
        .Include(b => b.Authors)

        .ToListAsync();

    }
}
