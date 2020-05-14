/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Windows.Forms;
using CefSharp;

namespace Chrominimum.Handlers
{
	internal class KeyboardHandler : IKeyboardHandler
	{
		public bool OnKeyEvent(IWebBrowser webBrowser, IBrowser browser, KeyType type, int keyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
		{
			var handled = false;

			if (IsReloadShortcut(type, keyCode, modifiers))
			{
				webBrowser.Reload();
				handled = true;
			}

			return handled;
		}

		public bool OnPreKeyEvent(IWebBrowser webBrowser, IBrowser browser, KeyType type, int keyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
		{
			if (IsReloadShortcut(type, keyCode, modifiers))
			{
				isKeyboardShortcut = true;
			}

			return false;
		}

		private bool IsReloadShortcut(KeyType type, int keyCode, CefEventFlags modifiers)
		{
			var f5 = keyCode == (int) Keys.F5;
			var r = keyCode == (int) Keys.R;
			var ctrl = modifiers.HasFlag(CefEventFlags.ControlDown);

			return type == KeyType.KeyUp && (f5 || ctrl && r);
		}
	}
}
