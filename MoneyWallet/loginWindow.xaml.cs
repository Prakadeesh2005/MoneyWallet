using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoneyWallet
{
    /// <summary>
    /// Interaction logic for loginWindow.xaml
    /// </summary>
    public partial class loginWindow : Window
    {
        private UserService userService = new UserService();
        public loginWindow()
        {
            InitializeComponent();
        }
        // Login button
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter email and password.");
                return;
            }

            User user = userService.Login(email,password,out string message);
            if (user == null)
            {
                MessageBox.Show(message);
            }
            else
            {
                MessageBox.Show(message);
                MainWindow wallet = new MainWindow(user.uid, user.username);
                wallet.Show();
                this.Close();
            }
        }
        // Open registration window
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        }
    }
}
    

