using MailClient.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications.Messages;

namespace MailClient.View
{
    public partial class Inbox : Page
    {
        private MailAccount iUserAccount;
        private Configuration iConfigurationPage;
        private int iPageSize = 10;
        private int CurrentPageNumber = 1;
        private int LastPage
        {
            get
            {
                return (this.iDataGridItemsSource.Count | 0 + this.iPageSize - 1) / this.iPageSize;
            }
        }

        private ObservableCollection<MailMessage> iDataGridItemsSource;

        public Inbox(MailAccount pMailAccount)
        {
            InitializeComponent();
            this.Init(pMailAccount);
        }

        private async void Init(MailAccount pMailAccount)
        {
            this.iUserAccount = pMailAccount;
            this.iConfigurationPage = new Configuration(this.iUserAccount);
            this.iDataGridItemsSource = new ObservableCollection<MailMessage>();
            this.tbMailAddress.Text = iUserAccount.MailAddress.Value;
            await this.UpdateInbox();
        }

        private async Task UpdateInbox()
        {
            try
            {
                this.iSpinner.IsIndeterminate = true;
                await Facade.Instance.UpdateInbox(this.iUserAccount, this.iPageSize);
                Facade.Instance.Notifier.ShowSuccess("Casilla de correo actualizada");
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }
            finally
            {
                this.iSpinner.IsIndeterminate = false;
                this.UpdateDataGridSource(this.iUserAccount.MailAddress.FromMessages);
            }
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            await this.UpdateInbox();
            this.InboxMenu_SendedMailMessages(sender, e);
        }

        private void btnNewMessage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new NewMessage(this.iUserAccount));
        }

        private void BtnConfiguration_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(this.iConfigurationPage);
        }

        private void InboxMenu_SendedMailMessages(object sender, System.EventArgs e)
        {
            this.iDataGridItemsSource.Clear();
            this.CurrentPageNumber = 1;
            this.UpdateDataGridSource(this.iUserAccount.MailAddress.FromMessages);
            
        }

        private void InboxMenu_ReceivedMailMessages(object sender, System.EventArgs e)
        {
            this.iDataGridItemsSource.Clear();
            this.CurrentPageNumber = 1;
            this.UpdateDataGridSource(this.iUserAccount.MailAddress.ToMessages);
        }

        private void UpdateDataGridSource(IEnumerable<MailMessage> pSource)
        {
            pSource.ToList().ForEach(bMailMessage =>
            {
                this.iDataGridItemsSource.Add(bMailMessage);
            });
            this.ChangeCurrentPage();
        }

        private void InboxMenu_DraftMailMessages(object sender, System.EventArgs e)
        {

        }

        private void DtgMessages_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                DataGrid mDataGrid = sender as DataGrid;
                if (mDataGrid != null && mDataGrid.SelectedItem != null)
                {
                    //This is the code which helps to show the data when the row is double clicked.
                    DataGridRow bDataGridRow = mDataGrid.ItemContainerGenerator.ContainerFromItem(mDataGrid.SelectedItem) as DataGridRow;
                    int bMailMessageId = ((MailMessage)bDataGridRow.Item).Id;
                    MailMessage bMailMessage = this.iDataGridItemsSource.Single(bSendedMailMessage => bSendedMailMessage.Id == bMailMessageId);
                    if (bMailMessage == null)
                        throw new Exception();

                    this.NavigationService.Navigate(new ViewMessage(bMailMessage));
                }
            }
            catch (Exception bException)
            {
                Facade.Instance.Notifier.ShowError(bException.Message);
            }
        }

        private void ChangeCurrentPage()
        {
            this.dtgMessages.ItemsSource = this.iDataGridItemsSource.Skip((this.CurrentPageNumber - 1) * this.iPageSize).Take(this.iPageSize);
            this.tbCurrentPage.Text = this.CurrentPageNumber.ToString();
            this.tbLastPage.Text = this.LastPage.ToString();
        }

        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageNumber = 1;
            this.ChangeCurrentPage();
        }

        private void BtnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageNumber = this.CurrentPageNumber > 1 ? this.CurrentPageNumber-1 : 1;
            this.ChangeCurrentPage();
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageNumber = this.CurrentPageNumber < this.LastPage ? this.CurrentPageNumber+1 : this.LastPage;
            this.ChangeCurrentPage();
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentPageNumber = this.LastPage;
            this.ChangeCurrentPage();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.dtgMessages.ItemsSource = this.iDataGridItemsSource.Where(bItems => this.Filter(bItems, this.tbSearch.Text));
        }

        private bool Filter(MailMessage pTarget, string pQuery)
        {
            return pTarget.GetType()
            .GetProperties()
            .Any(bProperty =>
            {
                var value = bProperty.GetValue(pTarget);
                return value != null && value.ToString().Contains(pQuery);
            });
        }
    }
}
