using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WarehouseManagementSystem
{
    public partial class FactoryForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WMSdbMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public FactoryForm()
        {
            InitializeComponent();
            LoadFactory();
        }
        public void LoadFactory()
        {
            int i = 0;
            dgvFactory.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbFactory", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvFactory.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FactoryModuleForm moduleForm = new FactoryModuleForm();
            moduleForm.btnSave.Enabled = true;
            moduleForm.btnUpdate.Enabled = false;
            moduleForm.ShowDialog();
            LoadFactory();
        }

        private void dgvFactory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvFactory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                FactoryModuleForm factoryModule = new FactoryModuleForm();
                factoryModule.lblFId.Text = dgvFactory.Rows[e.RowIndex].Cells[2].Value.ToString();
                factoryModule.txtFName.Text = dgvFactory.Rows[e.RowIndex].Cells[1].Value.ToString();
                factoryModule.txtFLocation.Text = dgvFactory.Rows[e.RowIndex].Cells[3].Value.ToString();
                factoryModule.txtFPhone.Text = dgvFactory.Rows[e.RowIndex].Cells[4].Value.ToString();

                factoryModule.btnSave.Enabled = false;
                factoryModule.btnUpdate.Enabled = true;
                factoryModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this factory?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbFactory WHERE fid LIKE '" + dgvFactory.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully deleted!");
                }
            }
            LoadFactory();
        }
    }
}
