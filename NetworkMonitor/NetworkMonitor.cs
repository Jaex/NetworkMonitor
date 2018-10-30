#region License Information (GPL v3)

/*
    Copyright (c) Jaex

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;

namespace NetworkMonitor
{
    public class NetworkMonitor
    {
        public delegate void NetworkStatusEventHandler(bool isConnected, DateTime date);
        public event NetworkStatusEventHandler NetworkStatusChanged;

        public bool IsMonitoring { get; private set; }
        public bool IsConnected { get; private set; }
        public int DisconnectCount { get; private set; }

        public int FailThreshold { get; set; } = 4;
        public string[] PingAddresses { get; set; } = new string[] { "8.8.8.8", "8.8.4.4", "1.1.1.1", "1.0.0.1" };
        public int PingInterval { get; set; } = 1000;
        public int PingTimeout { get; set; } = 4000;
        public int TotalTimeoutTime => PingTimeout + ((FailThreshold - 1) * PingInterval);

        private int failCount;
        private bool isFirstEvent = true;
        private DateTime firstFailDate;
        private int taskIndex;
        private int pingAddressIndex;
        private Timer monitorTimer;
        private object initialLock = new object();
        private object monitorLock = new object();

        public void Start()
        {
            if (!IsMonitoring)
            {
                IsMonitoring = true;

                if (monitorTimer != null)
                {
                    monitorTimer.Dispose();
                }

                monitorTimer = new Timer(state => CheckNetworkStatus(), null, 0, PingInterval);
            }
        }

        public void Stop()
        {
            if (IsMonitoring)
            {
                monitorTimer.Dispose();
                monitorTimer = null;

                IsMonitoring = false;
            }
        }

        private bool CheckNetworkStatus()
        {
            int index;
            string address;

            lock (initialLock)
            {
                taskIndex++;
                index = taskIndex;
                if (pingAddressIndex >= PingAddresses.Length) pingAddressIndex = 0;
                address = PingAddresses[pingAddressIndex];
                pingAddressIndex++;
            }

            DebugWriteLine(index, $"Sending ping to {address}");

            PingReply reply = SendPing(address, PingTimeout);
            bool result = reply != null && reply.Status == IPStatus.Success;

            lock (monitorLock)
            {
                if (result)
                {
                    DebugWriteLine(index, $"Result: {reply.RoundtripTime}ms");

                    failCount = 0;

                    if (!IsConnected)
                    {
                        IsConnected = true;

                        if (isFirstEvent)
                        {
                            isFirstEvent = false;
                        }
                        else
                        {
                            OnNetworkStatusChanged(IsConnected, DateTime.Now);
                        }
                    }
                }
                else
                {
                    DebugWriteLine(index, "Result: Timeout");

                    failCount++;

                    if (IsConnected)
                    {
                        if (failCount == 1)
                        {
                            firstFailDate = DateTime.Now;
                        }

                        if (failCount >= FailThreshold)
                        {
                            IsConnected = false;
                            DisconnectCount++;
                            OnNetworkStatusChanged(IsConnected, firstFailDate);
                        }
                    }
                }
            }

            return result;
        }

        protected void OnNetworkStatusChanged(bool isConnected, DateTime date)
        {
            if (NetworkStatusChanged != null)
            {
                NetworkStatusChanged(isConnected, date);
            }
        }

        private PingReply SendPing(string address, int timeout)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    return ping.Send(address, timeout);
                }
            }
            catch
            {
            }

            return null;
        }

        private void DebugWriteLine(int index, string text)
        {
            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - [{index}] {text}");
        }
    }
}