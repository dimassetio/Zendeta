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
    public partial class FormPayment : Form
    {
        public FormPayment()
        {
            InitializeComponent();
        }

        private void bunifuPanel13_Click(object sender, EventArgs e)
        {
            FormDashboard obj =     new FormDashboard();
            obj.Show();
            this.Hide();
        }

        private void bunifuPanel12_Click(object sender, EventArgs e)
        {
            FormKalendar obj = new FormKalendar();
            obj.Show();
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

        private void bunifuDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            if (dgv_app.SelectedRows != null)
            {
                 int id = int.Parse(dt_appointment.Rows[dgv_app.CurrentCell.RowIndex][0].ToString());
                FormAddPayment obj = new FormAddPayment();
                obj.id = id;
                obj.Show();
            }
            else
            {
                MessageBox.Show("Select Data Appointment");
            }
        }
        DataTable dt_appointment = new DataTable();
        DataTable dt_payout= new DataTable();
        DbHelper  dbHelper = new DbHelper();    

        private void loadDgvApp() {
            dt_appointment = dbHelper.getTable("Select appointment.id, patient.name, treatment.name, treatment.price, appointment.date, appointment.status from appointment inner join patient on patient.id = appointment.patientID inner join treatment on treatment.id = appointment.treatmentid where appointment.status = 'Done'");
            dgv_app.DataSource = dt_appointment;
        }

        private void loadPayout()
        {
            dt_payout = dbHelper.getTable("Select payment.id, doctor.name doctor, patient.name patient, amount, payment.date, method, payment.status from payment inner join appointment on appointment.id = payment.apppointmentid inner join doctor on doctor.id = doctorID inner join patient on patient.id = patientID");
            dgv_payout.DataSource = dt_payout;
        }
        private void FormPayment_Load(object sender, EventArgs e)
        {
            loadDgvApp();
            loadPayout();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            if (dgv_payout.SelectedRows != null)
            {
                int payId = int.Parse(dt_payout.Rows[dgv_payout.CurrentCell.RowIndex][0].ToString());
                DataTable dt = dbHelper.getTable($"Select * from payment where id = {payId}");
                int appId = int.Parse(dt.Rows[0]["apppointmentid"].ToString());
                FormAddPayment obj = new FormAddPayment();
                obj.id = appId;
                obj.payid = payId;
                obj.payRow = dt.Rows[0];
                obj.Show();
            }
            else
            {
                MessageBox.Show("Select Data Payment");
            }
        }

        private void bunifuButton23_Click(object sender, EventArgs e)
        {
            if (dgv_payout.SelectedRows != null)
            {
                int payId = int.Parse(dt_payout.Rows[dgv_payout.CurrentCell.RowIndex][0].ToString());
                if (MessageBox.Show("Deleted Data Cant be Restored", "Are You Sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        dbHelper.insert($"Delete From payment where id = {payId}");
                        loadPayout();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select Data Payment");
            }
        }
    }
}
