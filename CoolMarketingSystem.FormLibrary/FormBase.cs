using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace CoolMarketingSystem.FormLibrary
{
	public partial class FormBase : Form
	{
		#region   Initialized variables

		#region Button horizontal coordinate
		//x-coordinate of the menu button
		private int formMenuButtonX = 0;

		//x-ordinate of the minimize button
		private int formMinButtonX = 0;

		//x-ordinate of the maximize button
		private int formMaxButtonX = 0;

		//x-ordinate of the close button
		private int formCloseButtonX = 0;

		#endregion

		#region Operation area width
		private int formOperationAreaWidth = 0;
		#endregion

		#region Images related

		Image imgTopBg; //image of the top header area
        Image imgBottonmMiddle;//image of the middle bottom of the form
		Image imgMiddleLeft;//image of the left border
		Image imgMiddleRight; //image of the right border

		#endregion

		#region Button controls(min,max,close,restore,menu)
		Image imgFormMin;//image of the minimize button
		Image imgFormMax;//image of the maximize button
		Image imgFormClose;//image of the close button
		Image imgFormRestore;//image of the restore button
		Image imgFormMenu;//image of the menu button

		#endregion

		#region Form icon
		private Image imgIcon;
		#endregion

		public ContextMenuStrip SystemMenu;

		private Font titleFont;

		private Panel panelStatus;

		#endregion

		#region Window constant message
		const int WM_NCHITTEST = 0x0084;
		const int HTLEFT = 10;//message indicate at the left of the window
		const int HTRIGHT = 11;//message indciates at the right of the window
		const int HTTOP = 12;//message indicates at the top of the window
		const int HTTOPLEFT = 13;//message indicates at the top left corner of the window
		const int HTTOPRIGHT = 14;//message indicates at the top right corner of the window
		const int HTBOTTOM = 15;//message indicates at the bottom of the window
		const int HTBOTTOMLEFT = 0x10;//message indicates at the bottom left corner of the window
		const int HTBOTTOMRIGHT = 17;//message indicates at the bottom right corner of the window
		#endregion

		#region mouse location

		private Point mouseOffset; //used to record mouse pointer location

		private bool isTitleMouseDown = false; //whether mouse is pressed

		#endregion

		#region Fields

		private bool showMenu = false;

		private bool showStatusStrip = false;

		private bool isShowIcon = true;

		#endregion

		#region Public properties
		/// <summary>
		/// Get or set wheather to show the menu button
		/// </summary>
		public bool ShowMenu
		{
			get { return showMenu; }
			set
			{
				showMenu = value;
				this.Invalidate();
			}
		}

		/// <summary>
		/// Get or set whether to show the status strip
		/// </summary>
		public bool ShowStatusStrip
		{
			get { return showStatusStrip; }
			set
			{
				showStatusStrip = value;
				//SwitchBottomImage();
				this.Invalidate();

				//SetPanelSize();

				//SetStatusPanelSize();
			}
		}

		/// <summary>
		/// Get the main panel
		/// </summary>
		public Panel MainPanel
		{
			get { return this.panelForm; }
		}

		/// <summary>
		/// Get the status panel
		/// </summary>
		public Panel StatusPanel
		{
			get { return this.panelStatus; }
		}

		/// <summary>
		/// Get or set the main panel size
		/// </summary>
		public Size MainPanelSize
		{
			get { return this.MainPanel.Size; }
			set
			{
				this.MainPanel.Size = value;
				//SetPanelSize();
			}
		}

		/// <summary>
		/// Get or set the form icon
		/// </summary>
		public new Icon Icon
		{
			get { return base.Icon; }
			set
			{
				base.Icon = value;
				if (value == null)
				{
					isShowIcon = false;
				}
			}
		}

		#endregion

		#region Constructor

		public FormBase()
		{
			InitializeComponent();

			this.panelForm.AutoScroll = true;

			panelStatus = new Panel();
			panelStatus.Width = 100;
			panelStatus.Height = 200;

			this.SystemMenu = new ContextMenuStrip();

			this.InitializeFormIcon();

			//Initialize the theme information
			InitializeThemeInformation();

            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.MouseDown += new MouseEventHandler(MainFormBase_MouseDown);
			this.MouseUp += new MouseEventHandler(MainFormBase_MouseUp);
			this.MouseMove += new MouseEventHandler(MainFormBase_MouseMove);
			this.MouseDoubleClick += new MouseEventHandler(MainFormBase_MouseDoubleClick);
			this.MouseClick += new MouseEventHandler(MainFormBase_MouseClick);

			//set the minimum size and maxmum size of the form
			this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
			this.MinimumSize = new Size(100, 100);

			this.Load += new EventHandler(FormBase_Load);
		}

		#endregion

		#region Initialize the form icon
		private void InitializeFormIcon()
		{
			//initialize the icon image
			var iconStream = AssemblyHelper.GetEmbedResourceStream("CoolMarketingSystem.FormLibrary.Images.icon.ico");
			if (iconStream != null)
			{
				this.Icon = new Icon(iconStream);
			}
		}
		#endregion

		#region Initialize theme information
		/// <summary>
		/// Initialize the theme related  resources
		/// </summary>
		private void InitializeThemeInformation()
		{
			ThemeImagesInfo themeImageInfo = Theme.ThemeConfig.CurrentTheme.ThemeImages;

			//form header image
			imgTopBg = themeImageInfo.FormHeaderImage;

			//form bottom image
			imgBottonmMiddle = themeImageInfo.FormBottomImage;

			//form left/right border image
			imgMiddleLeft = themeImageInfo.FormLeftBorderImage;
			imgMiddleRight = themeImageInfo.FormRightBorderImage;

			//form operation button image
			imgFormMin = themeImageInfo.MinimizeButtonImage;
			imgFormMax = themeImageInfo.MaximizeButtonImage;
			imgFormClose = themeImageInfo.CloseButtonImage;
			imgFormRestore = themeImageInfo.RestoreButtonImage;
			imgFormMenu = themeImageInfo.MenuButtonImage;


			titleFont = Theme.ThemeConfig.CurrentTheme.ThemeFont;
		}

		/// <summary>
		/// Toggle the form bottom border image
		/// </summary>
		private void SwitchBottomImage()
		{
			if (this.showStatusStrip)
			{
				imgBottonmMiddle = Theme.ThemeConfig.CurrentTheme.ThemeImages.FormBottomWithStatusBarImage;
			}
			else
			{
				imgBottonmMiddle = Theme.ThemeConfig.CurrentTheme.ThemeImages.FormBottomImage;
			}
		}

		#endregion

		#region Load event

		/// <summary>
		/// Load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FormBase_Load(object sender, EventArgs e)
		{
			SetPanelSize();

			SetStatusPanelSize();
		}

        #endregion

        #region Set the location and size of the main panel 

        /// <summary>
        /// Set the location and size of the main panel 
        /// </summary>
        private void SetPanelSize()
		{
			this.panelForm.Location = new Point(this.imgMiddleLeft.Width, this.imgTopBg.Height);
			this.panelForm.Size = new Size(this.Width - this.imgMiddleLeft.Width - this.imgMiddleRight.Width, this.Height - imgTopBg.Height - this.imgBottonmMiddle.Height);
			this.panelForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
		}

		/// <summary>
		/// Set the location and size of the status panel
		/// </summary>
		private void SetStatusPanelSize()
		{
			if (this.ShowStatusStrip)
			{
				this.panelStatus.Location = new Point(this.imgMiddleLeft.Width, this.Height - this.imgBottonmMiddle.Height + 1);
				this.panelStatus.Size = new Size(this.Width - this.imgMiddleLeft.Width - this.imgMiddleRight.Width, this.imgBottonmMiddle.Height - 3);
				this.panelStatus.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

				this.Controls.Add(panelStatus);
			}
			else
			{
				this.Controls.Remove(panelStatus);
			}
		}

		#endregion

		#region Repaint the form

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

			this.BackColor = SystemColors.Control;
			//get the graphic
			Graphics g = e.Graphics;
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

			PaintHeaderArea(g, e);//paint the header

			PaintTitleAndIcon(g, e);

			PaintBottomArea(g, e);//paint the bottom area

			PaintLeftRightBorder(g, e);//paint the left and right border
        
			PaintOperationButtons(g, e);//paint the operation button
		}

		/// <summary>
		/// Paint the top area of the form
		/// </summary>
		/// <param name="g"></param>
		/// <param name="e"></param>
		private void PaintHeaderArea(Graphics g, PaintEventArgs e)
		{
			if(this.imgTopBg!=null)
			{
				g.DrawImage(imgTopBg, 0f, 0f, e.ClipRectangle.Width, imgTopBg.Height);
				//g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, this.Width, 50));
			}
		}

		/// <summary>
		/// Paint the title and the icon
		/// </summary>
		/// <param name="g"></param>
		/// <param name="e"></param>
		private void PaintTitleAndIcon(Graphics g, PaintEventArgs e)
		{
			if(this.imgTopBg!=null)
			{
				PointF location;

				SizeF fontSize = g.MeasureString(this.Text, titleFont);

				if (isShowIcon)
				{
					this.imgIcon = this.Icon.ToBitmap();

					g.DrawImage(this.imgIcon, 10, (this.imgTopBg.Height - 24) / 2, 24, 24);

					location = new PointF(35, (this.imgTopBg.Height - fontSize.Height) / 2);
				}
				else
				{
					location = new PointF(10, (this.imgTopBg.Height - fontSize.Height) / 2);
				}

				g.DrawString(this.Text, this.titleFont, new SolidBrush(Color.White), location);
			}
		}

		/// <summary>
		/// Paint the left and right border
		/// </summary>
		/// <param name="g"></param>
		/// <param name="e"></param>
		private void PaintLeftRightBorder(Graphics g, PaintEventArgs e)
		{
			if (this.imgMiddleLeft != null && this.imgTopBg != null)
			{
				//draw the left border image
				g.DrawImage(imgMiddleLeft, 0, imgTopBg.Height, imgMiddleLeft.Width, this.Height - imgTopBg.Height);
			}

			if (this.imgMiddleRight != null && this.imgTopBg != null)
			{
				//draw the right border image
				g.DrawImage(imgMiddleRight, this.Width - imgMiddleRight.Width, imgTopBg.Height, imgMiddleRight.Width, this.Height - imgTopBg.Height);
			}
		}

		/// <summary>
		/// Paint the bottom Area
		/// </summary>
		/// <param name="g"></param>
		/// <param name="e"></param>
		private void PaintBottomArea(Graphics g, PaintEventArgs e)
		{
			if (this.imgBottonmMiddle != null)
			{
				g.DrawImage(imgBottonmMiddle, 0, this.Height - imgBottonmMiddle.Height, this.Width, imgBottonmMiddle.Height);
			}
		}

		/// <summary>
		/// Paint the operation buttons
		/// </summary>
		/// <param name="g"></param>
		/// <param name="e"></param>
		private void PaintOperationButtons(Graphics g, PaintEventArgs e)
		{
			if (this.ControlBox)
			{
				int TotalWidth = this.Width;

				this.formOperationAreaWidth = 0;

				if (this.ShowMenu)
				{
					this.formMenuButtonX = TotalWidth - imgFormClose.Width;

					if (this.MaximizeBox)
					{
						this.formMenuButtonX -= imgFormMax.Width;
					}
					if (this.MinimizeBox)
					{
						this.formMenuButtonX -= imgFormMin.Width;
					}
					this.formMenuButtonX -= imgFormMenu.Width;

					//draw the menu button image
					g.DrawImage(imgFormMenu, this.formMenuButtonX, 0, imgFormMenu.Width, imgFormMenu.Height);

					this.formOperationAreaWidth += this.imgFormMenu.Width;
				}

				if (this.MinimizeBox)
				{
					//draw the minimize button image
					this.formMinButtonX = TotalWidth - imgFormClose.Width;
					if (this.MaximizeBox)
					{
						this.formMinButtonX -= imgFormMax.Width;
					}
					this.formMinButtonX -= imgFormMin.Width;


					g.DrawImage(imgFormMin, this.formMinButtonX, 0, imgFormMin.Width, imgFormMin.Height);

					this.formOperationAreaWidth += this.imgFormMin.Width;
				}
				if (this.MaximizeBox)
				{
					//draw maximize button image
					this.formMaxButtonX = TotalWidth - imgFormMax.Width - imgFormClose.Width;
					if (this.WindowState == FormWindowState.Maximized)
					{
						g.DrawImage(imgFormRestore, this.formMaxButtonX, 0, imgFormRestore.Width, imgFormRestore.Height);
					}
					else
					{
						g.DrawImage(imgFormMax, formMaxButtonX, 0, imgFormMax.Width, imgFormMax.Height);
					}

					this.formOperationAreaWidth += imgFormMax.Width;
				}

				//draw the close button image
				this.formCloseButtonX = TotalWidth - imgFormClose.Width;
				g.DrawImage(imgFormClose, this.formCloseButtonX, 0, imgFormClose.Width, imgFormClose.Height);

				//set the operations area width
				this.formOperationAreaWidth += this.imgFormClose.Width;
			}
		}

		#endregion

		#region Repaint the form while the form size is changed

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.Invalidate();
		}
		#endregion

		#region Override the process method of the window messages

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			switch (m.Msg)
			{
				case WM_NCHITTEST:
					if ((this.WindowState == FormWindowState.Normal && this.MaximizeBox && this.ControlBox))
					{
						Point vPoint = new Point(m.LParam.ToInt32());
                        vPoint = PointToClient(vPoint);
                        if (vPoint.X <= 5)
                        {
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPLEFT;
                            else if (vPoint.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else m.Result = (IntPtr)HTLEFT;
                        }
                        else if (vPoint.X >= ClientSize.Width - 5)
                        {
                            if (vPoint.Y <= 5)
                                m.Result = (IntPtr)HTTOPRIGHT;
                            else if (vPoint.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                            else m.Result = (IntPtr)HTRIGHT;
                        }
                        else if (vPoint.Y <= 5)
                        {
                            m.Result = (IntPtr)HTTOP;
                        }
                        else if (vPoint.Y >= ClientSize.Height - 5)
                        {
                            m.Result = (IntPtr)HTBOTTOM;
                        }
                    }
					break;
			}

		}

		#endregion

		#region Implement the form movement when drag the form header

		/// <summary>
		/// Mouse move event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MainFormBase_MouseMove(object sender, MouseEventArgs e)
		{
            if (this.isTitleMouseDown)
            {
                Point tempPos = MousePosition;
                this.Location = new Point(Location.X + (tempPos.X - this.mouseOffset.X), Location.Y + (tempPos.Y - this.mouseOffset.Y));
                this.mouseOffset = MousePosition;
                this.Invalidate();
            }
            else
            {
                //mouse hover processors
                if (this.isMouseInOperPanel(e.X, e.Y))
                {
                    OperationButtonHover(e.X, e.Y);
                }
                else
                {
                    ResetOperImage();
                    PaintOperationButtons(this.CreateGraphics(), null);
                }
            }
        }

		/// <summary>
		/// Mouse up event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MainFormBase_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				this.isTitleMouseDown = false;
			}
		}

		/// <summary>
		/// Mouse down event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MainFormBase_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				//whether the mouse in the title area
				if (this.IsMouseInTitle(e.X, e.Y))
				{
					this.isTitleMouseDown = true;
					this.mouseOffset = MousePosition;//get the position
				}
			}
		}

		#endregion

		#region Determine whether the mouse hover at the title area
		/// <summary>
		/// Determine whether the mouse hover at the title area
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private bool IsMouseInTitle(int x, int y)
		{
			if ((x > 0 && x < this.Width - this.formOperationAreaWidth && y > 0 && y < this.imgTopBg.Height))
			{
				return true;
			}
			return false;
		}

		#endregion

		#region Determine whether the mouse hover at the operation area
		/// <summary>
		/// Determine whether the mouse hover at the operation area
		/// </summary>
		/// <param name="x">x-coordinate</param>
		/// <param name="y">y-coordinate</param>/param>
		/// <returns></returns>
		private bool isMouseInOperPanel(int x, int y)
		{
			if (x > this.Width - this.formOperationAreaWidth && x < this.Width
				&& y > 0 && y < this.imgFormClose.Height)
			{
				return true;
			}
			return false;
		}
		#endregion

		#region Double click to implement the maxisize and minisize the form

		void MainFormBase_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.ControlBox)
			{
				if (this.MaximizeBox)
				{
					if (e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						//determine whether the mouse in the title are
						if (IsMouseInTitle(e.X, e.Y))
						{
							//if the window status is maximized, then restore it
							if (this.WindowState == FormWindowState.Maximized)
							{
								this.WindowState = FormWindowState.Normal;
							}
							//if the window status is normal, the maximize it
							else if (this.WindowState == FormWindowState.Normal)
							{
								this.WindowState = FormWindowState.Maximized;
							}
						}
					}
				}
			}
		}

		#endregion

		#region When the mouse move on the operation button

		private void OperationButtonHover(int x, int y)
		{
			ResetOperImage();

			ThemeImagesInfo imagesInfo = Theme.ThemeConfig.CurrentTheme.ThemeImages;

			//if it is menu button
			if (x >= this.formMenuButtonX && x <= this.formMenuButtonX + this.imgFormMenu.Width
				&& y > 0 && y < this.imgFormMenu.Height)
			{
				if (ShowMenu)
				{
					//toggle to the menu button hover image
					this.imgFormMenu = imagesInfo.MenuButtonHoverImage;
				}
			}

			//if it is the minimize button
			else if (x >= this.formMinButtonX && x <= this.formMinButtonX + this.imgFormMin.Width
				&& y > 0 && y < this.imgFormMin.Height)
			{
				if (this.MinimizeBox)
				{
					//toggle to the minimize button hover image
					this.imgFormMin = imagesInfo.MinimizeButtonHoverImage;
				}
			}


			//if it is max or maximize button 
			else if (x >= this.formMaxButtonX && x <= this.formMaxButtonX + this.imgFormMax.Width
				&& y > 0 && y < this.imgFormMax.Width)
			{
				if (this.MaximizeBox)
				{
					if (this.WindowState == FormWindowState.Maximized)
					{
						//toggle to the restore button hover image
						imgFormRestore = imagesInfo.RestoreButtonHoverImage;
					}
					else
					{
						//toggle to the maximize button hover image
						imgFormMax = imagesInfo.MaximizeButtonHoverImage;
					}
				}
			}


			//if it is close button
			else if (x >= this.formCloseButtonX && x <= this.Width
				&& y > 0 && y < this.imgFormClose.Height)
			{
				//toggle to the close button hover image
				imgFormClose = imagesInfo.CloseButtonHoverImage;
			}

			PaintOperationButtons(this.CreateGraphics(), null);
		}
		#endregion

		#region Reset the operation theme images
		/// <summary>
		/// Reset the operation theme images
		/// </summary>
		private void ResetOperImage()
		{
			ThemeImagesInfo themeImageInfo = Theme.ThemeConfig.CurrentTheme.ThemeImages;

			imgFormMin = themeImageInfo.MinimizeButtonImage;
			imgFormMax = themeImageInfo.MaximizeButtonImage;
			imgFormClose = themeImageInfo.CloseButtonImage;
			imgFormRestore = themeImageInfo.RestoreButtonImage;
			imgFormMenu = themeImageInfo.MenuButtonImage;
		}

		#endregion

		#region Operation button events
		void MainFormBase_MouseClick(object sender, MouseEventArgs e)
		{
			//whether show the control box
			if (this.ControlBox)
			{
				//if show the menu
				if (this.ShowMenu)
				{
					if (e.X >= this.formMenuButtonX && e.X <= this.formMenuButtonX + this.imgFormMenu.Width && e.Y <= imgFormMenu.Height)
					{
						Point p = new Point(this.formMenuButtonX + imgFormMenu.Width - this.SystemMenu.Width, imgFormMenu.Height + 2);
						this.SystemMenu.Show(p);
						return;
					}
				}

				//if has the minimize box button
				if (this.MinimizeBox)
				{
					if (e.X >= this.formMinButtonX && e.X <= this.formMinButtonX + this.imgFormMin.Width && e.Y <= imgFormMin.Height)
					{
						this.WindowState = FormWindowState.Minimized;
						return;
					}
				}
				//if show the maximize box
				if (this.MaximizeBox)
				{
					if (e.X >= this.formMaxButtonX && e.X <= this.formMaxButtonX + this.imgFormMax.Width && e.Y <= imgFormMax.Height)
					{
						if (this.WindowState != FormWindowState.Maximized)
							this.WindowState = FormWindowState.Maximized;
						else
							this.WindowState = FormWindowState.Normal;

						return;
					}
				}

				if (e.X >= this.formCloseButtonX && e.X <= this.formCloseButtonX + this.imgFormClose.Width && e.Y <= imgFormClose.Height)
				{
					this.Close();
				}

			}
		}
		#endregion

		#region Avoiding the form to flicker
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				const int WS_MINIMIZEBOX = 0x00020000;
				cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED 
				cp.Style = cp.Style | WS_MINIMIZEBOX;
				if (this.IsXpOr2003 == true)
				{
					cp.ExStyle |= 0x00080000;// Turn on WS_EX_LAYERED 
					this.Opacity = 1;
				}
				return cp;
			}
		}

		/// <summary>
		/// Determine whether the window is XP or windows 2003
		/// </summary>
		private Boolean IsXpOr2003
		{
			get
			{
				OperatingSystem os = Environment.OSVersion;
				Version vs = os.Version;
				if (os.Platform == PlatformID.Win32NT)
					if ((vs.Major == 5) && (vs.Minor != 0))
						return true;
					else
						return false;
				else
					return false;
			}
		}
		#endregion
	}
}