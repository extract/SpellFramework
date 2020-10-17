using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpellFramework
{
    public static class TypeHelper
    {
        public static bool firstTimeRun = false;
        public static Type GenerateMissingType(Type type)
        {
            if (!firstTimeRun)
            {
                FirstTimeMock();
                firstTimeRun = true;
            }
            if (ModConfiguration.generatedTypes.Exists(x => x.Name == type.Name)) return ModConfiguration.generatedTypes.Find(x => x.Name == type.Name);

            FieldInfo[] fields;
            Type parent;
            List<Type> parentTypes = GetParentTypes(type).ToList();
            for (int i = parentTypes.Count - 1; i >= 0; i--)
            {
                if (ModConfiguration.generatedTypes.Exists(x => x.Name == parentTypes[i].Name)) continue;
                fields = parentTypes[i].GetFields();
                if (i != parentTypes.Count - 1)
                {
                    parent = ModConfiguration.generatedTypes.Find(x => x.Name == parentTypes[i + 1].Name);
                }
                else
                {
                    parent = default;
                }
                if (parent == default) parent = null;
                ModConfiguration.generatedTypes.Add(MyTypeBuilder.CompileResultType(fields.ToList(), parentTypes[i].Name, parent));

            }
            fields = type.GetFields();
            parent = ModConfiguration.generatedTypes.Find(x => x.Name == parentTypes[0].Name);
            if (parent == default) parent = null;
            var myType = MyTypeBuilder.CompileResultType(fields.ToList(), type.Name, parent);
            ModConfiguration.generatedTypes.Add(myType);
            return myType;
        }

        private static void FirstTimeMock()
        {
            ModConfiguration.generatedTypes.Add(typeof(CatalogData));
            //ModConfiguration.generatedTypes.Add(typeof(MaterialData));
            //ModConfiguration.generatedTypes.Add(typeof(MaterialData.Collision));
            //ModConfiguration.generatedTypes.Add(typeof(SpellCastData));
            //ModConfiguration.generatedTypes.Add(typeof(SpellCastCharge));
            ModConfiguration.generatedTypes.Add(typeof(SpellCastProjectile));

            //ModConfiguration.generatedTypes.Add(typeof(CreatureData));
            //ModConfiguration.generatedTypes.Add(typeof(CreatureData.Sex));
            //ModConfiguration.generatedTypes.Add(typeof(CreatureData.Holder));
        }

        public static void GenerateTypes(Type type)
        {
            GenerateMissingType(type);
        }
        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            // is there any base type?
            if (type == null)
            {
                yield break;
            }

            // return all implemented or inherited interfaces
            foreach (var i in type.GetInterfaces())
            {
                yield return i;
            }

            // return all inherited types
            var currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;
                currentBaseType = currentBaseType.BaseType;
            }
        }
    }
}
