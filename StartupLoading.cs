using System;
using System.Reflection;
using System.Windows.Forms;
using UnityEngine;

namespace SpellFramework
{
    public partial class StartupLoading : Form
    {
        private string debugPath = Properties.Settings.Default.BASPath; //@"D:\Program Files (x86)\Steam\steamapps\common\Blade & Sorcery";
        Timer timer;
        public StartupLoading()
        {
            InitializeComponent();
            var path = String.IsNullOrEmpty(debugPath) ? Assembly.GetEntryAssembly().Location : debugPath;
            ModConfiguration.streamingAssetsPath = path + @"\BladeAndSorcery_Data\StreamingAssets";
            ModConfiguration.LoadDefault(); // Preference?

            timer = new Timer();
            timer.Interval = 5;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!ModConfiguration.doneLoading)
            {
                var clampCount = Mathf.Clamp(ModConfiguration.generatedTypes.Count, 0, 40);
                progressBar1.Value = clampCount * 100 / 40;
            }
            else
            {
                timer.Stop();
                timer.Dispose();
                this.DestroyHandle();
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
