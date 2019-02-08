using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	public class ProgressToken : IProgressToken
	{
		private int iTotalElements;
		private int iActualElements;
		private LifeTimer iLifeTimer;

		public ProgressTokenState State { get; private set; }

		public int Status { 
			get {	return this.iActualElements * 100 / this.iTotalElements;	}
		}

		public ProgressToken(int pTotalElements)
		{
			this.State = ProgressTokenState.Off;
			this.iTotalElements = pTotalElements;
			this.iActualElements = 0;
			this.iLifeTimer = new LifeTimer();
		}

		public TimeSpan GoFoward(int pStep = 1)
		{
			
			//solo si está encendido, sumar pStep
			if (this.State == ProgressTokenState.On)
			{
				if (this.State == 0)
					if (this.iLifeTimer.ElapsedTime.Ticks == 0)
						this.iLifeTimer.tic();

				this.iActualElements += pStep;
				//si se completó, terminar
				if (this.Status == 100)
				{
					this.State = ProgressTokenState.Ready;
					this.iLifeTimer.toc();
				}
			}
			return this.iLifeTimer.ElapsedTime;
		}

		public TimeSpan Cancel()
		{
			//solo si está encendido, se cancela
			if (this.State == ProgressTokenState.On)
			{
				this.State = ProgressTokenState.Canceled;
				this.iLifeTimer.toc();
			}

			return this.iLifeTimer.ElapsedTime;
		}
	}
}
