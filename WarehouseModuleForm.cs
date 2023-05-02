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
    public partial class WarehouseModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WMSdbMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public WarehouseModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWName.Text == "")
                {
                    MessageBox.Show("Please enter warehouse name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtWLocation.Text == "")
                {
                    MessageBox.Show("Please enter warehouse location!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtWPhone.Text == "")
                {
                    MessageBox.Show("Please enter a phone number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to save this warehouse?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbWarehouse(wname,wlocation,wphone)VALUES(@wname,@wlocation,@wphone)", con);
                    cm.Parameters.AddWithValue("@wname", txtWName.Text);
                    cm.Parameters.AddWithValue("@wlocation", txtWLocation.Text);
                    cm.Parameters.AddWithValue("@wphone", txtWPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Warehouse has been successfully saved.");
                    Clear();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtWName.Clear();
            txtWLocation.Clear();
            txtWPhone.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWName.Text == "")
                {
                    MessageBox.Show("Please enter warehouse name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtWLocation.Text == "")
                {
                    MessageBox.Show("Please enter warehouse location!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtWPhone.Text == "")
                {
                    MessageBox.Show("Please enter a phone number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to update this warehouse?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbWarehouse SET wname = @wname,wlocation = @wlocation,wphone = @wphone WHERE wid LIKE '" + lblWId.Text + "'", con);
                    cm.Parameters.AddWithValue("@wname", txtWName.Text);
                    cm.Parameters.AddWithValue("@wlocation", txtWLocation.Text);
                    cm.Parameters.AddWithValue("@wphone", txtWPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Warehouse has been successfully updated.");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

    }
}
