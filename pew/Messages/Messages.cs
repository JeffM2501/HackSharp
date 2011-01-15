using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;

namespace Messages
{
    public class MessageVersion
    {
        public static int Version = 1;
    }

    public class RootHost
    {
        public static string AuthURL = "http://www.awesomelaser.com/gauth/?";
        public static string ListURL = "http://www.awesomelaser.com/gauth/list.php?";
        public static string NewsURL = "http://www.awesomelaser.com/gauth/news.php?";
    }

    public class MessagePacker
    {
        public static NetBuffer PackName(int name)
        {
            NetBuffer buffer = new NetBuffer();
            buffer.Write(name);
            return buffer;
        }
    }

    public class MessageNames
    {
        public static int INVALID = 0;
        public static int HELLO = 1;
        public static int AUTH = 2;
    }

    public class MessageBuffer
    {
        public int Name = MessageNames.INVALID;

        public virtual NetBuffer Pack()
        {
            NetBuffer buffer = new NetBuffer();
            buffer.Write(Name);
            return buffer;
        }

        public virtual bool Unpack( NetBuffer buffer)
        {
            return true;
        }
    }

    public class AuthMessage : MessageBuffer
    {
        public int ID = -1;
        public int Token = -1;

        public AuthMessage()
        {
            Name = MessageNames.AUTH;
        }

        public override NetBuffer Pack()
        {
            NetBuffer buffer = base.Pack();
            buffer.Write(ID);
            buffer.Write(Token);
            return buffer;
        }

        public override bool Unpack(NetBuffer buffer)
        {
            if (!base.Unpack(buffer))
                return false;

            ID = buffer.ReadInt32();
            Token = buffer.ReadInt32();

            return true;
        }
    }
}
