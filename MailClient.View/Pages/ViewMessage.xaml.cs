using HtmlAgilityPack;
using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MailClient.View
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class ViewMessage : Page
    {
        private MailAccount iMailAccount;
        public ViewMessage(MailAccount pMailAccount, MailMessage pMailMessage)
        {
            InitializeComponent();
            this.iMailAccount = pMailAccount;

            this.tbFromMailAddress.Text = pMailMessage.From.Value;
            this.lvToMailAddresses.ItemsSource = pMailMessage.To.Select(bMailMessage => bMailMessage.Value).ToList();
            this.tbxMailSubject.Text = pMailMessage.Subject;

            HtmlDocument mHtmlDocument = new HtmlDocument();
            mHtmlDocument.LoadHtml(pMailMessage.Body);

            HtmlNode mHtmlNodeBody = mHtmlDocument.DocumentNode.SelectSingleNode("//body");
            if(mHtmlNodeBody != null)
            {
                this.tbBody.NavigateToString(mHtmlNodeBody.OuterHtml);
            }
            else
            {
                this.tbBody.NavigateToString(pMailMessage.Body);
            }
        }

        private void BtnCloseMessage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void BtnReply_Click(object sender, RoutedEventArgs e)
        {
            MailMessage mMailMessage = new MailMessage()
            {
                Subject = this.tbxMailSubject.Text,
                To = new List<MailAddress>() { new MailAddress() { Value = this.tbFromMailAddress.Text } }
            };

            this.NavigationService.Navigate(new NewMessage(this.iMailAccount, mMailMessage));
        }

        private void BtnReplyAll_Click(object sender, RoutedEventArgs e)
        {
            MailMessage mMailMessage = new MailMessage()
            {
                Subject = this.tbxMailSubject.Text,
                To = this.lvToMailAddresses.ItemsSource
                    .Cast<string>()
                    .Concat(new string[] { this.tbFromMailAddress.Text })
                    .Select(bItem => new MailAddress() { Value = bItem })
                    .ToList()
            };

            this.NavigationService.Navigate(new NewMessage(this.iMailAccount, mMailMessage));
        }
    }
}
