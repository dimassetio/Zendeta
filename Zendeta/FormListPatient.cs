using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zendeta
{
    public partial class FormListPatient : Form
    {
        public FormListPatient()
        {
            InitializeComponent();
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {
            FormDashboard obj = new FormDashboard();
            obj.Show();
            this.Hide();
        }

        private void bunifuPanel11_Click(object sender, EventArgs e)
        {
            FormKalendar obj = new FormKalendar();
            obj.Show();
            this.Hide();
        }

        private void bunifuPanel8_Click(object sender, EventArgs e)
        {
            FormPayment obj = new FormPayment();
            obj.Show();
            this.Hide();
        }

        private void bunifuIconButton2_Click(object sender, EventArgs e)
        {
            FormAddPatient obj = new FormAddPatient();
            obj.Show();
        }

        private void bunifuPictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void loadCBSort() {
            DataTable dt = new DataTable();
            dt.Columns.Add("value");
            //dt.Columns.Add("display");
            dt.Rows.Add("Created Date (Newest)");
            dt.Rows.Add("Created Date (Oldest)");
            cb_sort.DataSource = dt;
            cb_sort.DisplayMember = dt.Columns["value"].ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //String sql = "";
            if (cb_sort.SelectedIndex == 0)
            {
                loadDgv("Select id, name, city, datecreated from patient order by dateCreated desc");
            }
            else if(cb_sort.SelectedIndex == 1)
            {
                loadDgv("Select id, name, city, datecreated from patient order by dateCreated asc");
            }
        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanelTop_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void lbl_position_Click(object sender, EventArgs e)
        {

        }

        private void lbl_dokter_name_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuLabel9_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPictureBox10_Click(object sender, EventArgs e)
        {

        }
        DataTable patient_dt = new DataTable();
        DbHelper db = new DbHelper();
        private void loadPatientDt()
        {
            patient_dt.Columns.Add("Name");
            patient_dt.Columns.Add("Phone");
            patient_dt.Columns.Add("City");
            patient_dt.Columns.Add("Next Appointment");
            patient_dt.Columns.Add("Last Appointment");
            patient_dt.Columns.Add("Date");

        }

        private void loadTotalPatient() {
            DataTable dt = db.getTable("Select Count(*) from patient");
            lbl_total.Text = dt.Rows[0][0].ToString();
            
        }

        private void FormListPatient_Load(object sender, EventArgs e)
        {
            lbl_dokter_name.Text = DoctorClass.Name;
            lbl_position.Text = DoctorClass.Role;
            loadCBSort();
            btn_delete.Enabled = false;
            btn_edit.Enabled = false;
            loadDgv();
            loadTotalPatient();
            cb_sort.SelectedIndex = 0;
            
        }

        private void loadDgv(string sql = "Select id, name, city, datecreated from patient order by datecreated desc") {
            patient_dt = db.getTable(sql);
            dgv_patient.DataSource = patient_dt;
        }

        private void dgv_patient_CurrentCellChanged(object sender, EventArgs e)
        {
            if(dgv_patient.CurrentCell != null)
            {
                btn_delete.Enabled = true;
                btn_edit.Enabled = true;
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if (dgv_patient.SelectedRows != null)
            {
                int id = int.Parse(patient_dt.Rows[dgv_patient.CurrentCell.RowIndex][0].ToString());
                //MessageBox.Show(id.ToString());
                DataTable dt = db.getTable($"Select * from patient where id = {id}");
                FormAddPatient obj = new FormAddPatient();
                DataRow row = dt.Rows[0];
                obj.setValue(int.Parse(row["id"].ToString()),row["name"].ToString(), row["email"].ToString(), row["phone"].ToString(), row["gender"].ToString(), 
                    DateTime.Parse(row["birthday"].ToString()),row["address"].ToString(), row["city"].ToString(), row["postalcode"].ToString());
                obj.Show();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deleted Data Can't be Restored", "Are You Sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int id = int.Parse(patient_dt.Rows[dgv_patient.CurrentCell.RowIndex][0].ToString());
                db.insert($"Delete from patient where id = {id}");
                loadDgv();
                loadTotalPatient();
            }
            
        }

        private void dgv_patient_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_patient.SelectedRows != null)
            {
                int id = int.Parse(patient_dt.Rows[dgv_patient.CurrentCell.RowIndex][0].ToString());
                //FormAddPayment obj = new FormAddPayment();
                FormDetailPatient obj = new FormDetailPatient();
                obj.id = id;
                obj.Show();
            }
            else
            {
                MessageBox.Show("Select Data Appointment");
            }
        }
    }
}
