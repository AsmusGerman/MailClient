using MailClient.Shared;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MailClient.View
{
	public partial class Inbox : Page
	{
		private MailAccount iUserAccount;
		private Configuration iConfigurationPage;

		private ObservableCollection<MailMessageVM> iSendedMailMessages;
		private ObservableCollection<MailMessageVM> iReceivedMailMessages;


		public Inbox(MailAccount pMailAccount)
		{
			InitializeComponent();


			this.iUserAccount = pMailAccount;
			this.iConfigurationPage = new Configuration(this.iUserAccount);

			this.iSendedMailMessages = new ObservableCollection<MailMessageVM>();
			this.iReceivedMailMessages = new ObservableCollection<MailMessageVM>();

			this.tbMailAddress.Text = iUserAccount.MailAddress.Value;

			this.iSendedMailMessages.CollectionChanged += SendedMailMessages_CollectionChanged;
			this.iReceivedMailMessages.CollectionChanged += iReceivedMailMessages_CollectionChanged;
		}

		private void UpdateInbox() {

			Facade.Instance.UpdateInbox(this.iUserAccount, 1);
			IEnumerable<MailMessage> mNewSendedMessages = this.iUserAccount.MailAddress.FromMessages;

			foreach (MailMessage bMessage in mNewSendedMessages)
			{
				this.iSendedMailMessages.Add(MailMessageVM.FromMailMessage(bMessage));
			}

			IEnumerable<MailMessage> mNewiReceivedMessages = this.iUserAccount.MailAddress.ToMessages;

			foreach (MailMessage bMessage in mNewiReceivedMessages)
			{
				this.iReceivedMailMessages.Add(MailMessageVM.FromMailMessage(bMessage));
			}
		}

		private void SendedMailMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			this.dtgMessages.ItemsSource = this.iSendedMailMessages;
		}

		private void iReceivedMailMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			this.dtgMessages.ItemsSource = this.iReceivedMailMessages;
		}

		private void btnUpdate_Click(object sender, RoutedEventArgs e)
		{
			this.UpdateInbox();
		}

		private void btnNewMessage_Click(object sender, RoutedEventArgs e)
		{
			//new Message(this.iUserAccount.MailAddress.Value).Show();

		}

		private void BtnConfiguration_Click(object sender, RoutedEventArgs e)
		{
			this.NavigationService.Navigate(this.iConfigurationPage);
		}
	}
}
