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
    [Serializable]
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
        static int id=0;
        static bool isloaded = false;
        public static void init()
        {
            id = 0;
        }
        public static void add(Note x)
        {
            notes.Add(x);
            id++;
            Save("");//需要文件名
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
                Load("");//提供文件名或指定一常量文件名,不用加类型（.note）
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
            Save("");//文件名
        }

        public async static void Save(string filename)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile f = await folder.CreateFileAsync(filename+".note", CreationCollisionOption.ReplaceExisting);
            using (FileStream stream = new FileStream(f.CreateSafeFileHandle(), FileAccess.Write))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, notes);
            }
        }
        
        public async static void Load(string filename)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile f = await folder.TryGetItemAsync(filename+".note") as StorageFile;
            if(f != null)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(f.CreateSafeFileHandle(), FileAccess.Read))
                {
                    notes = (List<Note>)binaryFormatter.Deserialize(stream);
                    if (notes.Count != 0)
                        id = notes[notes.Count - 1].NoteID + 1;
                }
            }
        }
    }
}
