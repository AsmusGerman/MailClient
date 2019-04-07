using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	public class MailAddress : BaseEntity
	{
		/// <summary>
		/// Direccion de correo
		/// </summary>
		public string Value { get; set; }
		/// <summary>
		/// Cuenta del usuario asociado a la dirección de correo
		/// </summary>
		public virtual MailAccount MailAccount { get; set; }
		/// <summary>
		/// Mensajes para los cuales forma parte como destinatario
		/// </summary>
		public virtual ICollection<MailMessage> ToMessages { get; set; }
		/// <summary>
		/// Mensajes para los cuales forma parte como remitente
		/// </summary>
		public virtual ICollection<MailMessage> FromMessages { get; set; }

        public MailAddress()
        {
            this.ToMessages = new HashSet<MailMessage>();
            this.FromMessages = new HashSet<MailMessage>();
        }
    }
}
