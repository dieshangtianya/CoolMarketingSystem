using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolMarketingSystem.FormLibrary.Theme
{
	public class BlackTheme:ThemeBase
	{
		public BlackTheme()
		{
			this.ThemeImageFullPath = "CoolMarketingSystem.FormLibrary.Theme.ThemeImages.Black";

			this.LabelColor = Color.DarkCyan;

			this.LinkColor = Color.FromArgb(0, 120, 255);

			this.ShortCutDefaultColor = Color.White;

			this.ShortCutHoverColor = Color.WhiteSmoke;

			this.MenuDefaultColor = Color.FromArgb(68, 88, 82);

			this.MenuSelectedColor = Color.White;

			this.ShortCutItemBorderColor = Color.FromArgb(16, 141, 105);

			this.ButtonBackgroundColor = Color.FromArgb(120, 186, 231);

			this.ButtonForeColor = Color.FromArgb(42, 42, 42);
		}
	}
}
