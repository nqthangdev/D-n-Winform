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
    public partial class Login : Form
    {
        Function fn = new Function();
        String query;

        public Login()
        {
          
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            query = "select username, pass from employee where username = '" + txtUsername.Text + "' and pass = '" + txtPassword.Text + "'";
            DataSet ds = fn.getData(query);

            if (ds.Tables[0].Rows.Count != 0)
            {
                FormMain M = new FormMain();

                FormMain dash = new FormMain();
                this.Hide();
                dash.Show();
            }
            else
            {
                labelError.Visible = true;
                txtPassword.Clear();
            }
        }

        private void btnExit2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want exit ?", "Exit", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
            
        }

       

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }


    }
}
    

