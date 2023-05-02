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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WMSdbMS;Integrated Security=True");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadFactory();
            LoadProduct();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }
        public void LoadFactory()
        {
            int i = 0;
            dgvFactory.Rows.Clear();
            cm = new SqlCommand("SELECT fid,fname FROM tbFactory WHERE CONCAT(fid,fname) LIKE '%" + txtSearchFactory.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvFactory.Rows.Add(i, dr[1].ToString(), dr[0].ToString());
            }
            dr.Close();
            con.Close();

        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pid, pname, pprice, pdescription, pwarehouse) LIKE '%" + txtSearchProduct.Text + "%'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[1].ToString(), dr[0].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            con.Close();

        }

        private void txtSearchFactory_TextChanged(object sender, EventArgs e)
        {
            LoadFactory();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void numericUpDownQuantity_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt16(numericUpDownQuantity.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDownQuantity.Value = qty;
                return;
            }

            if (Convert.ToInt16(numericUpDownQuantity.Value) > 0)
            {
                int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(numericUpDownQuantity.Value);
                txtTotal.Text = total.ToString();

            }

        }

        private void dgvFactory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtFId.Text = dgvFactory.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtFName.Text = dgvFactory.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GetQty();
            if (Convert.ToInt16(numericUpDownQuantity.Value) > qty)
            {
                MessageBox.Show("Instock quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDownQuantity.Value = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value);
                txtTotal.Text = (Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(numericUpDownQuantity.Value)).ToString();
                return;
            }
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPId.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            int total = Convert.ToInt16(txtPrice.Text) * Convert.ToInt16(numericUpDownQuantity.Value);
            txtTotal.Text = total.ToString();
        }


        private void btnOrderInsert_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtFId.Text == "")
                {
                    MessageBox.Show("Please select factory!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPId.Text == "")
                {
                    MessageBox.Show("Please select product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to insert this order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cm = new SqlCommand("INSERT INTO tbOrder(odate, pid, fid, qty, price, total)VALUES(@odate, @pid, @fid, @qty, @price, @total)", con);
                    cm.Parameters.AddWithValue("@odate", dtOrder.Value);
                    cm.Parameters.AddWithValue("@pid", Convert.ToInt32(txtPId.Text));
                    cm.Parameters.AddWithValue("@fid", Convert.ToInt32(txtFId.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt32(numericUpDownQuantity.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt32(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt32(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been successfully inserted.");


                    cm = new SqlCommand("UPDATE tbProduct SET pqty = (pqty-@pqty) WHERE pid LIKE '" + txtPId.Text + "'", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(numericUpDownQuantity.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtFId.Clear();
            txtFName.Clear();

            txtPId.Clear();
            txtPName.Clear();

            txtPrice.Clear();
            numericUpDownQuantity.Value = 0;
            txtTotal.Clear();
            dtOrder.Value = DateTime.Now;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void GetQty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pid='" + txtPId.Text + "'", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                qty = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            con.Close();

        }
    }
}
