using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ctQuanLyKhachSan.All_User_Control
{
    public partial class Uc_CheckOut : UserControl
    {
        Function fn = new Function();
        String query;
        public Uc_CheckOut()
        {
            InitializeComponent();
        }

        private void Uc_CheckOut_Load(object sender, EventArgs e)
        {
            query = "SELECT customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender," +
                "customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType,"+
                " rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid WHERE chekout = 'NO'";
            DataSet ds = fn.getData(query);
            DataGripView1.DataSource = ds.Tables[0];
        }


        int id;
        private void DataGripView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGripView1.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
                id = int.Parse(DataGripView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCName.Text = DataGripView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRoom.Text = DataGripView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                if (MessageBox.Show("Are you sure ?", "Comfirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = txtCheckOutDate.Text;
                    query = "update customer set chekout = 'YES', checkout = '" + cdate + "' where cid = " + id + "update rooms set booked = 'NO' where roomNo = '" + txtRoom.Text + "'";
                    fn.setData(query, "Succes");
                    Uc_CheckOut_Load(this, null);
                    clearAll();
                }
                else
                {
                    MessageBox.Show("No information customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void clearAll()
        {
            txtCName.Clear();
            txtName.Clear();
            txtRoom.Clear();
            txtCheckOutDate.ResetText();
        }

        private void Uc_CheckOut_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            int check = int.Parse(txtName.Text);
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price, rooms.roomid " +
                        "from customer " +
                        "inner join rooms on customer.roomid = rooms.roomid " +
                        "where cid = " + check + "and checkout = 'NO'";
            if (e.KeyChar == (char)Keys.Enter)
            {
                DataSet ds = fn.getData(query);
                DataGripView1.DataSource = ds.Tables[0];
            }
        }
    }
}
