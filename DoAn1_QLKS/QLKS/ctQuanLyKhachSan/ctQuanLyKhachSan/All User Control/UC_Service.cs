using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.AxHost;

namespace ctQuanLyKhachSan.All_User_Control
{
    public partial class UC_Service : UserControl
    {
        Function fn = new Function();
        String query;
        public UC_Service()
        {
            InitializeComponent();
        }

        private void UC_Service_Load(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, rooms.roomNo from customer inner join rooms on customer.roomid = rooms.roomid where chekout = 'NO'";
            DataSet ds = fn.getData(query);
            GripViewTTin.DataSource = ds.Tables[0];
            InforService(sender, e);
            btnAllotCustomer_Click(sender, e);
        }


        private void InforService(object sender, EventArgs e)
        {
            query = "select total.tid, customer.roomid, customer.cname, total.cid, total.gia " +
                    "from customer " +
                    "inner join total on customer.cid = total.cid";
            DataSet ds = fn.getData(query);
            GripViewInforService.DataSource = ds.Tables[0];
        }

        private void txtSearchRoom_TextChanged(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, rooms.roomNo " +
                    "from customer inner " +
                    "join rooms on customer.roomid = rooms.roomid " +
                    "where roomNo like '" + txtSearchRoom.Text + "%' and chekout = 'NO'";
            DataSet ds = fn.getData(query);
            GripViewTTin.DataSource = ds.Tables[0];
        }
        
        private void btnrefesh_Click(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, rooms.roomNo " +
                    "from customer " +
                    "inner join rooms on customer.roomid = rooms.roomid " +
                    "where chekout = 'NO'";
            DataSet ds = fn.getData(query);
            GripViewTTin.DataSource = ds.Tables[0];
        }

        int id;

        private void GripViewTTin_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (GripViewTTin.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
                id = int.Parse(GripViewTTin.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtName.Text = GripViewTTin.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtRoomID.Text = GripViewTTin.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void btnAllotCustomer_Click(object sender, EventArgs e)
        {
         
                int sl;
                int rowselect = dataGridViewService.Rows.GetRowCount(DataGridViewElementStates.Selected);
                float total = 60000;
                Dictionary<string, int> serviceAmount = new Dictionary<string, int>()
            {
                {"001", 0 },//aqua
                {"002", 0 },//coca
                {"003", 0 },//pepsi
                {"004", 0 },//breakfast
                {"005", 0 },//pool
                {"006", 0 },//laundry
                {"007", 1 },//clean
                {"008", 0 }//carry
               
            };

                if (checkboxaqua.Checked == true)
                {
                    serviceAmount["001"] = 1;
                    total += 20000;
                    txttotal.Text = total.ToString();
                }
                if (checkBoxcoca.Checked == true)
                {
                    serviceAmount["002"] = 1;
                    total += 30000;
                    txttotal.Text = total.ToString();
                }
                if (checkBoxpepsi.Checked == true)
                {
                    serviceAmount["003"] = 1;
                    total += 30000;
                    txttotal.Text = total.ToString();
                }
                if (checkBoxbreakfast.Checked == true)
                {
                    serviceAmount["004"] = 1;
                    total += 150000;
                    txttotal.Text = total.ToString();
                }
                if (checkBox1pool.Checked == true)
                {
                    serviceAmount["005"] = 1;
                    total += 200000;
                    txttotal.Text = total.ToString();
                }
                if (checkBox2laundry.Checked == true)
                {
                    serviceAmount["006"] = 1;
                    total += 60000;
                    txttotal.Text = total.ToString();
                }
                if (checkBox4carry.Checked == true)
                {
                    serviceAmount["008"] = 1;
                    total += 80000;
                    txttotal.Text = total.ToString();
                }
                else if (checkboxaqua.Checked == false && checkBoxcoca.Checked == false && checkBoxpepsi.Checked == false && checkBoxbreakfast.Checked == false && checkBox1pool.Checked == false && checkBox2laundry.Checked == false && checkBox4carry.Checked == false)
                {
                    total = 60000;
                    txttotal.Text = total.ToString();
                }

                query = (@"select service.ServiceID , service.ServiceName , service.Price , service.Amount from service");
                DataSet ds = fn.getData(query);
                DataTable dt = ds.Tables[0];
                dataGridViewService.DataSource = ds.Tables[0];

                if (!dt.Columns.Contains("Amount"))
                {
                    dt.Columns.Add("Amount", typeof(int));
                }
                foreach (DataRow row in dt.Rows)
                {
                    string serviceID = row["ServiceID"].ToString();
                    if (serviceAmount.ContainsKey(serviceID))
                    {
                        row["Amount"] = serviceAmount[serviceID];
                    }
                }
                dataGridViewService.DataSource = dt;
            }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            String rid = txtRoomID.Text;
            Int64 gia = Int64.Parse(txttotal.Text);
            query = "insert into total ( cid, gia ) values (" + rid + "," + gia + ")";
            fn.setData(query, "Success");
        }

        private void btnBringtofont_Click(object sender, EventArgs e)
        {
            query = "select total.tid, customer.roomid, customer.cname, total.cid, total.gia from customer inner join total on customer.cid = total.cid";
            DataSet ds = fn.getData(query);
            GripViewInforService.DataSource = ds.Tables[0];
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            String tid = txtTotalID.Text;
            query = "delete from total where tid = " + tid + "";
            fn.setData(query, "Success");
            InforService(this,null);
        }

        private void GripViewInforService_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (GripViewTTin.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
                id = int.Parse(GripViewTTin.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtTotalID.Text = GripViewInforService.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        
    }
}
