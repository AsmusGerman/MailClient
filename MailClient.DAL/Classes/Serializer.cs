using System.IO;
using System.Xml.Serialization;

namespace MailClient.DAL
{
	public static class Serializer
	{
		public static void WriteToFile<T>(T pSerializableObject, string pFileName) where T : class
		{
			XmlSerializer mSerializer = new XmlSerializer(typeof(T));
			TextWriter mWriter = new StreamWriter(pFileName);
			mSerializer.Serialize(mWriter, pSerializableObject);
			mWriter.Close();
		}
		public static T ReadFromFile<T>(string pFileName) where T : class
		{
			XmlSerializer mSerializer = new XmlSerializer(typeof(T));
			FileStream mFileStream = new FileStream(pFileName, FileMode.Open);
			return (T)mSerializer.Deserialize(mFileStream);
		}
	}
}
