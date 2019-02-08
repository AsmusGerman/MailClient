using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient.Shared
{
	public enum CredentialValidationResponse
	{
		InvalidCredential,
		ValidCredentialByAlias,
		ValidCredentialByMailAddress
	}
}
