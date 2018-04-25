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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace UWPTeamWork
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NotePage : Page
    {
        private List<Note> Notes;
        public NotePage()
        {
            this.InitializeComponent();
            Notes = NoteManager.GetNotes();
        }
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var Note = (Note)e.ClickedItem;
            Frame.Navigate(typeof(ShowPage),Note);
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note{ NoteID = NoteManager.getid(), MyTime = DateTime.Now, MyText = "", CoverImage = "Assets/timg.jpg" } ;
            NoteManager.add(note);
            Frame.Navigate(typeof(ShowPage),note);
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
