using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace SpellFramework
{

    public static class MyTypeBuilder
    {
        public static object CreateNewObject(List<FieldInfo> fields)
        {
            var myType = CompileResultType(fields, "MyDynamicType", null);
            return Activator.CreateInstance(myType);
        }
        public static Type CompileResultType(List<FieldInfo> fields, string typeSig, Type baseType)
        {
            TypeBuilder tb = GetTypeBuilder(typeSig);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            if (baseType != null)
            {
                tb.SetParent(baseType);
            }
            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            foreach (var field in fields)
            {
                Type fieldType = field.FieldType;
                Type generatedFieldType = null;
                var parentTypes = field.FieldType.GetParentTypes();
                bool isUnityType = false;
                if (field.FieldType.Name.Contains("Color"))
                {
                    continue;
                }
                foreach (var parentType in parentTypes)
                {
                    if (parentType.Namespace == "UnityEngine")
                    {
                        isUnityType = true;
                        break;
                    }
                }
                if (field.IsNotSerialized || !field.IsPublic) continue;
                if (field.FieldType.Namespace == "UnityEngine" ||
                    field.FieldType.IsEnum ||
                    field.FieldType.IsInterface ||
                    isUnityType)
                    continue;
                if (field.FieldType.Namespace == "System.Collections.Generic" &&
                    field.FieldType.FullName.Contains("UnityEngine"))
                {
                    continue;
                }

                if (field.FieldType.Namespace == "UnityEngine" && field.FieldType.Name.Contains("Data"))
                {
                    continue;
                }

                if (field.FieldType.Namespace == "ThunderRoad")
                {
                    if (!ModConfiguration.generatedTypes.Exists(x => x.Name == field.FieldType.Name))
                    {
                        generatedFieldType = TypeHelper.GenerateMissingType(field.FieldType);
                    }
                    else
                    {
                        generatedFieldType = ModConfiguration.generatedTypes.Find(x => x.Name == field.FieldType.Name);
                    }
                }
                else if (field.FieldType.FullName.Contains("ThunderRoad") && field.FieldType.Namespace == "System.Collections.Generic")
                {
                    //var declaringType = TypeHelper.GenerateMissingType(field.DeclaringType);
                    generatedFieldType = typeof(List<object>);

                }


                CreateProperty(tb, field.Name, generatedFieldType == null ? field.FieldType : generatedFieldType);
            }

            Type objectType = tb.CreateType();
            return objectType;
        }

        private static TypeBuilder GetTypeBuilder(string typeSig)
        {
            var typeSignature = typeSig;
            var an = new AssemblyName("SpellFramework." + typeSignature);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout,
                    null);
            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField(propertyName, propertyType, FieldAttributes.Public);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                    MethodAttributes.Public |
                    MethodAttributes.SpecialName |
                    MethodAttributes.HideBySig,
                    null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            //propertyBuilder.SetGetMethod(getPropMthdBldr);
            //propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }
}
