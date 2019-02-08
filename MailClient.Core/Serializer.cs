using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MailClient.Core
{
	public class Serializer
	{
		private static Serializer iInstance;

		private Serializer()
		{

		}

		public static Serializer Instance
		{
			get
			{
				if (iInstance == default(Serializer))
					iInstance = new Serializer();
				return iInstance;
			}
		}

		public void WriteToFile<T>(T pSerializableObject, string pFileName) where T : class
		{
			XmlSerializer mSerializer = new XmlSerializer(typeof(T));
			TextWriter mWriter = new StreamWriter(pFileName);
			mSerializer.Serialize(mWriter, pSerializableObject);
			mWriter.Close();
		}
		public T ReadFromFile<T>(string pFileName) where T : class
		{
			XmlSerializer mSerializer = new XmlSerializer(typeof(T));
			FileStream mFileStream = new FileStream(pFileName, FileMode.Open);
			return (T)mSerializer.Deserialize(mFileStream);
		}
	}
}
