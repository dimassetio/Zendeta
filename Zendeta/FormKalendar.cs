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
    public partial class FormKalendar : Form
    {
        public FormKalendar()
        {
            InitializeComponent();
        }

        private void bunifuFormDock1_FormDragging(object sender, Bunifu.UI.WinForms.BunifuFormDock.FormDraggingEventArgs e)
        {

        }

        private void bunifuPanel1_Click(object sender, EventArgs e)
        {
            FormPayment obj = new FormPayment();    
            obj.Show();
            this.Hide();
        }

        private void bunifuPanel4_Click(object sender, EventArgs e)
        {
            FormListPatient obj = new FormListPatient();
            obj.Show();
            this.Hide();
        }

        private void bunifuPanel6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuPanel12_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuPanel13_Click(object sender, EventArgs e)
        {
            FormDashboard obj = new FormDashboard();
            obj.Show();
            this.Hide();
        }

        private void bunifuPanel14_Click(object sender, EventArgs e)
        {
            FormListPatient obj = new FormListPatient();
            obj.Show();
            this.Hide();
        }

        private void bunifuPanel1_Click_1(object sender, EventArgs e)
        {
            FormPayment obj = new FormPayment();
            obj.Show();
            this.Hide();
        }

        private void bunifuIconButton1_Click(object sender, EventArgs e)
        {
            FormAddJadwal obj = new FormAddJadwal();
            obj.Show();
        }

        private void bunifuButton22_Click(object sender, EventArgs e)
        {
            FormRequestApproval obj = new FormRequestApproval();
            obj.Show();
        }

        DbHelper dbHelper = new DbHelper();
        DataTable dt = new DataTable(); 

        private void loadDgv()
        {
             dt = dbHelper.getTable("Select appointment.id, name, date, status from appointment inner join patient on patient.id = appointment.patientId order by appointment.dateCreated desc");
           
            dgv_kalendar.DataSource = dt;
           
        }



        private void FormKalendar_Load(object sender, EventArgs e)
        {
            loadDgv();   
        }
        private void setDone()
        {
          
            
                if (MessageBox.Show("Please Confirm this Appointment Was Done", "Set Done ", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        int id = int.Parse(dt.Rows[dgv_kalendar.CurrentCell.RowIndex][0].ToString());
                        dbHelper.insert($"Update Appointment Set Status = 'Done' where id = {id}");
                        loadDgv();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //throw;
                    }
                }

            
        }
        private void dgv_kalendar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string status = dt.Rows[dgv_kalendar.CurrentCell.RowIndex][3].ToString();
            if (e.ColumnIndex == 0)
            {
                if (status == "Pending")
                {
                    if (MessageBox.Show("Please Confirm to Approve this Request", "Approve this", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        try
                        {
                        int id = int.Parse(dt.Rows[dgv_kalendar.CurrentCell.RowIndex][0].ToString());
                        dbHelper.insert($"Update Appointment Set Status = 'Approved' where id = {id}");
                        loadDgv();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            //throw;
                        }
                    } ;
                    
                }
            }
            if (e.ColumnIndex == 1)
            {
                if (status == "Pending")
                {
                    MessageBox.Show("Approve this appointment before set it done");
                }
                if(status == "Approved")
                {
                    setDone();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = $"Select appointment.id, name, date, status from appointment inner join patient on patient.id = appointment.patientId order by appointment.dateCreated desc";
            if (comboBox1.SelectedItem.ToString() != "All")
            {
                sql = $"Select appointment.id, name, date, status from appointment inner join patient on patient.id = appointment.patientId where appointment.status = '{comboBox1.SelectedItem}' order by appointment.dateCreated desc ";
            }
            try
            {
                dt = dbHelper.getTable(sql);
                dgv_kalendar.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
    }
}
