using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	public interface IProgressToken
	{
		/// <summary>
		/// progreso [0 .. 100]
		/// </summary>
		int Status { get; }

		/// <summary>
		/// estado actual
		/// </summary>
		ProgressTokenState State { get; }

		/// <summary>
		/// añade pStep al progreso y si termina devuelve el tiempo ejecutado
		/// </summary>
		/// <param name="pStep"></param>
		TimeSpan GoFoward(int pStep = 1);

		/// <summary>
		/// cancela el progreso y devuelve el tiempo ejecutado
		/// </summary>
		/// <returns></returns>
		TimeSpan Cancel();
	}
}
