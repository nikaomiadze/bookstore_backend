using BookStore_backend.models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BookStore_backend.packages
{
    public interface IPKG_USER
    {
        void Add_user(User user);
        void Add_order(Order order);
        void Complete_order(Complete_order order);
        List<Book> Get_Books();

    }
    public class PKG_USER:PKG_BASE , IPKG_USER
    {
        public PKG_USER(IConfiguration configuration) : base(configuration) { }
        public void Add_user(User user) {

            OracleConnection conn = new OracleConnection
            {
                ConnectionString = ConnStr
            };
            try
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "olerning.PKG_BOOKSTORE_USER.add_user";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("v_first_name", OracleDbType.Varchar2).Value = user.First_name;
                cmd.Parameters.Add("v_last_name", OracleDbType.Varchar2).Value = user.Last_name;
              
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            conn.Close();
        }
        public void Add_order(Order order)
        {
            OracleConnection conn = new OracleConnection
            {
                ConnectionString = ConnStr
            };
            try
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "olerning.PKG_BOOKSTORE_USER.add_order";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("v_user_id", OracleDbType.Int32).Value = order.User_id;
                cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = order.Book_id;
                cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = order.Quantity;


                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            conn.Close();
        }
        public void Complete_order(Complete_order order)
        {
            OracleConnection conn = new OracleConnection
            {
                ConnectionString = ConnStr
            };
            try
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "olerning.PKG_BOOKSTORE_USER.complete_order";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("v_book_id", OracleDbType.Int32).Value = order.Book_id;
                cmd.Parameters.Add("v_order_id", OracleDbType.Int32).Value = order.Order_id;


                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            conn.Close();
        }
        public List<Book> Get_Books()
        {

            List<Book> list = new List<Book>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_BOOKSTORE_USER.get_book";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Book book = new Book();
                book.Id = int.Parse(reader["id"].ToString());
                book.Book_name = reader["book_name"].ToString();
                book.Author = reader["author"].ToString();
                book.Quantity = int.Parse(reader["quantity"].ToString());
                book.Price = int.Parse(reader["price"].ToString());
               
                list.Add(book);
            }
            conn.Close();
            return list;
        }


    }
}

