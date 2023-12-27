using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginPopupTest
{
    public partial class PopupForm : Form
    {
        public PopupForm()
        {
            InitializeComponent();
        }

        private void PopupForm_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                lblParam3.Text += "-" + i.ToString();  
                Thread.Sleep(1000);
            }

            this.Close();
        }
    }
}
