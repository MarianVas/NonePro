using System.Windows;
using static WPFNotepad.DataBase.DBProccessor;

namespace NonePro
{
    /// <summary>
    /// Interaction logic for EnterForm.xaml
    /// </summary>
    public partial class EnterForm : Window
    {
        public EnterForm()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string password = PassTextBox.Password;
            string rpassword = RPassTextBox.Password;
            string email = MailTextBox.Text;

            if (password == "" || email == "" || rpassword == "")
            {
                MessageBox.Show("Fill all forms!");
                return;
            }
            else if (password != rpassword)
            {
                MessageBox.Show("Passwords does not match!");
                return;
            }
            else if (CheckMail(email))
            {
                MessageBox.Show("User with same email already exist!");
                return;
            }

            CreateNewUser(email, password);
            MessageBox.Show("User Created Succesfuly!");
        }
    }
}
