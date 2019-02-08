using System;
using System.IO;
using MailClient.Core;
using MailClient.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailClient.Test
{
    [TestClass]
    public class UTSerializer
    {
        [TestMethod]
        public void SerializeAndDeserialize()
        {
            string mServiceFileName = Directory.GetCurrentDirectory() + "\\services.xml";
            MailServiceCollection mSerializableObject = new MailServiceCollection();
            mSerializableObject.MailServices = new MailService[]
                {
                    new MailService()
                    {
                        Name = "gmail",
                        Protocols = new Protocol[]
                        {
                            new Protocol()
                            {
                                Name = "smtp",
                                Host = "smtp.gmail.com",
                                Port = 587,
                                SSL = true

                            },
                            new Protocol()
                            {
                                Name = "pop3",
                                Host = "pop.gmail.com",
                                Port = 995,
                                SSL = true
                            }

                        }
                    },
                    new MailService()
                    {
                        Name = "yahoo",
                         Protocols = new Protocol[]
                        {
                            new Protocol()
                            {
                                Name = "smtp",
                                Host = "smtp.mail.yahoo.com",
                                Port = 587,
                                SSL = true

                            },
                            new Protocol()
                            {
                                Name = "pop3",
                                Host = "pop.mail.yahoo.com",
                                Port = 995,
                                SSL = true
                            }

                        }
                    }
                };
            Serializer.Instance.WriteToFile(mSerializableObject, mServiceFileName);
            MailServiceCollection mDeserializedObject = Serializer.Instance.ReadFromFile<MailServiceCollection>(mServiceFileName);
            Assert.ReferenceEquals(mSerializableObject, mDeserializedObject);
        }
    }
}
