using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	public class MailAccount : BaseEntity
	{
		/// <summary>
		/// Nombre identificatorio de la cuenta del usuario
		/// </summary>
		public string Alias { get; set; }
		/// <summary>
		/// Contraseña de la cuenta del usuario
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		/// Correo electronico asociado a la cuenta del usuario
		/// </summary>
		public virtual MailAddress MailAddress { get; set; }
		/// <summary>
		/// Información de si el usuario dio de baja la cuenta (no se elimina, se da de baja)
		/// </summary>
		public bool Deleted { get; set; }
		/// <summary>
		/// Carpeta de descarga asociada a la cuenta del usuario
		/// </summary>
		public string DownloadsFolder { get; set; }

		/// <summary>
		/// Devuelve el host (ej: gmail, yahoo) de la cuenta del usuario
		/// </summary>
		/// <returns></returns>

		public string GetMailServiceHost()
		{
			return new System.Net.Mail.MailAddress(this.MailAddress.Value).Host.Split('.').First().ToLower();
		}
	}
}
