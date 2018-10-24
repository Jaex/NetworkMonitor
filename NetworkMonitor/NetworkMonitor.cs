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
        public string[] PingAddresses { get; set; } = new string[] { "8.8.8.8", "8.8.4.4" };
        public int PingInterval { get; set; } = 1000;
        public int PingTimeout { get; set; } = 4000;
        public int TotalTimeoutTime => PingTimeout + ((FailThreshold - 1) * PingInterval);

        private int failCount = 0;
        private bool isFirstEvent = true;
        private DateTime firstFailDate;
        private Random random = new Random();
        private Timer monitorTimer;
        private object monitorLock = new object();
        private int taskIndex;

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
            int index = ++taskIndex;
            string address = PingAddresses[random.Next(PingAddresses.Length)];

            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - [{index}] Sending ping to {address}");

            bool result = SendPing(address, PingTimeout);

            lock (monitorLock)
            {
                if (result)
                {
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

            Debug.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - [{index}] Result: {result}");

            return result;
        }

        protected void OnNetworkStatusChanged(bool isConnected, DateTime date)
        {
            if (NetworkStatusChanged != null)
            {
                NetworkStatusChanged(isConnected, date);
            }
        }

        private bool SendPing(string address, int timeout)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(address, timeout);
                    return reply != null && reply.Status == IPStatus.Success;
                }
            }
            catch
            {
            }

            return false;
        }
    }
}