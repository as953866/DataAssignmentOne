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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm form = new CustomerForm();
            form.Show();
            form.HomeForm = this;
            this.Hide();
        }

        private void btnMovie_Click(object sender, EventArgs e)
        {
            MovieForm form = new MovieForm();
            form.Show();
            form.HomeForm = this;
            this.Hide();
        }

        private void btnMovieRent_Click(object sender, EventArgs e)
        {
            MovieRentForm form = new MovieRentForm();
            form.Show();
            form.HomeForm = this;
            this.Hide();
        }
    }
}
