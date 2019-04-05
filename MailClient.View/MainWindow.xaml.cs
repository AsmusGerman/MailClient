using MailClient.Shared;
using MailClient.Shared.Exceptions;
using MailClient.View.Components;
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

namespace MailClient.View
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.AlertComponent.CloseAlert += AlertComponent_CloseAlert;
		}

		private void AlertComponent_CloseAlert(object sender, EventArgs e)
		{
			this.AlertDialog.IsOpen = false;
		}

		private void HandleException(Exception pException)
		{
			this.AlertDialog.IsOpen = true;
			this.AlertComponent.Body = pException.Message;
		}

		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.pwbPassword.Password) ||
				string.IsNullOrEmpty(this.tbxAlias.Text) &&
				string.IsNullOrEmpty(this.tbxMailAddress.Text))
				{
					InvalidOperationException bInnerException = new InvalidOperationException("Al menos uno de los cuadros de texto está en blanco o es un espacio vacío");
					throw new WellKnownException("No se pudo continuar porque existen campos incompletos", bInnerException);
				}

				MailAccount mMailAccount = null;
				if (!string.IsNullOrEmpty(this.tbxAlias.Text))
				{
					mMailAccount = Facade.Instance.LoginByAlias(this.tbxAlias.Text, this.pwbPassword.Password);
				}
				else
				{
					mMailAccount = Facade.Instance.LoginByMailAddress(this.tbxMailAddress.Text, this.pwbPassword.Password);
				}

				new AccountWindow(mMailAccount).Show();
			}
			catch (Exception bException)
			{
				this.HandleException(bException);
			}

		}

		private void btnRegister_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.pwbPassword.Password) && string.IsNullOrEmpty(this.tbxAlias.Text))
					if (string.IsNullOrEmpty(this.pwbPassword.Password) && string.IsNullOrEmpty(this.tbxMailAddress.Text))
					{
						InvalidOperationException bInnerException = new InvalidOperationException("Al menos uno de los cuadros de texto está en blanco o es un espacio vacío");
						throw new WellKnownException("No se pudo continuar porque existen campos incompletos", bInnerException);
					}

				Facade.Instance.Register(this.tbxAlias.Text, this.tbxMailAddress.Text, this.pwbPassword.Password);
			}
			catch (Exception bException)
			{
				this.HandleException(bException);
			}
		}
	}
}
