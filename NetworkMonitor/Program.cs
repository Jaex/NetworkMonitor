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
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace NetworkMonitor
{
    internal static class Program
    {
        public const string ApplicationName = "Network Monitor";

        public static string PersonalFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), ApplicationName);

        private const string LogsFolderName = "Logs";

        public static string LogsFolder => Path.Combine(PersonalFolder, LogsFolderName);

        public static string LogsFilePath
        {
            get
            {
                string filename = $"NetworkMonitor-Log-{DateTime.Now:yyyy-MM}.txt";
                return Path.Combine(LogsFolder, filename);
            }
        }

        public static bool Silent { get; private set; }

        public static Settings Settings { get; private set; } = new Settings();

        [STAThread]
        private static void Main(string[] args)
        {
            Silent = args.Any(x => x.Equals("-silent", StringComparison.InvariantCultureIgnoreCase));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}