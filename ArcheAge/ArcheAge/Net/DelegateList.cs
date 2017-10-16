using ArcheAge.ArcheAge.Net.Connections;
using ArcheAge.ArcheAge.Structuring;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAge.ArcheAge.Net
{
    /// <summary>
    /// Delegate List That Contains Information About Received Packets.
    /// Contains a list of delegates receiving packet information。
    /// </summary>
    public class DelegateList
    {
        private static int m_Maintained;
        private static PacketHandler<LoginConnection>[] m_LHandlers;
        private static PacketHandler<ClientConnection>[] m_CHandlers;
        private static Dictionary<int, PacketHandler<ClientConnection>[]> levels;
        private static LoginConnection m_CurrentLoginServer;

        public static LoginConnection CurrentLoginServer
        {
            get { return m_CurrentLoginServer; }
        }

        public static Dictionary<int, PacketHandler<ClientConnection>[]> ClientHandlers
        {
            get { return levels; }
        }

        public static PacketHandler<LoginConnection>[] LHandlers
        {
            get { return m_LHandlers; }
        }

        public static void Initialize()
        {
            m_LHandlers = new PacketHandler<LoginConnection>[0x20];
            //m_LHandlers = new PacketHandler<ClientConnection>[0x30];
            levels = new Dictionary<int, PacketHandler<ClientConnection>[]>();

            RegisterDelegates();
        }
        //Registration service
        private static void RegisterDelegates()
        {
            //-------------- Login - Game Communication Packets ------------
            Register(0x00, new OnPacketReceive<LoginConnection>(Handle_GameRegisterResult)); //Taken Fully
            Register(0x01, new OnPacketReceive<LoginConnection>(Handle_AccountInfoReceived)); //Taken Fully

            //-------------- Client Communication Packets ------------------
            //Client communication office
            //-------------- Using - Packet Level - Packet Opcode(short) - Receive Delegate -----


            //Register(0x01, 0x00, new OnPacketReceive<ClientConnection>(OnPacketReceive_ClientAuthorized));
            Register(0x01, 0x00, new OnPacketReceive<ClientConnection>(OnPacketReceive_Client01));
        }



        #region Client Callbacks Implementation

        //Verify the user login authority I do not know how to use, discarded
        public static void OnPacketReceive_ClientAuthorized(ClientConnection net, PacketReader reader)
        {
            //B3 04 00 00 B3 04 00 00 8C 28 22 00 E7 F0 0C C6 FF FF FF FF 00 
            long protocol = reader.ReadLEInt64(); //Protocols?
            int sessionId = reader.ReadLEInt32(); //User Session Id
            int accountId = reader.ReadLEInt32(); //Account Id
            Account m_Authorized = ClientConnection.CurrentAccounts.FirstOrDefault(kv => kv.Value.Session == sessionId && kv.Value.AccountId == accountId).Value;
            if (m_Authorized != null)
            {
                net.Dispose();
                Logger.Trace("客户端 {0} 已配置：无法播放。", net);
            }
            else
            {
                net.CurrentAccount = m_Authorized;
                net.SendAsync(new NP_ClientConnected());
            }
        }

        //When the data is 01
        public static void OnPacketReceive_Client01(ClientConnection net,PacketReader reader)
        {
            net.SendAsync(new NP_Client01());
        }

        #endregion

        #region Callbacks Implementation

        private static void Handle_AccountInfoReceived(LoginConnection net, PacketReader reader)
        {
            //Set Account Info
            Account account = new Account();
            account.AccountId = reader.ReadInt32();
            account.AccessLevel = reader.ReadByte();
            account.Membership = reader.ReadByte();
            account.Name = reader.ReadDynamicString();
            //account.Password = reader.ReadDynamicString();
            account.Session = reader.ReadInt32();
            account.LastEnteredTime = reader.ReadInt64();
            account.LastIp = reader.ReadDynamicString();

            Console.WriteLine(account.Session);
            if (ClientConnection.CurrentAccounts.ContainsKey(account.Session))
            {
                //Already
                Account acc = ClientConnection.CurrentAccounts[account.Session];
                if (acc.Connection != null)
                {
                    acc.Connection.Dispose(); //Disconenct  
                    Logger.Trace("Account (" + acc.Name + ") was forced to break");
                }
                else
                {
                    ClientConnection.CurrentAccounts.Remove(account.Session);
                }
            }
            else
            {
                Logger.Trace("Account《 {0}》: Authorization", account.Name);
                ClientConnection.CurrentAccounts.Add(account.Session, account);
            }
        }

        private static void Handle_GameRegisterResult(LoginConnection con, PacketReader reader)
        {
            bool result = reader.ReadBoolean();
            if (result)
                Logger.Trace("Successfully install the login server");
            else
                Logger.Trace("Some problems arise when installing the login server");
            if(result)
               m_CurrentLoginServer = con;
        }

        #endregion

        private static void Register(short opcode, OnPacketReceive<LoginConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<LoginConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(byte level, short opcode, OnPacketReceive<ClientConnection> e)
        {
            if (!levels.ContainsKey(level))
            {
                PacketHandler<ClientConnection>[] handlers = new PacketHandler<ClientConnection>[0xFFFF];
                handlers[opcode] = new PacketHandler<ClientConnection>(opcode, e);
                levels.Add(level, handlers);
            }
            else
            {
                levels[level][opcode] = new PacketHandler<ClientConnection>(opcode, e);
            }
        }

    }
}
