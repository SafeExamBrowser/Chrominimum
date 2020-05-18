/*
 * Copyright (c) 2020 ETH Zürich, Educational Development and Technology (LET)
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Chrominimum
{
	internal partial class VersionWindow : Form
	{
		internal VersionWindow()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			var executable = Assembly.GetExecutingAssembly();
			var build = FileVersionInfo.GetVersionInfo(executable.Location).FileVersion;
			var copyright = executable.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
			var title = executable.GetCustomAttribute<AssemblyTitleAttribute>().Title;
			var version = executable.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
			var logo = new Bitmap(executable.GetManifestResourceStream($"{nameof(Chrominimum)}.Logo.png"));

			BuildLabel.Text = $"Build {build}";
			CopyrightLabel.Text = copyright;
			TitleLabel.Text = $"{title} {version}";
			LogoPictureBox.Image = logo;
			LogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
		}

		private void CloseButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
