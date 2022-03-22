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
    public partial class FormAddPayment : Form
    {
        public FormAddPayment()
        {
            InitializeComponent();
        }

        public int id = 0;
        public int payid = 0;
        DbHelper    db = new DbHelper();
        DataRow dr;
        public DataRow payRow;

        private void loadProperties() {
            if (id>0)
            {
                string sql = $"Select appointment.id appId, patient.name patient, treatment.name treatment, doctor.name doctor, patient.email patientmail, * from appointment inner join patient on patient.id = appointment.patientID inner join treatment on treatment.id = treatmentID inner join doctor on doctor.id = doctorID where appointment.id = {id}";
                DataTable dt = db.getTable(sql);
                DataRow row = dt.Rows[0];
                dr = row;
                lbl_name.Text = row["patient"].ToString();
                lbl_gender.Text = row["gender"].ToString();
                lbl_birthday.Text = DateTime.Parse(row["birthday"].ToString()).ToShortDateString();
                lbl_email.Text = row["patientmail"].ToString();
                lbl_address.Text = row["address"].ToString();
                lbl_city.Text = row["city"].ToString();
                lbl_date.Text = row["date"].ToString();
                lbl_doctor.Text = row["doctor"].ToString();
                lbl_treatment.Text = row["treatment"].ToString();
                lbl_price.Text = $"Rp {row["price"]} ,-";

                if (payid > 0 && payRow != null) 
                {
                    cb_method.SelectedItem = payRow["method"].ToString();
                    dp_date.Value = DateTime.Parse(payRow["date"].ToString());
                }
            }
        }


        private void FormAddPayment_Load(object sender, EventArgs e)
        {
            loadProperties();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            if (cb_method.SelectedItem != null)
            {
                string sql = "";
                if (payid > 0)
                {
                    sql = $"Update payment set  method = @method, date = @date where id = {payid}";
                }
                else {
                     sql = $"Insert into payment values (@appid, @price, @method, @date, 'Paid')";
                }
                    SqlCommand cmd = new SqlCommand(sql);
                if (payid == 0)
                {
                    cmd.Parameters.AddWithValue("appid", dr["appid"]);
                    cmd.Parameters.AddWithValue("price", dr["price"]);
                }
                    cmd.Parameters.AddWithValue("method", cb_method.SelectedItem);
                    cmd.Parameters.AddWithValue("date", dp_date.Value);

                    try
                    {

                        db.insertCmd(cmd);
                        db.insert($"Update Appointment Set status = 'Paid' where id = {dr["appid"]}");
                        FormPayment obj = new FormPayment();
                        obj.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //throw;
                    }
                
            }
            else
            {
                MessageBox.Show("Select Payment Method");
            }
        }
    }
}
