using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Hosts;
using Lidgren.Network;

namespace PewServe
{
    partial class Server
    {
        Host NetHost;

        public delegate void MessageHandler(Message msg, ServerPlayer player);

        Dictionary<PlayerStatus,Dictionary<int, MessageHandler> > MessageHandlers = new Dictionary<PlayerStatus,Dictionary<int, MessageHandler>>();
        Dictionary<NetConnection, ServerPlayer> Players = new Dictionary<NetConnection, ServerPlayer>();

        public Server(int port)
        {
            NetHost = new Host(port);
        }

        public void Stop()
        {
            NetHost.Kill();
        }

        protected void AddHandler(PlayerStatus status, int code, MessageHandler handler)
        {
            if (!MessageHandlers.ContainsKey(status))
                MessageHandlers.Add(status,new Dictionary<int, MessageHandler>());

            if (MessageHandlers[status].ContainsKey(code))
                MessageHandlers[status][code] = handler;
            else
                MessageHandlers[status].Add(code, handler);
        }

        public void Broadcast(NetBuffer msg, NetChannel channel)
        {
            NetHost.Broadcast(msg, channel);
        }

        public void Update()
        {
            NetConnection newConnect = NetHost.GetPentConnection();
            while (newConnect != null)
            {
                PlayerConnect(newConnect);
                newConnect = NetHost.GetPentConnection();
            }

            NetConnection newDisconnect = NetHost.GetPentDisconnection();
            while (newDisconnect != null)
            {
                PlayerDisconnect(newConnect);
                newDisconnect = NetHost.GetPentDisconnection();
            }

            Message msg = NetHost.GetPentMessage();
            while (msg != null)
            {
                if (Players.ContainsKey(msg.Sender))
                {
                    ServerPlayer player = Players[msg.Sender];

                    if (MessageHandlers.ContainsKey(player.Status))
                    {
                        if (MessageHandlers[player.Status].ContainsKey(msg.Name))
                            MessageHandlers[player.Status][msg.Name](msg, player);
                        else
                        {
                            //out of band message
                        }
                    }
                    else
                    {
                        // empty status
                    }
                }
                msg = NetHost.GetPentMessage();
            }

            // logic think
        }

        void PlayerConnect(NetConnection con)
        {
            if (Players.ContainsKey(con))
            {
                // wtf?
                return;
            }

            Players.Add(con, new ServerPlayer(con, NetHost));
            StartPlayer(Players[con]);
        }

        void PlayerDisconnect(NetConnection con)
        {
            if (!Players.ContainsKey(con))
            {
                // wtf?
                return;
            }

            ServerPlayer player = Players[con];
            player.Remove();
            Players.Remove(con);
            EndPlayer(player);
        }
    }
}
