using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseManagementSystem
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            WTimer.Start();
        }

        int startPoint = 0;

        private void WTimer_Tick(object sender, EventArgs e)
        {
            startPoint += 2;
            WProgressBar.Value = startPoint;
            if (WProgressBar.Value == 100)
            {
                WProgressBar.Value = 0;
                WTimer.Stop();
                LoginForm login = new LoginForm();
                this.Hide();
                login.ShowDialog();
            }
        }
    }
}
