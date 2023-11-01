using ScanFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanFile
{
    public class WriteToConsole
    {
        static string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
        static string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        static string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
        public static async Task WriteResult(HashResponse hashResponse)
        {
            Console.WriteLine($"{RED}SAMPLE OUTPUT:");
            Console.WriteLine($"{RED}Filename: " + GREEN + hashResponse.file_info.display_name);
            Console.WriteLine($"{RED}OverallStatus: " + GREEN + hashResponse.scan_results.scan_all_result_a);
            Console.WriteLine();
            foreach (var item in hashResponse.scan_results.scan_details)
            {
                var threat = string.IsNullOrWhiteSpace(item.Value.threat_found) ? "Clean" : item.Value.threat_found;
                Console.WriteLine($"{RED}Engine: " + GREEN + item.Key);
                Console.WriteLine($"{RED}ThreatFound: " + GREEN + threat);
                Console.WriteLine($"{RED}ScanResult: " + Convert.ToInt32(item.Value.scan_result_i));
                Console.WriteLine($"{RED}DefTime: " + item.Value.def_time);
                Console.WriteLine();
            }
            Console.WriteLine($"{GREEN}END");
            Console.WriteLine($"{NORMAL}Press enter to exit!");
            var quit = Console.ReadLine();
        }

        public static async Task WriteLine(string color, string message)
        {
            Console.WriteLine(color + message);
        }
    }
}
