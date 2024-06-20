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
    public partial class UC_AddRoom : UserControl
    {
        Function fn = new Function();
        String query;
        public UC_AddRoom()
        {
            InitializeComponent();
        }

        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
            query = "select * from rooms";
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }

        private void txtAdd_Click(object sender, EventArgs e)
        {
            if(txtRoomNo.Text !="" && txtRoomType.Text !="" && txtBed.Text !="" && txtPrice.Text != "")
            {
                String roomno = txtRoomNo.Text;
                String type = txtRoomType.Text;
                String bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                query = "insert into rooms (roomNo, roomType, bed, price) values ('" + roomno + "','" + type + "','" + bed + "'," + price + ")";
                fn.setData(query, "Success");

                UC_AddRoom_Load(this, null);
                clearAll();
            }
        }
        public void clearAll()
        {
            txtRoomNo.Clear();
            txtRoomType.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();
        }

        private void txtAdd_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void txtAdd_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);
        }

        private void txtRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
