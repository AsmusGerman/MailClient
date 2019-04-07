using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications.Messages;

namespace MailClient.View
{
	/// <summary>
	/// Interaction logic for Configuration.xaml
	/// </summary>
	public partial class Configuration : Page
	{
        private MailAccount iMailAccount;
		public Configuration(MailAccount pMailAccount)
		{
			InitializeComponent();
            this.iMailAccount = pMailAccount;
			this.tbxAlias.Text = this.iMailAccount.Alias;
			this.tbxMailAddress.Text = this.iMailAccount.MailAddress.Value;
        }

        private async void BtnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.iMailAccount.Deleted = true;
                await Facade.Instance.UpdateAccount();
                Window mParentWindow = Window.GetWindow(this);
                mParentWindow.Close();
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private async void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.iMailAccount.Alias != this.tbxAlias.Text && !string.IsNullOrEmpty(this.tbxAlias.Text))
                    this.iMailAccount.Alias = this.tbxAlias.Text;

                if (this.iMailAccount.MailAddress.Value != this.tbxMailAddress.Text && !string.IsNullOrEmpty(this.tbxMailAddress.Text))
                    this.iMailAccount.MailAddress.Value = this.tbxMailAddress.Text;

                if (this.iMailAccount.Password == this.pwbOldPassword.Password
                    && this.iMailAccount.Password != this.pwbNewPassword.Password
                    && !string.IsNullOrEmpty(this.pwbOldPassword.Password)
                    && !string.IsNullOrEmpty(this.pwbNewPassword.Password))
                    this.iMailAccount.Password = this.pwbNewPassword.Password;

                await Facade.Instance.UpdateAccount();
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }
            finally
            {
                this.NavigationService.GoBack();
            }
        }

        private void PwbOldPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.iMailAccount.Password != this.pwbOldPassword.Password)
                {
                    throw new Exception();
                }
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }

        }
    }
}
