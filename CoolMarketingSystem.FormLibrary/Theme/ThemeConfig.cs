using System;
using System.Collections.Generic;
using System.Text;

namespace CoolMarketingSystem.FormLibrary.Theme
{
    public class ThemeConfig
    {
        private static ThemeBase currentTheme = null;

        public static ThemeBase CurrentTheme
        {
            get 
            {
                if (currentTheme == null)
                    SetTheme(Themes.Blue);
                return currentTheme; 
            }
        }

        /// <summary>
        /// 创建主题
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public static void SetTheme(Themes theme)
        {
            switch (theme)
            {
                case Themes .Blue :
                    currentTheme = new BlueTheme();
                    break;
                case Themes .Orance :
                    break;
                case Themes .Red :
                    break;
				case Themes.Black:
					currentTheme = new BlackTheme();
					break;
				default:
					currentTheme = new BlackTheme();
					break;
            }

			currentTheme.LoadThemeImageInformation(currentTheme.ThemeImages);
        }

		/// <summary>
		/// Set the theme
		/// </summary>
		/// <param name="theme"></param>
		public static void SetTheme(ThemeBase theme)
		{
			currentTheme = theme;
		}

        /// <summary>
        /// 主题类型
        /// </summary>
        public enum Themes
        {
            Blue,
            Red,
            Orance,
			Black
        }
    }
}
