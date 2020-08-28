/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.SystemComponents.Contracts.Audio;
using SafeExamBrowser.SystemComponents.Contracts.Keyboard;
using SafeExamBrowser.SystemComponents.Contracts.PowerSupply;
using SafeExamBrowser.SystemComponents.Contracts.WirelessNetwork;
using SafeExamBrowser.UserInterface.Contracts.Browser;
using SafeExamBrowser.UserInterface.Contracts.Shell;
using SafeExamBrowser.UserInterface.Contracts.Windows.Data;
using SafeExamBrowser.UserInterface.Contracts.Windows;

namespace SafeExamBrowser.UserInterface.Contracts
{
	/// <summary>
	/// The factory for user interface elements which cannot be instantiated at the composition root.
	/// </summary>
	public interface IUserInterfaceFactory
	{
		/// <summary>
		/// Creates a system control which allows to change the audio settings of the computer.
		/// </summary>
		ISystemControl CreateAudioControl(IAudio audio, Location location);

		/// <summary>
		/// Creates a system control which allows to change the keyboard layout of the computer.
		/// </summary>
		ISystemControl CreateKeyboardLayoutControl(IKeyboard keyboard, Location location);

		/// <summary>
		/// Creates a lock screen with the given message, title and options.
		/// </summary>
		ILockScreen CreateLockScreen(string message, string title, IEnumerable<LockScreenOption> options);

		/// <summary>
		/// Creates a new log window which runs on its own thread.
		/// </summary>
		IWindow CreateLogWindow(ILogger logger);

		/// <summary>
		/// Creates a password dialog with the given message and title.
		/// </summary>
		IPasswordDialog CreatePasswordDialog(string message, string title);

		/// <summary>
		/// Creates a password dialog with the given message and title.
		/// </summary>
		IPasswordDialog CreatePasswordDialog(TextKey message, TextKey title);

		/// <summary>
		/// Creates a system control displaying the power supply status of the computer.
		/// </summary>
		ISystemControl CreatePowerSupplyControl(IPowerSupply powerSupply, Location location);

		// /// <summary>
		// /// Creates a new splash screen which runs on its own thread.
		// /// </summary>
		// ISplashScreen CreateSplashScreen(AppConfig appConfig = null);

		/// <summary>
		/// Creates a new taskbar.
		/// </summary>
		ITaskbar CreateTaskbar(ILogger logger);

		// /// <summary>
		// /// Creates a new taskview.
		// /// </summary>
		// ITaskview CreateTaskview();

		/// <summary>
		/// Creates a system control which allows to change the wireless network connection of the computer.
		/// </summary>
		ISystemControl CreateWirelessNetworkControl(IWirelessAdapter wirelessAdapter, Location location);
	}
}
