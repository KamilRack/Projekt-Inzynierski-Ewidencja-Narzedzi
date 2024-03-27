using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Narzedzia.Models
{
    public class ParentHomeModel
    {
        public List<Awaria> Awarie { get; set; }
        public List<Narzedzie> Narzedzia { get; set; }
    }
}
