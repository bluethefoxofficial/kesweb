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
    class TabsControl : TabControl
    {
        /// <summary>
        /// Initialize new instance of TabsControl class.
        /// </summary>
        public TabsControl()
            : base()
        {
            //---- Set default properties.
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.SizeMode = TabSizeMode.Fixed;

            //---- attach event handles.
            this.DrawItem += new DrawItemEventHandler(HandleDrawItem);
            this.MouseDown += new MouseEventHandler(checkClose);
            this.MouseMove += new MouseEventHandler(checkHighlight);
            this.MouseLeave += new EventHandler(removeHover);
            this.SelectedIndexChanged += new EventHandler(TabsControl_SelectedIndexChanged);
        }

        /// <summary>
        /// Left and right padding of the tab.
        /// </summary>
        const int TAB_PADDING = 5;

        /// <summary>
        /// Draw the tab item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HandleDrawItem(object sender, DrawItemEventArgs e)
        {
            int iconX = e.Bounds.X, iconY = 0, closeX = e.Bounds.Right, closeY = 0;

            TabPage tabPage = this.TabPages[e.Index];

            Tab tab = this.TabPages[e.Index] as Tab;

            //--- I used this as a rectangle because otherwise there is nasty looking line below each tab.
            Rectangle innerRec = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height + 2);

            //--- Draw the border background.
            if (e.State == DrawItemState.Selected)
            {
                //---- Selected tab.
                using (var brush = new SolidBrush(Color.White))
                    e.Graphics.FillRectangle(brush, innerRec);
            }
            else if (tab != null && tab.IsHovering)
            {
                //---- When hovering over not selected tab.
                using (var brush = new LinearGradientBrush(innerRec, Color.White, Color.LightBlue, LinearGradientMode.Vertical))
                    e.Graphics.FillRectangle(brush, innerRec);
            }
            else
            {
                //---- Non selected tab.
                using (var brush = new LinearGradientBrush(innerRec, Color.White, Color.LightGray, LinearGradientMode.Vertical))
                    e.Graphics.FillRectangle(brush, innerRec);
            }
            
            //--- Draw the tab incon if exists.
            if (this.ImageList != null && (tabPage.ImageIndex > -1 || !string.IsNullOrEmpty(tabPage.ImageKey)))
            {
                Image img;

                if (tabPage.ImageIndex > -1)
                    img = this.ImageList.Images[tabPage.ImageIndex];
                else
                    img = this.ImageList.Images[tabPage.ImageKey];

                if (img != null)
                {
                    iconX = innerRec.X + TAB_PADDING;
                    iconY = (innerRec.Height - img.Height) / 2;

                    e.Graphics.DrawImageUnscaled(img, iconX, iconY + innerRec.Y);

                    iconX += img.Width;
                }
            }

            //---- Draw the close button
            if (tab != null)
            {
                if (tab.HasCloseButton)
                {
                    Image closeImg = tab.CloseImage;

                    if (tab.IsHoveringClose && tab.CloseImageHover != null)
                        closeImg = tab.CloseImageHover;

                    if (closeImg != null)
                    {
                        closeX = innerRec.Right - TAB_PADDING - closeImg.Width;
                        closeY = (innerRec.Height - closeImg.Height + innerRec.Y) / 2;

                        tab.CloseButtonRectangle = new Rectangle(closeX, closeY, closeImg.Width, closeImg.Height);

                        e.Graphics.DrawImageUnscaled(closeImg, closeX, closeY + innerRec.Y);

                        e.DrawFocusRectangle();
                    }
                }
            }

            //---- Draw the text. If the text is too long then cut off the end and add '...' To avoid this behaviour, set the TabControl.ItemSize to larger value.

            var tabText = this.TabPages[e.Index].Text;
            var tabFont = e.State == DrawItemState.Selected ? new Font(e.Font, FontStyle.Bold) : e.Font;
            var tabTextSize = e.Graphics.MeasureString(tabText, tabFont);

            var textX = iconX + TAB_PADDING;
            var textY = (innerRec.Height - tabTextSize.Height + innerRec.Y) / 2;

            //--- calculate if the text fits as is. If not then trim it.
            if (textX + tabTextSize.Width > closeX - TAB_PADDING)
                tabText = trimTextToFit(e.Graphics, tabText.Substring(0, tabText.Length - 1), tabFont, closeX - TAB_PADDING - textX);

            e.Graphics.DrawString(tabText, tabFont, Brushes.Black, textX, textY + innerRec.Y);
        }

        /// <summary>
        /// Trims the text to fit the given width. 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        static string trimTextToFit(Graphics g, string text, Font font, int width)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            
            string txtToFit = text + "...";
            if (g.MeasureString(txtToFit, font).Width <= width)
                return txtToFit;
            else
                //--- Remove the last character and try again.
                return trimTextToFit(g, text.Substring(0, text.Length - 1), font, width);
        }

        /// <summary>
        /// Check if clicked on close button and close tab if necessary.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkClose(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (TabPage tabPage in this.TabPages)
                {
                    if (tabPage is Tab)
                    {
                        Tab tab = tabPage as Tab;

                        if (tab.CloseButtonRectangle != null && tab.CloseButtonRectangle.Contains(e.Location))
                        {
                            tab.Close();
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check or remove highlighting for a tab and close button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void checkHighlight(object sender, MouseEventArgs e)
        {
            foreach (TabPage tabPage in this.TabPages)
            {
                if (tabPage is Tab)
                {
                    Tab tab = tabPage as Tab;

                    //---- Check for hovering on a close button.
                    
                    if (tab.CloseButtonRectangle != null)
                    {
                        if (tab.CloseButtonRectangle.Contains(e.Location) && !tab.IsHoveringClose)
                        {
                            tab.IsHoveringClose = true;
                            this.Invalidate();
                        }
                        else if (!tab.CloseButtonRectangle.Contains(e.Location) && tab.IsHoveringClose)
                        {
                            tab.IsHoveringClose = false;
                            this.Invalidate();
                        }
                    }

                    //---- Check for hovering on a tab.
                    
                    if (this.GetTabRect(tab.Index).Contains(e.Location) && !tab.IsHovering)
                    {
                        tab.IsHovering = true;
                        this.Invalidate();
                    }
                    else if (!this.GetTabRect(tab.Index).Contains(e.Location) && tab.IsHovering)
                    {
                        tab.IsHovering = false;
                        this.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Remove all hovering from the tabs control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void removeHover(object sender, EventArgs e)
        {
            foreach (TabPage tabPage in this.TabPages)
            {
                if (tabPage is Tab)
                {
                    Tab tab = tabPage as Tab;

                    tab.IsHovering = false;

                    tab.IsHoveringClose = false;
                }
            }

            this.Invalidate();
        }

        /// <summary>
        /// Redraw when changing tabs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TabsControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
    }
}
