using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using ThunderRoad;

namespace SpellFramework
{


    public static class ModConfiguration
    {
        static public Mod current;
        static public string streamingAssetsPath = null;


        //private List<CatalogData> catalogDatas = new List<CatalogData>();
        //private List<EffectData> effects = new List<EffectData>(); // 
        //private List<ContainerData> containers = new List<ContainerData>();
        public static readonly int CategoryCount = Enum.GetNames(typeof(Catalog.Category)).Length;
        public static List<dynamic>[] data = new List<dynamic>[CategoryCount];

        public static List<Type> generatedTypes = new List<Type>();
        public static bool doneLoading = false;
        public static List<Type> allowedTypes = new List<Type>();
        public static Type baseType;
        public static string[] allowedTypesStr = new string[] {
            "SpellCastProjectile",
            "SpellCastCharge",
            "SpellCastData",
            "SpellCastGravity",
            "SpellCastLightning"
        };

        public static void LoadDefault()
        {
            DataManager.LoadDefaultJson();

            doneLoading = true;

        }
        public static void LoadMod(string manifest)
        {
            current = new Mod("", "", generatedTypes[0]);
            Spell.SetAttributeTable(current);

        }

        public static dynamic ConvertDynamic(dynamic data, Type type)
        {
            if (type.GetFields().Where(x => x.FieldType.Name.Contains("Color")).Count() > 0)
            {
                foreach (var col in type.GetFields().Where(x => x.FieldType.Name.Contains("Color")))
                {
                    Debug.Log("asdasd" + col.Name);
                }
            }
            string shit = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject(shit, type);
        }

        public static void LoadModProfile(string loadedFile)
        {
            current = DataManager.LoadProfileJson(loadedFile);
        }

        public static void SaveModProfile()
        {
            DataManager.SaveProfileJson();
        }

        public static void SaveToModDirectory()
        {
            DataManager.SaveToModDirectory();
        }

    }


}
