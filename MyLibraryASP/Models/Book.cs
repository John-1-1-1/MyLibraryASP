namespace MyLibraryASP.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AutorId { get; set; }
        public int CategoryId { get; set; }
        public Autor? Autor { get; set; }
        public Category? Category { get; set; }
        public byte[] Image { get; set; }
        public int ShelfId { get; set; }
        public Shelf? Shelf { get; set; }
        public int LableId { get; set; }
        public Lable? Lable { get; set; }
    }
}
