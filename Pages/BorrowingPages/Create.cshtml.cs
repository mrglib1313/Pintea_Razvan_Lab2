using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pintea_Razvan_Lab2.Pages.BorrowingPages;

public class CreateModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public CreateModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
 {
 var bookList = _context.Book
 .Include(b => b.Authors)
 .Select(x => new
 {
     x.ID,
     BookFullName = x.Title + " - " + x.Authors.LastName + " " +
x.Authors.FirstName
 });

    ViewData["BookID"] = new SelectList(bookList, "ID",
   "BookFullName");
    ViewData["MemberID"] = new SelectList(_context.Member, "ID",
   "FullName");
 return Page();
 }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Borrowing.Add(Borrowing);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
