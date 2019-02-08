using System;
using System.Collections.Generic;

namespace MailClient.Shared
{
	[Serializable]
	public class MailService
	{
        /// <summary>
        /// nombre del servicio de correo
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// colección de los protocolos definidos
        /// </summary>
        public Protocol[] Protocols { get; set; }
	}
}
