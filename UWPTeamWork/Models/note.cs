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
        public string summary { get; set; }
    }

    public class NoteManager
    {
        static List<Note> notes = new List<Note>();
        static int id;
        static bool isloaded = false;
        public static void init()
        {
            id=0;
        }
        public static void add(Note x)
        {
            notes.Add(x);
            id++;
            Save();
        }
        public static int getid()
        {
            return id;
        }
        public static List<Note> GetNotes()
        {
            //var notes = new List<Note>();
            //notes.Add(new Note { NoteID = 1, MyTime = DateTime.Now, MyText = "sadasd", CoverImage = "Assets/timg.jpg" });
           // notes.Add(new Note { NoteID = 0x3f3f3f3f, MyText="", CoverImage = "Assets/add.jpg" });
           if(isloaded == false)
            {
                isloaded = true;
                Load();
            }
            return notes;
        }

        public static void DeleteNote(Note note)
        {
            for(int i=0; i < notes.Count; i++)
            {
                if(notes[i].NoteID == note.NoteID)
                {
                    notes.Remove(notes[i]);
                }
            }
            Save();
        }
        
        public static void Save()
        {

        }
        
        public static void Load()
        {

        }
    }
}
