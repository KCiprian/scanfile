using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanFile.Models
{
    public class HashResponse
    {
        public int scan_result_history_length { get; set; }
        public bool? success { get; set; }
        public Error? error { get; set; }
        public ScanResults scan_results { get; set; }
        public FileInfo file_info { get; set; }
    }
    public class Error
    {
        public string code { get; set; }
        public string[] messages { get; set; }
    }

    public class ScanResults
    {
        public int scan_all_result_i { get; set; }
        public int current_av_result_i { get; set; }
        public DateTime start_time { get; set; }
        public int total_time { get; set; }
        public int total_avs { get; set; }
        public int total_detected_avs { get; set; }
        public int progress_percentage { get; set; }
        public string scan_all_result_a { get; set; }
        public string current_av_result_a { get; set; }

        public IDictionary<string, Scans> scan_details { get; set; }
    }

    public class Scans
    {
        public int scan_time { get; set; }
        public DateTime def_time { get; set; }
        public bool scan_result_i { get; set; }
        public string threat_found { get; set; }
    }

    public class FileInfo
    {
        public string display_name { get; set; }
    }
    
}
