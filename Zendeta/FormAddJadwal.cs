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

namespace Zendeta
{
    public partial class FormAddJadwal : Form
    {
        public FormAddJadwal()
        {
            InitializeComponent();
        }

        private void bunifuButton25_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string storeQuery() => $"Insert into Appointment Values ({DoctorClass.ID},{cb_patient.SelectedValue},{cb_treatment.SelectedValue}, @date, @datecreated, 'Pending')";
        private void bunifuButton24_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(storeQuery());
            cmd.Parameters.AddWithValue("date", dp_date.Value);
            cmd.Parameters.AddWithValue("datecreated", DateTime.Now);
            try
            {
                dbHelper.insertCmd(cmd);
                FormKalendar obj = new FormKalendar();
                obj.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        DbHelper dbHelper = new DbHelper();
        private void loadCBTreatment() {
            DataTable dt = dbHelper.getTable("Select * From treatment");
            cb_treatment.DataSource = dt;
            cb_treatment.ValueMember = dt.Columns["id"].ToString();
            cb_treatment.DisplayMember = dt.Columns["name"].ToString();
        }
        private void loadCBPatient()
        {
            DataTable dt = dbHelper.getTable("Select * From patient");
            cb_patient.DataSource = dt;
            cb_patient.ValueMember = dt.Columns["id"].ToString();
            cb_patient.DisplayMember = dt.Columns["name"].ToString();
        }


        private void FormAddJadwal_Load(object sender, EventArgs e)
        {
            loadCBTreatment();
            loadCBPatient();
            lbl_email.Text = DoctorClass.Email;
            lbl_name.Text = DoctorClass.Name;
            lbl_role.Text = DoctorClass.Role;
        }
    }
}
