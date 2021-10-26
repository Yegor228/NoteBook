using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string Title {  get; set; }

        public string Text {  get; set; }

        public int UserId {  get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; }

    }
}
