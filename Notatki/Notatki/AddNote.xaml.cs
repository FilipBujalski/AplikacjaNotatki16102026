using System.Windows;
using System.Windows.Input;

namespace Notatki
{
    public partial class AddNote : Window
    {
        public string NoteTitle { get; private set; }
        public string NoteDescription { get; private set; }

        public AddNote()
        {
            InitializeComponent();
        }

        private void Header_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            NoteTitle = TitleTextBox.Text;
            NoteDescription = DescriptionTextBox.Text;
            this.DialogResult = true;
            this.Close();
        }
    }
}