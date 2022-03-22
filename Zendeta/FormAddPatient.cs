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
    public partial class FormAddPatient : Form
    {
        public FormAddPatient()
        {
            InitializeComponent();
        }
        public int id = 0;

        public void setValue(int cid,String name, string email, string phone, string gender, DateTime birthday,  string address, string city, string postal) 
        {
            id =cid;
            tb_name.Text = name;
            tb_email.Text = email;
            tb_phone.Text = phone;
            if (gender == "Male")
            {
                rb_male.Checked = true;
            }
            if (gender == "Female")
            {
                rb_male.Checked = true;
            }
            dp_birthday.Value = birthday;
            tb_address.Text = address;
            tb_city.Text = city;
            tb_postal.Text = postal;

        }

        private void bunifuDatePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuRadioButton1_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuRadioButton2_CheckedChanged2(object sender, Bunifu.UI.WinForms.BunifuRadioButton.CheckedChangedEventArgs e)
        {

        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel4_Click(object sender, EventArgs e)
        {

        }

        private void FormAddPatient_Load(object sender, EventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel7_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string getGender() {
            if (rb_female.Checked)
            {
                return "Female";

            }
            else if (rb_male.Checked)
            {
               return "Male";
            }
            return "";
        }

        DbHelper db = new DbHelper();
        string storequery() => $"Insert into patient values (" +
                $"'{tb_name.Text}', " +
                $"'{tb_email.Text}', " +
                $"'{tb_phone.Text}', " +
                $"'{getGender()}'," +
                $"@birthday, " +
                $"'{tb_address.Text}', " +
                $"'{tb_city.Text}', " +
                $"{tb_postal.Text}, " +
                $"'Active Member', " +
                $"@date " +
                $")";

        string updateQuery() => $"Update Patient Set " +
                $"name = '{tb_name.Text}', " +
                $"email = '{tb_email.Text}', " +
                $"phone = '{tb_phone.Text}', " +
                $"gender = '{getGender()}'," +
                $"birthday = @birthday, " +
                $"address = '{tb_address.Text}', " +
                $"city = '{tb_city.Text}', " +
                $"postalcode = {tb_postal.Text} " +
                $" Where id = {id}";
        private void btn_save_Click(object sender, EventArgs e)
        {

            string sql = id == 0 ? storequery():updateQuery() ;
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("birthday", dp_birthday.Value);
            if (id == 0)
            {
            cmd.Parameters.AddWithValue("date", dp_birthday.Value);
            //cmd.Parameters.AddWithValue("date", DateTime.Now);
            }

            try
            {
               var res = db.insertCmd(cmd);
               this.Close();
                FormListPatient obj = new FormListPatient();
                obj.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
    }
}
