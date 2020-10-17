using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ThunderRoad;

namespace SpellFramework
{
    public partial class ReferenceManager : Form
    {
        DataGridViewCellEventArgs eventArgs;
        Type fieldType;
        int category;
        //List<CatalogData> allowedValues = new List<CatalogData>();
        string fieldValue = "";
        string targetSource = "";
        AutoCompleteStringCollection autoCompSrc = new AutoCompleteStringCollection();
        public static event EventHandler ReferenceComplete;
        protected virtual void OnReferenceComplete(EventArgs e)
        {
            EventHandler handler = ReferenceComplete;
            handler?.Invoke(this, e);
        }
        public ReferenceManager(FieldInfo fieldInfo, DataGridViewCellEventArgs e)
        {
            FormClosing += ReferenceManager_FormClosing;
            fieldValue = fieldInfo.GetValue(ModConfiguration.current.instancedType).ToString();
            fieldInfo.GetValue(ModConfiguration.current.instancedType);
            string fieldTypeName = Spell.dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value as string;
            fieldTypeName = fieldTypeName.Replace("Id", "Data");
            Catalog.Category categorylocal = Catalog.Category.Brain;
            foreach (var en in Enum.GetValues(typeof(Catalog.Category)))
            {
                var index = fieldTypeName.IndexOf(en.ToString() + "Data");
                if (index < 0) continue;
                var split = fieldTypeName.Substring(index);
                fieldType = ModConfiguration.generatedTypes.First(x => x.Name == split);
                category = (int)Enum.Parse(typeof(Catalog.Category), en.ToString());
                categorylocal = (Catalog.Category)en;
                break;
            }
            if (fieldType == null)
            {
                fieldType = ModConfiguration.generatedTypes.First(x => x.Name == "ItemPhysic");
                category = (int)Catalog.Category.Item;
            }

            foreach (var data in ModConfiguration.data[category])
            {
                //var dota = ModConfiguration.ConvertDynamic(data, ModConfiguration.generatedTypes.FirstOrDefault(x => x.Name.Contains(categorylocal.ToString())));
                autoCompSrc.Add(fieldType.GetField("id").GetValue(data));
                //allowedValues.Add(data);
                if (fieldType.GetField("id").GetValue(data) != null) targetSource = "Default";
            }
            foreach (var data in ModConfiguration.current.modData[category])
            {
                autoCompSrc.Add(fieldType.GetField("id").GetValue(data));
                //allowedValues.Add(data);
                if (fieldType.GetField("id").GetValue(data) != null) targetSource = ModConfiguration.current.modName;
            }
            if (string.IsNullOrEmpty(targetSource))
            {
                targetSource = "Default";
            }


            //allowedValues.ForEach(x => autoCompSrc.Add(x.id));



            InitializeComponent();
            if (targetSource != "Default")
            {
                labelDefault.Visible = false;
            }
            else
            {
                labelDefault.Visible = true;
            }
            fieldValueLabel.Text = fieldValue + " (" + fieldType.Name + ")";

            var copyField = fieldValue.Replace("Fire", ModConfiguration.current.modName);
            copyRenameText.Text = copyField;

            eventArgs = e;
            ReferenceText.AutoCompleteCustomSource = autoCompSrc;
            ReferenceText.AutoCompleteMode = AutoCompleteMode.Suggest;
            ReferenceText.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //category = (Catalog.Category)Enum.Parse(typeof(Catalog.Category), fieldTypeName.Replace("Data", ""));


        }

        private void ReferenceManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnReferenceComplete(null);

        }

        private void ReferenceManager_Load(object sender, EventArgs e)
        {

        }
        private void fieldValueLabel_Click(object sender, EventArgs e)
        {

        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            dynamic result;
            if (targetSource == "Default")
            {
                string val = Spell.dataGrid.Rows[eventArgs.RowIndex].Cells[eventArgs.ColumnIndex].Value as string;
                //fieldType.GetField("id").GetValue();
                result = ModConfiguration.data[category].FirstOrDefault(x => x.GetType().GetField("id").GetValue(x) == val);
            }
            else
            {
                throw new NotImplementedException();
            }
            //result = ModConfiguration.current.fieldInfoList.FirstOrDefault(x => x.FieldType == typeof(string) && ((string)x.GetValue(ModConfiguration.current.instancedType)) == fieldValue);

            //var obj = ModConfiguration.FindAndCopy(targetSource);
            ModConfiguration.current.CreateData(result, fieldType, copyRenameText.Text, category);
            ChangeReference(copyRenameText.Text);
            this.Hide();
        }

        public new void Hide()
        {
            OnReferenceComplete(null);
            base.Hide();
        }

        private void changeRefButton_Click(object sender, EventArgs e)
        {
            string val = ReferenceText.Text;

            if (!autoCompSrc.Contains(val))
            {
                Debug.Log(val + " is not a valid input.");
                return;
            }
            ChangeReference(val);
            this.Hide();
        }

        private void ChangeReference(string refName)
        {
            string val = refName;

            var result = ModConfiguration.current.fieldInfoList.FirstOrDefault(x => x.FieldType == typeof(string) && ((string)x.GetValue(ModConfiguration.current.instancedType)) == fieldValue);
            ModConfiguration.current.fieldInfoList.FirstOrDefault(x => x == result).SetValue(ModConfiguration.current.instancedType, ReferenceText.Text);
            Spell.dataGrid.Rows[eventArgs.RowIndex].Cells[eventArgs.ColumnIndex].Value = refName;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
