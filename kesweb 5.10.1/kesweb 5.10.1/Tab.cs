/***********************************************************************************************************
 * 
 * Copyright (c) 2014 Stimulus Technology http://www.stresstimulus.com
 * 
 ***********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TabsControl_Sample
{
    /// <summary>
    /// A TabsControl tab with a close button.
    /// </summary>
    class Tab : TabPage
    {
        /// <summary>
        /// Initializes new instance of Tab class.
        /// </summary>
        public Tab()
            : base()
        {
            createCloseButtons();
        }

        /// <summary>
        /// Initializes new instance of Tab class and specifies the text for the tab.
        /// </summary>
        /// <param name="text"></param>
        public Tab(string text)
            : base(text)
        {
            createCloseButtons();
        }

        /// <summary>
        /// Create the close buttons.
        /// </summary>
        void createCloseButtons()
        {
            CloseImage = GetCloseButton(false);

            CloseImageHover = GetCloseButton(true);
        }

        /// <summary>
        /// If tab will have close button
        /// </summary>
        public bool HasCloseButton { get; set; }

        Image m_closeImg;
        /// <summary>
        /// The close image.
        /// </summary>
        public Image CloseImage
        {
            get
            {
                return m_closeImg;
            }
            set
            {
                HasCloseButton = value != null;

                m_closeImg = value;
            }
        }

        /// <summary>
        /// Close image when mouse hovers over it.
        /// </summary>
        public Image CloseImageHover { get; set; }

        /// <summary>
        /// If curently mouse if hovering over the tab.
        /// </summary>
        internal bool IsHovering { get; set; }

        /// <summary>
        /// If curently mouse if hovering over the close button.
        /// </summary>
        internal bool IsHoveringClose { get; set; }

        /// <summary>
        /// The close button rectangle.
        /// </summary>
        internal Rectangle CloseButtonRectangle { get; set; }

        /// <summary>
        /// Draw the X close icon.
        /// </summary>
        /// <param name="hover"></param>
        /// <returns></returns>
        Image GetCloseButton(bool hover)
        {
            var bitmap = new Bitmap(16, 16);
            bitmap.MakeTransparent();

            using (var graphic = Graphics.FromImage(bitmap))
            {
                using (var graphPath = new GraphicsPath())
                {
                    var xPoints = new Point[]{
                new Point(4, 4),
                new Point(6, 4),
                new Point(8, 6),
                new Point(10, 4),
                new Point(12, 4),
                new Point(9, 7),
                new Point(9, 8),
                new Point(12, 11),
                new Point(10, 11),
                new Point(8, 9),
                new Point(6, 11),
                new Point(4, 11),
                new Point(7, 8),
                new Point(7, 7)};

                    Color xColor = hover ? Color.Firebrick : Color.Gray;

                    graphPath.AddPolygon(xPoints);
                    using (var pen = new Pen(xColor))
                    {
                        graphic.DrawPolygon(pen, graphPath.PathPoints);
                        using (var brush = new SolidBrush(xColor))
                        {
                            graphic.FillPolygon(brush, graphPath.PathPoints);
                        }
                    }
                }
            }

            return bitmap;
        }

        /// <summary>
        /// The index of the tab.
        /// </summary>
        public int Index
        {
            get
            {
                if (ParentTabControl == null)
                    return -1;

                return ParentTabControl.TabPages.IndexOf(this);
            }
        }

        /// <summary>
        /// Parent tabcontrol is exists.
        /// </summary>
        TabsControl ParentTabControl
        {
            get
            {
                return this.Parent as TabsControl;
            }
        }

        /// <summary>
        /// Raised when the tab is closed.
        /// </summary>
        public event EventHandler TabClosed;

        /// <summary>
        /// Close the tab.
        /// </summary>
        public void Close()
        {
            if (ParentTabControl != null)
            {
                var selectedIndex = this.Index - 1;

                if (selectedIndex < 0)
                    selectedIndex = 0;

                ParentTabControl.SelectedIndex = selectedIndex;

                ParentTabControl.Controls.Remove(this);

                if (TabClosed != null)
                    TabClosed(this, EventArgs.Empty);

                this.Dispose(true);
            }
        }
    }

}
