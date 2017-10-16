﻿using LocalCommons.Native.Network;
using LocalCommons.Native.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAgeLogin.ArcheAge.Network
{
    /// <summary>
    /// Sends Information About That Login Was right and we can continue =)
    /// </summary>
    public sealed class NP_AcceptLogin : NetPacket
    {
        public NP_AcceptLogin() : base(0x00, true)
        {
            ns.Write((short)0x01);
            ns.Write((byte)0x00);
            ns.Write((short)0x0402); 
            ns.Write((short)0x14);
            ns.Write((int)0x00);
        }
    }

    /// <summary>
    /// Sends Request To Specified Game Server by Entered Information
    /// </summary>
    public sealed class NP_SendGameAuthorization : NetPacket
    {
        public NP_SendGameAuthorization(GameServer server, int m_AccountId) : base(0x0A, true)
        {
            string[] ipArray = server.IPAddress.Split('.');

            if (ipArray.Length == 4)
            {
                //未知
                ns.Write((byte)0x01);
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);
                ns.Write((byte)0x7e);
                //ns.Write((byte)0x5c);//未知
                //ns.Write((byte)0x4b);//未知
                //ns.Write((byte)0xe8);//未知
                //ns.Write((byte)0xf6);//未知
                //for (int i = 3; i<ipArray.Length;i--)
                //{
                //    ns.Write((byte)Convert.ToInt32(ipArray[i]));
                //}
                //主地址
                for (int i = 3; i > -1; i--)
                {
                    var cd = Convert.ToInt32(ipArray[i].ToString());
                    ns.Write((byte)Convert.ToInt32(ipArray[i].ToString()));
                }
            }
            else
            {
                //未知
                //ns.Write((byte)0x5c);//未知
                //ns.Write((byte)0x4b);//未知
                //ns.Write((byte)0xe8);//未知
                //ns.Write((byte)0xf6);//未知
                ns.Write((byte)0x01);
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);
                ns.Write((byte)0x7e);
                //主地址
                ns.Write((byte)0x01);
                ns.Write((byte)0x00);
                ns.Write((byte)0x00);
                ns.Write((byte)0x7e);
            }
            //ns.Write((int)m_AccountId);
            //ns.WriteASCIIFixedNoSize(server.IPAddress, server.IPAddress.Length);
            ns.Write((short)server.Port);
            ns.Write((short)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
        }
    }

    /// <summary>
    /// Sends Information About Current Servers To Client.
    /// 关于服务器信息发送给客户端
    /// </summary>
    public sealed class NP_ServerList : NetPacket
    {
        /// <summary>
        /// 发送服务器列表
        /// </summary>
        public NP_ServerList() : base(0x08, true)
        {
            List<GameServer> m_Current = GameServerController.CurrentGameServers.Values.ToList<GameServer>();

            //写入服务器数量
            ns.Write((byte)m_Current.Count);
            foreach (GameServer server in m_Current)
            {
                ns.Write((byte)server.Id);
                ns.Write((short)0x00);
                ns.WriteASCIIFixed(server.Name, server.Name.Length);
                byte online = server.IsOnline() ? (byte)0x01 : (byte)0x02; //1在线 2离线
                ns.Write((byte)online); //Server Status - 0x00 
                int status = server.CurrentAuthorized.Count >= server.MaxPlayers ? 0x01 : 0x00; 
                ns.Write((int)status); //Server Status - 0x00 - 正常 / 0x01 - 负载 / 0x02 - 排队
                ns.Write((int)0x00); //Undefined
                ns.Write((short)0x00); //Undefined
            }

            //写入当前用户账号数
            ns.Write((byte)0x00);
            /*
            ns.Write((byte)0x01); //Last Server Id?
            ns.Write((short)0x288C); //Current Users???
            ns.Write((short)0x22); //Undefined
            ns.Write((short)0x174); //Undefined
            ns.Write((short)0x3DEF); //Undefined
            ns.Write((byte)0x00); //Undefined
            
            //String? CharName? Probably Last Character.
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);
            
            //Undefined
            ns.Write((byte)0x01);
            ns.Write((byte)0x02);

            //String? 
            //Undefined //Ten Char String Undefined
            ns.WriteASCIIFixed("Raphael", "Raphael".Length);
            ns.Write((int)0x00); //undefined
            ns.Write((int)0x00); //undefined
            */
        }
    }

    /// <summary>
    /// Sends Information about that Password Were Correct
    /// 发送账号密码正确的信息
    /// If Not - Send NP_FailLogin.
    /// 如果没有错误的话
    /// </summary>
    public sealed class NP_PasswordCorrect : NetPacket
    {
        public NP_PasswordCorrect(int sessionId) : base(0x03, true)
        {
            //ns.Write((int)sessionId);
            ns.Write((short)0x755c);
            ns.Write((byte)0x1a);
            ns.Write((int)0x00);
            ns.Write((int)0x00);
            //string encrypted = "f3d02d5dda564e7bb4320de5b27f5c78";
            //ns.WriteASCIIFixed("\u", '\u'.Length);
        }
    }

    /// <summary>
    /// Sends Information About Rijndael(AES) Key
    /// 发送信息加密（AES）的管件
    /// </summary>
    public sealed class NP_AESKey : NetPacket
    {
        public NP_AESKey() : base(0x04, true)
        {
            //Rijndael / SHA256
            ns.Write((int)5000); //Undefined? 5000
            //le - string
            ns.WriteASCIIFixed("xnDekI2enmWuAvwL", 16); //Always 16?
            byte[] b = new byte[32];
            ns.Write(b, 0, b.Length);
        }
    }

    /// <summary>
    /// Sends Message Box About That Error Occured While Logging In.
    /// 发送信息框关于登陆错误的问题
    /// </summary>
    public sealed class NP_FailLogin : NetPacket
    {
        public NP_FailLogin() : base(0x0C, true)
        {
            ns.Write((byte)0x02); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x00);//Undefined
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class NP_EditMessage : NetPacket
    {
        public NP_EditMessage() : base(0x0C, true)
        {
            ns.Write((short)0x0d); // Reason
        }
    }
    
    /// <summary>
    /// 重复登陆
    /// </summary>
    public sealed class NP_DuplicateLogin:NetPacket
    {
        public NP_DuplicateLogin() : base(0x07, true)
        {
            ns.Write((short)0x0c); // Reason
            ns.Write((short)0x00);//Undefined
            ns.Write((short)0x03);//Undefined
        }

    }
}
