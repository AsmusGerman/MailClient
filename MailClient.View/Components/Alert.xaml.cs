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

namespace MailClient.View.Components
{
	/// <summary>
	/// Interaction logic for Alert.xaml
	/// </summary>
	public partial class Alert : UserControl
	{
		public event EventHandler CloseAlert;

		public String Header { get; set; }
		public String Body { get; set; }

		public Alert()
		{
			InitializeComponent();
		}

		private void BtnCloseDialog_Click(object sender, RoutedEventArgs e)
		{
			this.CloseAlert?.Invoke(this, e);
		}
	}
}
