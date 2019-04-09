using MailClient.Shared;
using MailClient.Shared.Exceptions;
using System;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Messages;

namespace MailClient.View
{
    public partial class MainWindow : Window
    {
        public static Notifier Notifier { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            MainWindow.Notifier = Facade.Instance.GetNotifier(this);
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
                        throw new WellKnownException("No se pudo continuar porque existen campos incompletos");
                    }
                }

                Notifier.ShowSuccess("¡Bienvenido nuevamente!");
                //acceder a la vista de la cuenta
                new AccountWindow(mMailAccount).Show();
            }
            catch (Exception bException)
            {
                Notifier.ShowError(bException.Message);
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
                    throw new WellKnownException("No se pudo continuar porque existen campos incompletos");
                }

                await Facade.Instance.Register(this.tbxAlias.Text, this.tbxMailAddress.Text, this.pwbPassword.Password);
                Notifier.ShowSuccess("¡La cuenta fue registrada con éxito!");

                //se limpian los controles
                this.tbxAlias.Clear();
                this.tbxMailAddress.Clear();
                this.pwbPassword.Clear();
            }
            catch (Exception bException)
            {
                Notifier.ShowError(bException.Message);
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Notifier.ShowInformation("¡Bienvenido!");
            Notifier.ShowInformation("Si aún no tienes cuenta, completa los tres campos y haz click en Registrar");
            Notifier.ShowInformation("Si ya tienes una cuenta, ingresa el alias o la dirección de correo, la constraseña de tu cuenta y haz click en Ingresar.");
        }
    }
}
