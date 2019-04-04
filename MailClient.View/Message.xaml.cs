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
using System.Windows.Shapes;

namespace MailClient.View
{
	/// <summary>
	/// Interaction logic for Message.xaml
	/// </summary>
	public partial class Message : Window
	{
		public MailMessageVM NewMessage { get; set; }
		private IList<string> To;
		public Message()//string pMailAddress = "")
		{
			InitializeComponent();
			this.NewMessage = new MailMessageVM();
			this.NewMessage.From = ""; //pMailAddress;

			this.To = new List<string>();


		}

		private void btnNewMessage_Click(object sender, RoutedEventArgs e)
		{

		}

		private void tbxToMailAddresses_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter || e.Key == Key.OemComma)
			{
				string bMailAddress = this.tbxToMailAddresses.Text.TrimEnd(';');
				if (this.IsValidMailAddress(bMailAddress)) { 
					if (!this.lvToMailAddresses.Items.Contains(bMailAddress))
					{
						this.lvToMailAddresses.Items.Add(bMailAddress);
					}
				this.tbxToMailAddresses.Clear();
				}
			}
		}

		private bool IsValidMailAddress(string pMailAddress)
		{
			try
			{
				new System.Net.Mail.MailAddress(pMailAddress);
				return true;
			}
			catch (Exception)
			{
				this.HandleException(new Exception("El formato del correo ingresado no es válido"));
				return false;
			}
		}

		private void HandleException(Exception pException)
		{
		}

		private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			Label mToMailAddress = (Label)sender;
			this.tbxToMailAddresses.Text = mToMailAddress.Content + ";";
		}
	}
}
