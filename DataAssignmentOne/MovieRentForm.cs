using DataAssignmentOne.DBUtility;
using DataAssignmentOne.Operation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataAssignmentOne
{
    public partial class MovieRentForm : Form
    {
        public Form HomeForm { get; set; }
        public DBOperation operation;
        DataTable RentTable = new DataTable();
        int rmid;


        public MovieRentForm()
        {
            InitializeComponent();
            operation = new DBOperation();
            BindComboBox(); LoadDB();
        }

        private void LoadDB()
        {
            DataTableColumns();
            DataSet dataset = operation.GetRentedMovieDetails();
            if(radioOut.Checked)
            {
                dataset = operation.GetOutRentedMovieDetails();
            }
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                RentTable.Rows.Add(row["rmid"], row["name"], row["address"], row["phone_no"], row["title"],row["rented_cost"],row["date_rented"],row["date_returned"]);
            }
            dgvRentGrid.DataSource = RentTable;
        }

        private void DataTableColumns()
        {
            RentTable.Clear();
            try
            {
                RentTable.Columns.Add("RMID");
                RentTable.Columns.Add("Customer Name");
                RentTable.Columns.Add("Address");                
                RentTable.Columns.Add("Phone No");
                RentTable.Columns.Add("Movie Title");
                RentTable.Columns.Add("Rented Cost");
                RentTable.Columns.Add("Rented Date");
                RentTable.Columns.Add("Return Date");
            }
            catch (Exception ex)
            {

            }
        }

        private void BindComboBox()
        {
            DataTable tableCustomer = operation.ViewCustomerDetails();
            comboCustomer.ValueMember = "cust_id";
            comboCustomer.DisplayMember = "name";
            comboCustomer.DataSource = tableCustomer;
            DataTable tableMovie = operation.ViewMovieDetails();
            comboMovie.ValueMember = "movie_id";
            comboMovie.DisplayMember = "title";
            comboMovie.DataSource = tableMovie;
        }

        private void MovieRentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            operation.CloseConnection();
            this.HomeForm.Show();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            string message = "";
            bool all_valid = true;
            DateTime rented_date = dtpDate.Value;

            if (comboCustomer.SelectedIndex == 0)
            {
                all_valid = false;
                message += "Please Choose Any Customer\n\n";
            }

            if( comboMovie.SelectedIndex == 0)
            {
                all_valid = false;
                message += "Please Choose Any Movie\n\n";
            }

            if (all_valid)
            {
                float rental_cost = float.Parse(labelRent.Text.Trim());
                int movie_id = int.Parse(comboMovie.SelectedValue.ToString());
                int cust_id = int.Parse(comboCustomer.SelectedValue.ToString());
                if (operation.IssueMovieToCustomer(movie_id,cust_id,rental_cost,rented_date))
                {
                    message = "Movie is Issued and its Details are Saved in Database";
                    LoadDB();
                }
                else
                {
                    message = "There are some failure in Issue the Movie to Customer";
                }
            }
            MessageBox.Show(message);
        }

        private void comboMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboMovie.SelectedIndex!=0)
            {
                labelRent.Text = operation.GetMovieRent(int.Parse(comboMovie.SelectedValue.ToString())).ToString();
            }
            else
            {
                labelRent.Text = "None";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LoadDB();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if(rmid!=0)
            {
                if(operation.ReturnMovie(rmid,DateTime.Now))
                {
                    MessageBox.Show("Movie is Successfuly returned");
                    LoadDB();
                }
                else
                {
                    MessageBox.Show(" There are some issued in Returning Movie");
                }
                rmid = 0;
            }
            else
            {
                MessageBox.Show("Please Click on a Valid Row of Grid");
            }
        }

        private void dgvRentGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rmid = int.Parse(dgvRentGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                btnReturn.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (rmid != 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Remove Record From Database?", "Movie System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string message = "";
                    if (operation.DeleteRentedDetails(rmid))
                    {
                        message = "Movie Rented Details are Removed from Database";
                        rmid = 0;
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in removing Movie Rented Details in Database";
                    }
                    MessageBox.Show(message);
                }
            }            
        }
    }
}
