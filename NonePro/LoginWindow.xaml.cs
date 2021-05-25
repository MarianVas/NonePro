using NonePro;
using System.Windows;
using System.Windows.Controls;
using static WPFNotepad.DataBase.DBProccessor;

namespace WPFNotepad
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string mailText;
        private string passText;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenConnection();

            if (!CheckConnection())
                MessageBox.Show("Program can`t reach database!");

            if (VerificateUser(mailText, passText))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
                return;
            }
            MessageBox.Show("Incorrect username or password");
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            EnterForm enterForm = new EnterForm();
            enterForm.Show();
        }

        private void MailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mailText = MailTextBox.Text;
        }

        private void PassTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passText = PassTextBox.Password;
        }
    }
}
