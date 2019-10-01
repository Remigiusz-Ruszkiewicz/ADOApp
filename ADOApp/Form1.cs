using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ADOApp.Models1;

namespace ADOApp
{
    
    public partial class Form1 : Form
    {
        private int id = 0;
        private int selectedRow = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            EfRepository context = new EfRepository();
            dataGridView1.DataSource = context.GetCars();
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            EfRepository context = new EfRepository();
            Car car = new Car
            {
                Name = tbName.Text,
                ModelId = int.Parse(tbModel.Text)
            };

            int id = context.Add(car);
            if (id ==0)
            {

            }
            MessageBox.Show(id.ToString());
            dataGridView1.DataSource = context.GetCars();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (dataGridView1.SelectedRows.Count==0)
            {
                btnUpdate.Enabled = false;
                return;
            }
            btnUpdate.Enabled = true;
            id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            tbName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            tbModel.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {

            EfRepository context = new EfRepository();
            Car car = new Car
            {
                Id = id,
                Name = tbName.Text,
                ModelId = int.Parse(tbModel.Text)
            };
            MessageBox.Show(context.Update(car).ToString());
            dataGridView1.DataSource = context.GetCars();
            dataGridView1.Rows[selectedRow].Selected = true;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            EfRepository context = new EfRepository();
            if (context.Delete(id))
            {
                MessageBox.Show("Usunięto");
                dataGridView1.DataSource = context.GetCars();
                tbModel.Text = "";
                tbName.Text = "";
            }
        }
    }
}
