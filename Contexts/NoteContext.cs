using System.Data.Entity;
using NoteBook.Models;

namespace NoteBook.Contexts
{
    internal class NoteContext : DbContext
    {
        public NoteContext() : base("DbConnection")
        { }

        public DbSet<Note> Notes { get; set; }
    }
}
