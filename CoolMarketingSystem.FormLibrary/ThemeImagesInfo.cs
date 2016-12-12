using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolMarketingSystem.FormLibrary
{
	/// <summary>
	/// Theme images information
	/// </summary>
	public class ThemeImagesInfo
	{
		#region Form border images

		/// <summary>
		/// Get or set the head border image of the form 
		/// </summary>
		public Image FormHeaderImage { get; set; }

		/// <summary>
		/// Get or set the bottom border image of the form
		/// </summary>
		public Image FormBottomImage { get; set; }

		/// <summary>
		/// Get or set the bottom border image (with the status bar) of the form
		/// </summary>
		public Image FormBottomWithStatusBarImage { get; set; }

		/// <summary>
		/// Get or set the left border image of the form
		/// </summary>
		public Image FormLeftBorderImage { get; set; }

		/// <summary>
		/// Get or set the right border image of the form
		/// </summary>
		public Image FormRightBorderImage { get; set; }

		#endregion

		#region Operation button images

		/// <summary>
		/// Get or set the minimize button image of the form
		/// </summary>
		public Image MinimizeButtonImage { get; set; }

		/// <summary>
		/// Get or set the minimize button image when the mouse hover on it
		/// </summary>
		public Image MinimizeButtonHoverImage { get; set; }

		/// <summary>
		/// Get or set the minimize button image of the form
		/// </summary>
		public Image MaximizeButtonImage { get; set; }

		/// <summary>
		/// Get or set the maximize button image when the mouse hover on it
		/// </summary>
		public Image MaximizeButtonHoverImage { get; set; }

		/// <summary>
		/// Get or set the close button image of the form
		/// </summary>
		public Image CloseButtonImage { get; set; }

		/// <summary>
		/// Get or set the close button image when the mouse hover on it
		/// </summary>
		public Image CloseButtonHoverImage { get; set; }

		/// <summary>
		/// Get or set the restore button image of the form
		/// </summary>
		public Image RestoreButtonImage { get; set; }

		/// <summary>
		/// Get or set the restore button image when the mouse hover on it
		/// </summary>
		public Image RestoreButtonHoverImage { get; set; }

		/// <summary>
		/// Get or set the menu button image of the form
		/// </summary>
		public Image MenuButtonImage { get; set; }

		/// <summary>
		/// Get or set the menu button image when the mouse hover on it
		/// </summary>
		public Image MenuButtonHoverImage { get; set; }

		#endregion
	}
}
