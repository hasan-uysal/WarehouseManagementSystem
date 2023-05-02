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
    public partial class FactoryModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WMSdbMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        public FactoryModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFName.Text == "")
                {
                    MessageBox.Show("Please enter factory name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtFLocation.Text == "")
                {
                    MessageBox.Show("Please enter factory location!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtFPhone.Text == "")
                {
                    MessageBox.Show("Please enter factory phone number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you sure you want to save this factory?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbFactory(fname,flocation,fphone)VALUES(@fname,@flocation,@fphone)", con);
                    cm.Parameters.AddWithValue("@fname", txtFName.Text);
                    cm.Parameters.AddWithValue("@flocation", txtFLocation.Text);
                    cm.Parameters.AddWithValue("@fphone", txtFPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Factory has been successfully saved.");
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
            txtFName.Clear();
            txtFLocation.Clear();
            txtFPhone.Clear();
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
                if (txtFName.Text == "")
                {
                    MessageBox.Show("Please enter factory name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtFLocation.Text == "")
                {
                    MessageBox.Show("Please enter factory location!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtFPhone.Text == "")
                {
                    MessageBox.Show("Please enter factory phone number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to update this factory?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("UPDATE tbFactory SET fname = @fname,flocation = @flocation,fphone = @fphone WHERE fid LIKE '" + lblFId.Text + "'", con);
                    cm.Parameters.AddWithValue("@fname", txtFName.Text);
                    cm.Parameters.AddWithValue("@flocation", txtFLocation.Text);
                    cm.Parameters.AddWithValue("@fphone", txtFPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Factory has been successfully updated.");
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
