using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NoteBook.Models;
using NoteBook.Contexts;

namespace NoteBook
{
    public partial class NoteBookForm : Form
    {
        public NoteBookForm(int userId)
        {
            InitializeComponent();
            LoginId = userId;
            using (var nt = new NoteContext())
            {
                someNotes = nt.Notes.Where(p => p.UserId == LoginId).ToList();
                foreach (var a in someNotes)
                {
                    listBox1.Items.Add(a.Title);
                }
            }

        }

        private List<Note> someNotes;
        private string titlePic;
        private string path = @"C:\Users\egor5\OneDrive\Рабочий стол\Лютая резня\NoteBook\Resources\";
        private string FileName;
        private int LoginId;

        private void button1_Click(object sender, EventArgs e)
        {
            if (textInput.Text != "" && titleInput.Text != "")
                CreateNote();
            
        }

        private void picButt_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            this.FileName = openFileDialog1.FileName;
            
            var newFileName = FileName.Split('\\');

            titlePic = FileName;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            string str = listBox1.SelectedItem.ToString();
            using (var nt = new NoteContext())
            {
                using (var at = new AttachmentContext())
                {
                    var note = nt.Notes.Where(p => p.Title == str.ToString()).ToList();
                    foreach (var a in note)
                    {
                        
                        var attaches = at.Attachments.Where(p=>p.NoteId == a.Id).ToList();
                        if (attaches.Count > 0)
                        {
                            infoTable.AppendText($"Note: {a.Title}\n  text: {a.Text}\n    Picture:\n   ");
                            AppendPictures(path + attaches[0].NoteId.ToString());
                            infoTable.AppendText("\n");
                        }
                        else
                            infoTable.AppendText($"Note: {a.Title}\n  text: {a.Text}\n\n");
                    }
                }
            }
        }

        private void CreateNote()
        {
            
            if (titlePic == null)
            {
                using (var nt = new NoteContext()) 
                {
                    Note note = new Note()
                    {
                        Title = titleInput.Text,
                        Text = textInput.Text,
                        UserId = LoginId
                    };
                    infoTable.AppendText($"Note: {note.Title}\n  text: {note.Text}\n\n");
                    nt.Notes.Add(note);
                    nt.SaveChanges();
                    listBox1.Items.Add(note.Title);
                }      
            }
            else
            {
                using(var nt = new NoteContext())
                {
                    Note note = new Note()
                    {
                        Title = titleInput.Text,
                        Text = textInput.Text,
                        UserId = LoginId,
                    };
                    nt.Notes.Add(note);
                    nt.SaveChanges();
                    using(var at = new AttachmentContext())
                    {
                        Attachment attach = new Attachment()
                        {
                            Img = path,
                            NoteId = note.Id
                        };
                        File.Copy(FileName, path + note.Id);
                        at.Attachments.Add(attach);
                        at.SaveChanges();
                        
                    }
                    infoTable.AppendText($"Note: {note.Title}\n  text: {note.Text}\n    Picture:\n   ");
                    AppendPictures(titlePic);
                    infoTable.AppendText("\n Added!\n\n");
                    listBox1.Items.Add(note.Title);
                }
            }
        }

        private void AppendPictures(string namePic)
        {
            Image img = Image.FromFile(namePic);
            Clipboard.SetImage(img);
            infoTable.ReadOnly = false;
            infoTable.Paste();
            infoTable.ReadOnly = true;
        }
    }
}
