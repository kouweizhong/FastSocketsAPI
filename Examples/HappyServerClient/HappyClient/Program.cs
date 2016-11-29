using System.Net;
using HappyLib.Packets;
using FastSockets.Networking;
using System.Collections.Generic;
using System;

namespace HappyClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client c = new Client();

            // subscribe to our event
            c.OnMessagePacketReceived += OnMessagePacketReceived;

            c.Connect(new IPEndPoint(IPAddress.Loopback, 12341), 12340);

            PacketDesc_Message msg = new PacketDesc_Message();
            msg.PacketTarget = EConnectionType.SECTOR_SERVER;
            msg.Message = "I like Trains!";
            c.SendPacketToParent(msg);

            while (c.IsConnected())
            {
                c.Update();
            }
            c.Shutdown();
        }

        static void OnMessagePacketReceived(KeyValuePair<ClientConnection, object> conPkt)
        {
            Console.WriteLine("The Message was received, and the event called!");
        }
    }
}
