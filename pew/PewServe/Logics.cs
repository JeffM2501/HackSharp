using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hosts;
using Lidgren.Network;
using Messages;

namespace PewServe
{
    enum PlayerStatus
    {
        New,
        Connecting,
        Connected,
        Playing,
        Limboed,
        Disconnecting,
        Disconnected,
    }

    class ServerPlayer
    {
        public class Credentials
        {
            public int UID = -1;
            public int Token = -1;
            public string Name = string.Empty;
        }

        Credentials AuthInfo = new Credentials();

        Host NetHost;
        NetConnection Connection;

        public PlayerStatus Status = PlayerStatus.New;

        public ServerPlayer(NetConnection con, Host host)
        {
            NetHost = host;
            Connection = con;
        }

        public void Send(NetBuffer message, NetChannel channel)
        {
            NetHost.SendMessage(Connection, message, channel);
        }

        public void Remove()
        {
            if (NetHost == null)
                return;

            NetHost.DisconnectUser(Connection);
            Connection = null;
            NetHost = null;
            Status = PlayerStatus.Disconnected;
        }
    }

    partial class Server
    {
        void InitHandlers()
        {
            AddHandler(PlayerStatus.New,MessageNames.AUTH,Auth);
        }

        void StartPlayer( ServerPlayer player )
        {
            player.Send(MessagePacker.PackName(MessageNames.HELLO), NetChannel.ReliableInOrder1);
        }

        void EndPlayer( ServerPlayer player )
        {

        }

        void Auth(Message msg, ServerPlayer player)
        {
            AuthMessage m = new AuthMessage();
            m.Unpack(msg.Data);

            player.Status = PlayerStatus.Connecting;
        }
    }
}
