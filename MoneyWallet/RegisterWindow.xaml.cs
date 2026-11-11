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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserService userService = new UserService();
        public RegisterWindow()
        {
            InitializeComponent();
        }
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            bool result = userService.Register(username,email,password,out string message);
            MessageBox.Show(message);
            if (result)
            {
                loginWindow login = new loginWindow();
                login.Show();
                this.Close();
            }
        }
        private void btnBack_Click(object sender,RoutedEventArgs e)
        {
            loginWindow login = new loginWindow();
            login.Show();
            this.Close();
        }
    }
}
    

