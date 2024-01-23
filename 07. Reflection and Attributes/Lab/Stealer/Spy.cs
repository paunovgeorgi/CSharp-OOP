using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string investigatedClass, params string[] requestedFields)
        {

            Type classType = Type.GetType(investigatedClass);

            FieldInfo[] classFields = classType.GetFields((BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public));
            StringBuilder sb = new();

            sb.AppendLine($"Class under investigation: {investigatedClass}");

            Object classInstance = Activator.CreateInstance(classType, new object[] { });

            foreach (FieldInfo field in classFields.Where(f=> requestedFields.Contains(f.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
            }

            return sb.ToString().TrimEnd();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            Type type = Type.GetType(className);
            FieldInfo[] fields = type.GetFields((BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public));

            MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] nonPublicMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            StringBuilder sb = new();



            foreach (FieldInfo field in fields)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (var nonPublicMethod in nonPublicMethods.Where(npm => npm.Name.StartsWith("get")))
            {
                sb.AppendLine($"{nonPublicMethod.Name} has to be public!");
            }

            foreach (var publicMethod in publicMethods.Where(pm => pm.Name.StartsWith("set")))
            {
                sb.AppendLine($"{publicMethod.Name} has to be private!");
            }

            return sb.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string className)
        {
            Type type = Type.GetType(className);

            MethodInfo[] privateMethods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

            StringBuilder sb = new();

            sb.AppendLine($"All Private Methods of Class: {className}");
            sb.AppendLine($"Base Class: {type.BaseType.Name}");

            foreach (var method in privateMethods)
            {
                sb.AppendLine(method.Name);
            }

            return sb.ToString();
        }

        public string GetAllMethods(string className)
        {
            StringBuilder sb = new();

            Type type = Type.GetType(className);

            MethodInfo[] methods = type.GetMethods((BindingFlags)60);

            foreach (var method in methods.Where(m=>m.Name.StartsWith("get")))
            {
                sb.AppendLine($"{method.Name} will return {method.ReturnType}");
            }
            foreach (var method in methods.Where(m => m.Name.StartsWith("set")))
            {
                sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
            }

            return sb.ToString().TrimEnd();
        }

    }
}
