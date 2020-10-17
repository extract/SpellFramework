using SpellFramework.Properties;
using System;
using System.IO;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using Color = System.Drawing.Color;

namespace SpellFramework
{
    public partial class Loader : Form
    {
        private Color currentEnterColor = Color.Empty;

        public Loader()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dialogResults = folderBrowserDialog1.ShowDialog();
            pathText.Text = folderBrowserDialog1.SelectedPath;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private bool VerifyPath()
        {
            return (String.IsNullOrEmpty(pathText.Text) ||
                    String.IsNullOrWhiteSpace(pathText.Text) ||
                    !(pathText.Text.Contains("\\") || pathText.Text.Contains("/")) ||
                    !pathText.Text.Contains("&"));
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            if (currentEnterColor == Color.Empty)
                currentEnterColor = EnterButton.ForeColor;


            if (VerifyPath())
            {
                EnterButton.ForeColor = Color.OrangeRed;
                return;
            }
            EnterButton.ForeColor = currentEnterColor;
            if (Directory.Exists(pathText.Text + "/" + @"\BladeAndSorcery_Data\StreamingAssets"))
            {
                Settings.Default.BASPath = pathText.Text;
                Properties.Settings.Default.Save();

                Application.Restart();
            }
        }

        private void pathText_TextChanged(object sender, EventArgs e)
        {
            if (currentEnterColor == Color.Empty)
                currentEnterColor = EnterButton.ForeColor;
            if (!VerifyPath())
            {
                EnterButton.ForeColor = Color.LightGreen;
            }
            else if (EnterButton.ForeColor != EnterButton.ForeColor)
            {
                EnterButton.ForeColor = Color.OrangeRed;
            }
        }
    }
}
