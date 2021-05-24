using NonePro.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static WPFNotepad.DataBase.DBProccessor;

namespace NonePro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingList<NoteModel> noteList;

        public MainWindow()
        {
            InitializeComponent();
        }

        ~MainWindow()
        {
            CloseConnection();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CheckUser())
            {
                noteList = GetNote();
                noteList.ListChanged += NoteList_ListChanged;
                dgList.ItemsSource = noteList;
            }
        }

        private void NoteList_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    InsertNote();
                    break;
                case ListChangedType.ItemChanged:
                    UpdateNote(dgList.SelectedItem as NoteModel);
                    break;
            }
        }

        private void dgList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            noteList = GetNote();
            if (noteList.Count > 0 && dgList.SelectedIndex >= 0 && dgList.SelectedIndex < noteList.Count)
            {
                TextBox.Text = noteList[dgList.SelectedIndex].Text;
            }
        }

        private void SaveButtonClick(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.S && noteList.Count > dgList.SelectedIndex)
                UpdateText(noteList[dgList.SelectedIndex].NoteID, TextBox.Text);
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (noteList.Count > dgList.SelectedIndex)
                UpdateText(noteList[dgList.SelectedIndex].NoteID, TextBox.Text);
        }

        private void DeleteKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && dgList.SelectedIndex >= 0)
                DeleteNote(dgList.SelectedItem as NoteModel);
        }
    }
}
