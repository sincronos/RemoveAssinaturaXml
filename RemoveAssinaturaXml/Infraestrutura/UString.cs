using System.Text.RegularExpressions;

namespace RemoveAssinaturaXml.Infraestrutura
{
    public static class UString
    {

        public static string SoNumero(string campo)
        {
            string resultString = string.Empty;
            Regex regexObj = new Regex(@"[^\d]");
            resultString = regexObj.Replace(campo, "");
            return resultString;
        }
      
    }
}
