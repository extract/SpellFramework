using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ThunderRoad;

namespace SpellFramework
{
    public static class DataManager
    {
        public static void SaveProfileJson()
        {
            var jsonNetSerializerSettings = GetDefaultJsonSettings();

            string serializedProfile = JsonConvert.SerializeObject(ModConfiguration.current, Formatting.Indented, jsonNetSerializerSettings);
            var filePath = ModConfiguration.streamingAssetsPath + "/_SFProfiles/" + ModConfiguration.current.author + "_" + ModConfiguration.current.modName + ".json";
            var dirPath = ModConfiguration.streamingAssetsPath + "/_SFProfiles";
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            foreach (string dirs in Directory.GetDirectories(dirPath))
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(dirs + "/").GetFiles(ModConfiguration.current.modName + "_" + ModConfiguration.current.author + ".json", SearchOption.AllDirectories))
                {
                    filePath = fileInfo.FullName;
                }
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                File.AppendAllText(filePath + "~", serializedProfile);
                File.Copy(filePath + "~", filePath);
                File.Delete(filePath);
            }
            else
            {
                File.AppendAllText(filePath, serializedProfile);
            }
        }

        public static Mod LoadProfileJson(string path)
        {
            JsonSerializerSettings jsonNetSerializerSettings = Catalog.GetJsonNetSerializerSettings();
            jsonNetSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            jsonNetSerializerSettings.TypeNameHandling = TypeNameHandling.Arrays;

            var serializedObject = File.ReadAllText(path);
            return JsonConvert.DeserializeObject(serializedObject, typeof(Mod), jsonNetSerializerSettings) as Mod;
        }

        public static void LoadDefaultJson()
        {
            Catalog.Category[] array = (Catalog.Category[])Enum.GetValues(typeof(Catalog.Category));
            for (int i = 0; i < array.Length; i++)
            {
                LoadFromJson(array[i]);

            }
        }
        public static void LoadModJson()
        {
            if (String.IsNullOrEmpty(ModConfiguration.current.modName)) return;

            Catalog.Category[] array = (Catalog.Category[])Enum.GetValues(typeof(Catalog.Category));
            for (int i = 0; i < array.Length; i++)
            {
                LoadFromJson(ModConfiguration.current.modName, array[i]);
            }
        }

        public static void SaveToModDirectory()
        {
            var path = ModConfiguration.streamingAssetsPath + "/" + ModConfiguration.current.modName;

            for (int i = 0; i < ModConfiguration.current.modData.Length; i++)
            {

                List<dynamic> cat = (List<dynamic>)ModConfiguration.current.modData[i];
                if (!Directory.Exists(path + "/" + Enum.GetName(typeof(Catalog.Category), i)))
                {
                    Directory.CreateDirectory(path);
                }
                if (cat.Count > 0)
                {
                    DirectoryInfo directoryInfo;
                    var catPath = path + "/" + Enum.GetName(typeof(Catalog.Category), i);
                    if (!Directory.Exists(catPath))
                    {
                        directoryInfo = Directory.CreateDirectory(catPath);
                    }
                    foreach (var fileObj in cat)
                    {
                        var id = fileObj.GetType().GetField("id").GetValue(fileObj);
                        var filePath = path + "/" + Enum.GetName(typeof(Catalog.Category), i) + "/" + Enum.GetName(typeof(Catalog.Category), i) + "_" + id + ".json";

                        Type externalType = Type.GetType(fileObj.type);
                        var instantiatedExternalObject = Activator.CreateInstance(externalType);
                        var internalType = fileObj.GetType(); //TypeHelper.GenerateMissingType(externalType); // Converts to internal type
                        dynamic internalObject = ModConfiguration.ConvertDynamic(fileObj, internalType);

                        // Foreach internalField find the externalField corresponding to it and add the values
                        // from the internal object to the external object.
                        foreach (var internalField in internalType.GetFields())
                        {
                            var commonFieldExternal = externalType.GetFields().FirstOrDefault(x => x.Name == internalField.Name);
                            if (commonFieldExternal == null) continue;
                            var internalObjectValue = internalField.GetValue(internalObject);
                            // TODO: Check if list, if list then ... do stuff?
                            commonFieldExternal.SetValue(instantiatedExternalObject, internalObjectValue);
                        }

                        var jsonNetSerializerSettings = GetDefaultJsonSettings();
                        File.WriteAllText(filePath, JsonConvert.SerializeObject(instantiatedExternalObject, jsonNetSerializerSettings));
                    }
                }
            }
            foreach (var field in typeof(Mod).GetProperties())
            {
                if (!Attribute.IsDefined(field, typeof(SerializableAttribute))) continue;


            }

        }

        public static void LoadFromJson(Catalog.Category category)
        {
            ModConfiguration.data[(int)category] = new List<dynamic>();

            if (ModConfiguration.streamingAssetsPath == null)
            {
                return;
            }
            GetDefaultJsonSettings();
            foreach (string text2 in Directory.GetDirectories(ModConfiguration.streamingAssetsPath + "/" + "Default"))
            {

                string fileName = Path.GetFileName(text2);
                foreach (FileInfo fileInfo in new DirectoryInfo(text2 + "/").GetFiles(category.ToString() + "_*.json", SearchOption.AllDirectories))
                {

                    Debug.Log("JSON loader - Found file: " + fileInfo.Name);
                    string value = File.ReadAllText(fileInfo.FullName);
                    value = value.Replace("$type", "type");
                    var sub = Regex.Match(value, "\\\"type\\\":\\s+\\\"(\\w+\\.[\\w\\d]+, [\\w\\d-]+)\\\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                    string typeStr = sub.Groups[1].Value;
                    Type type = Type.GetType(typeStr);
                    var internalType = TypeHelper.GenerateMissingType(type);

                    var usedType = internalType;
                    try
                    {
                        dynamic catalogData = Activator.CreateInstance(internalType);

                        catalogData = JsonConvert.DeserializeObject(value, usedType) as CatalogData;

                        catalogData.type = typeStr;

                        if (catalogData != null)
                            ModConfiguration.data[(int)category].Add(catalogData);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        private static JsonSerializerSettings GetDefaultJsonSettings()
        {
            JsonSerializerSettings jsonNetSerializerSettings = Catalog.GetJsonNetSerializerSettings();
            jsonNetSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            jsonNetSerializerSettings.TypeNameHandling = TypeNameHandling.Arrays;
            return jsonNetSerializerSettings;
        }

        public static void LoadFromJson(string modName, Catalog.Category category)
        {
            if (String.IsNullOrEmpty(modName) || modName == "Default") return;
            var dirList = Directory.GetDirectories(ModConfiguration.streamingAssetsPath + "/" + ModConfiguration.current.modName + "/", "*", SearchOption.TopDirectoryOnly);
            List<string> dirStrList = new List<string>();
            dirStrList.AddRange(dirList);
            dirStrList.Add(ModConfiguration.streamingAssetsPath + "/" + ModConfiguration.current.modName);
            foreach (string text2 in dirStrList)
            {
                if (text2.Contains("Default")) return;
                string fileName = Path.GetFileName(text2);

                foreach (FileInfo fileInfo in new DirectoryInfo(text2 + "/").GetFiles(category.ToString() + "_*.json", SearchOption.AllDirectories))
                {

                    Debug.Log("JSON loader - Found file: " + fileInfo.Name);
                    string value = File.ReadAllText(fileInfo.FullName);
                    value = value.Replace("$type", "type");
                    var sub = Regex.Match(value, "\\\"type\\\":\\s+\\\"(\\w+\\.[\\w\\d]+, [\\w\\d-]+)\\\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                    string typeStr = sub.Groups[1].Value;

                    Type type = Type.GetType(typeStr);

                    if (type == null)
                    {
                        Debug.Log("Type is null, loading all DLL's inside mod directory ---");
                        var asm = LoadDLL();
                        foreach (var atype in asm.GetTypes())
                        {
                            if (atype.FullName.Contains("+")) continue;
                            var r = typeStr.Split('.')[1].Split(',')[0]; // FUCKING YOLO
                            if (atype.Name.Contains(r))
                            {
                                type = TypeHelper.GenerateMissingType(atype);
                                Debug.Log("Type " + r + " was found in " + asm.FullName + " successful");
                                break;
                            }
                        }
                        if (type == null)
                        {
                            Debug.LogWarning("Failed to find type... type is null, skipping...");
                            continue;
                        }
                    }
                    TypeHelper.GenerateMissingType(type);

                    var usedType = ModConfiguration.generatedTypes.Find(x => x.Name == "CatalogData");
                    try
                    {
                        CatalogData catalogData = Activator.CreateInstance(ModConfiguration.generatedTypes.Find(x => x.Name == type.Name)) as CatalogData;

                        catalogData = JsonConvert.DeserializeObject(value, usedType) as CatalogData;

                        if (catalogData != null)
                            ModConfiguration.data[(int)category].Add(catalogData);
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }

        private static Assembly LoadDLL()
        {
            var dirList = Directory.GetDirectories(ModConfiguration.streamingAssetsPath + "/" + ModConfiguration.current.modName + "/", "*", SearchOption.TopDirectoryOnly);
            List<string> dirStrList = new List<string>();
            dirStrList.AddRange(dirList);
            dirStrList.Add(ModConfiguration.streamingAssetsPath + "/" + ModConfiguration.current.modName);
            foreach (string text2 in dirStrList)
            {
                foreach (FileInfo fileInfo in new DirectoryInfo(text2 + "/").GetFiles("*.dll", SearchOption.TopDirectoryOnly))
                {
                    var asm = Assembly.LoadFrom(fileInfo.FullName);
                    return asm;
                }
            }
            return null;
        }
    }
}
