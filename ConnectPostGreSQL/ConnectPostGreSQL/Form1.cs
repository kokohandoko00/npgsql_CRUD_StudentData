using Npgsql;
using System.Data;
using System.Windows.Forms;

namespace ConnectPostGreSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=5432;Username=postgres;Password=Javierpedro2;Database=ListofName";
        //public static NpgsqlConnection conn = new NpgsqlConnection(connectionString: connstring);
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private DataGridViewRow r;
        

        public void Form1_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            btnLoaddata.PerformClick();


        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        public void btnLoaddata_Click_1(object sender, EventArgs e)
        {
            try
            {
                dgvData.DataSource = null;
                conn.Open();
                sql = "select * from st_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                dgvData.DataSource = dt;
                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:"+ex.Message, "FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                
                sql = @"select * from st_insert(:_firstname,:_midname,:_lastname)"; //select sql anyar
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_firstname", txtFirstName.Text); //add value from txt to column firstname
                cmd.Parameters.AddWithValue("_midname", txtMidName.Text);
                cmd.Parameters.AddWithValue("_lastname", txtLastName.Text);
                conn.Close();
                MessageBox.Show("Data Murid Berhasil diinputkan", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnLoaddata.PerformClick();
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    conn.Close();
                    MessageBox.Show("Data Murid Berhasil diinputkan","Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLoaddata.PerformClick();
                    txtFirstName.Text = txtLastName.Text = txtMidName.Text = null;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Insert FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang akan diupdate", "Good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                conn.Open();
                sql = @"select * from st_update(:_id,:_firstname,:_midname,:_lastname)"; //select sql anyar
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", r.Cells["_id"].Value.ToString());
                cmd.Parameters.AddWithValue("_firstname", txtFirstName.Text); //add value from txt to column firstname
                cmd.Parameters.AddWithValue("_midname", txtMidName.Text);
                cmd.Parameters.AddWithValue("_lastname", txtLastName.Text);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    conn.Close();
                    MessageBox.Show("Data Murid Berhasil diupdate", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLoaddata.PerformClick();
                    txtFirstName.Text = txtLastName.Text = txtMidName.Text = null;
                    r = null;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Update FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                r = dgvData.Rows[e.RowIndex];
                txtFirstName.Text = r.Cells["_firstname"].Value.ToString();
                txtMidName.Text = r.Cells["_midname"].Value.ToString();
                txtLastName.Text = r.Cells["_lastname"].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris data yang akan diupdate", "Good!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Apakah benar anda ingin menghapus data "+r.Cells["_firstname"].Value.ToString()+" ?", "Hapus data terkonfirmasi",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)

                try
            {
                conn.Open();
                sql = @"select * from st_delete(:_id)"; //select sql anyar
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_id", r.Cells["_id"].Value.ToString());
               
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    conn.Close();
                    MessageBox.Show("Data Murid Berhasil dihapus", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btnL.PerformClick(); //kalo insertnya diklik 
                    btnLoaddata.PerformClick();
                    txtFirstName.Text = txtLastName.Text = txtMidName.Text = null;
                    r = null;

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Update FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}