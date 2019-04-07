using MailClient.Shared;
using MailClient.Shared.Exceptions;
using System;
using System.Windows;
using ToastNotifications.Messages;

namespace MailClient.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAccount mMailAccount = null;
                //inicia spinner
                this.iSpinner.IsIndeterminate = true;

                //corroborar que los campos no estén vacíos
                if (!string.IsNullOrEmpty(this.pwbPassword.Password))
                {
                    if (!string.IsNullOrEmpty(this.tbxAlias.Text))
                    {
                        mMailAccount = await Facade.Instance.LoginByAlias(this.tbxAlias.Text, this.pwbPassword.Password);
                    }
                    else if (!string.IsNullOrEmpty(this.tbxMailAddress.Text))
                    {
                        mMailAccount = await Facade.Instance.LoginByMailAddress(this.tbxMailAddress.Text, this.pwbPassword.Password);
                    }
                    else
                    {
                        InvalidOperationException bInnerException = new InvalidOperationException("Al menos uno de los cuadros de texto está en blanco o es un espacio vacío");
                        throw new WellKnownException("No se pudo continuar porque existen campos incompletos", bInnerException);
                    }
                }

                Facade.Instance.Notifier.ShowSuccess("¡Bienvenido nuevamente!");
                //acceder a la vista de la cuenta
                new AccountWindow(mMailAccount).Show();
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }
            finally
            {
                //finaliza spinner
                this.iSpinner.IsIndeterminate = false;
            }
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.pwbPassword.Password)
                    || string.IsNullOrEmpty(this.tbxAlias.Text)
                    || string.IsNullOrEmpty(this.tbxMailAddress.Text))
                {
                    InvalidOperationException bInnerException = new InvalidOperationException("Al menos uno de los cuadros de texto está en blanco o es un espacio vacío");
                    throw new WellKnownException("No se pudo continuar porque existen campos incompletos", bInnerException);
                }

                await Facade.Instance.Register(this.tbxAlias.Text, this.tbxMailAddress.Text, this.pwbPassword.Password);
                Facade.Instance.Notifier.ShowSuccess("¡La cuenta fue registrada con éxito!");

                //se limpian los controles
                this.tbxAlias.Clear();
                this.tbxMailAddress.Clear();
                this.pwbPassword.Clear();
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }
        }
    }
}
