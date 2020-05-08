/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Chrominimum
{
	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			var settings = new CefSettings();

			settings.CefCommandLineArgs.Add("enable-media-stream");

			var success = Cef.Initialize(settings, true, default(IApp));

			if (success)
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWindow());
			}
			else
			{
				MessageBox.Show("Failed to initialize the browser engine!", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			Cef.Shutdown();
		}
	}
}
