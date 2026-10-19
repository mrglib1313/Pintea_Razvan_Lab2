using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Migrations;
using Pintea_Razvan_Lab2.Models;

namespace Pintea_Razvan_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public CreateModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        ViewData["PublisherID"] = new SelectList(_context.Set<Publishers>(), "ID",
            "PublisherName");
        ViewData["AuthorsID"] = new SelectList(
            _context.Authors
                .Select(a => new
                {
                    a.ID,
                    FullName = a.FirstName + " " + a.LastName
                }),
            "ID",
            "FullName");

        var book = new Book();
        book.BookCategories = new List<BookCategory>();

        PopulateAssignedCategoryData(_context, book);

        return Page();
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var newBook = new Book();
        if (selectedCategories != null)
        {
            newBook.BookCategories = new List<BookCategory>();
            foreach (var cat in selectedCategories)
            {
                var catToAdd = new BookCategory
                {
                    CategoryID = int.Parse(cat)
                };
                newBook.BookCategories.Add(catToAdd);
            }
        }
        Book.BookCategories = newBook.BookCategories;
        _context.Book.Add(Book);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}
