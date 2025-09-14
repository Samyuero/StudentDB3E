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
using System.Xml.Linq;


namespace StudentCRUDApp
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=localhost;Initial Catalog=StudentDB;Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                LoadData();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Students (FirstName, LastName, Age, Course) VALUES (@FirstName, @LastName, @Age, @Course)", conn);
                cmd.Parameters.AddWithValue("@FirstName", txtfname.Text);
                cmd.Parameters.AddWithValue("@LastName", txtlname.Text);
                cmd.Parameters.AddWithValue("@Age", txtage.Text);
                cmd.Parameters.AddWithValue("@Course", txtcourse.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Students", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            int studentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE StudentID = @StudentID", conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                txtfname.Text = selectedRow.Cells["FirstName"].Value.ToString();
                txtlname.Text = selectedRow.Cells["LastName"].Value.ToString();
                txtcourse.Text = selectedRow.Cells["Course"].Value.ToString();
                txtage.Text = selectedRow.Cells["Age"].Value.ToString();

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StudentID"].Value);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Students SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Course = @Course WHERE StudentID = @StudentID", conn);
                cmd.Parameters.AddWithValue("@FirstName", txtfname.Text);
                cmd.Parameters.AddWithValue("@LastName", txtlname.Text);
                cmd.Parameters.AddWithValue("@Age", txtage.Text);
                cmd.Parameters.AddWithValue("@Course", txtcourse.Text);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
