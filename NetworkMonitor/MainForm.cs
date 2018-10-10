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

using NetworkMonitor.Properties;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NetworkMonitor
{
    public partial class MainForm : Form
    {
        private NetworkMonitor networkMonitor;
        private bool forceClose;

        public MainForm()
        {
            InitializeComponent();
            Icon = Resources.Icon;
            niMain.Icon = Resources.Icon;

            networkMonitor = new NetworkMonitor();
            networkMonitor.NetworkStatusChanged += NetworkMonitor_NetworkStatusChanged;
            networkMonitor.StartMonitorThread();

            UpdateStatusBar();
        }

        private void NetworkMonitor_NetworkStatusChanged(bool isConnected, DateTime date)
        {
            if (!IsDisposed)
            {
                Invoke((Action)delegate
                {
                    AddConnectionStatus(isConnected, date, true);
                    UpdateStatusBar();
                });
            }
        }

        private void AddConnectionStatus(bool isConnected, DateTime date, bool showBalloonTip)
        {
            string text = isConnected ? "Connected." : "Disconnected.";

            AppendLog(date, text);

            ListViewItem lvi = new ListViewItem();
            lvi.ForeColor = isConnected ? Color.Green : Color.Firebrick;
            lvi.Text = date.ToString();
            lvi.SubItems.Add(text);
            lvMain.Items.Add(lvi);

            if (showBalloonTip)
            {
                niMain.ShowBalloonTip(5000, "Network monitor", text, ToolTipIcon.Info);
            }
        }

        // For testing purposes
        private void AddFakeConnectionStatus(int count)
        {
            DateTime date = DateTime.Now;
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                date = date.AddSeconds(random.Next(300, 3600));
                AddConnectionStatus(false, date, false);
                date = date.AddSeconds(random.Next(1, 60));
                AddConnectionStatus(true, date, false);
            }
        }

        private void AppendLog(DateTime date, string text)
        {
            if (!Directory.Exists(Program.LogsFolder))
            {
                Directory.CreateDirectory(Program.LogsFolder);
            }

            File.AppendAllText(Program.LogsFilePath, $"{date} - {text}" + Environment.NewLine);
        }

        private void OpenLogFile()
        {
            try
            {
                if (File.Exists(Program.LogsFilePath))
                {
                    Process.Start(Program.LogsFilePath);
                }
                else if (Directory.Exists(Program.LogsFolder))
                {
                    Process.Start(Program.LogsFolder);
                }
            }
            catch
            {
            }
        }

        private void UpdateStatusBar()
        {
            tsslStatus.Text = "Disconnect count: " + networkMonitor.DisconnectCount;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !forceClose)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void OpenMainWindow()
        {
            if (!Visible)
            {
                Show();
            }

            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }

            BringToFront();
            Activate();
        }

        private void niMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OpenMainWindow();
            }
        }

        private void tsmiOpenMainWindow_Click(object sender, EventArgs e)
        {
            OpenMainWindow();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            forceClose = true;
            Close();
        }

        private void tsbCopyAll_Click(object sender, EventArgs e)
        {
            if (lvMain.Items.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (ListViewItem lvi in lvMain.Items)
                {
                    sb.AppendLine(lvi.Text + " - " + lvi.SubItems[1].Text);
                }

                string text = sb.ToString().Trim();
                Clipboard.SetText(text);
            }
        }

        private void tsbOpenLogFile_Click(object sender, EventArgs e)
        {
            OpenLogFile();
        }
    }
}