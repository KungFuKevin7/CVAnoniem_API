using System.Diagnostics;

namespace REST_API
{
    public class Redactor
    {
        public static string Redact(int userID, string toRedact) 
        {

            ProcessStartInfo proc = new ProcessStartInfo();

            proc.FileName = "C:\\Python310\\python.exe";
            proc.Arguments = $"C:\\Users\\kevin\\source\\repos\\REST_API\\REST_API\\PDFer.py " +
                             $"{userID} \"{toRedact}\"";


            proc.UseShellExecute = false;
            proc.RedirectStandardOutput = true;
            using (Process process = Process.Start(proc))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);
                }
            }
            return $"PDF-testopslag\\Censored{userID}.pdf";
        }
    }
}
