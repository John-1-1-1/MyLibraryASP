using System.ComponentModel.DataAnnotations;

namespace MyLibraryASP.Models
{
    public class Reader: People
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
