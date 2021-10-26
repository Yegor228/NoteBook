using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook.Models
{
    public  class Attachment
    {
        public int Id {  get; set; }
        public string Img {  get; set; }

        public int NoteId {  get; set; }

        public virtual Note Note {  get; set; }

    }
}
