using ScanFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScanFile
{
    public class APIClient
    {
        private const string hashUri = "https://api.metadefender.com/v4/hash/";
        private const string fileUri = "https://api.metadefender.com/v4/file/";
        public static async Task<string> HashLookup(string sha256, string apiKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("apikey", apiKey);
                var response = await client.GetAsync(hashUri + sha256);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> AnalyzeFile(string apiKey, string fileUrl)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(fileUri),
                Headers =
                {
                    { "apikey", apiKey },
                    { "filename", fileUrl }
                },
                Content = new StringContent(fileUrl, Encoding.UTF8, $"application/octet-stream")
            };
            using (var response = await client.SendAsync(request))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> FetchFileResult(string apiKey, string dataId)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(fileUri + dataId),
                Headers =
                {
                    { "apikey", apiKey },
                    { "x-file-metadata", "1" }
                }
            };
            using (var response = await client.SendAsync(request)) 
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
