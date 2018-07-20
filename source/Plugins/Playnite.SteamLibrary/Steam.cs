﻿using Microsoft.Win32;
using Playnite.SDK.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playnite.SteamLibrary
{
    public class Steam
    {
        public static string LoginUsersPath
        {
            get => Path.Combine(InstallationPath, "config", "loginusers.vdf");
        }

        public static string InstallationPath
        {
            get
            {
                using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam"))
                {
                    if (key != null)
                    {
                        return key.GetValue("SteamPath")?.ToString().Replace('/', '\\') ?? string.Empty;
                    }
                }

                return string.Empty;
            }
        }

        public static bool IsInstalled
        {
            get
            {
                if (string.IsNullOrEmpty(InstallationPath) || !Directory.Exists(InstallationPath))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static GameState GetAppState(int id)
        {
            var state = new GameState();
            var rootString = @"Software\Valve\Steam\Apps\" + id.ToString();
            var root = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default);
            var appKey = root.OpenSubKey(rootString);
            if (appKey != null)
            {
                if (appKey.GetValue("Installed")?.ToString() == "1")
                {
                    state.Installed = true;
                }

                if (appKey.GetValue("Launching")?.ToString() == "1")
                {
                    state.Launching = true;
                }

                if (appKey.GetValue("Running")?.ToString() == "1")
                {
                    state.Running = true;
                }

                if (appKey.GetValue("Updating")?.ToString() == "1")
                {
                    state.Installing = true;
                }
            }

            return state;
        }
    }
}
