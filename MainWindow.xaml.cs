using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2ToufiqCharania
{
    public partial class MainWindow : Window
    {
        Login newLogin = new Login();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            // If username is empty, notify user to enter username
            if (string.IsNullOrEmpty(username.Text))
            {
                MessageBox.Show("Please enter a username");
                return;
            }
            // If password is empty, notify user to enter password
            else if (string.IsNullOrEmpty(password.Password))
            {
                MessageBox.Show("Please enter your password");
                return;
            }

            // Validate user's credentials and displays the corresponding message
            var validUser = newLogin.ValidateUser(username.Text, password.Password);

            if (!validUser)
            {
                MessageBox.Show("Invalid Username or Password");
                return;
            }

            MessageBox.Show("Login Successful");

            // Upon successful login, display main window for loan calculator
            LoanCalculatorWindow loanCalculatorWindow = new LoanCalculatorWindow();
            loanCalculatorWindow.Show();
            // Closes the login window
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            // Closes application if user clicks the Cancel button
            this.Close();
        }
    }
}
