using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ThunderRoad;

namespace SpellFramework
{
    [Serializable]
    public struct Manifest
    {
        public string name;
        public string description;
        public string author;
        public string modVersion;
        public string gameVersion;
    }

    [Serializable]
    public class Mod
    {
        public string modName;
        public string author;
        public Type baseType;
        [JsonIgnore]
        public List<FieldInfo> fieldInfoList = new List<FieldInfo>();

        [JsonIgnore]
        public TreeNode treeNode;

        public dynamic instancedType;
        public Manifest manifest;
        public List<dynamic>[] modData = new List<dynamic>[ModConfiguration.CategoryCount];

        public Mod(string modName, string author, Type baseType)
        {
            manifest.author = author;
            manifest.name = modName;
            manifest.description = modName + " by " + author + " - using Spell Framework";
            manifest.gameVersion = "8.3";
            manifest.modVersion = "1.0.0";
            this.modName = modName;
            this.author = author;
            this.baseType = baseType;
            instancedType = Activator.CreateInstance(baseType) as dynamic;
            foreach (var field in baseType.GetFields())
            {
                fieldInfoList.Add(field);
            }
            for (int i = 0; i < ModConfiguration.CategoryCount; i++)
            {
                modData[i] = new List<dynamic>();
            }
        }

        public void CreateData(dynamic data, Type type, string newName, int category)
        {
            dynamic d = Activator.CreateInstance(type);

            foreach (var field in type.GetFields())
            {
                field.SetValue(d, field.GetValue(ModConfiguration.ConvertDynamic(data, type)));
            }
            type.GetField("id").SetValue(d, newName);

            modData[category].Add(d);
        }
        public void LoadFromJson(string loadFromJson)
        {
            string value = File.ReadAllText(loadFromJson);

            instancedType = JsonConvert.DeserializeObject(value, baseType) as dynamic;

            Dictionary<string, string> categoryDirectory = new Dictionary<string, string>();
            treeNode = new TreeNode(ModConfiguration.streamingAssetsPath + "\\" + modName);
            Catalog.Category[] categoryArray = (Catalog.Category[])Enum.GetValues(typeof(Catalog.Category));
            foreach (var category in categoryArray)
            {
                var treeCatIndex = treeNode.Nodes.Add(new TreeNode(category.ToString()));
                bool removeNode = true;
                foreach (var fieldAttribute in fieldInfoList)
                {
                    if (!(fieldAttribute.FieldType == typeof(string) && fieldAttribute.Name.Contains("Id"))) continue;

                    string fieldValue = fieldAttribute.GetValue(instancedType) as string;
                    var parentBroke = true;
                    foreach(var parent in TypeHelper.GetParentTypes(fieldAttribute.DeclaringType))
                    {
                        if (parent.Name == category.ToString())
                            parentBroke = false;
                    }
                    if (parentBroke) break;
                    if (String.IsNullOrEmpty(fieldValue)) continue;

                    var data = ModConfiguration.data[(int)category].FirstOrDefault(x => x.id.Contains(fieldValue));
                    if (data == null) continue;
                    var id = data.id;
                    treeNode.Nodes[treeCatIndex].Nodes.Add(id);
                    removeNode = false;
                }
                if (removeNode) treeNode.Nodes.RemoveAt(treeCatIndex);
            }
        }



        public void SetBaseType(Type baseType)
        {
            this.baseType = baseType;
            instancedType = Activator.CreateInstance(baseType) as CatalogData;
            foreach (var field in baseType.GetFields())
            {
                fieldInfoList.Add(field);
            }

        }

    }
}