/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;

namespace Chrominimum
{
	public class Options
	{
		[Option("allow-navigation", Default=false, Required = false)]
		public bool AllowNavigation { get; set; }

		[Option("disable-reload", Default=false, Required = false)]
		public bool DisableReload { get; set; }

		[Option("maximized", Default=false, Required = false)]
		public bool ShowMaximized { get; set; }

		[Option("hide-menu", Default=false, Required = false)]
		public bool HideMenu { get; set; }

		[Option("logdir", Required = false)]
		public string LogDir { get; set; }

		[Option("quitPasswordHash", Required = false)]
		public string QuitPasswordHash { get; set; }

		[Value(0)]
		public string ProgramName { get; set; }

		[Value(1)]
		public string StartUrl { get; set; }
	}

	internal class AppSettings
	{
		internal bool AllowReload { get; set; }
		internal bool AllowNavigation { get; set; }
		internal bool ShowMaximized { get; set; }
		internal bool ShowMenu { get; set; }
		internal string StartUrl { get; set; }
		internal string LogDir { get; set; }
		internal DateTime StartTime { get; set; }
		internal string QuitPasswordHash { get; set; }

		internal const string AppName = "SEBLight";

		internal void Initialize()
		{
			var appDataLocalFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), AppName);
			// StartTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds().ToString();
			StartTime = DateTime.Now;

			var result = Parser.Default.ParseArguments<Options>(Environment.GetCommandLineArgs())
				.WithParsed(options => {
						AllowNavigation = options.AllowNavigation;
						AllowReload = !options.DisableReload;
						ShowMenu = !options.HideMenu;
						ShowMaximized = options.ShowMaximized;
						StartUrl = !String.IsNullOrEmpty(options.StartUrl) && IsValidStartUrl(options.StartUrl)
							? options.StartUrl
							: ConfigurationManager.AppSettings["StartUrl"];
						LogDir = !String.IsNullOrEmpty(options.LogDir)
							? options.LogDir
							: Path.Combine(appDataLocalFolder, "Logs");
						QuitPasswordHash = options.QuitPasswordHash;
					});

			var args = Environment.GetCommandLineArgs();
		}

		private bool IsValidStartUrl(string value)
		{
			var valid = false;

			valid |= Regex.IsMatch(value, @".+\..+");
			valid |= Regex.IsMatch(value, @".+\..+\..+");
			valid |= Uri.IsWellFormedUriString(value, UriKind.Absolute);

			return valid;
		}
	}
}
