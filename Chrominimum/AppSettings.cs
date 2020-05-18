/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Chrominimum
{
	internal class AppSettings
	{
		internal bool AllowReload { get; set; }
		internal bool AllowNavigation { get; set; }
		internal bool ShowMaximized { get; set; }
		internal bool ShowMenu { get; set; }
		internal string StartUrl { get; set; }

		internal void Initialize()
		{
			var args = Environment.GetCommandLineArgs();

			AllowNavigation = args.Any(a => a.Equals("allow-navigation"));
			AllowReload = args.All(a => !a.Equals("disable-reload"));
			ShowMaximized = args.Any(a => a.Equals("maximized"));
			ShowMenu = args.All(a => !a.Equals("hide-menu"));
			StartUrl = args.Length > 1 && IsValidStartUrl(args[1]) ? args[1] : ConfigurationManager.AppSettings["StartUrl"];
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
