using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CoolMarketingSystem.FormLibrary
{
	/// <summary>
	/// Theme base class
	/// </summary>
    public abstract class ThemeBase
    {
        #region Properties

		#region Theme global properties

		/// <summary>
        /// Get or set the full path of the theme images
        /// </summary>
		internal string ThemeImageFullPath { get; set; }

        /// <summary>
        /// Get or set the theme font
        /// </summary>
		public Font ThemeFont { get; set; }

		#endregion

		#region Label theme properties

		/// <summary>
        /// Get or set the lable color
        /// </summary>
		public Color LabelColor { get; set; }

        /// <summary>
        /// Get or set the link text color
        /// </summary>
		public Color LinkColor { get; set; }

		#endregion

		#region Button theme properties

		/// <summary>
        /// Get or set the button backpground color
        /// </summary>
		public Color ButtonBackgroundColor { get; set; }

		/// <summary>
		/// Get or set the button forecolor
		/// </summary>
		public Color ButtonForeColor { get; set; }

		#endregion

		#region Shortcut theme properties
		/// <summary>
        /// Get or set the shortcut detfault color
        /// </summary>
		public Color ShortCutDefaultColor { get; set; }

        /// <summary>
        /// Get or set the shortcut hover color
        /// </summary>
		public Color ShortCutHoverColor { get; set; }

		/// <summary>
		/// Get or set the color of the shortcut item border
		/// </summary>
		public Color ShortCutItemBorderColor { get; set; }

		#endregion

		#region Menu theme properties
		/// <summary>
        /// Get or set the menu default color
        /// </summary>
		public Color MenuDefaultColor { get; set; }

        /// <summary>
        /// Get or set the color of the selected menu item
        /// </summary>
		public Color MenuSelectedColor { get; set; }

		#endregion

		#region Theme images properties
		public ThemeImagesInfo ThemeImages
		{
			get;
			private set;
		}
		#endregion

		#endregion

		#region Constructor

		public ThemeBase()
        {
			this.ThemeImages = new ThemeImagesInfo();
			this.ThemeFont = new Font("Microsoft YaHei ", 10.0f, FontStyle.Regular, GraphicsUnit.Point);
        }

        #endregion

		#region Virtual methods
		public virtual void LoadThemeImageInformation(ThemeImagesInfo themeImagesInfo)
		{
			//header image
			themeImagesInfo.FormHeaderImage = AssemblyHelper.GetThemeEmbedImage("common_title_bg.gif");

			//bottom image
			themeImagesInfo.FormBottomImage = AssemblyHelper.GetThemeEmbedImage("common_bottom_bg.gif");

			//middle border images
			themeImagesInfo.FormLeftBorderImage = AssemblyHelper.GetThemeEmbedImage("bordderLeft.gif");
			themeImagesInfo.FormRightBorderImage = AssemblyHelper.GetThemeEmbedImage("bordderLeft.gif");

			//operation button images
			themeImagesInfo.MinimizeButtonImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_min.gif");
			themeImagesInfo.MinimizeButtonHoverImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_min_hover.gif");

			themeImagesInfo.MaximizeButtonImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_max.gif");
			themeImagesInfo.MaximizeButtonHoverImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_max_hover.gif");

			themeImagesInfo.CloseButtonImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_close.gif");
			themeImagesInfo.CloseButtonHoverImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_close_hover.gif");

			themeImagesInfo.RestoreButtonImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_restore.gif");
			themeImagesInfo.RestoreButtonHoverImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_restore_hover.gif");

			themeImagesInfo.MenuButtonImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_menu.gif");
			themeImagesInfo.MenuButtonHoverImage = AssemblyHelper.GetThemeEmbedImage("skin_btn_menu_hover.gif");
		}
		#endregion
	}
}
