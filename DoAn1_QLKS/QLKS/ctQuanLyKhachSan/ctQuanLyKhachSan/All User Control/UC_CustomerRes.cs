using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctQuanLyKhachSan.All_User_Control
{
    public partial class UC_CustomerRes : UserControl
    {
        Function fn = new Function();
        String query;

        public UC_CustomerRes()
        {
            InitializeComponent();
        }

        public void setComboBox(String query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getForCombo(query);

            while(sdr.Read())
            {
                for(int i=0; i<sdr.FieldCount; i++)
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();
        }

        private void UC_CustomerRes_Load(object sender, EventArgs e)
        {

        }

        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }

        private void txtRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Items.Clear();

            query = "select roomNo from rooms where bed = '" + txtBed.Text + "' and roomType = '" + txtRoom.Text + "' and booked = 'NO'";
            setComboBox(query, txtRoomNo);
        }

        int rid;
        private void txtRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomid from rooms where roomNo = '" + txtRoomNo.Text + "'";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());
        }

        private void btnAllotCustomer_Click(object sender, EventArgs e)
        {
            if(txtNName.Text != "" && txtContact.Text != "" && txtNation.Text != "" && txtID.Text != "" && txtAddress.Text != "" && txtPrice.Text != "")
            {
                string name = txtNName.Text;
                long mobile = long.Parse(txtContact.Text);
                string national = txtNation.Text;
                string gender = txtGender.Text;
                string dob = txtDob.Text;
                string idproof = txtID.Text;
                string address = txtAddress.Text;
                string checkin = txtRegistDate.Text;

                query = "insert into customer(cname, mobile, nationality, gender, dob, idproof, address, checkin, roomid) " +
                        "values (N'" + name + "'," + mobile + ", N'" + national + "','" + gender + "','" + dob + "','" + idproof + "', N'" + address + "','" + checkin + "'," + rid + ") " +
                        "update rooms set booked = 'YES' where roomNo = '" + txtRoomNo.Text + "'";
                fn.setData(query, "Room Number " + txtRoomNo.Text + ". Customer registration is complete !");
                clearAll();
            }
            else
            {
                MessageBox.Show("Please fill all information !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearAll()
        {
            txtNName.Clear();
            txtContact.Clear();
            txtNation.Clear();
            txtGender.SelectedIndex = -1;
            txtDob.ResetText(); 
            txtID.Clear();
            txtAddress.Clear();
            txtRegistDate.ResetText();
            txtBed.SelectedIndex = -1;
            txtRoom.SelectedIndex = -1;
            txtRoomNo.Items.Clear();
            txtPrice.Clear();
        }

        private void UC_CustomerRes_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void txtNName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
