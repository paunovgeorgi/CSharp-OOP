using System.Reflection;

namespace Stealer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Spy spy = new Spy();

            //    Type type = typeof(Hacker);
            //    PropertyInfo propInfo = type.GetProperty("username");

            //    Console.WriteLine(propInfo.Name);

            //    string result = spy.StealFieldInfo("Stealer.Hacker", "username", "password");

            //    Console.WriteLine(result);
            //

          //string result = spy.AnalyzeAccessModifiers("Stealer.Hacker");

          string result = spy.GetAllMethods("Stealer.Hacker");
          Console.WriteLine(result);

        }

    }
}