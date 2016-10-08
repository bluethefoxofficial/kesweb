using System;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using Microsoft.VisualBasic;
namespace kesweb_5._10._1
{
    public partial class myweb : UserControl
    {
        public myweb()
        {
            InitializeComponent();
            try
            {
                initbrowser();
            }catch
            {
               
            }
        }


   
        private void wbb_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myweb_Load(object sender, EventArgs e)
        {
           
        }

        ChromiumWebBrowser chrome;
        public void initbrowser()
        {
           
            CefSettings cs = new CefSettings();

            Cef.Initialize(cs);
            chrome = new ChromiumWebBrowser("www.google.com");
            wbb.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;
            chrome.BringToFront();
            
          
        


        }
        void OnFullscreenModeChange(IWebBrowser browserControl, IBrowser browser, bool fullscreen)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            chrome.SetZoomLevel(trackBar1.Value);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            chrome.ShowDevTools();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel3.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Hide();
        }
        private void Chrome_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            textBox1.Text = chrome.Address;
        }

        private void roundbutton1_Click(object sender, EventArgs e)
        {
            chrome.Back();
            
        }

        private void roundbutton2_Click(object sender, EventArgs e)
        {
            chrome.Forward();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                chrome.Load(textBox1.Text);
            }catch
            {

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel4.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.Show();
            panel4.BringToFront();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("well were in beta so GIVE US A BREAK");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    chrome.Load(textBox1.Text);
                }catch
                {

                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
