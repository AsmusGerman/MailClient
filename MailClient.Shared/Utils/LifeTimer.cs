using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MailClient.Shared.Exceptions;

namespace MailClient.Shared
{
	/// <summary>
	/// maneja el tiempo de ejecución de algún proceso
	/// </summary>
	public class LifeTimer
	{
		private Stopwatch iWatch;

		/// <summary>
		/// valor de la instancia una vez ejecutado
		/// </summary>
		public TimeSpan ElapsedTime { 
			get {
				//si el reloj está detenido
				if (!this.iWatch.IsRunning)
					return this.iWatch.Elapsed;
				else
					return new TimeSpan(0);
			}
		}

		/// <summary>
		/// constructor
		/// </summary>
		public LifeTimer()
		{
			this.iWatch = Stopwatch.StartNew();
		}

		/// <summary>
		/// empieza a contar
		/// </summary>
		public void tic() {
			this.iWatch.Start();
		}

		/// <summary>
		/// deja de contar
		/// </summary>
		public void toc() {
			if (this.iWatch.IsRunning)
				this.iWatch.Stop();
		}
	}
}
