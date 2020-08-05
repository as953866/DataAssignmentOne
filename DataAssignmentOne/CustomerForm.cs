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
    public partial class CustomerForm : Form
    {
        public Form HomeForm { get; set; }
        public DBOperation operation;
        DataTable CustomerTable = new DataTable();
        int cust_id;

        public CustomerForm()
        {
            InitializeComponent();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            operation = new DBOperation();
            LoadDB();
        }

        private void LoadDB()
        {
            DataTableColumns();
            DataSet dataset = operation.GetCustomerDetails();
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                CustomerTable.Rows.Add(row["cust_id"], row["first_name"], row["last_name"], row["address"], row["phone_no"]);
            }
            dgvCustomerGrid.DataSource = CustomerTable;
        }

        private void DataTableColumns()
        {
            CustomerTable.Clear();
            try
            {
                CustomerTable.Columns.Add("Customer ID");
                CustomerTable.Columns.Add("First Name");
                CustomerTable.Columns.Add("Last Name");
                CustomerTable.Columns.Add("Address");
                CustomerTable.Columns.Add("Phone No");
            }
            catch (Exception ex)
            {

            }
        }

        private void CustomerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            operation.CloseConnection();
            this.HomeForm.Show();
        }

        private void dgvCustomerGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cust_id = int.Parse(dgvCustomerGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                textFirstName.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                textLastName.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                textAddress.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[3].Value.ToString();
                textPhoneNo.Text = dgvCustomerGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string message = "";
            bool all_valid = true;
            string first_name = textFirstName.Text.Trim();
            string last_name = textLastName.Text.Trim();
            string address = textAddress.Text.Trim();
            string phone_no = textPhoneNo.Text.Trim();

            if (Validation.IsEmpty(first_name))
            {
                all_valid = false;
                message += "Please Enter Some Value in First Name\n\n";
            }

            if (Validation.IsEmpty(last_name))
            {
                all_valid = false;
                message += "Please Enter Some Value in Last Name\n\n";
            }

            if (Validation.IsEmpty(address))
            {
                all_valid = false;
                message += "Please Enter Some Value in Address\n\n";
            }

            if (Validation.IsEmpty(phone_no))
            {
                all_valid = false;
                message += "Please Enter Some Value in Phone No\n\n";
            }
            else if(!Validation.IsOnlyDigitInString(phone_no))
            {
                all_valid = false;
                message += "Phone No Only Contains Digit\n\n";

            }
            if (all_valid)
            {
                if (operation.InsertCustomerDetails(first_name,last_name,address,phone_no))
                {
                    message = "New Customer Details are Saved in Database";
                    LoadDB();
                }
                else
                {
                    message = "There are some failure in Saving Customer Details in Database";
                }
            }
            MessageBox.Show(message);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cust_id != 0)
            {
                string message = "";
                bool all_valid = true;
                string first_name = textFirstName.Text.Trim();
                string last_name = textLastName.Text.Trim();
                string address = textAddress.Text.Trim();
                string phone_no = textPhoneNo.Text.Trim();

                if (Validation.IsEmpty(first_name))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in First Name\n\n";
                }

                if (Validation.IsEmpty(last_name))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Last Name\n\n";
                }

                if (Validation.IsEmpty(address))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Address\n\n";
                }

                if (Validation.IsEmpty(phone_no))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Phone No\n\n";
                }
                else if (!Validation.IsOnlyDigitInString(phone_no))
                {
                    all_valid = false;
                    message += "Phone No Only Contains Digit\n\n";

                }
                if (all_valid)
                {
                    if (operation.UpdateCustomerDetails(cust_id,first_name, last_name, address, phone_no))
                    {
                        message = "Customer Details are Updated in Database";
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in Saving Customer Details in Database";
                    }
                }
                MessageBox.Show(message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cust_id != 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Remove Record From Database?", "Movie System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string message = "";
                    if (operation.DeleteCustomerDetails(cust_id))
                    {
                        message = "Customer Details are Removed from Database";
                        cust_id = 0;
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in removing Customer Details in Database";
                    }
                    MessageBox.Show(message);
                }
            }
        }
    }
}
