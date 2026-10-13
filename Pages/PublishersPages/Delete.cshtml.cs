using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.PublishersPages;

public class DeleteModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public DeleteModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Publishers Publishers { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publishers = await _context.Publishers.FirstOrDefaultAsync(m => m.ID == id);
        if (publishers is null)
        {
            return NotFound();
        }
        else
        {
            Publishers = publishers;
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var publishers = await _context.Publishers.FindAsync(id);
        if (publishers != null)
        {
            Publishers = publishers;
            _context.Publishers.Remove(Publishers);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
