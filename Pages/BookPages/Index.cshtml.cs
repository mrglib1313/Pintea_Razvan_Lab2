using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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
    public BookData BookD { get; set; } = default!;
    public int BookID { get; set; }
    public int CategoryID { get; set; }
    public string TitleSort { get; set; }
    public string AuthorSort { get; set; }
    public string CurrentFilter { get; set; }




    public async Task OnGetAsync(
        int? id,
        int? categoryID,
        string? sortOrder,
        string? searchString)
    {
        searchString = searchString?.Trim();

        BookD = new BookData();

        TitleSort = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";
        CurrentFilter = searchString ?? "";

        BookD.Books = await _context.Book
            .Include(b => b.Publisher)
            .Include(b => b.Authors)
            .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
            .AsNoTracking()
            .ToListAsync();



        if (!string.IsNullOrWhiteSpace(searchString))
        {
            string search = searchString.Trim().ToLower();

            BookD.Books = BookD.Books
                .Where(b =>
                    (b.Title != null &&
                     b.Title.ToLower().Contains(search)) ||
                    (b.Authors != null &&
                     ((b.Authors.FirstName != null &&
                       b.Authors.FirstName.ToLower().Contains(search)) ||
                      (b.Authors.LastName != null &&
                       b.Authors.LastName.ToLower().Contains(search)) ||
                      (b.Authors.FirstName + " " + b.Authors.LastName)
                          .ToLower().Contains(search)))
                )
                .ToList();
        }



        if (id != null)
        {
            BookID = id.Value;

            var book = BookD.Books.FirstOrDefault(b => b.ID == id.Value);

            if (book != null && book.BookCategories != null)
            {
                BookD.Categories = book.BookCategories
                    .Select(bc => bc.Category);
            }
        }

       
        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Title).ToList();
                break;

            case "author":
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Authors == null
                        ? ""
                        : b.Authors.FullName).ToList();
                break;

            case "author_desc":
                BookD.Books = BookD.Books
                    .OrderByDescending(b => b.Authors == null
                        ? ""
                        : b.Authors.FullName).ToList();
                break;

            default:
                BookD.Books = BookD.Books
                    .OrderBy(b => b.Title).ToList();
                break;
        }
    }
}

