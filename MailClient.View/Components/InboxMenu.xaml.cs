using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MailClient.View.Components
{
	/// <summary>
	/// Interaction logic for InboxMenu.xaml
	/// </summary>
	public partial class InboxMenu : UserControl
	{
        public event EventHandler SendedMailMessages;
        public event EventHandler ReceivedMailMessages;
        public event EventHandler DraftMailMessages;

        public InboxMenu()
		{
			InitializeComponent();
		}

        private void BtnSendedMailMessages_Click(object sender, RoutedEventArgs e)
        {
            this.SendedMailMessages?.Invoke(this, e);

        }

        private void BtnReceivedMailMessages_Click(object sender, RoutedEventArgs e)
        {
            this.ReceivedMailMessages?.Invoke(this, e);
        }

        private void BtnDraftMailMessages_Click(object sender, RoutedEventArgs e)
        {
            this.DraftMailMessages?.Invoke(this, e);
        }
    }
}
