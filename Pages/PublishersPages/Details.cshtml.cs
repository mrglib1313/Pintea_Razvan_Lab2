using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.PublishersPages;

public class DetailsModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;
    public DetailsModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

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
}
