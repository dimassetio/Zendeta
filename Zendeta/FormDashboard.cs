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
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel12_Click(object sender, EventArgs e)
        {
            FormKalendar formKalendar = new FormKalendar();
            formKalendar.Show();
            this.Hide();
        }

        private void bunifuPanel14_Click(object sender, EventArgs e)
        {
            FormListPatient obj = new FormListPatient();
            obj.Show();
            this.Hide();
            
        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {
            FormPayment obj = new FormPayment();
            obj.Show();
            this.Hide();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            FormRequestApproval obj = new FormRequestApproval();
            obj.Show();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            FormKalendar obj = new FormKalendar();
            obj.Show();
        }

        private void z(object sender, EventArgs e)
        {

        }
        DbHelper dbHelper = new DbHelper();

        public int loadApprovalRequest() {
            DataTable dt = dbHelper.getTable("Select count(*) total from appointment where status = 'Pending'");
            return int.Parse(dt.Rows[0]["total"].ToString());
        }
        public int loadUpcomingAppointment() {
            DataTable dt = dbHelper.getTable("Select count(*) total from appointment where status = 'Approved'");
            return int.Parse(dt.Rows[0]["total"].ToString());
        }
        DateTime startMonth = DateTime.Today.AddDays(-DateTime.Today.Day);
        DateTime endMonth = DateTime.Today.AddDays(-DateTime.Today.Day).AddDays(DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        public int loadThisMonthAppointment() {
            SqlCommand cmd = new SqlCommand($"Select count(*) total from appointment where status = 'paid' where date between @dateFrom and @dateTo");
          
            cmd.Parameters.AddWithValue("dateFrom", startMonth );
            cmd.Parameters.AddWithValue("dateTo", endMonth);
            DataTable dt = dbHelper.getTableCmd(cmd);
            return int.Parse(dt.Rows[0]["total"].ToString());
        }   
        public int loadTodayAppointment() {
            SqlCommand cmd = new SqlCommand($"Select count(*) total from appointment  where date between @dateFrom and @dateTo");
          
            cmd.Parameters.AddWithValue("dateFrom", DateTime.Today );
            cmd.Parameters.AddWithValue("dateTo", DateTime.Today.AddDays(1));
            DataTable dt = dbHelper.getTableCmd(cmd);
            return int.Parse(dt.Rows[0]["total"].ToString());
        }    
        public int loadAllTimeAppointment() {
            SqlCommand cmd = new SqlCommand($"Select count(*) total from appointment where status = 'Paid'");
           
            DataTable dt = dbHelper.getTableCmd(cmd);
            return int.Parse(dt.Rows[0]["total"].ToString());
        }

       private int loadAllTimeIncome() {
            SqlCommand cmd = new SqlCommand($"Select sum(amount) total from payment where status = 'Paid'");

            DataTable dt = dbHelper.getTableCmd(cmd);
            return int.Parse(dt.Rows[0]["total"].ToString());
        }

        private void loadChart()
        {
            //DataTable dt = dbHelper.getTable("Select From Appointment")
        }
           
        public int loadThisMonthPatient()
        {
            SqlCommand cmd = new SqlCommand($"Select count(*) total from patient where datecreated between @dateFrom and @dateTo");
            cmd.Parameters.AddWithValue("dateFrom", startMonth);
            cmd.Parameters.AddWithValue("dateTo", endMonth);
            DataTable dt = dbHelper.getTableCmd(cmd);
            return int.Parse(dt.Rows[0]["total"].ToString());
        }

        public int loadAllTimePatient()
        {
            SqlCommand cmd = new SqlCommand($"Select count(*) total from patient");

            DataTable dt = dbHelper.getTableCmd(cmd);
            return int.Parse(dt.Rows[0]["total"].ToString());
        }
        DataTable dt_treatment = new DataTable();
        private void loadTableTreatment()
        {
            dt_treatment = dbHelper.getTable("Select  * From treatment ");
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            loadTableTreatment();
            lbl_jumlah_app_req.Text = loadApprovalRequest().ToString();
            lbl_upcoming.Text = loadUpcomingAppointment().ToString();
            thismonth_patient.Text = loadThisMonthPatient().ToString();
            allTime_patient.Text = loadAllTimePatient().ToString();
            treatment1.Text = dt_treatment.Rows[0]["name"].ToString();
            treatment2.Text = dt_treatment.Rows[1]["name"].ToString();
            treatment3.Text = dt_treatment.Rows[2]["name"].ToString();
            today_appointment.Text = loadTodayAppointment().ToString();
            lbl_all_income.Text = $"Rp {loadAllTimeIncome()},-";

        }
    }
}
