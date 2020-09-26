﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Genie.Yuk;

namespace Genie.Win10.Utility
{
    public class Client
    {
        private String m_IPAddress;

        public Client(String IPAddress = "127.0.0.1")
        {
            m_IPAddress = IPAddress;
        }

        public void ConnectServer()
        {
            try
            {
                String server = Dns.GetHostName();
                IPAddress iIPAddress = IPAddress.Parse(m_IPAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + e.ToString());
            }
        }

        public CancellationTokenSource Handler(Object swapChainPanel, int Width, int Height)
        {
            WinUtility win = new WinUtility();

            GameGraphics gg = new GameGraphics(swapChainPanel, Width, Height);

            CancellationTokenSource source1 = new CancellationTokenSource();
            CancellationToken token1 = source1.Token;

            Action myAction = (Action)(() =>
            {
                gg.Run(token1);
            });

            Task t = win.OnUiThread(myAction);

            source1.Cancel();
            return source1;
        }
    }
}