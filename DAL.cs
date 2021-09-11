using System;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace DataAccessLayer
{
    public class DAL
    {
        DataTable result1 = new DataTable();

        static string constr = "data source=DESKTOP-TL0FRLL\\SQLEXPRESS;Initial Catalog=EAMS;integrated security=True";  //Connection String
        public void DisplayEmployee()
        {
            DataTable dt = ExecuteData("select * from EMP_TABLE");
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("====================================================================================================================================================");
                Console.WriteLine("--------------------------------------------------- EMPLOYEE DETAILS -------------------------------------------------------------------------------");
                Console.WriteLine("====================================================================================================================================================");

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(" {0,-8} | {1,-20} | {2,-8} | {3,-12} | {4,-8} | {5,-20} | {6,-10} | {7,-10} | {8,-10}  ", row["Emp_Id"].ToString(), row["Emp_Name"].ToString(),
                     row["Emp_Dept"].ToString(), row["Emp_Salary"].ToString(), row["Emp_DOJ"].ToString(), row["Emp_New_Role"].ToString(), row["Emp_Appr_Date"].ToString(), row["Emp_Appr_Salary"].ToString(), row["HIKE"].ToString());

                    //Console.WriteLine(row["Emp_Id"].ToString() + "  " + row["Emp_Name"].ToString() + "    " + row["Emp_Dept"].ToString() + "    " +
                    //  row["Emp_Salary"].ToString() + "    " + row["Emp_Current_Role"].ToString() + "    " + row["Emp_DOJ"].ToString() + "    " + row["Emp_New_Role"].ToString() + "    " + row["Emp_Appr_Date"].ToString() + "    " + row["Emp_Appr_Salary"].ToString()+"    "+row["HIKE"].ToString());
                }

                Console.WriteLine("=====================================================================================================================================================" + Environment.NewLine);
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("No Employee Records found in the Database Table ....");
                Console.WriteLine(Environment.NewLine);
            }
        }

        public DataTable ExecuteData(string query)
        {
           
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))  //This block will release the memory when object is not in use
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    SqlDataAdapter da = new SqlDataAdapter(sqlcmd);   //Disconnected Architecture
                    da.Fill(result1);
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result1;
        }

        public bool ExecuteCommand(string query)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(constr))
                {
                    sqlcon.Open();
                    SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                    sqlcmd.ExecuteNonQuery();                                     //It Executes the DML Queries
                    sqlcon.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
                throw;
            }
            return true;
        }

        //---------- Insertion of New Employee Details -----------------------------------------------------------
        public void AddNewEmployee(Employee employee)
        {
            //-------------- Insertion Query for New Employee Details --------------------------------------------           
            ExecuteCommand(string.Format("insert into EMP_TABLE(Emp_Name,Emp_Dept,Emp_Salary,Emp_Current_Role,Emp_DOJ) values('{0}','{1}','{2}','{3}','{4}')",
            employee.Emp_Name, employee.Emp_Dept, employee.Emp_Salary, employee.Emp_Current_Role, employee.Emp_DOJ));
            
        }

        //-----------Modifying the Existing Employee Role Using Perticular Employee ID -------------------------
        public void EditEmployee(Employee employee)
        {
           
            ExecuteCommand(string.Format("UPDATE Emp_table SET Emp_New_Role = '"  + employee.Emp_New_Role + "', Emp_Appr_Date =  '" + employee.Emp_Appr_Date + "', Emp_Appr_Salary = ' " + employee.Emp_Appr_Salary + "', HIKE =  '" + employee.Emp_Hike + "' WHERE Emp_Id =  '" + employee.Emp_Id + "' "));
        }


        //------- Here we update the Employee Salary and also its Job Role after the Appraisal using Employee ID -------------------------------
        // Also Shows how much Employee Got the Salry Hike in THE Hike Colomn
        public Employee GetEditDetailFromUser()
        {
            int Emp_Id;
            string Emp_Current_Role = string.Empty;
            string Emp_Hke = string.Empty;
            decimal Emp_Appr_Salary;

            Console.WriteLine("Enter the Employee ID of The Employee You want to Give Appraisal");
            Emp_Id = Convert.ToInt32(Console.ReadLine());

            ExecuteData(string.Format("SELECT Emp_Salary from Emp_TABLE where Emp_Id= " + Emp_Id + " "));
            decimal salary = Convert.ToDecimal(result1.Rows[0]["Emp_Salary"]);

            Console.WriteLine("Enter The New Employee Role which You Provide after the Getting Appraisal to the Employee");
            Emp_Current_Role = Console.ReadLine();

            Console.WriteLine("Enter the Employee Appraisal Hike in Percentage you Want to give the Employee:");
            Console.WriteLine("HIKE should be in this Format Like --- 10, 20 ,30 etc......");
            Emp_Hke = Console.ReadLine();
            decimal Emp_Hike = Convert.ToDecimal(Emp_Hke);

            //------------- Calculating the Salary After the Getting Appraisal --------------------------------
            Emp_Appr_Salary = (salary * Emp_Hike / 100) + salary;
            Console.WriteLine("---------------------- Employee Appraisal UPDATED Successfully ----------------------------");


            Employee employee = new Employee()
            {
                Emp_Id = Emp_Id,
                Emp_New_Role = Emp_Current_Role,
                Emp_Appr_Date = DateTime.Now,
                Emp_Appr_Salary = Emp_Appr_Salary,
                Emp_Hike = Emp_Hike,
            };
            return employee;
        }


        // Delete The Perticular Role of Employee 
        public void DeleteEmployeeRole()
        {
            int Emp_Id;
            Console.WriteLine("Enter Employee No which you want to delete the role from the database :");
            Emp_Id = Convert.ToInt32(Console.ReadLine());
             
            // Update Query for removing perticular record from thye cell....................
            ExecuteCommand(string.Format("UPDATE Emp_table SET Emp_New_Role = null WHERE Emp_Id = " + Emp_Id + " "));
            Console.WriteLine("Employee Role Deleted Successfully from the Database!........" + Environment.NewLine);
        }


        public Employee GetInputFromUser()
        {
            // Employee ID is Set as IDENTITY so No Need to take from User it automaticaly Increseas Count of ID
            string Emp_Name = string.Empty;
            string Emp_Dept = string.Empty;
            decimal Emp_Salary;
            string Emp_Current_Role = string.Empty;
            DateTime Emp_DOJ;
            Console.WriteLine("============================================================================================");

            Console.WriteLine("Enter the Employee Name: ");
            Emp_Name = Convert.ToString(Console.ReadLine());
            while (string.IsNullOrEmpty(Emp_Name))                                 // Name Validation........1          
            {
                Console.WriteLine("Name Filed is Required ,Please Enter Employee Name Again.");
                //Console.WriteLine(".");
                Emp_Name = Convert.ToString(Console.ReadLine());
            }

            Console.WriteLine("Enter the Employee Department: ");
            Emp_Dept = Convert.ToString(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("Enter the Employee Salary: ");
                Emp_Salary = Convert.ToDecimal(Console.ReadLine());                    // Employee Salary Validation 2 (Should Be Greater Than 10000)

                if (Emp_Salary > 10000)
                {
                    break;//Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("ReEnter salary");
                    continue;
                }
            }

            Console.WriteLine("Enter the Employee Currently Assigned Role: ");
            Emp_Current_Role = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter the Employee Joining Date.....Format(YYYY-MM-DD)");
            Emp_DOJ = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("---------------------------- Employee Data Submitted Successfully -----------------------------!");
              
            //----- Propert Class Object
            Employee employee = new Employee()
            {
                Emp_Name = Emp_Name,
                Emp_Dept = Emp_Dept,
                Emp_Salary = Emp_Salary,
                Emp_Current_Role = Emp_Current_Role,
                Emp_DOJ = Emp_DOJ,
            };

            return employee;
        }

        //............................... Employee__Appraisal__Report----List of Employees who joined as a Intern and Now are Managers----- ........................... 
        public void AppraisalReport_1()
        {
            DataTable dt = ExecuteData("select Emp_Id,Emp_Name,Emp_Current_Role,Emp_New_Role from Emp_TABLE where Emp_Current_Role='Intern' and Emp_New_Role='Manager'");
            if (dt.Rows.Count > 0)
            {
                //Console.WriteLine(Environment.NewLine);
                Console.WriteLine("==========================================================================================");
                Console.WriteLine("----------------- List of Employees who joined as a Intern and Now are Managers-----------");
                Console.WriteLine("==========================================================================================");

                foreach (DataRow row in dt.Rows)
                { 
                    Console.WriteLine(row["Emp_Id"].ToString() + "  " + row["Emp_Name"].ToString() + "  " + row["Emp_Current_Role"].ToString() + " " + row["Emp_New_Role"].ToString());
                }

                Console.WriteLine("============================================================================================" + Environment.NewLine);
                dt.Rows.Clear();
            }
            else
            {
               Console.WriteLine("No Such Employee Records found in the Database Table ....");
               // Console.WriteLine(Environment.NewLine);
            }

        }
        //.................................... Employee Who have Not Getting New Role After Appraisal  ..........................
        public void AppraisalReport_2()
        {
            DataTable dt = ExecuteData("select Emp_Id,Emp_Name,Emp_New_Role,Emp_Appr_Date,Emp_Appr_Salary from EMP_TABLE where Emp_New_Role is Null and Emp_Appr_Date is Null and Emp_Appr_Salary is Null");
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("==========================================================================================");
                Console.WriteLine("------------------------- List of Employees who did not have Appraisal -------------------");
                Console.WriteLine("==========================================================================================");

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["Emp_Id"].ToString()+"  "+row["Emp_Name"].ToString() + "  " + row["Emp_New_Role"].ToString() + "  " + row["Emp_Appr_Date"].ToString() + "  " + row["Emp_Appr_Salary"].ToString());
                }

                Console.WriteLine("===========================================================================================" + Environment.NewLine);
                dt.Rows.Clear();
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("No Such Employee Records found in the Database Table ....");
                Console.WriteLine(Environment.NewLine);
            }

        }

        //.................................... Employees for who role was not changed after appraisal  .............................................
        public void AppraisalReport_3()
        {
            DataTable dt = ExecuteData("select Emp_Id,Emp_Name,Emp_Current_Role,Emp_New_Role from EMP_TABLE where Emp_Current_Role in (Emp_New_Role)");
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("==========================================================================================");
                Console.WriteLine("--------------- List of Employees For Who Role Was Not changed After Appraisal -----------");
                Console.WriteLine("==========================================================================================");

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["Emp_Id"].ToString()+"  "+row["Emp_Name"].ToString() + "  " + row["Emp_Current_Role"].ToString()+" "+row["Emp_New_Role"].ToString());
                }

                Console.WriteLine("==========================================================================================" + Environment.NewLine);
                dt.Rows.Clear();
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("No Such Employee Records found in the Database Table ....");
                Console.WriteLine(Environment.NewLine);
            }

        }

        //.................................... Employee with maximum appraisals   .............................................
        public void AppraisalReport_4()
        {
            //------ This Query Shows Whoes Employee Getting Maximun Hike in Appraisal with its Details .......................
            DataTable dt = ExecuteData("select Emp_Id,Emp_Name,Emp_Appr_Salary,HIKE from EMP_TABLE where HIKE=(select max(HIKE) from EMP_TABLE)");
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("===========================================================================================");
                Console.WriteLine("-------------------------------- Employees with maximum appraisals ------------------------");  
                Console.WriteLine("===========================================================================================");

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine(row["Emp_Id"].ToString() + "  " + row["Emp_Name"].ToString() + "  " + row["Emp_Appr_Salary"].ToString()+" "+row["HIKE"].ToString());
                }

                Console.WriteLine("===========================================================================================" + Environment.NewLine);
                dt.Rows.Clear();
            }
            else
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("No Such Employee Records found in the Database Table ....");
                Console.WriteLine(Environment.NewLine);
            }

        }
    }
}
