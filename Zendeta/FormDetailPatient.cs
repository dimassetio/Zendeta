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
    public partial class FormDetailPatient : Form
    {
        public FormDetailPatient()
        {
            InitializeComponent();
        }

        private void bunifuPanel11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel23_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel24_Click(object sender, EventArgs e)
        {

        }
        public int id = 0;
        DbHelper db = new DbHelper();

        private void loadProperties()
        {
            if (id > 0)
            {
                string sql = $"Select * from patient where id = {id}";
                DataTable dt = db.getTable(sql);
                DataRow row = dt.Rows[0];
                
                lbl_name.Text = row["name"].ToString();
                lbl_gender.Text = row["gender"].ToString();
                lbl_birthday.Text = DateTime.Parse(row["birthday"].ToString()).ToShortDateString();
                lbl_email2.Text = row["email"].ToString();
                lbl_address.Text = row["address"].ToString();
                lbl_city.Text = row["city"].ToString();
                lbl_postal.Text = row["postalcode"].ToString();
                lbl_joined.Text = row["dateCreated"].ToString();
                lbl_membership.Text = row["membership"].ToString();

                
            }
        }
        public int loadAllAppointment()
        {

            DataTable dt = db.getTable($"Select count(*) total from appointment where patientid = {id}");
            return int.Parse(dt.Rows[0]["total"].ToString());
        }
        public int loadUpcomingAppointment()
        {
            DataTable dt = db.getTable($"Select count(*) total from appointment where patientid = {id}");
            return int.Parse(dt.Rows[0]["total"].ToString());
        }
        private void loadDgv()
        {
           string sql = $"Select name, date, status from appointment inner join doctor on doctor.id = appointment.doctorId where patientID = {id} order by appointment.dateCreated desc";
            dt_app = db.getTable(sql);
            dgv_appointment.DataSource = dt_app;
        }

        DataTable dt_app = new DataTable();
        private void FormDetailPatient_Load(object sender, EventArgs e)
        {
            loadProperties();
            loadDgv();
            lbl_total.Text = loadAllAppointment().ToString();
        }

        private void lbl_birthday_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lbl_gender_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
