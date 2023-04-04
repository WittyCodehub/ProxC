using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ProxC;

class Program
{
    static void Main(string[] args)
    {
        void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Console.WriteLine("Windows");
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Console.WriteLine("Linux");
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Console.WriteLine("OSX");
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
        
        Console.Clear();
        Console.WriteLine(Constants.Logo);
        Console.WriteLine("ProxC - Cross Platform C# proxy");
        
        Console.Write("Enter URL: ");
        var url = Console.ReadLine();

        OpenBrowser(url);
    }
}