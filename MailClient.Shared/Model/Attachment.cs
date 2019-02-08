using System;
using System.Collections.Generic;

namespace MailClient.Shared
{
	/// <summary>
	/// Modelo de archivo adjunto
	/// </summary>
	public class Attachment : BaseEntity
	{
		/// <summary>
		/// Nombre del archivo junto a su extensión (no es el nombre completo)
		/// </summary>
		public string FileName { get; set; }
		
		/// <summary>
		/// Mensaje al cual está asociado
		/// </summary>
		public virtual MailMessage Message { get; set; }

	}
}
