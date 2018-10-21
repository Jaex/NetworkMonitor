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

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace NetworkMonitor
{
    public static class Helpers
    {
        private static readonly string StartupRegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private static readonly string StartupRegistryValue = $"\"{Application.ExecutablePath}\" -silent";

        private static string GetRegistryValue(string path, string name = null)
        {
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(path))
            {
                if (rk != null)
                {
                    return rk.GetValue(name, null) as string;
                }
            }

            return null;
        }

        private static bool CheckRegistry(string path, string name = null, string value = null)
        {
            string registryValue = GetRegistryValue(path, name);

            if (registryValue != null)
            {
                return value == null || registryValue.Equals(value, StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }

        public static bool CheckStartup()
        {
            try
            {
                return CheckRegistry(StartupRegistryPath, Program.ApplicationName, StartupRegistryValue);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return false;
        }

        public static void SetStartup(bool create)
        {
            try
            {
                using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(StartupRegistryPath, true))
                {
                    if (rk != null)
                    {
                        if (create)
                        {
                            rk.SetValue(Program.ApplicationName, StartupRegistryValue, RegistryValueKind.String);
                        }
                        else
                        {
                            rk.DeleteValue(Program.ApplicationName, false);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}