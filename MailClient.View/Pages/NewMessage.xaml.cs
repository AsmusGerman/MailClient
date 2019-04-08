using MailClient.Shared;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications.Messages;

namespace MailClient.View
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class NewMessage : Page
    {
        private MailAccount iMailAccount;
        private MailMessage iMailMessage;

        public NewMessage(MailAccount pMailAccount)
        {
            InitializeComponent();
            this.tbFromMailAddress.Text = "Nuevo Mensaje";
            this.iMailAccount = pMailAccount;
        }

        public NewMessage(MailAccount pMailAccount, MailMessage pMailMessage)
        {
            InitializeComponent();
            this.tbFromMailAddress.Text = "Mensaje de respuesta";
            this.iMailAccount = pMailAccount;
            this.iMailMessage = pMailMessage;

            this.lvToMailAddresses.ItemsSource = pMailMessage.To.Select(bMailMessages => bMailMessages.Value);
            this.tbxMailSubject.Text = "RE:" + pMailMessage.Subject;
            this.tbBody.Text = Environment.NewLine + new string('-', 100) + Environment.NewLine + pMailMessage.Body;
        }

        #region handlers for ToMailAddress ListView

        private void TbxToMailAddresses_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.OemComma)
            {
                this.UpdateMailAddressListView();
            }
        }

        private void TbxToMailAddresses_LostFocus(object sender, RoutedEventArgs e)
        {
            this.UpdateMailAddressListView();
        }

        private void UpdateMailAddressListView()
        {
            try
            {
                string bMailAddress = this.tbxToMailAddresses.Text.TrimEnd(';');
                if (this.IsValidMailAddress(bMailAddress))
                {
                    if (!this.lvToMailAddresses.Items.Contains(bMailAddress))
                    {
                        this.lvToMailAddresses.Items.Add(bMailAddress);
                    }
                    this.tbxToMailAddresses.Clear();
                }
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }
        }

        private bool IsValidMailAddress(string pMailAddress)
        {
            try
            {
                new System.Net.Mail.MailAddress(pMailAddress);
                return true;
            }
            catch (Exception bException)
            {
                Exception mException = new Exception("El formato del correo ingresado no es válido", bException);
                Facade.Instance.Notifier.ShowError(mException.Message);
                return false;
            }
        }
        #endregion

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label mToMailAddress = (Label)sender;
            this.lvToMailAddresses.Items.Remove(mToMailAddress.Content);
        }

        private async void BtnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailMessage mMailMessage = new MailMessage()
                {
                    Subject = this.tbxMailSubject.Text,
                    To = lvToMailAddresses.Items.Cast<string>().Select(bMailAddress => new MailAddress()
                    {
                        Value = bMailAddress

                    }).ToList(),
                    Body = this.tbBody.Text
                };
                await Facade.Instance.Send(this.iMailAccount, mMailMessage);
                this.NavigationService.GoBack();
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }

        }

        private void BtnCloseMessage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
