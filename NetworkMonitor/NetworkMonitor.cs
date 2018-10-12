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
using System.Threading.Tasks;

namespace NetworkMonitor
{
    public class NetworkMonitor
    {
        public delegate void NetworkStatusEventHandler(bool isConnected, DateTime date);
        public event NetworkStatusEventHandler NetworkStatusChanged;

        public bool IsMonitoring { get; private set; }
        public bool IsConnected { get; private set; }
        public int DisconnectCount { get; private set; }

        public int FailThreshold { get; set; } = 5;
        public string[] PingAddresses { get; set; } = new string[] { "8.8.8.8", "8.8.4.4" };
        public int PingInterval { get; set; } = 1000;
        public int PingTimeout { get; set; } = 1000;

        private int failCount = 0;
        private bool isFirstEvent = true;
        private DateTime firstFailDate;
        private Random random = new Random();
        private bool isStopRequested;

        public void StartMonitorThread()
        {
            if (!IsMonitoring)
            {
                IsMonitoring = true;
                isStopRequested = false;

                Task.Run(() =>
                {
                    Stopwatch timer = new Stopwatch();

                    while (!isStopRequested)
                    {
                        timer.Restart();
                        CheckNetworkStatus();
                        int elapsed = (int)timer.ElapsedMilliseconds;
                        if (elapsed < PingInterval)
                        {
                            Thread.Sleep(PingInterval - elapsed);
                        }
                    }

                    IsMonitoring = false;
                });
            }
        }

        public void StopMonitorThread()
        {
            if (IsMonitoring)
            {
                isStopRequested = true;
            }
        }

        private bool CheckNetworkStatus()
        {
            string address = PingAddresses[random.Next(PingAddresses.Length)];
            bool result = SendPing(address, PingTimeout);

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