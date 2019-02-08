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
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace MailClient.View
{
	/// <summary>
	/// Interaction logic for AccountWindow.xaml
	/// </summary>
	public partial class AccountWindow : Window
	{
		private Inbox iInboxPage;
		private Message iMessagePage;
		private Configuration iConfigurationPage;

		public AccountWindow(MailAccount pMailAccount)
		{
			InitializeComponent();

			this.iInboxPage = new Inbox(pMailAccount);
			this.iConfigurationPage = new Configuration(pMailAccount);
		}

		private void NavigateToInboxPage() {
			this.fPageNavigation.Navigate(this.iInboxPage);
		}
	}
}
