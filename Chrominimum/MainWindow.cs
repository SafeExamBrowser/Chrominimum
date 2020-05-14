/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Chrominimum.Handlers;

namespace Chrominimum
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			var url = LoadStartUrl();
			var browser = new ChromiumWebBrowser(url)
			{
				Dock = DockStyle.Fill
			};

			browser.DisplayHandler = new DisplayHandler(this);
			browser.KeyboardHandler = new KeyboardHandler();
			browser.LifeSpanHandler = new LifeSpanHandler();
			browser.LoadError += Browser_LoadError;
			browser.MenuHandler = new ContextMenuHandler();
			browser.TitleChanged += Browser_TitleChanged;

			Height = Screen.PrimaryScreen.WorkingArea.Height;
			Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Top);
			Width = Screen.PrimaryScreen.WorkingArea.Width / 2;

			Controls.Add(browser);
		}

		private string LoadStartUrl()
		{
			var configUrl = ConfigurationManager.AppSettings["StartUrl"];
			var commandLineArgs = Environment.GetCommandLineArgs();
			var commandLineUrl = commandLineArgs.Length > 1 ? commandLineArgs[1] : default(string);
			var startUrl = string.IsNullOrWhiteSpace(commandLineUrl) ? configUrl : commandLineUrl;

			return startUrl;
		}

		private void Browser_TitleChanged(object sender, TitleChangedEventArgs e)
		{
			Invoke(new Action(() => Text = e.Title));
		}

		private void Browser_LoadError(object sender, LoadErrorEventArgs e)
		{
			if (e.ErrorCode != CefErrorCode.None && e.ErrorCode != CefErrorCode.Aborted)
			{
				e.Frame.LoadHtml($"<html><body>Failed to load '{e.FailedUrl}'!<br />{e.ErrorText} ({e.ErrorCode})</body></html>");
			}
		}
	}
}
