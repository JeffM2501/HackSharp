/*
    Open Combat/Projekt 2501
    Copyright (C) 2010  Jeffery Allen Myers

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

using Lidgren.Network;
using System.Threading;


namespace Clients
{
    public class CryptoClient : Client
    {
        static int RSA_KEY_MESSAGE = 1;
        static int CRYPTO_SECRET_MESSAGE = 2;
        static int CRYPTO_SECRET_VERIFY = 3;
        static int CRYPTO_ACCEPT = 4;
     //   static int CRYPTO_DENY = 5;

        public CryptoClient() : base()
        {
        }

        public CryptoClient(string _host, int _port) : base(_host,_port)
        {
        }

        protected enum CryptoHostState
        {
            Connected,
            SentSecret,
            SentVerify,
            Authenticated,
            Invalid,
        }

        protected CryptoHostState cryptoState = CryptoHostState.Invalid;

        protected AesManaged aes;

        protected RSAParameters RSAKey;
        protected RSACryptoServiceProvider RSA;

        private byte[] MakeSecret()
        {
            byte[] secret = new byte[16];
            new Random().NextBytes(secret);
            aes = new AesManaged();
            aes.Key = secret;
            aes.IV = secret;

            return secret;
        }

        private byte[] EncryptBuffer(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream encrypt = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    encrypt.Write(buffer, 0, buffer.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();
                    return ms.ToArray();
                }
            }
        }

        private byte[] DecryptBuffer(byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream decrypt = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    decrypt.Write(buffer, 0, buffer.Length);
                    decrypt.FlushFinalBlock();
                    decrypt.Close();
                    return ms.ToArray();
                }
            }
        }

        protected override void Connected(NetBuffer data)
        {
            cryptoState = CryptoHostState.Connected;
        }

        protected override NetBuffer ProcessInboundMessage(NetBuffer message)
        {
            switch(cryptoState)
            {
                case CryptoHostState.Connected:
                    {
                        int code = message.ReadInt32();
                        if (code == RSA_KEY_MESSAGE)
                        {
                            XmlSerializer s = new XmlSerializer(typeof(RSAParameters));
                            string b = message.ReadString();
                            StringReader reader = new StringReader(b);
                            RSAKey = (RSAParameters)s.Deserialize(reader);
                            RSA = new RSACryptoServiceProvider();
                            RSA.ImportParameters(RSAKey);

                            NetBuffer msg = new NetBuffer();
                            msg.Write((Int32)CRYPTO_SECRET_MESSAGE);

                            byte[] secret = RSA.Encrypt(MakeSecret(),false);
                            msg.Write((Int32)secret.Length);
                            msg.Write(secret);
                            client.SendMessage(msg,NetChannel.ReliableInOrder1);
                            cryptoState = CryptoHostState.SentSecret;
                        }
                        else
                        {
                            cryptoState = CryptoHostState.Invalid;
                            client.Disconnect("Bad Crypto");
                        }
                    }
                    return null;

                case CryptoHostState.SentSecret:
                    {
                        int code = message.ReadInt32();
                        if (code == CRYPTO_SECRET_VERIFY)
                        {
                            // set em as real and let the base class call any events it needs to
                            string verify = new UTF8Encoding().GetString(DecryptBuffer(message.ReadBytes(message.ReadInt32())));

                            NetBuffer b = new NetBuffer();
                            b.Write(CRYPTO_SECRET_VERIFY);
                            byte[] cryptoBuffer = EncryptBuffer(new UTF8Encoding().GetBytes(verify));
                            b.Write(cryptoBuffer.Length);
                            b.Write(cryptoBuffer);
                            client.SendMessage(b, NetChannel.ReliableInOrder1);

                            cryptoState = CryptoHostState.SentVerify;
                        }
                        else
                        {
                            cryptoState = CryptoHostState.Invalid;
                            client.Disconnect("Bad Crypto");
                        }
                    }
                   return null;

                case CryptoHostState.SentVerify:
                    {
                        int code = message.ReadInt32();
                        if (code == CRYPTO_ACCEPT)
                        {
                            cryptoState = CryptoHostState.Authenticated;
                            base.Connected(message);
                        }
                        else
                        {
                            cryptoState = CryptoHostState.Invalid;
                            client.Disconnect("Bad Crypto");
                        }
                    }
                    return null;

                case CryptoHostState.Authenticated:
                    return new NetBuffer(DecryptBuffer(message.ReadBytes(message.LengthBytes)));
            }
            return message;
        }

        protected override NetBuffer ProcessOutboundMessage(NetBuffer message)
        {
            if (cryptoState == CryptoHostState.Authenticated)
                return new NetBuffer(EncryptBuffer(message.ReadBytes(message.LengthBytes)));

            return message;
        }
    }
}
