using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.BLL.Configuration
{
	public class BLLProfile : Profile
	{

		protected BLLProfile() : base(nameof(BLLProfile))
		{
			CreateMap<Shared.MailMessage, System.Net.Mail.MailMessage>()
				.ForMember(dest => dest.From, opt => opt.MapFrom(source => new System.Net.Mail.MailAddress(source.From.Value)))
				.ForMember(dest => dest.To, opt => opt.MapFrom(source => {
					System.Net.Mail.MailAddressCollection mMailAddressCollection = new System.Net.Mail.MailAddressCollection();
					foreach (Shared.MailAddress bMailAddress in source.To)
					{
						mMailAddressCollection.Add(bMailAddress.Value);
					}
					return mMailAddressCollection;
				}))

				;
		}
	}
}
