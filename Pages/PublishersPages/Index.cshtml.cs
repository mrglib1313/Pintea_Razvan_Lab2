using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Pintea_Razvan_Lab2.Models;
using Pintea_Razvan_Lab2.Models.ViewModels;

namespace Pintea_Razvan_Lab2.Pages.PublishersPages;

public class IndexModel : PageModel
{
    private readonly Pintea_Razvan_Lab2Context _context;

    public IndexModel(Pintea_Razvan_Lab2Context context)
    {
        _context = context;
    }

    public PublisherIndexData PublisherData { get; set; } = new();
public IList<Publishers> Publishers { get; set; } = new List<Publishers>();
    public int PublisherID { get; set; }
    public int BookID { get; set; }

    public async Task OnGetAsync(int? id, int? bookID)
    {
        PublisherData = new PublisherIndexData();
        
    PublisherData.Publishers = await _context.Publishers

            .Include(p => p.Books)
                .ThenInclude(b => b.Authors)
            .OrderBy(p => p.PublisherName)
            .ToListAsync();
        Publishers = PublisherData.Publishers.ToList();
        if (id != null)
        {
            PublisherID = id.Value;

            var publisher = PublisherData.Publishers
                .SingleOrDefault(p => p.ID == id.Value);

            if (publisher != null)
            {
                PublisherData.Books = publisher.Books;
            }
        }
    }
}
