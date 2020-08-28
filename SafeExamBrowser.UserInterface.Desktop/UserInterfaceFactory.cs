/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using FontAwesome.WPF;
using SafeExamBrowser.Applications.Contracts;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Settings.Browser;
using SafeExamBrowser.SystemComponents.Contracts.Audio;
using SafeExamBrowser.SystemComponents.Contracts.Keyboard;
using SafeExamBrowser.SystemComponents.Contracts.PowerSupply;
using SafeExamBrowser.SystemComponents.Contracts.WirelessNetwork;
using SafeExamBrowser.UserInterface.Contracts;
using SafeExamBrowser.UserInterface.Contracts.Browser;
using SafeExamBrowser.UserInterface.Contracts.Shell;
using SafeExamBrowser.UserInterface.Contracts.Windows;
using SafeExamBrowser.UserInterface.Contracts.Windows.Data;
using SafeExamBrowser.UserInterface.Desktop.Windows;

namespace SafeExamBrowser.UserInterface.Desktop
{
	public class UserInterfaceFactory : IUserInterfaceFactory
	{
		private IText text;

		public UserInterfaceFactory(IText text)
		{
			this.text = text;

			InitializeFontAwesome();
		}

		public ISystemControl CreateAudioControl(IAudio audio, Location location)
		{
			return new Controls.Taskbar.AudioControl(audio, text);
		}

		public ISystemControl CreateKeyboardLayoutControl(IKeyboard keyboard, Location location)
		{
			return new Controls.Taskbar.KeyboardLayoutControl(keyboard, text);
		}

		public ILockScreen CreateLockScreen(string message, string title, IEnumerable<LockScreenOption> options)
		{
			return Application.Current.Dispatcher.Invoke(() => new LockScreen(message, title, text, options));
		}

		public IWindow CreateLogWindow(ILogger logger)
		{
			var window = default(LogWindow);
			var windowReadyEvent = new AutoResetEvent(false);
			var windowThread = new Thread(() =>
			{
				window = new LogWindow(logger, text);
				window.Closed += (o, args) => window.Dispatcher.InvokeShutdown();
				window.Show();

				windowReadyEvent.Set();

				System.Windows.Threading.Dispatcher.Run();
			});

			windowThread.SetApartmentState(ApartmentState.STA);
			windowThread.IsBackground = true;
			windowThread.Start();

			windowReadyEvent.WaitOne();

			return window;
		}

		public IPasswordDialog CreatePasswordDialog(string message, string title)
		{
			return Application.Current.Dispatcher.Invoke(() => new PasswordDialog(message, title, text));
		}

		public IPasswordDialog CreatePasswordDialog(TextKey message, TextKey title)
		{
			return Application.Current.Dispatcher.Invoke(() => new PasswordDialog(text.Get(message), text.Get(title), text));
		}

		public ISystemControl CreatePowerSupplyControl(IPowerSupply powerSupply, Location location)
		{
			return new Controls.Taskbar.PowerSupplyControl(powerSupply, text);
		}

		public ITaskbar CreateTaskbar(ILogger logger)
		{
			return new Taskbar(logger);
		}

		// public ITaskview CreateTaskview()
		// {
		// 	return new Taskview();
		// }

		public ISystemControl CreateWirelessNetworkControl(IWirelessAdapter wirelessAdapter, Location location)
		{
			return new Controls.Taskbar.WirelessNetworkControl(wirelessAdapter, text);
		}

		private void InitializeFontAwesome()
		{
			// To be able to use FontAwesome in XAML icon resources, we need to make sure that the FontAwesome.WPF assembly is loaded into
			// the AppDomain before attempting to load an icon resource - thus the creation of an unused image below...
			ImageAwesome.CreateImageSource(FontAwesomeIcon.FontAwesome, Brushes.Black);
		}
	}
}
