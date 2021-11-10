using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace PhanTichChungKhoan.WebApp.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string ListBrowsers { get; set; }

        public void OnGet()
        {
            List<string> viewers = new List<string>();
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                RegistryKey webClientsRootKey = hklm.OpenSubKey(@"SOFTWARE\Clients\StartMenuInternet");
                if (webClientsRootKey != null)
                    foreach (var subKeyName in webClientsRootKey.GetSubKeyNames())
                        if (webClientsRootKey.OpenSubKey(subKeyName) != null)
                            if (webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell") != null)
                                if (webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell").OpenSubKey("open") != null)
                                    if (webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command") != null)
                                    {
                                        string commandLineUri = (string)webClientsRootKey.OpenSubKey(subKeyName).OpenSubKey("shell").OpenSubKey("open").OpenSubKey("command").GetValue(null);
                                        if (string.IsNullOrEmpty(commandLineUri))
                                            continue;
                                        commandLineUri = commandLineUri.Trim("\"".ToCharArray());
                                        //ViewerApplication viewer = new ViewerApplication();
                                        //viewer.Executable = commandLineUri;
                                        //viewer.Name = (string)webClientsRootKey.OpenSubKey(subKeyName).GetValue(null);
                                        //viewers.Add(viewer);
                                        viewers.Add((string)webClientsRootKey.OpenSubKey(subKeyName).GetValue(null));
                                    }
            }

            if (viewers.Count > 0)
                ListBrowsers = string.Join(" | ", viewers);
        }

        public void OnGetClearProcess()
        {
            var listProc = Process.GetProcesses();
            foreach (var proc in listProc)
            {
                if (proc.ProcessName.Equals("chromedriver") || proc.ProcessName.Equals("chrome")
                    || proc.ProcessName.Equals("msedge") || proc.ProcessName.Equals("MicrosoftWebDriver"))
                {
                    proc.Kill();
                }
            }
            
        }
    }
}
