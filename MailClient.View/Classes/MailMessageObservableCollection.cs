using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.View
{
	public class MailMessageObservableCollection: ObservableCollection<MailMessageVM>
	{
		public MailMessageObservableCollection() : base()
		{

		}
	}
}
