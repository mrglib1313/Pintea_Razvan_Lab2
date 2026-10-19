using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Migrations;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public EditModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        var book = await _context.Book

                 .Include(b => b.Publisher)
          .Include(b => b.BookCategories).ThenInclude(b => b.Category)
         .Include(b => b.Authors)
         .AsNoTracking()
         .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        PopulateAssignedCategoryData(_context, book);


        Book = book;
        ViewData["PublisherID"] = new SelectList(_context.Set<Publishers>(), "ID",
"PublisherName");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.

    public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }
        //se va include Author conform cu sarcina de la lab 2
        var bookToUpdate = await _context.Book
            .Include(i => i.Publisher)
            .Include(i => i.BookCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
        if (bookToUpdate == null)
        {
            return NotFound();
        }
        //se va modifica AuthorID conform cu sarcina de la lab 2
        if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            i => i.Title, i => i.AuthorsID,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        UpdateBookCategories(_context, selectedCategories, bookToUpdate);
        PopulateAssignedCategoryData(_context, bookToUpdate);
        return Page();

    }
}

    