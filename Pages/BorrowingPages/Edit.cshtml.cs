
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.BorrowingPages;

public class EditModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public EditModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Borrowing Borrowing { get; set; } = default!;

    private void PopulateDropdowns()
    {
        var bookList = _context.Book
            .Include(b => b.Authors)
            .Select(b => new
            {
                b.ID,
                BookFullName = b.Title + " - " +
                    b.Authors.LastName + " " +
                    b.Authors.FirstName
            });

        ViewData["MemberID"] = new SelectList(
            _context.Member,
            "ID",
            "FullName",
            Borrowing.MemberID);

        ViewData["BookID"] = new SelectList(
            bookList,
            "ID",
            "BookFullName",
            Borrowing.BookID);
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
            return NotFound();

        Borrowing = await _context.Borrowing
            .FirstOrDefaultAsync(b => b.ID == id);

        if (Borrowing == null)
            return NotFound();

        PopulateDropdowns();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            PopulateDropdowns();
            return Page();
        }

        _context.Attach(Borrowing).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Borrowing.Any(e => e.ID == Borrowing.ID))
                return NotFound();

            throw;
        }

        return RedirectToPage("./Index");
    }
}
