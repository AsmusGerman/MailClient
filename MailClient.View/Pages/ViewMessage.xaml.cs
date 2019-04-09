using HtmlAgilityPack;
using MailClient.Shared;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Messages;

namespace MailClient.View
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class ViewMessage : Page
    {
        private MailAccount iMailAccount;
        private MailMessage iMailMessage;
        public ViewMessage(MailAccount pMailAccount, MailMessage pMailMessage)
        {
            InitializeComponent();
            this.iMailAccount = pMailAccount;
            this.iMailMessage = pMailMessage;

            this.tbFromMailAddress.Text = pMailMessage.From.Value;
            this.lvToMailAddresses.ItemsSource = pMailMessage.To.Select(bMailMessage => bMailMessage.Value).ToList();
            this.tbxMailSubject.Text = pMailMessage.Subject;

            HtmlDocument mHtmlDocument = new HtmlDocument();
            mHtmlDocument.LoadHtml(pMailMessage.Body);

            HtmlNode mHtmlNodeBody = mHtmlDocument.DocumentNode.SelectSingleNode("//body");
            if (mHtmlNodeBody != null)
            {
                this.tbBody.NavigateToString(mHtmlNodeBody.OuterHtml);
            }
            else if(!string.IsNullOrEmpty(pMailMessage.Body))
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

        private async void BtnExportMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog mSaveFileDialog = new SaveFileDialog
                {
                    FileName = string.Join("-", this.tbFromMailAddress.Text, this.tbxMailSubject.Text)
                };

                if (mSaveFileDialog.ShowDialog() == true)
                {
                    if (mSaveFileDialog.FileName != "")
                    {
                        string mFormatedMessage = await Facade.Instance.ConvertMessageToFormat(Path.GetExtension(mSaveFileDialog.FileName).Remove(0,1), this.iMailMessage);
                        byte[] mBytes = Encoding.UTF8.GetBytes(mFormatedMessage);

                        using (FileStream mFileStream = (FileStream)mSaveFileDialog.OpenFile())
                        {
                            mFileStream.Seek(0, SeekOrigin.End);
                            await mFileStream.WriteAsync(mBytes, 0, mBytes.Length);
                        }
                    }
                }
            }
            catch (Exception bException)
            {
                AccountWindow.Notifier.ShowError(bException.Message);
            }
            
        }
    }
}
