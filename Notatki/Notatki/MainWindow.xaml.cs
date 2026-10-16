using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Notatki
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public partial class MainWindow : Window
    {
        public ObservableCollection<NoteModel> Notes { get; set; } = new ObservableCollection<NoteModel>();
        private readonly string filePath = "notes.json";

        public MainWindow()
        {
            InitializeComponent();

            LoadNotes(); // wczytaj przy starcie
            NotesListView.ItemsSource = Notes;

            this.Closing += MainWindow_Closing; // zapisz przy zamykaniu
        }

        private void NotesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddNote addNoteWindow = new AddNote();
            addNoteWindow.Owner = this;
            if (addNoteWindow.ShowDialog() == true)
            {
                if (!string.IsNullOrWhiteSpace(addNoteWindow.NoteTitle))
                {
                    Notes.Add(new NoteModel
                    {
                        Title = addNoteWindow.NoteTitle,
                        Description = addNoteWindow.NoteDescription
                    });

                    SaveNotes(); // zapisz po dodaniu
                }
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveNotes();
        }

        private void SaveNotes()
        {
            string jsonString = JsonSerializer.Serialize(Notes);
            File.WriteAllText(filePath, jsonString);
        }

        private void LoadNotes()
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                var loadedNotes = JsonSerializer.Deserialize<ObservableCollection<NoteModel>>(jsonString);

                if (loadedNotes != null)
                {
                    Notes = loadedNotes;
                }
            }
        }
    }
}