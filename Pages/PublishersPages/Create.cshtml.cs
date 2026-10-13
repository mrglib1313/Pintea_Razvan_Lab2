using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.PublishersPages;

public class CreateModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public CreateModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Publishers Publishers { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Publishers.Add(Publishers);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
