using System;
using System.Collections.Generic;

namespace MailClient.Shared
{
	public class Tag : BaseEntity
	{
		/// <summary>
		/// Valor del tag
		/// </summary>
		public string Value { get; set; }
		/// <summary>
		/// Mensajes asociados al tag
		/// </summary>
		public virtual ICollection<MailMessage> Messages { get; set; }

        public Tag()
        {
            this.Messages = new HashSet<MailMessage>();
        }
	}
}
