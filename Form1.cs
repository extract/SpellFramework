using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace SpellFramework
{
    public partial class Spell : Form
    {
        private const string CUSTOM_BASE = "Custom";
        FolderBrowserDialog streamingAssetsDialog = new FolderBrowserDialog();
        static Assembly basAsm;
        static List<Assembly> referencesAsm = new List<Assembly>();
        public static RichTextBox localDebugConsole;
        public static bool startupComplete = false;
        public static DataGridView dataGrid;
        public static DataGridViewCellStyle style;
        public Spell()
        {
            InitializeComponent();
            localDebugConsole = debugConsole;
            style = new DataGridViewCellStyle();
            style.ForeColor = Color.Gray;
            style.SelectionBackColor = Color.Empty;
            style.SelectionForeColor = Color.Empty;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var data in ModConfiguration.generatedTypes)
            {
                Debug.Log(data.Name + " " + data.GetParentTypes().First().Name);
            }
            startupComplete = true;
            var auto = new AutoCompleteStringCollection();
            foreach (var generatedType in ModConfiguration.allowedTypesStr)
            {

                auto.Add(generatedType);
            }
            customBaseText.AutoCompleteCustomSource = auto;
            customBaseText.AutoCompleteMode = AutoCompleteMode.Suggest;
            customBaseText.GotFocus += CustomBaseText_GotFocus;
            customBaseText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            dataGrid = dataGridAttribute;
            dataGrid.CellContentClick += DataGrid_CellContentClick;
            ReferenceManager.ReferenceComplete += ReferenceManager_ReferenceComplete;
            Debug.Log("----DEFAULT LOADING COMPLETE----");
            ModConfiguration.LoadMod("Default");

        }

        private void ReferenceManager_ReferenceComplete(object sender, EventArgs e)
        {
            ReferenceManager.ReferenceComplete -= ReferenceManager_ReferenceComplete;
            Debug.Log("Shut down reference manager");
        }

        static public void SetAttributeTable(Mod mod)
        {


            dataGrid.Rows.Clear();
            var rows = dataGrid.Rows;
            for (int i = 0; i < mod.baseType.GetFields().Count(); i++)
            {
                if (mod.fieldInfoList[i].Name.Contains("hash") ||
                    mod.fieldInfoList[i].Name.Contains("exlude")) continue;
                var inte = rows.Add(new DataGridViewRow());
                if (mod.fieldInfoList[i].FieldType.Name.Contains("Data"))
                {

                    rows[inte].ReadOnly = true;
                    foreach (DataGridViewCell c in rows[inte].Cells)
                    {


                        c.Style = style;
                        c.ToolTipText = "This value can not be edited";

                    }
                }


                string value = mod.fieldInfoList[i].GetValue(mod.instancedType) == null ? "" : mod.fieldInfoList[i].GetValue(mod.instancedType).ToString();
                rows[inte].SetValues(i,
                    mod.fieldInfoList[i].Name,
                    mod.fieldInfoList[i].FieldType.Name,
                    value);
                if (!mod.fieldInfoList[i].FieldType.IsPrimitive &&
                    !mod.fieldInfoList[i].FieldType.IsEquivalentTo(typeof(string)) &&
                    mod.fieldInfoList[i].GetValue(mod.instancedType) != null)
                {
                    for (int j = 0; j < mod.fieldInfoList[i].FieldType.GetFields().Count(); j++)
                    {

                        var row = rows.Add(new DataGridViewRow());
                        var field = mod.fieldInfoList[i].FieldType.GetFields()[j];
                        rows[row].SetValues(j,
                            mod.fieldInfoList[i].FieldType.Name + " + " + field.Name,
                            field.FieldType.Name,
                            field.GetValue(mod.fieldInfoList[i].GetValue(mod.instancedType)));
                    }
                }
                if (mod.fieldInfoList[i].Name.Contains("Id"))
                {
                    var text = rows[inte].Cells[dataGrid.ColumnCount - 1].Value;
                    var cellButton = new DataGridViewButtonCell();
                    cellButton.Value = text;
                    rows[inte].Cells[dataGrid.ColumnCount - 1] = cellButton;

                }
            }


        }

        private static void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;


            if ((senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewButtonCell) &&
                e.RowIndex >= 0)
            {
                var mod = ModConfiguration.current;
                string val = senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
                var fieldResult = ModConfiguration.current.fieldInfoList.FirstOrDefault(x => x.FieldType == typeof(string) && ((string)x.GetValue(ModConfiguration.current.instancedType)) == val);
                if (fieldResult == default) return;
                new ReferenceManager(fieldResult, e).ShowDialog();

            }
        }

        private static void Spell_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CustomBaseText_GotFocus(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void selectStreamingAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.Log("test");
        }


        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            do
            {
                var baseType = ModConfiguration.generatedTypes.Find(x => x.Name.Contains(customBaseText.Text));
                if (!IsValid(baseType)) break;

                ModConfiguration.current = new Mod(modName.Text, authorName.Text, baseType);
                ModConfiguration.baseType = baseType;
                Debug.Log("Generating a mod with base type " + ModConfiguration.current.baseType);
                ModConfiguration.current.LoadFromJson(ModConfiguration.streamingAssetsPath + "/Default/Spells/Spell_Fire.json");
                SetAttributeTable(ModConfiguration.current);
                treeView1.Nodes.Add(ModConfiguration.current.treeNode);
                treeView1.Update();
                return;
            } while (true);
            Debug.Log("Mod was not generated. Requirements:\n1. The mod- and author names may only use ASCII [A-z,0-9].\n2. The base type must be selected.");

        }

        private bool VerifyModInfo()
        {
            return (ModConfiguration.baseType != null &&
                    !String.IsNullOrEmpty(authorName.Text) &&
                    !String.IsNullOrEmpty(modName.Text) &&
                    modName.Text.All(x => x <= sbyte.MaxValue)); // Add more checks :)
        }

        private void baseTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var g = baseTypeList.Items[baseTypeList.SelectedIndex].ToString().Split(' ')[0];
            if (!g.Equals(CUSTOM_BASE))
            {
                var baseType = ModConfiguration.generatedTypes.Find(x => x.Name.Contains(g));

                customBaseText.ReadOnly = true;
                customBaseText.Text = baseType.Name;
                ModConfiguration.baseType = baseType;
            }
            else
            {
                customBaseText.ReadOnly = false;

                customBaseText.Text = "";
            }


        }

        private void customBaseText_TextChanged(object sender, EventArgs e)
        {
            /*var baseType = ModConfiguration.generatedTypes.Find(x => x.Name.Contains(customBaseText.Text));
            if (IsValid(baseType))
            {
                customBaseText.Font = new System.Drawing.Font(customBaseText.Font, System.Drawing.FontStyle.Bold);
                
            }
            else
            {
                customBaseText.Font = new System.Drawing.Font(customBaseText.Font, System.Drawing.FontStyle.Regular);                
            }*/
        }

        private bool IsValid(Type baseType)
        {
            return !(baseType == null ||
                    baseType == default);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void resetToDefaultPathToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var g = new AppDomainSetup().PrivateBinPathProbe;
            var ag = new AppDomainSetup().PrivateBinPath;
            MessageBox.Show(" " + g + " - " + ag, "Reset BAS Path", MessageBoxButtons.YesNo);
            var results = MessageBox.Show("Are you sure you want to reset the BAS path?\nThis will restart the program.", "Reset BAS Path", MessageBoxButtons.YesNo);
            if (results == DialogResult.Yes)
            {


                Properties.Settings.Default.BASPath = null;
                Properties.Settings.Default.Save();
                Application.Restart();
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "File|*.json";
            openFileDialog1.Title = "Select profile .json file";
            var res = openFileDialog1.ShowDialog();
            if (!File.Exists(openFileDialog1.FileName)) return;
            ModConfiguration.LoadModProfile(openFileDialog1.FileName);


        }

        private void savToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModConfiguration.SaveModProfile();
            ModConfiguration.SaveToModDirectory();

        }
    }
}
