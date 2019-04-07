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
using ToastNotifications.Messages;

namespace MailClient.View
{
	/// <summary>
	/// Interaction logic for AccountWindow.xaml
	/// </summary>
	public partial class AccountWindow : Window
	{
		private Inbox iInboxPage;

		public AccountWindow(MailAccount pMailAccount)
		{
			InitializeComponent();

            try
            {
                this.iInboxPage = new Inbox(pMailAccount);
                this.FramePageNavigation.Navigate(this.iInboxPage);
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }

		}
	}
}
