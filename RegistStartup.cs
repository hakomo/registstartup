using Hakomo.Library;
using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RegistStartup {

    class RegistStartup {

        [STAThread]
        private static void Main() {
            using(new TemporaryCurrentDirectory(Application.StartupPath)) {
                foreach(string path in new string[] { "..\\yamy-0.03\\yamy.exe", "..\\fitwin\\fitwin.exe", "..\\launcher.exe" }) {
                    string p = Path.GetFullPath(path);
                    var s = (new WshShell()).CreateShortcut(string.Format("{0}\\{1}.lnk",
                        Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileNameWithoutExtension(p)));
                    s.IconLocation = p + ",0";
                    s.TargetPath = p;
                    s.WindowStyle = 7;
                    s.WorkingDirectory = Path.GetDirectoryName(p);
                    try {
                        s.Save();
                        Process.Start(s.FullName);
                    } catch(UnauthorizedAccessException) {
                    } finally {
                        Marshal.ReleaseComObject(s);
                    }
                }
            }
        }
    }
}
