using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml.Style;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using OfficeOpenXml.DataValidation;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System.Data.SqlClient;
using System.Net;


namespace ctQuanLyKhachSan.All_User_Control
{
    public partial class UC_CustomerDetail : UserControl
    {
        Function fn = new Function();
        String query;
        public UC_CustomerDetail()
        {
            InitializeComponent();
        }

        private void txtSearchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtSearchName.SelectedIndex == 0)
            {
                query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price " +
                        "from customer " +
                        "inner join rooms on customer.roomid = rooms.roomid ";
                    getRecord(query);
            }
            else if(txtSearchName.SelectedIndex == 1)
            {
                query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price " +
                        "from customer " +
                        "inner join rooms on customer.roomid = rooms.roomid " +
                        "where checkout is null";
                getRecord(query);
            }
            else if (txtSearchName.SelectedIndex == 2)
            {
                query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.idproof, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price " +
                        "from customer " +
                        "inner join rooms on customer.roomid = rooms.roomid " +
                        "where checkout is not null";
                getRecord(query);
            }
        }
        private void getRecord(String query)
        {
            DataSet ds = fn.getData(query);
            DataGripView1.DataSource = ds.Tables[0];
        }

        private void UC_CustomerDetail_Load(object sender, EventArgs e)
        {

        }
        private void ExportExcel(string path)
        {
            Excel.Application application = new Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = application.ActiveSheet;

            worksheet.Range["A1"].Value = "DANH SÁCH KHÁCH HÀNG";
            worksheet.Range["A1"].Font.Size = 16;
            worksheet.Range["A1"].Font.Bold = true;
            worksheet.Range["A1:M1"].Merge();
            worksheet.Range["A1:M1"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            worksheet.Range["A1:M1"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
            worksheet.Range["A1:M1"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);


            worksheet.Range["A3:M3"].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
            worksheet.Range["A3:M3"].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            worksheet.Range["A3:M3"].Font.Bold = true;

            int dem = 4;
            for (int i = 0; i < DataGripView1.Columns.Count; i++)
            {
                application.Cells[3, i + 1] = DataGripView1.Columns[i].HeaderText;
            }
            for (int i = 0; i < DataGripView1.Rows.Count; i++)
            {
                for (int j = 0; j < DataGripView1.Columns.Count; j++)
                {
                    application.Cells[i + 4, j + 1] = DataGripView1.Rows[i].Cells[j].Value;
                }
                dem++;
            }
            worksheet.Range["A3:M" +dem].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            application.Columns.AutoFit();
            application.ActiveWorkbook.SaveCopyAs(path);
            application.ActiveWorkbook.Saved = true;
        }

        private void btExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export Excel";
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportExcel(saveFileDialog.FileName);
                    MessageBox.Show("Xuất file thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi! " + ex.Message);
                }
            }

        }
        int id;
        string name;
        string mobile;
        string nationality;
        string gender;
        string dob;
        string addr;
        private void DataGripView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGripView1.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
                id = int.Parse(DataGripView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                name = DataGripView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                mobile = DataGripView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                nationality = DataGripView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                gender = DataGripView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                dob = DataGripView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                addr = DataGripView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lenh;
            if (MessageBox.Show("Are you sure?", "Notification!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                lenh = @"DELETE FROM customer WHERE customer.cid = " + id;
                DataSet ds = fn.getData(lenh);
                getRecord(query);
                MessageBox.Show("Done!", "Notification!", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error!", "Notification!", MessageBoxButtons.OK);

            }
        }

        private void ts_option_MouseLeave(object sender, EventArgs e)
        {
            
        }

        private void ts_option_MouseHover(object sender, EventArgs e)
        {
            ts_option.ShowDropDown();
        }
    }
}
    

