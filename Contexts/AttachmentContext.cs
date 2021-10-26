using System.Data.Entity;
using NoteBook.Models;

namespace NoteBook.Contexts
{
    public class AttachmentContext : DbContext
    {
        public AttachmentContext() : base("DbConnection")
        { }

        public DbSet<Attachment> Attachments { get; set; }
    }
}
