/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Chrominimum.Handlers;

namespace Chrominimum
{
	internal partial class MainWindow : Form
	{
		private AppSettings settings;
		private ChromiumWebBrowser browser;

		internal MainWindow(AppSettings settings)
		{
			this.settings = settings;

			InitializeComponent();
		}

		private const int CP_NOCLOSE_BUTTON = 0x200;

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ClassStyle |= CP_NOCLOSE_BUTTON;
				return cp;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			InitializeBrowser();
			InitializeMenu();
			InitializeWindow();
		}

		private void InitializeBrowser()
		{
			browser = new ChromiumWebBrowser(settings.StartUrl)
			{
				Dock = DockStyle.Fill
			};

			browser.DisplayHandler = new DisplayHandler(this);
			browser.KeyboardHandler = new KeyboardHandler();
			browser.LifeSpanHandler = new LifeSpanHandler();
			browser.LoadError += Browser_LoadError;
			browser.MenuHandler = new ContextMenuHandler();
			browser.TitleChanged += Browser_TitleChanged;

			Controls.Add(browser);
		}

		private void InitializeMenu()
		{
			if (settings.ShowMenu)
			{
				var actions = new MenuItem("Actions");
				var help = new MenuItem("Help");

				Menu = new MainMenu();

				if (settings.AllowNavigation)
				{
					actions.MenuItems.Add("Navigate backwards", (o, args) => browser.GetBrowser().GoBack());
					actions.MenuItems.Add("Navigate forwards", (o, args) => browser.GetBrowser().GoForward());
				}

				if (settings.AllowReload)
				{
					actions.MenuItems.Add("Reload", (o, args) => browser.GetBrowser().Reload());
				}

				help.MenuItems.Add("Show version", (o, args) => ShowVersion());

				if (settings.AllowNavigation || settings.AllowReload)
				{
					Menu.MenuItems.Add(actions);
				}

				Menu.MenuItems.Add(help);
			}
		}

		private void InitializeWindow()
		{
			if (settings.ShowMaximized)
			{
				WindowState = FormWindowState.Maximized;
			}
			else
			{
				Height = Screen.PrimaryScreen.WorkingArea.Height;
				Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2, Screen.PrimaryScreen.WorkingArea.Top);
				Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
			}
		}

		private void ShowVersion()
		{
			new VersionWindow().ShowDialog(this);
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
