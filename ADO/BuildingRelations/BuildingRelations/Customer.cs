using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BuildingRelations
{
    class Customer
    {
        static void Main(string[] args)
        {
            //1. let us create an in-memory cache (dataset)
            DataSet dsCustomerRelations = new DataSet("CustomerRelations");

            //2. add data table 1
            DataTable dtCustomers = new DataTable("Customers");

            //3. define columns to the datatable
            DataColumn[] dcCustomers = new DataColumn[4];
            dcCustomers[0] = new DataColumn("CustID", System.Type.GetType("System.Int32"));
            dcCustomers[1] = new DataColumn("CustName", System.Type.GetType("System.String"));
            dcCustomers[2] = new DataColumn("CustCountry", System.Type.GetType("System.String"));
            dcCustomers[3] = new DataColumn("CustTypeID", System.Type.GetType("System.Int32"));

            //4. Add the above columns to the datatable
            dtCustomers.Columns.Add(dcCustomers[0]);
            dtCustomers.Columns.Add(dcCustomers[1]);
            dtCustomers.Columns.Add(dcCustomers[2]);
            dtCustomers.Columns.Add(dcCustomers[3]);

            //5. now let us add rows with data
            DataRow drCustRows = dtCustomers.NewRow();
            drCustRows["CustID"] = 1;
            drCustRows["CustName"] = "Santhosh";
            drCustRows["CustCountry"] = "Bengaluru";
            drCustRows["CustTypeID"] = 1;

            //6. now add the above row to the table collection
            dtCustomers.Rows.Add(drCustRows);

            //repeat step 5 and 6 for as many rows we want to have in the datatable
            drCustRows = dtCustomers.NewRow();
            drCustRows["CustID"] = 2;
            drCustRows["CustName"] = "Srinivas";
            drCustRows["CustCountry"] = "Kasargod";
            drCustRows["CustTypeID"] = 3;

            dtCustomers.Rows.Add(drCustRows);

            drCustRows = dtCustomers.NewRow();
            drCustRows["CustID"] = 3;
            drCustRows["CustName"] = "Vinay";
            drCustRows["CustCountry"] = "Davanagere";
            drCustRows["CustTypeID"] = 1;

            dtCustomers.Rows.Add(drCustRows);

            drCustRows = dtCustomers.NewRow();
            drCustRows["CustID"] = 4;
            drCustRows["CustName"] = "Guru";
            drCustRows["CustCountry"] = "Mumbai";
            drCustRows["CustTypeID"] = 3;

            dtCustomers.Rows.Add(drCustRows);

            drCustRows = dtCustomers.NewRow();
            drCustRows["CustID"] = 5;
            drCustRows["CustName"] = "Shivraj";
            drCustRows["CustCountry"] = "Chennai";
            drCustRows["CustTypeID"] = 4;

            dtCustomers.Rows.Add(drCustRows);

            drCustRows = dtCustomers.NewRow();
            drCustRows["CustID"] = 6;
            drCustRows["CustName"] = "Bharath";
            drCustRows["CustCountry"] = "Delhi";
            drCustRows["CustTypeID"] = 4;

            dtCustomers.Rows.Add(drCustRows);

            //7. create another table
            DataTable dtCustType = new DataTable("CustomerType");

            //8. Columns for table 2
            DataColumn[] dcType = new DataColumn[2];
            dcType[0] = new DataColumn("CustTypeID", System.Type.GetType("System.Int32"));
            dcType[1] = new DataColumn("CustType", System.Type.GetType("System.String"));

            //9. adding columns to the table
            dtCustType.Columns.Add(dcType[0]);
            dtCustType.Columns.Add(dcType[1]);

            //10. rows for table 2 and add it to the tables by repeating 
            DataRow drType = dtCustType.NewRow();
            drType["CustTypeID"] = 1;
            drType["CustType"] = "Premium";

            dtCustType.Rows.Add(drType);

            drType = dtCustType.NewRow();
            drType["CustTypeID"] = 2;
            drType["CustType"] = "Standard";

            dtCustType.Rows.Add(drType);

            drType = dtCustType.NewRow();
            drType["CustTypeID"] = 3;
            drType["CustType"] = "Enterprise";

            dtCustType.Rows.Add(drType);

            drType = dtCustType.NewRow();
            drType["CustTypeID"] = 4;
            drType["CustType"] = "Trial";

            dtCustType.Rows.Add(drType);

            //11. add both the tables to the dataset
            dsCustomerRelations.Tables.Add(dtCustomers);
            dsCustomerRelations.Tables.Add(dtCustType);

            //12. setting relationship between 2 datatables of the dataset
            //12.1 primary key and foreign key
            DataColumn parent = dsCustomerRelations.Tables["CustomerType"].Columns["CustTypeID"];
            DataColumn child = dsCustomerRelations.Tables["Customers"].Columns["CustTypeID"];

            //12.2 setting the relation
            DataRelation custRel = new DataRelation("CustTypeRelation", parent, child);

            //12.3 adding relation to the dataset
            dsCustomerRelations.Relations.Add(custRel);

            //13. Display Data as per the relationship
            Console.WriteLine("===================================================================");
            Console.WriteLine("Customer Type ID       |       Customer Type     ");
            Console.WriteLine("-----------------------------------------------------------------");

            foreach (DataRow row in dsCustomerRelations.Tables["CustomerType"].Rows)
                Console.WriteLine("{0}                      |       {1}", row["CustTypeID"], row["CustType"]);

            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("CustID \t    |    CustName\t    |    CustCountry\t    |    Customer Type");
            Console.WriteLine("---------------------------------------------------------------------------------------");

            foreach (DataRow row in dsCustomerRelations.Tables["Customers"].Rows)
            {
                int irow = int.Parse(row["CustTypeID"].ToString());
                DataRow currentrow = dsCustomerRelations.Tables["CustomerType"].Rows[irow - 1];
                Console.WriteLine("{0}    \t    |    {1}    \t    |    {2}    \t    |    {3}", row["CustID"],
                    row["CustName"], row["CustCountry"], currentrow["CustType"]);
            }
            Console.Read();
        }
    }
}

