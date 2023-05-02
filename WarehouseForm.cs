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

namespace WarehouseManagementSystem
{
    public partial class WarehouseForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WMSdbMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public WarehouseForm()
        {
            InitializeComponent();
            LoadWarehouse();
        }
        public void LoadWarehouse()
        {
            int i = 0;
            dgvWarehouse.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbWarehouse", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvWarehouse.Rows.Add(i, dr[1].ToString(), dr[0].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WarehouseModuleForm moduleForm = new WarehouseModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadWarehouse();
        }

        private void dgvFactory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvWarehouse.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                WarehouseModuleForm warehouseModule = new WarehouseModuleForm();
                warehouseModule.lblWId.Text = dgvWarehouse.Rows[e.RowIndex].Cells[2].Value.ToString();
                warehouseModule.txtWName.Text = dgvWarehouse.Rows[e.RowIndex].Cells[1].Value.ToString();
                warehouseModule.txtWLocation.Text = dgvWarehouse.Rows[e.RowIndex].Cells[3].Value.ToString();
                warehouseModule.txtWPhone.Text = dgvWarehouse.Rows[e.RowIndex].Cells[4].Value.ToString();

                warehouseModule.btnSave.Enabled = false;
                warehouseModule.btnUpdate.Enabled = true;
                warehouseModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this warehouse?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbWarehouse WHERE wid LIKE '" + dgvWarehouse.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadWarehouse();
        }
    }
}
