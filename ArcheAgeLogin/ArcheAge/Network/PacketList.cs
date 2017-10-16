using ArcheAgeLogin.ArcheAge.Holders;
using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;
using LocalCommons.Native.Logging;
using LocalCommons.Native.Network;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Packet List That Contains All Game / Client Packet Delegates.
    /// </summary>
    public static class PacketList
    {
        private static int m_Maintained;
        private static PacketHandler<GameConnection>[] m_GHandlers;
        private static PacketHandler<ArcheAgeConnection>[] m_LHandlers;

        public static PacketHandler<GameConnection>[] GHandlers
        {
            get { return m_GHandlers; }
        }

        public static PacketHandler<ArcheAgeConnection>[] LHandlers
        {
            get { return m_LHandlers; }
        }

        public static void Initialize()
        {
            m_GHandlers = new PacketHandler<GameConnection>[0x20];
            m_LHandlers = new PacketHandler<ArcheAgeConnection>[0x30];

            Registration();
        }

        private static void Registration()
        {
            //Game Server Packets
            Register(0x00, new OnPacketReceive<GameConnection>(Handle_RegisterGameServer));//Level registration server
            //Register(0x00, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected));//等级注册服务器
            Register(0x02, new OnPacketReceive<GameConnection>(Handle_UpdateCharacters));//Level update server

            //Client Packets
            Register(0x01, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //Account login service
            Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn_Continue)); //Login verification service
            Register(0x06, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn_Continue)); //登陆验证服务
            //Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_SignIn)); //Boarding landing service
            //Register(0x05, new OnPacketReceive<ArcheAgeConnection>(Handle_05));//Login verification
            Register(0x0c, new OnPacketReceive<ArcheAgeConnection>(Handle_RequestServerList)); //Return to the server list
            //Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_0d));//Returns the server address based on the server id //0b 00 0d 00 00 00 00 00 00 00 00 36（36 may be server id）
            Register(0x0d, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected));//Returns the server address based on the server id

            //Register(0x08, new OnPacketReceive<ArcheAgeConnection>(Handle_RequestServerList)); //Return to the server list
            //Register(0x09, new OnPacketReceive<ArcheAgeConnection>(Handle_ServerSelected)); //Server query
        }


        #region Game Server Delegates

        private static void Handle_UpdateCharacters(GameConnection net, PacketReader reader)
        {
            int accountId = reader.ReadInt32();
            int characters = reader.ReadInt32();

            Account currentAc = AccountHolder.AccountList.FirstOrDefault(n => n.AccountId == accountId);
            currentAc.Characters = characters;
        }

        private static void Handle_RegisterGameServer(GameConnection net, PacketReader reader)
        {
            byte id = reader.ReadByte();
            short port = reader.ReadInt16();

            string ip = reader.ReadDynamicString();
            string password = reader.ReadDynamicString();

            bool success = GameServerController.RegisterGameServer(id, password, net, port, ip);
            net.SendAsync(new NET_GameRegistrationResult(success));
        }

        #endregion

        #region Client Delegates
        private static void Handle_SignIn(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset += 10; //Static Data - 0A 00 00 00 07 00 00 00 00 00 

            string m_RLogin = reader.ReadStringSafe(reader.ReadLEInt16()); //Reading Login

            Account n_Current = AccountHolder.AccountList.FirstOrDefault(n => n.Name == m_RLogin);
            if (n_Current == null)
            {
                //Make New Temporary
                if (Settings.Default.Account_AutoCreation)
                {
                    Account m_New = new Account();
                    m_New.AccountId = AccountHolder.AccountList.Count + 1;
                    m_New.LastEnteredTime = Utility.CurrentTimeMilliseconds();
                    m_New.AccessLevel = 0;
                    m_New.LastIp = net.ToString();
                    m_New.Membership = 0;
                    m_New.Name = m_RLogin;
                    net.CurrentAccount = m_New;
                    AccountHolder.AccountList.Add(m_New);
                }
                else
                    net.CurrentAccount = null;
            }
            else
            {
                net.CurrentAccount = n_Current;
            }
            net.SendAsync(new NP_AcceptLogin());

        }

        private static void Handle_SignIn_Continue(ArcheAgeConnection net, PacketReader reader)
        {
            //HOW TO DECRYPT IT ????
            //string password = "";
            //如果账户未空，登陆失败
            if (net.CurrentAccount == null)
            {
                //返回登陆失败信息
                net.SendAsync(new NP_FailLogin());
                return;
            }

            /* TODO
            if (net.CurrentAccount.Password == null)
            {
                //Means - New Account.
                net.CurrentAccount.Password = password;
            }
            else
            {
                //Checking Password
                if (net.CurrentAccount.Password != password)
                {
                    net.SendAsync(new NP_FailLogin());
                    return;
                }
            }
            */
            net.SendAsync(new NP_AcceptLogin());
            net.CurrentAccount.Session = net.GetHashCode();
            net.SendAsync(new NP_PasswordCorrect(net.CurrentAccount.Session));
            Logger.Trace("账户登陆: " + net.CurrentAccount.Name);
            GameServerController.AuthorizedAccounts.Add(net.CurrentAccount.AccountId, net.CurrentAccount);
        }

        //发送服务器列表（基于抓包）
        private static void Handle_05(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsyncHex(new NP_PasswordCorrect(1));
        }

        //返回服务器连接成数据包
        private static void Handle_0d(ArcheAgeConnection net, PacketReader reader)
        {
            net.SendAsync0d(new NP_PasswordCorrect(1));
            //net.SendAsync(new NP_ServerList());
        }

        private static void Handle_RequestServerList(ArcheAgeConnection net, PacketReader reader)
        {
            byte[] unknown = reader.ReadByteArray(8); //unk?
            net.SendAsync(new NP_ServerList());
        }

        private static void Handle_ServerSelected(ArcheAgeConnection net, PacketReader reader)
        {
            reader.Offset += 10; //00 00 00 00 00 00 00 00  Undefined Data
            byte serverId = reader.ReadByte();
            GameServer server = GameServerController.CurrentGameServers.FirstOrDefault(n => n.Value.Id == serverId).Value;
            if (server.CurrentConnection != null)
            {
                if (GameServerController.AuthorizedAccounts.ContainsKey(net.CurrentAccount.AccountId))
                {
                    net.movedToGame = true;
                    GameServerController.AuthorizedAccounts.Remove(net.CurrentAccount.AccountId);
                    server.CurrentConnection.SendAsync(new NET_AccountInfo(net.CurrentAccount));
                    net.SendAsync(new NP_SendGameAuthorization(server, net.CurrentAccount.AccountId));
                    server.CurrentAuthorized.Add(net.CurrentAccount.AccountId);
                }
            }
            else
                net.Dispose();
        }

        #endregion

        private static void Register(short opcode, OnPacketReceive<ArcheAgeConnection> e)
        {
            m_LHandlers[opcode] = new PacketHandler<ArcheAgeConnection>(opcode, e);
            m_Maintained++;
        }

        private static void Register(short opcode, OnPacketReceive<GameConnection> e)
        {
            m_GHandlers[opcode] = new PacketHandler<GameConnection>(opcode, e);
            m_Maintained++;
        }
    }
}
