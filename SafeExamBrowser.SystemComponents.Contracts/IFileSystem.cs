﻿/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace SafeExamBrowser.SystemComponents.Contracts
{
	/// <summary>
	/// Provides access to file system operations.
	/// </summary>
	public interface IFileSystem
	{
		/// <summary>
		/// Deletes the item at the given path, if it exists. Directories will be completely deleted, including all subdirectories and files.
		/// </summary>
		void Delete(string path);
	}
}
