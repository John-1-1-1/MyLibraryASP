using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLibraryASP.Models;

namespace MyLibraryASP.Data
{
    public class MyLibraryASPContext : DbContext
    {
        public MyLibraryASPContext (DbContextOptions<MyLibraryASPContext> options)
            : base(options)
        {
        }

        public DbSet<MyLibraryASP.Models.Autor>? Autor { get; set; }

        public DbSet<MyLibraryASP.Models.Book>? Book { get; set; }

        public DbSet<MyLibraryASP.Models.Category>? Category { get; set; }

        public DbSet<MyLibraryASP.Models.Lable>? Lable { get; set; }

        public DbSet<MyLibraryASP.Models.Reader>? Reader { get; set; }

        public DbSet<MyLibraryASP.Models.Shelf>? Shelf { get; set; }

        public DbSet<MyLibraryASP.Models.ReadingLog>? ReadingLog { get; set; }

    }
}
