﻿using System;
using System.Collections.Generic;

namespace MailClient.Shared
{
	public class MailMessage : BaseEntity
	{
		/// <summary>
		/// Correos electronicos a los que se envió
		/// </summary>
		public virtual ICollection<MailAddress> To { get; set; }
		/// <summary>
		/// Correos electronicos a los que se envió con copia oculta
		/// </summary>
		public virtual ICollection<MailAddress> Bcc { get; set; }
		/// <summary>
		/// Correos electronicos a los que se envió con copia 
		/// </summary>
		public virtual ICollection<MailAddress> Cc { get; set; }
		/// <summary>
		/// Correos electronicos a los que se responder conjunto con el remitente
		/// </summary>
		public virtual ICollection<MailAddress> ReplyTo { get; set; }
		/// <summary>
		/// Adjuntos
		/// </summary>
		public virtual ICollection<Attachment> Attachments { get; set; }
		/// <summary>
		/// Lista de palabras clave del mensaje
		/// </summary>
		public virtual ICollection<Tag> Tags { get; set; }

		/// <summary>
		/// Correo electronico del remitente
		/// </summary>
		public virtual MailAddress From { get; set; }
		/// <summary>
		/// Cuenta de correo del usuario (si existe)
		/// </summary>
		public virtual MailAccount MailAccount { get; set; }

		/// <summary>
		/// Id externo del mensaje
		/// </summary>
		public string ExternalId { get; set; }
		/// <summary>
		/// Cuerpo del mensaje
		/// </summary>
		public string Body { get; set; }
		/// <summary>
		/// Asunto del mensaje
		/// </summary>
		public string Subject { get; set; }

	}
}