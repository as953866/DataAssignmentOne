using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAssignmentOne.DBUtility
{
    public class DBOperation
    {
        private string connectionString = @"Data Source=SINGH-786\SQLEXPRESS;Initial Catalog=moviestore;Integrated Security=True;";

        private SqlConnection conn;

        public DBOperation()
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        public bool CheckConnectionState()
        {                                                                                                         
            if(conn != null && conn.State == ConnectionState.Open )
            {
                return true;
            }
            return false;
        }

        public void CloseConnection()
        {
            if(conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

        public bool InsertMovieDetails(string title,float rating,int release_year,string genre,float rental_cost)
        {
            try
            {
                string query = "insert into movies(title,rating,release_year,genre,rental_cost) values(@title,@rating,@release_year,@genre,@rental_cost)";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@title", title));
                cmd.Parameters.Add(new SqlParameter("@rating", rating));
                cmd.Parameters.Add(new SqlParameter("@release_year", release_year));
                cmd.Parameters.Add(new SqlParameter("@genre", genre));
                cmd.Parameters.Add(new SqlParameter("@rental_cost", rental_cost));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool UpdateMovieDetails(int movie_id,string title, float rating, int release_year, string genre, float rental_cost)
        {
            try
            {
                string query = "update movies set title=@title,rating=@rating,release_year=@release_year,genre=@genre,rental_cost=@rental_cost where movie_id = @movie_id ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@title", title));
                cmd.Parameters.Add(new SqlParameter("@rating", rating));
                cmd.Parameters.Add(new SqlParameter("@release_year", release_year));
                cmd.Parameters.Add(new SqlParameter("@genre", genre));
                cmd.Parameters.Add(new SqlParameter("@rental_cost", rental_cost));
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteMovieDetails(int movie_id)
        {
            try
            {
                string query = "delete from movies where movie_id = @movie_id ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet GetMovieDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "select * from movies";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        public float GetMovieRent(int movie_id)
        {
            float rental_cost = 0;
            try
            {
                string query = "select rental_cost from movies where movie_id = @movie_id";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    rental_cost = float.Parse(reader[0].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
            }
            return rental_cost;
        }

        public bool InsertCustomerDetails(string first_name, string last_name, string address, string phone_no)
        {
            try
            {
                string query = "insert into customer(first_name,last_name,address,phone_no) values(@first_name,@last_name,@address,@phone_no)";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@first_name", first_name));
                cmd.Parameters.Add(new SqlParameter("@last_name", last_name));
                cmd.Parameters.Add(new SqlParameter("@address", address));
                cmd.Parameters.Add(new SqlParameter("@phone_no", phone_no));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateCustomerDetails(int cust_id, string first_name, string last_name,  string address, string phone_no)
        {
            try
            {
                string query = "update customer set first_name=@first_name,last_name=@last_name,address=@address,phone_no=@phone_no  where cust_id = @cust_id ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@first_name", first_name));
                cmd.Parameters.Add(new SqlParameter("@last_name", last_name));
                cmd.Parameters.Add(new SqlParameter("@address", address));
                cmd.Parameters.Add(new SqlParameter("@phone_no", phone_no));
                cmd.Parameters.Add(new SqlParameter("@cust_id", cust_id));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCustomerDetails(int cust_id)
        {
            try
            {
                string query = "delete from customer where cust_id = @cust_id ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@cust_id", cust_id));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet GetCustomerDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "select * from customer";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        public DataTable ViewCustomerDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from viewCustomer ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "--Select Customer--" };
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public DataTable ViewMovieDetails()
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "select * from viewMovie ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                DataRow dr = dt.NewRow();
                dr.ItemArray = new object[] { 0, "--Select Movie--" };
                dt.Rows.InsertAt(dr, 0);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public bool IssueMovieToCustomer(int movie_id,int cust_id,float rented_cost,DateTime date_rented)
        {
            try
            {
                string query = "insert into rented_movies(movie_id_fk,cust_id_fk,rented_cost,date_rented,date_returned) values(@movie_id,@cust_id,@rented_cost,@date_rented,null)";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@movie_id", movie_id));
                cmd.Parameters.Add(new SqlParameter("@cust_id", cust_id));
                cmd.Parameters.Add(new SqlParameter("@rented_cost", rented_cost));
                cmd.Parameters.Add(new SqlParameter("@date_rented", date_rented));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet GetRentedMovieDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "prcShowRentedMovies";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        public DataSet GetOutRentedMovieDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "prcShowOutRentedMovies";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
            }
            return ds;
        }
        public bool ReturnMovie(int rmid, DateTime date_returned)
        {
            try
            {
                string query = "update rented_movies set date_returned = @date_returned where rmid = @rmid";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@date_returned", date_returned));
                cmd.Parameters.Add(new SqlParameter("@rmid", rmid));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool DeleteRentedDetails(int rmid)
        {
            try
            {
                string query = "delete from rented_movies where rmid = @rmid ";
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.Parameters.Add(new SqlParameter("@rmid", rmid));
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
