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
    public partial class MovieForm : Form
    {
        public Form HomeForm { get; set; }
        public DBOperation operation;
        DataTable MovieTable = new DataTable();
        int movieID;

        public MovieForm()
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
            DataSet dataset = operation.GetMovieDetails();
            foreach(DataRow row in dataset.Tables[0].Rows)
            {
                MovieTable.Rows.Add(row["movie_id"], row["title"], row["genre"], row["rating"], row["rental_cost"], row["release_year"]);
            }
            dgvMovieGrid.DataSource = MovieTable;
        }

        private void DataTableColumns()
        {
            MovieTable.Clear();
            try
            {
                MovieTable.Columns.Add("Movie ID");
                MovieTable.Columns.Add("Movie Title");
                MovieTable.Columns.Add("Genre");
                MovieTable.Columns.Add("Rating");
                MovieTable.Columns.Add("Rental Cost");
                MovieTable.Columns.Add("Release Year");                
            }
            catch(Exception ex)
            {

            }
        }

        private void MovieForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            operation.CloseConnection();
            this.HomeForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string message = "";
            bool all_valid = true;
            string title = textMovieTitle.Text.Trim();
            string year = textReleaseYear.Text.Trim();
            string genre = textGenre.Text.Trim();
            float rating = (float)numericRating.Value;
            if(Validation.IsEmpty(title))
            {
                all_valid = false;
                message += "Please Enter Some Value in Title\n\n";
            }

            if (Validation.IsEmpty(year))
            {
                all_valid = false;
                message += "Please Enter Some Value in Release Year\n\n";
            }
            else if( year.Length != 4 )
            {
                all_valid = false;
                message += "Please Enter Four Digit in Release Year\n\n";
            }
            else if(!Validation.IsNumber(year))
            {
                all_valid = false;
                message += "Please Enter Number in Release Year\n\n";
            }

            if (Validation.IsEmpty(genre))
            {
                all_valid = false;
                message += "Please Enter Some Value in Genre\n\n";
            }
            if(all_valid)
            {
                int release_year = int.Parse(year);
                float rental_cost = 5;
                if(release_year < DateTime.Now.Year - 5 )
                {
                    rental_cost = 2;
                }
                if(operation.InsertMovieDetails(title,rating,release_year,genre,rental_cost))
                {
                    message = "New Movie Details are Saved in Database";
                    LoadDB();
                }
                else
                {
                    message = "There are some failure in saveing Movie Details in Database";
                }
            }
            MessageBox.Show(message);
        }

        private void dgvMovieGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                movieID = int.Parse(dgvMovieGrid.Rows[e.RowIndex].Cells[0].Value.ToString());
                textGenre.Text = dgvMovieGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                textMovieTitle.Text = dgvMovieGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                textReleaseYear.Text = dgvMovieGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                numericRating.Value = int.Parse(dgvMovieGrid.Rows[e.RowIndex].Cells[3].Value.ToString());
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(movieID!=0)
            {
                string message = "";
                bool all_valid = true;
                string title = textMovieTitle.Text.Trim();
                string year = textReleaseYear.Text.Trim();
                string genre = textGenre.Text.Trim();
                float rating = (float)numericRating.Value;
                if (Validation.IsEmpty(title))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Title\n\n";
                }

                if (Validation.IsEmpty(year))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Release Year\n\n";
                }
                else if (year.Length != 4)
                {
                    all_valid = false;
                    message += "Please Enter Four Digit in Release Year\n\n";
                }
                else if (!Validation.IsNumber(year))
                {
                    all_valid = false;
                    message += "Please Enter Number in Release Year\n\n";
                }

                if (Validation.IsEmpty(genre))
                {
                    all_valid = false;
                    message += "Please Enter Some Value in Genre\n\n";
                }
                if (all_valid)
                {
                    int release_year = int.Parse(year);
                    float rental_cost = 5;
                    if (release_year < DateTime.Now.Year - 5)
                    {
                        rental_cost = 2;
                    }
                    if (operation.UpdateMovieDetails(movieID, title, rating, release_year, genre, rental_cost))
                    {
                        message = "Movie Details are Updated in Database";
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in saving Movie Details in Database";
                    }
                }
                MessageBox.Show(message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (movieID != 0)
            {
                DialogResult result = MessageBox.Show("Are You Sure To Remove Record From Database?", "Movie System", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(result==DialogResult.Yes)
                {
                    string message = "";
                    if (operation.DeleteMovieDetails(movieID))
                    {
                        message = "Movie Details are Removed from Database";
                        LoadDB();
                    }
                    else
                    {
                        message = "There are some failure in removing Movie Details in Database";
                    }
                    MessageBox.Show(message);
                }
            }
        }
    }
}
