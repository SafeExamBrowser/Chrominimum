/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Configuration;
using System.Text.RegularExpressions;
using CommandLine;

namespace Chrominimum
{
	internal class AppSettings
	{
		[Option("allow-navigation", HelpText = "Determines whether the user may navigate the browser history.")]
		public bool AllowNavigation { get; set; }

		[Option("allow-reload", HelpText = "Determines whether the user may reload the current page.")]
		public bool AllowReload { get; set; }

		[Option("show-maximized", HelpText = "Determines whether the browser window will be maximized on startup.")]
		public bool ShowMaximized { get; set; }

		[Option("show-menu", HelpText = "Determines whether the user may access the browser menu.")]
		public bool ShowMenu { get; set; }

		[Option("start-url", HelpText = "The initial URL to be loaded on startup.")]
		public string StartUrl { get; set; }

		[Option("store-cache", HelpText = "Stores the browser cache on disk.")]
		public bool StoreCache { get; set; }

		[Option("user-agent-suffix", HelpText = "A suffix to be appended to the user agent of the browser.")]
		public string UserAgentSuffix { get; set; }

		internal void Initialize()
		{
			StartUrl = IsValidStartUrl(StartUrl) ? StartUrl : ConfigurationManager.AppSettings["StartUrl"];
		}

		private bool IsValidStartUrl(string value)
		{
			var valid = false;

			if (!string.IsNullOrWhiteSpace(value))
			{
				valid |= Regex.IsMatch(value, @".+\..+");
				valid |= Regex.IsMatch(value, @".+\..+\..+");
				valid |= Uri.IsWellFormedUriString(value, UriKind.Absolute);
			}

			return valid;
		}
	}
}
