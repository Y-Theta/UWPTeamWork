using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace xBindDataExample.Models
{
    public class Note
    {
        public DateTime MyTime { get; set; }
        public string MyText { get; set; }
        public string CoverImage { get; set; }
        public int NoteID { get; set; }
    }

    public class NoteManager
    {
        public static List<Note> GetNotes()
        {
            var notes = new List<Note>();
            notes.Add(new Note { NoteID = 1, MyTime = DateTime.Now, MyText = "sadasd", CoverImage = "Assets/timg.jpg" });
           // notes.Add(new Note { NoteID = 0x3f3f3f3f, MyText="", CoverImage = "Assets/add.jpg" });

            return notes;
        }
    }
}
