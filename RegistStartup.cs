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
                foreach(var path in new string[] { @"..\fitwin\fitwin.exe", @"..\launcher.exe",
                        @"..\yamy-0.03\yamy.exe", @"C:\Program Files (x86)\Mozilla Thunderbird\thunderbird.exe" }) {
                    var p = Path.GetFullPath(path);
                    var s = (new WshShell()).CreateShortcut(string.Format("{0}\\{1}.lnk",
                        Environment.GetFolderPath(Environment.SpecialFolder.Startup), Path.GetFileNameWithoutExtension(p)));
                    s.IconLocation = p + ",0";
                    s.TargetPath = p;
                    s.WindowStyle = 7;
                    s.WorkingDirectory = Path.GetDirectoryName(p);
                    try {
                        s.Save();
                        Process.Start(s.FullName);
                    } finally {
                        Marshal.ReleaseComObject(s);
                    }
                }
            }
        }
    }
}
