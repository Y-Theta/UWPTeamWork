using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using xBindDataExample.Models;


namespace UWPTeamWork
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NotePage : Page
    {
        private List<Note> Notes;
        //private HashSet<Note> set; 

        public NotePage()
        {
            Notes = NoteManager.GetNotes();
            this.InitializeComponent();
            
        }
        private void GridView_ItemClick(object sender, ItemClickEventArgs ee)
        {
            /*try
            {
                var box = (CheckBox)ee.ClickedItem;
                var myNote = (Note)ee.ClickedItem;
                if (box.IsChecked == true)
                {
                    set.Add(myNote);
                }
                else
                {
                    set.Remove(myNote);
                }
                return;
            }
            catch (Exception e)
            {
                var myNote = (Note)ee.ClickedItem;
                Frame.Navigate(typeof(ShowPage), myNote);
            }*/
            var myNote = (Note)ee.ClickedItem;
            Frame.Navigate(typeof(ShowPage), myNote);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note{ summary = "Summary",NoteID = NoteManager.getid(), MyTime = DateTime.Now, MyText = "", CoverImage = "Assets/timg.jpg" } ;
            NoteManager.add(note);
            Notes = NoteManager.GetNotes();
           // Frame.Navigate(typeof(NotePage));
            Frame.Navigate(typeof(ShowPage),note);
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
            Notes = NoteManager.GetNotes();
        }

        /*private void delete_button(object sender, RoutedEventArgs e)
        {
            /*foreach (Note de in set)
            {
                NoteManager.DeleteNote(de);
            }
            set.Clear();
            for(int i = 0; i < Notes.Count; i++)
            {
                myGridView.Selec
            }
            Frame.Navigate(typeof(NotePage));
        }*/
    }
}
