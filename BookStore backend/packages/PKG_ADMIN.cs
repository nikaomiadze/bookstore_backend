using BookStore_backend.models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BookStore_backend.packages
{
    public interface IPKG_ADMIN
    {
        void Add_book(Book book);
        List<Order> Get_Orders();

    }
    public class PKG_ADMIN:PKG_BASE, IPKG_ADMIN
    {
        public PKG_ADMIN(IConfiguration configuration) : base(configuration) { }

        public void Add_book(Book book) {

            OracleConnection conn = new OracleConnection
            {
                ConnectionString = ConnStr
            };
            try
            {
                conn.Open();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "olerning.PKG_BOOKSTORE_ADMIN.add_book";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("v_book_name", OracleDbType.Varchar2).Value = book.Book_name;
                cmd.Parameters.Add("v_author", OracleDbType.Varchar2).Value = book.Author;
                cmd.Parameters.Add("v_quantity", OracleDbType.Int32).Value = book.Quantity;
                cmd.Parameters.Add("v_price", OracleDbType.Int32).Value = book.Price;


                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

            conn.Close();
        }
        public List<Order> Get_Orders()
        {

            List<Order> list = new List<Order>();

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_BOOKSTORE_ADMIN.get_order";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Order order = new Order();
                order.Id = int.Parse(reader["id"].ToString());
                order.User_name = reader["user_name"].ToString();
                order.Book_name = reader["book_name"].ToString();
                order.Quantity = int.Parse(reader["quantity"].ToString());
                order.Order_price = int.Parse(reader["order_price"].ToString());

                list.Add(order);
            }
            conn.Close();
            return list;
        }

    }
}
