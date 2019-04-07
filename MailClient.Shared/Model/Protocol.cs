using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	[Serializable]
	public class Protocol : IProtocol
	{
        /// <summary>
        /// nombre del protocolo
        /// </summary>
		public string Name { get; set; }
        /// <summary>
        /// dirección de host
        /// </summary>
		public string Host { get; set; }
        /// <summary>
        /// puerto por el cual se realiza la conexión
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// ssl
        /// </summary>
		public bool SSL { get; set; }

	}
}
