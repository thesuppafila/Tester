using System;
using System.Windows;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows.Documents;
using System.Text.RegularExpressions;

namespace Tester.Views
{
    /// <summary>
    /// Логика взаимодействия для DemoView.xaml
    /// </summary>
    public partial class DemoView : Window
    {
        public DemoView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string sourceCode1 = @"using System;
using System.Text.RegularExpressions;
public class Program {
    public int Test (string input) { return int.Parse(Regex.Match(" + "\"год 1928 год\", \"\\\\d+\").Value); " +
                    "\n}\n}";

            string text = new TextRange(codeBox.Document.ContentStart, codeBox.Document.ContentEnd).Text;
            text = text.Replace("\\", "\\\\");
            string sourceCode = @"using System;
                                using System.Text.RegularExpressions;
                public class Program {
                    public int Test (string input) { " +
            text +
        "\n}\n}";

            var compParms = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true
            };
            compParms.ReferencedAssemblies.Add("System.Dll");
            compParms.ReferencedAssemblies.Add("System.Core.Dll");
            compParms.ReferencedAssemblies.Add("System.Text.RegularExpressions.dll");
            compParms.ReferencedAssemblies.Add("System.Collections.dll");
            var csProvider = new CSharpCodeProvider();
            CompilerResults compilerResults =
                csProvider.CompileAssemblyFromSource(compParms, sourceCode);
            foreach (CompilerError err in compilerResults.Errors)
            {
                MessageBox.Show(err.ErrorText);
            }
            object typeInstance =
              compilerResults.CompiledAssembly.CreateInstance("Program");
            MethodInfo mi = typeInstance.GetType().GetMethod("Test");
            try
            {
                string ss = Regex.Match("год 1928 год", @"\d+").Value;
                int methodOutput = (int)mi.Invoke(typeInstance, new object[] { "год 1928 год" });
                string s = (methodOutput == 1928) ? "Верно" : "Неверно";
                MessageBox.Show(s);
            }
            catch (Exception ee)
            { }
        }
    }
}
