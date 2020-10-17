using SpellFramework.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpellFramework
{
    static class Program
    {
        public static Spell currentSpell;
        public static Loader currentLoader;
        public static StartupLoading loading;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
            Application.SetCompatibleTextRenderingDefault(false);
            if (!String.IsNullOrEmpty(Settings.Default.BASPath))
            {
                //var appDomainSetup = new AppDomainSetup();
                //appDomainSetup.ApplicationBase = Settings.Default.BASPath;
                //appDomainSetup.PrivateBinPath = @"\BladeAndSorcery_Data\Managed";
                //var domain = AppDomain.CreateDomain("test", null, appDomainSetup);
                //var str = domain.SetupInformation.ApplicationBase + domain.SetupInformation.PrivateBinPath;
                loading = new StartupLoading();
                Application.Run(loading);

                currentSpell = new Spell();
                Application.Run(currentSpell);
                //Debug.Log("testtest" + str + " test");
            }
            else
            {
                currentLoader = new Loader();
                Application.Run(currentLoader);
            }
        }
    }

    class Debug
    {
        public static void Log(string text)
        {
            Console.WriteLine(text);
            if (Program.currentSpell != null)
            {
                var str = "> " + text + "\n";
                Spell.localDebugConsole.Text = Spell.localDebugConsole.Text.Insert(0, str);
                if (!Spell.startupComplete) return;
                Spell.localDebugConsole.SelectionStart = 0;
                Spell.localDebugConsole.SelectionLength = str.Count();
                Spell.localDebugConsole.SelectionFont = new Font(Spell.localDebugConsole.Font, FontStyle.Bold);
                Spell.localDebugConsole.Select(0, 0);
            }

        }

        public static void LogWarning(string text)
        {
            Console.WriteLine("WARNING: " + text);
            if (Program.currentSpell != null)
            {
                var str = "WARNING: " + text + "\n";
                Spell.localDebugConsole.Text = Spell.localDebugConsole.Text.Insert(0, str);
                if (!Spell.startupComplete) return;
                Spell.localDebugConsole.SelectionStart = 0;
                Spell.localDebugConsole.SelectionLength = str.Count();
                Spell.localDebugConsole.SelectionFont = new Font(Spell.localDebugConsole.Font, FontStyle.Bold);
                Spell.localDebugConsole.Select(0, 0);
            }

        }

        public void LogException(Exception exception, UnityEngine.Object context)
        {
            Spell.localDebugConsole.AppendText("\n> " + context);
        }

        public void LogFormat(UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] args)
        {

        }
    }
}
