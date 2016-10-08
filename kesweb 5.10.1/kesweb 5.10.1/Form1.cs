using System;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.Drawing;
using kesweb_5._10._1;

namespace kesweb_5._10._1
{
    public partial class Form1 : Form
    {
        public int LEADING_SPACE { get; private set; }
        public int CLOSE_AREA { get; private set; }
        public int CLOSE_SPACE { get; private set; }

        public Form1()
        {
            InitializeComponent();
         
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {

            

        }

        private void myweb1_Load(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_DrawItem_1(object sender, DrawItemEventArgs e)
        {
       
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
         
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TabsControl_Sample.Tab tab = new TabsControl_Sample.Tab();
            tabscontrol.TabPages.Add(tab);
            tab.Text = "NewTab";
            tabscontrol.SelectedTab = tab;
            myweb mw = new myweb();
            tabscontrol.SelectedTab.Controls.Add(mw);
            mw.Dock = DockStyle.Fill;
        }

        private void tabscontrol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabscontrol.SelectedTab == removetab1)
            {
                TabsControl_Sample.Tab tab = new TabsControl_Sample.Tab();
                tabscontrol.TabPages.Add(tab);
                tab.Text = "NewTab";
                tabscontrol.SelectedTab = tab;
                myweb mw = new myweb();
                tabscontrol.SelectedTab.Controls.Add(mw);
                mw.Dock = DockStyle.Fill;
            }
        }
    }
   }
