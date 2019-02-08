using System;
using System.Text;
using System.Security.Cryptography;
using MailClient.Shared;

namespace MailClient.BLL
{
	public class DPEntryptor : IEncryptor
	{
		private static readonly byte[] iEntropy = { 6, 0, 7, 1, 5, 9 };

		public string Decrypt(string pString)
		{
			try
			{
				//convierte el texto en un array de bytes
				byte[] encryptedText = Convert.FromBase64String(pString);

				//obtiene el texto original con Unprotect
				byte[] originalText = ProtectedData.Unprotect(encryptedText, iEntropy, DataProtectionScope.CurrentUser);

				// devuelve el string resultante
				return Encoding.Unicode.GetString(originalText);
			}
			catch (Exception bException)
			{
				throw new FailOnDecrypt(Resources.Exceptions.Decrypt_FailOnDecrypt, bException);
			}

		}

		public string Encrypt(string pString)
		{
			try
			{
				// convierte el texto en un arrray de bytes
				byte[] originalText = Encoding.Unicode.GetBytes(pString);

				// encripta usando Protect
				byte[] encryptedText = ProtectedData.Protect(originalText, iEntropy, DataProtectionScope.CurrentUser);

				//devuelve el string resultante
				return Convert.ToBase64String(encryptedText);
			}
			catch (Exception bException)
			{
				throw new FailOnEncrypt(Resources.Exceptions.Encrypt_FailOnEncrypt, bException);
			}
			
		}
	}
}
