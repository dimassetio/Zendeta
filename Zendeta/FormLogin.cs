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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        DbHelper dbHelper = new DbHelper();

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            
            if (tb_email.Text == string.Empty || tb_password.Text == string.Empty)
            {
                //tb_email.Text.LastIndexOf
                MessageBox.Show("Fill All Fields!");
            }
            else
            {
                DataTable dt = dbHelper.getTable($"Select * from doctor where email = '{tb_email.Text}'");
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("User Not Found");
                } else
                {   
                    DataRow row = dt.Rows[0];
                    if (tb_password.Text != row["password"].ToString())
                    {
                        MessageBox.Show("Wrong Password");

                    } else
                    {
                        DoctorClass.Name = row["name"].ToString();
                        DoctorClass.Email = row["email"].ToString();
                        DoctorClass.Role = row["role"].ToString();
                        DoctorClass.ID = int.Parse(row["id"].ToString());
                        FormDashboard obj = new FormDashboard();
                        obj.Show();
                        this.Hide();
                    }
                }
            }
        }
    }
}
