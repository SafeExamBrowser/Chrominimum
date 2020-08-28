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

using SafeExamBrowser.I18n;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Logging;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.UserInterface.Contracts;
using SafeExamBrowser.UserInterface.Contracts.Shell;
using SafeExamBrowser.UserInterface.Desktop;

using SafeExamBrowser.Settings.SystemComponents;
using SafeExamBrowser.SystemComponents.Audio;
using SafeExamBrowser.SystemComponents.Keyboard;
using SafeExamBrowser.SystemComponents.PowerSupply;
using SafeExamBrowser.SystemComponents.WirelessNetwork;


namespace Chrominimum
{

	internal class SEBContext : ApplicationContext
	{
		private MainWindow browser;
		private ILogger logger;
		private IText text;

		private IUserInterfaceFactory uiFactory;
		private ITaskbar taskbar;

		internal SEBContext(AppSettings settings)
		{
				logger = new Logger();
				InitializeLogging(settings);
				InitializeText();

				uiFactory = new UserInterfaceFactory(text);
				taskbar = uiFactory.CreateTaskbar(logger);
				taskbar.Show();

				var audioSettings = new AudioSettings();
				var audio = new Audio(audioSettings, new ModuleLogger(logger, nameof(Audio)));
				taskbar.AddSystemControl(uiFactory.CreateAudioControl(audio, Location.Taskbar));

				var keyboard = new Keyboard(new ModuleLogger(logger, nameof(Keyboard)));
				taskbar.AddSystemControl(uiFactory.CreateKeyboardLayoutControl(keyboard, Location.Taskbar));

				var powerSupply = new PowerSupply(new ModuleLogger(logger, nameof(PowerSupply)));
				taskbar.AddSystemControl(uiFactory.CreatePowerSupplyControl(powerSupply, Location.Taskbar));

				var wirelessAdapter = new WirelessAdapter(new ModuleLogger(logger, nameof(WirelessAdapter)));
				taskbar.AddSystemControl(uiFactory.CreateWirelessNetworkControl(wirelessAdapter, Location.Taskbar));

				browser = new MainWindow(settings);
				browser.Show();
				browser.Closed += new EventHandler(OnBrowserClosed);
		}

		private void OnBrowserClosed(object sender, EventArgs e)
		{
			ExitThread();
		}

		private void InitializeLogging(AppSettings settings)
		{
			var logFilePrefix = settings.StartTime.ToString("yyyy-MM-dd\\_HH\\hmm\\mss\\s");
			var runtimeLog = Path.Combine(settings.LogDir, $"{logFilePrefix}_Runtime.log");
			var logFileWriter = new LogFileWriter(new DefaultLogFormatter(), runtimeLog);

			logFileWriter.Initialize();
			logger.Subscribe(logFileWriter);
		}

		private void InitializeText()
		{
			text = new Text(new ModuleLogger(logger, nameof(Text)));
		}

	}

	public static class Program
	{
		[STAThread]
		public static void Main()
		{
			var appSettings = new AppSettings();
			var cefSettings = new CefSettings();

			appSettings.Initialize();
			cefSettings.CefCommandLineArgs.Add("enable-media-stream");

			var success = Cef.Initialize(cefSettings, true, default(IApp));

			if (success)
			{
				string s = System.IO.Packaging.PackUriHelper.UriSchemePack;

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new SEBContext(appSettings));
			}
			else
			{
				MessageBox.Show("Failed to initialize the browser engine!", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			Cef.Shutdown();
		}
	}
}
