using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.IO;

namespace CoolMarketingSystem.FormLibrary
{
    /// <summary>
    /// Assembly helper class
    /// </summary>
    internal class AssemblyHelper
    {
		private static Assembly currentAssembly = Assembly.GetAssembly(typeof(AssemblyHelper));

		/// <summary>
		/// Get theme images which embeded in the assembly by the image name
		/// </summary>
		/// <param name="imageName"></param>
		/// <returns></returns>
		internal static Image GetThemeEmbedImage(string imageName)
		{
			string path = Theme.ThemeConfig.CurrentTheme.ThemeImageFullPath;

			var stream = currentAssembly.GetManifestResourceStream(path + "." + imageName);

			if (stream != null) {
				return Image.FromStream(stream);
			}

			return null;
		}

		/// <summary>
		/// Get Embeded image by the full name
		/// </summary>
		/// <param name="imageFullName"></param>
		/// <returns></returns>
		internal static Image GetEmbedImage(string imageFullName)
		{
			var stream = currentAssembly.GetManifestResourceStream(imageFullName);

			if (stream != null)
			{
				return Image.FromStream(stream);
			}

			return null;
		}

		/// <summary>
		/// Get the embed resource stream by the full path
		/// </summary>
		/// <param name="fileFullPath"></param>
		/// <returns></returns>
		internal static Stream GetEmbedResourceStream(string fileFullPath)
		{
			return currentAssembly.GetManifestResourceStream(fileFullPath);
		}
    }
}
