using System;
using System.Data;
using System.Data.SqlClient;
namespace Model
{
    public class Employee
    {
		public int Emp_Id { get; set; }
		public string Emp_Name { get; set; }
		public string Emp_Dept { get; set; }
		public decimal Emp_Salary { get; set; }
		public string Emp_Current_Role { get; set; }
		public DateTime Emp_DOJ { get; set; }
		public string Emp_New_Role { get; set; }
		public DateTime Emp_Appr_Date { get; set; }
		public decimal Emp_Appr_Salary { get; set; }
		public decimal Emp_Hike { get; set; }
	}
}
