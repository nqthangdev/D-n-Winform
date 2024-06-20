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
    public partial class UC_TraKS : UserControl
    {
        Function fn = new Function();
        String query;
        public UC_TraKS()
        {
            InitializeComponent();
        }

        private void UC_TraKS_Load(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price " +
                    "from customer " +
                    "inner join rooms on customer.roomid = rooms.roomid " +
                    "inner join total on customer.cid = total.cid " +
                    "where customer.chekout = 'NO'";
            DataSet ds = fn.getData(query);
            GripViewTraPhong.DataSource = ds.Tables[0];
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price, total.gia as PriceService " +
                    "from customer " +
                    "inner join rooms on customer.roomid = rooms.roomid " +
                    "inner join total on customer.cid = total.cid " +
                    "where customer.cname like '" + txtName.Text + "%' and customer.chekout = 'NO'";
            DataSet ds = fn.getData(query);
            GripViewTraPhong.DataSource = ds.Tables[0];
        }
        int id;
        private void GripViewTraPhong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GripViewTraPhong.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
                id = int.Parse(GripViewTraPhong.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtCName.Text = GripViewTraPhong.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRoom.Text = GripViewTraPhong.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtCName.Text != "")
            {
                if (MessageBox.Show("Are you sure ?", "Comfirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = txtDateCheckOut.Text;
                    query = "update customer set chekout = 'YES', checkout = '" + cdate + "' where cid = " + id + "update rooms set booked = 'NO' where roomNo = '" + txtRoom.Text + "'";
                    fn.setData(query, "Succes");
                    UC_TraKS_Load(this, null);
                }
            }
            else
            {
                MessageBox.Show("No information customer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
