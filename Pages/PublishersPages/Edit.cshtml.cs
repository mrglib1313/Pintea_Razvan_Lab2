using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.PublishersPages;

public class EditModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public EditModel(Pintea_Razvan_Lab2Context context)
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
        Publishers = publishers;
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Attach(Publishers).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PublishersExists(Publishers.ID))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToPage("./Index");
    }

    private bool PublishersExists(int id)
    {
        return _context.Publishers.Any(e => e.ID == id);
    }
}
