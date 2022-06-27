using System.ComponentModel.DataAnnotations;

namespace MyLibraryASP.Models
{
    public class ReadingLog
    {

        public int Id { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int ReaderId { get; set; }
        public Reader? Reader { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfReturn { get; set; }
    }
}
