using System;
using System.Data;

namespace BuildingRelations
{
    class Relations_Constraints
    {
        public static void Main()
        {
            DataSet ds = new DataSet();

            DataTable ClassTable = ds.Tables.Add("OurClass");
            ClassTable.Columns.Add("CId", typeof(int));
            ClassTable.Columns.Add("ClassName", typeof(string));

            DataTable StudentTable = ds.Tables.Add("Students");
            StudentTable.Columns.Add("ClassId", typeof(int));
            StudentTable.Columns.Add("SID", typeof(int));
            StudentTable.Columns.Add("SName", typeof(string));

            //initialize pk constraint
            ClassTable.PrimaryKey = new DataColumn[] { ClassTable.Columns["CId"] };

            //add relations to the dataset
            ds.Relations.Add("classstudent", ClassTable.Columns["CId"], StudentTable.Columns["ClassId"]);


            //to set the foreign key constraint
            DataColumn dcclassid = ds.Tables["OurClass"].Columns["CId"];
            DataColumn dcstudentid = ds.Tables["Students"].Columns["ClassId"];

            ForeignKeyConstraint fkc = new ForeignKeyConstraint("csfkc", dcclassid, dcstudentid);

            //set the rules for foreign key constraint
            fkc.DeleteRule = Rule.SetNull;
            fkc.UpdateRule = Rule.Cascade;

            //add a unique constraint
            UniqueConstraint namecons = new UniqueConstraint(new DataColumn[] { ClassTable.Columns["ClassName"] });

            ds.Tables["OurClass"].Constraints.Add(namecons);

            //now let us give some data and check how these keys and constraints are working

            DataRow dr1 = ds.Tables["OurClass"].NewRow();
            dr1["CId"] = 1;
            dr1["ClassName"] = "Fifth";
            ClassTable.Rows.Add(dr1);

            dr1 = ds.Tables["OurClass"].NewRow();
            dr1["CId"] = 2;
            dr1["ClassName"] = "Sixth";
            ClassTable.Rows.Add(dr1);

            dr1 = ds.Tables["OurClass"].NewRow();
            dr1["CId"] = 3;
            dr1["ClassName"] = "Seventh";
            ClassTable.Rows.Add(dr1);

            dr1 = ds.Tables["OurClass"].NewRow();
            dr1["CId"] = 4;
            dr1["ClassName"] = "Eighth";
            ClassTable.Rows.Add(dr1);

            // Now, let's add data to the Students table
            DataRow dr2 = ds.Tables["Students"].NewRow();
            dr2["ClassId"] = 1;
            dr2["SID"] = 1;
            dr2["SName"] = "John";
            StudentTable.Rows.Add(dr2);

            dr2 = ds.Tables["Students"].NewRow();
            dr2["ClassId"] = 2;
            dr2["SID"] = 2;
            dr2["SName"] = "Jane";
            StudentTable.Rows.Add(dr2);

            dr2 = ds.Tables["Students"].NewRow();
            dr2["ClassId"] = 3;
            dr2["SID"] = 3;
            dr2["SName"] = "Jack";
            StudentTable.Rows.Add(dr2);

            dr2 = ds.Tables["Students"].NewRow();
            dr2["ClassId"] = 4;
            dr2["SID"] = 4;
            dr2["SName"] = "Jill";
            StudentTable.Rows.Add(dr2);

            //Uncomment to test primary key violation
             dr1 = ds.Tables["OurClass"].NewRow();
            dr1["CId"] = 1; // primary key violation
            dr1["ClassName"] = "Ninth";
            ClassTable.Rows.Add(dr1);

            // Uncomment to test foreign key violation
            // dr2 = ds.Tables["Students"].NewRow();
            // dr2["ClassId"] = 5; // foreign key violation
            // dr2["SID"] = 5;
            // dr2["SName"] = "Infinite";
            // StudentTable.Rows.Add(dr2);

            Console.WriteLine("Classes:");
            foreach (DataRow row in ClassTable.Rows)
            {
                Console.WriteLine($"CId: {row["CId"]}, ClassName: {row["ClassName"]}");
            }

            Console.WriteLine("\nStudents:");
            foreach (DataRow row in StudentTable.Rows)
            {
                Console.WriteLine($"ClassId: {row["ClassId"]}, SID: {row["SID"]}, SName: {row["SName"]}");
            }

            Console.Read();
        }
    }
}
