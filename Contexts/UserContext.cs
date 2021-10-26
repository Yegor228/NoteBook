using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteBook.Models;

namespace NoteBook.Contexts
{
    internal class UserContext : DbContext
    {
        public UserContext() : base("DbConnection")
        { }

        public DbSet<User> Users {  get; set; }
    }
}
