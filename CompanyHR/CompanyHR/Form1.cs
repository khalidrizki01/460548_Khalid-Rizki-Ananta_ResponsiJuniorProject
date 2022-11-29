using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CompanyHR
{
    public partial class FormHR : Form
    {
        private Employee employee;
        private int idGenerator;
        static string connstring = "Host=localhost;Port=2022;Username=postgres;Password=informatika;Database=ResponsiKhalid";
        NpgsqlConnection conn = new NpgsqlConnection(connstring);
        private DataTable dt;
        private NpgsqlCommand cmd;
        string sql = null;
        DataGridViewRow r;
        
        public FormHR()
        {
            InitializeComponent();
            idGenerator = LoadData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            insertEmployee();
            idGenerator = LoadData();
            ClearBoxes();
        }

        private int LoadData()
        {
            int lastID = 0;
            try
            {
                conn.Open();
                dgvEmployee.DataSource = null;
                sql = "SELECT id_employee, nama, departemen FROM tb_employee LEFT JOIN tb_dept ON tb_employee.id_dept = tb_dept.id ORDER BY id_employee DESC;";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                //MessageBox.Show(dt.Rows.Count.ToString());
                dgvEmployee.DataSource = dt;
                // MessageBox.Show(dgvEmployee.TopLeftHeaderCell.Value.ToString());
                lastID = dt.Rows.Count;
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lastID;
        }

        private void insertEmployee()
        {
            try
            {
                conn.Open();
                employee = new Employee(idGenerator, tbNamaKaryawan.Text, tbDepKaryawan.Text);
                sql = "INSERT INTO tb_employee VALUES ('" + employee.Id + "','" + employee.Name + "','" + employee.Dept + "')";
                cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearBoxes()
        {
            tbNamaKaryawan.Text = "";
            tbDepKaryawan.Text = "";
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

            if (e.RowIndex >= 0)
            {
                r = dgvEmployee.Rows[e.RowIndex];
                tbNamaKaryawan.Text = r.Cells["nama"].Value.ToString();
                string Dept = r.Cells["departemen"].Value.ToString();
                string idDept = null;
                if (Dept == "Engineer") idDept = "ENG";
                else if (Dept == "Developer") idDept = "Developer";
                else if (Dept == "Product Manager") idDept = "PM";
                else if (Dept == "Finance") idDept = "FIN";
                tbDepKaryawan.Text = idDept;
            }
        }

        private void EditEmployee()
        {

            if (r == null)
            {
                MessageBox.Show("Mohon pilih baris yang akan diupdate");
                return;
            }
            try
            {
                conn.Open();

                sql = "UPDATE tb_employee SET nama = '" + tbNamaKaryawan.Text+"', id_dept='" + tbDepKaryawan.Text + "' WHERE id_employee="+(int)r.Cells["id_employee"].Value+";";
                cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                conn.Close();
;            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditEmployee();
            idGenerator = LoadData();
            ClearBoxes();
        }

        private void lblDepKaryawan_Click(object sender, EventArgs e)
        {

        }
    }
}
