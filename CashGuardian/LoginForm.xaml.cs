using CashGuardian.Models;
using CashGuardian.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CashGuardian
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            try
            {
                User user = AuthenticationService.Authenticate(username, password);

                MainWindow Main = new MainWindow();
                Main.Show();

                this.Close();
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BoxChanged(object sender, TextChangedEventArgs e)
        {
            LoginButton.IsEnabled = !string.IsNullOrEmpty(UsernameTextBox.Text) && !string.IsNullOrEmpty(PasswordBox.Password);
        }

        private void BoxChanged(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = !string.IsNullOrEmpty(UsernameTextBox.Text) && !string.IsNullOrEmpty(PasswordBox.Password);
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Username")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = Brushes.Black;
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = Brushes.Gray;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "")
            {
                PasswordBox.Foreground = Brushes.Black;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "")
            {
                PasswordBox.Foreground = Brushes.Gray;
            }
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://kakadu.s3.eu-central-1.amazonaws.com/test-folder/photo_2024-04-01_17-38-05.jpg",
                UseShellExecute = true
            });
        }

        private bool _isPasswordVisible = false;

        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            // show and hide password feature
            _isPasswordVisible = !_isPasswordVisible;
            if (_isPasswordVisible)
            {
                PasswordBox.Visibility = Visibility.Collapsed;
                PlainTextPasswordBox.Visibility = Visibility.Visible;
                PlainTextPasswordBox.Text = PasswordBox.Password;
                ((Button)sender).Content = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/open-eye.png")), Opacity = 0.9 };
                ((ToolTip)((Button)sender).ToolTip).Content = "Hide Password";
            }
            else
            {
                PasswordBox.Visibility = Visibility.Visible;
                PlainTextPasswordBox.Visibility = Visibility.Collapsed;
                PasswordBox.Password = PlainTextPasswordBox.Text;
                ((Button)sender).Content = new Image { Source = new BitmapImage(new Uri("pack://application:,,,/close-eye.png")), Opacity = 0.9 };
                ((ToolTip)((Button)sender).ToolTip).Content = "Show Password";
            }
        }


    }
}
