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
using System.Drawing;
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
                    string text = isConnected ? "Connected." : "Disconnected.";

                    ListViewItem lvi = new ListViewItem();
                    lvi.ForeColor = isConnected ? Color.Green : Color.Firebrick;
                    lvi.Text = date.ToString();
                    lvi.SubItems.Add(text);
                    lvMain.Items.Add(lvi);

                    UpdateStatusBar();

                    niMain.ShowBalloonTip(5000, "Network monitor", text, ToolTipIcon.Info);
                });
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
    }
}