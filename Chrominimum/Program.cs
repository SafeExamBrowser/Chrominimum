/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using CommandLine;

namespace Chrominimum
{
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			var appSettings = new AppSettings();
			var cefSettings = new CefSettings();
			var osVersion = $"{Environment.OSVersion.Version.Major}.{Environment.OSVersion.Version.Minor}";

			Parser.Default.ParseArguments<AppSettings>(args).WithParsed(s => appSettings = s);

			appSettings.Initialize();

			if (appSettings.StoreCache)
			{
				cefSettings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nameof(Chrominimum), "Cache");
			}

			cefSettings.CefCommandLineArgs.Add("enable-media-stream");
			cefSettings.PersistSessionCookies = false;
			cefSettings.UserAgent = $"Mozilla/5.0 (Windows NT {osVersion}) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/{Cef.ChromiumVersion} {appSettings.UserAgentSuffix}";

			var success = Cef.Initialize(cefSettings, true, default(IApp));

			if (success)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWindow(appSettings));
			}
			else
			{
				MessageBox.Show("Failed to initialize the browser engine!", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			Cef.Shutdown();
		}
	}
}
