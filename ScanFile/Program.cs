using Newtonsoft.Json;
using ScanFile.Models;
using ScanFile;
using System.Security.Cryptography;
using System.Timers;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.IO.Enumeration;

//var fileUrl = @"D:\sn.txt";
string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";

Console.WriteLine($"{NORMAL}Paste API Key: ");
var apiKey = Console.ReadLine();
Console.WriteLine($"{NORMAL}Paste File Path: ");
var fileUrl = Console.ReadLine();
try
{
    using (FileStream fileStream = File.OpenRead(fileUrl))
    {
        SHA256 sha256 = SHA256.Create();
        byte[] hashValue = sha256.ComputeHash(fileStream);
        string stringSha256 = Convert.ToHexString(hashValue);


        var response = await APIClient.HashLookup(stringSha256, apiKey);
        var jsonResponse = JsonConvert.DeserializeObject<HashResponse>(response);

        if (jsonResponse.error == null)
        {
            await WriteToConsole.WriteResult(jsonResponse);
        }
        if (jsonResponse.error.code == "401006")
        {
            Console.WriteLine(jsonResponse.error.messages[0]);
            Console.WriteLine($"{NORMAL}Press enter to exit!");
            var quit = Console.ReadLine();
        }
        else
        {
            Console.WriteLine(jsonResponse.error.code + ": " + jsonResponse.error.messages[0]);
            var fileResponse = await APIClient.AnalyzeFile(apiKey, fileUrl);
            var jsonFileResponse = JsonConvert.DeserializeObject<FileResponse>(fileResponse);
            if (jsonFileResponse.status == "inqueue")
            {
                Console.WriteLine($"{NORMAL}File is in queue, waiting 10 seconds!");
                int s = 1;
                while (true)
                {
                    s = (s % 10) + 1;
                    Thread.Sleep(1000);
                    if (s == 10)
                    {
                        var fetchFileResult = await APIClient.FetchFileResult(apiKey, jsonFileResponse.data_id);
                        var jsonFetchFileResult = JsonConvert.DeserializeObject<HashResponse>(fetchFileResult);
                        if (jsonFetchFileResult.error == null)
                        {
                            await WriteToConsole.WriteResult(jsonFetchFileResult);
                            break;
                        }
                        Console.WriteLine($"{NORMAL}File is in queue, waiting 10 seconds!");
                    }
                }

            }
        }
    }
}
catch (Exception)
{
    Console.WriteLine($"{NORMAL}Please enter a valid file path. Format: Drive:/folder/../file.extension");
    Console.WriteLine($"{NORMAL}Press enter to exit!");
    var quit = Console.ReadLine();
}
