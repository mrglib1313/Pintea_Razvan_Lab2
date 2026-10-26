using Pintea_Razvan_Lab2.Migrations;

namespace Pintea_Razvan_Lab2.Models.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publishers> Publishers { get; set; }
        = new List<Publishers>();
        public IEnumerable<Book>? Books { get; set; }

    }
}
