using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Connected_Eg1
{
    //-----------------------Business Access Layer BAL--------------------
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }

        public DataAccess da;
        public string InsertRegion()
        {
            Console.WriteLine("Enter Region ID");
            RegionId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Region Description");
            RegionDescription = Console.ReadLine();
            string retval = DataAccess.InsertRegion(RegionId, RegionDescription);
            return retval;
        }

        public SqlDataReader DisplayRegion()
        {
            SqlDataReader sdr = DataAccess.DisplayRegion();
            return sdr;
        }
    }
    //-----------------------------Data Access Layer DAL-------------------
    public class DataAccess
    {
        public static SqlConnection con;
        public static SqlCommand cmd;
        public static SqlDataReader dr;

        public static SqlConnection getcon()
        {
            con = new SqlConnection(@"data source=ICS-LT-17YRQ73\SQLEXPRESS; initial catalog=Northwind;" + "user id=sa; password=santu2001;");
            con.Open();
            return con;
        }

        public static string InsertRegion(int Rid, string rdesc)
        {
            con = getcon();
            cmd = new SqlCommand("insert into region values(@rid, @rdesc)", con);
            cmd.Parameters.AddWithValue("@rid", Rid);
            cmd.Parameters.AddWithValue("@rdesc", rdesc);
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
                return "Success";
            else
                return "Failed";
        }

        public static SqlDataReader DisplayRegion()
        {
            con = getcon();
            cmd = new SqlCommand("Select * from region", con);
            dr = cmd.ExecuteReader();
            return dr;
        }
    }

    //---------------------Client Program-------------------------

    class Client
    {
        static void Main()
        {
            Region region = new Region();

            // Insert region
            string result = region.InsertRegion();
            Console.WriteLine(result);

            // Display regions
            SqlDataReader myreader = region.DisplayRegion();
            while (myreader.Read())
            {
                Console.WriteLine(myreader[0] + " " + myreader[1]);
            }

            Console.Read();
        }
    }
}

