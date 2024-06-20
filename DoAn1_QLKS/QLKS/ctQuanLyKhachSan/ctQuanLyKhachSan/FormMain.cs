using ctQuanLyKhachSan.All_User_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctQuanLyKhachSan
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            uC_AddRoom1.Visible = true;
            uC_AddRoom1.BringToFront();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            uC_Home1.Visible = true;
            uC_AddRoom1.Visible = false;
            uC_CustomerRes1.Visible = false;
            uC_CustomerDetail1.Visible = false;
            uC_TraKS1.Visible = false;
            uC_Service1.Visible = false;
            uC_Employee1.Visible = false;
            btnHome.PerformClick();
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCustRegistration_Click(object sender, EventArgs e)
        {
            uC_CustomerRes1.Visible = true;
            uC_CustomerRes1.BringToFront();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            uC_Home1.Visible = true;
            uC_Home1.BringToFront();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            uC_TraKS1.Visible = true;
            uC_TraKS1.BringToFront();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want exit ?", "Exit", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void btnCustomerDetail_Click(object sender, EventArgs e)
        {
            uC_CustomerDetail1.Visible = true;
            uC_CustomerDetail1.BringToFront();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            uC_Employee1.Visible = true;
            uC_Employee1.BringToFront();
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            uC_Service1.Visible = true;
            uC_Service1.BringToFront();
        }
    }
}
