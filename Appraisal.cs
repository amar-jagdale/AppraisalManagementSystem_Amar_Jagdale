using System;
using DataAccessLayer;
using Model;
namespace AMS
{
    class Appraisal
    {
        static void Main(string[] args)
        {
            Choices();
        }

        public static void Choices()
        {
            Console.WriteLine("=============================== APPRAISAL MANAGEMENT SYSTEM ==========================");
            Console.WriteLine("1: INSERT NEW EMPLOYEE DATA");
            Console.WriteLine("2: MODIFICATION BASED ON APPRAISAL OF EMPLOYEE DATA");
            Console.WriteLine("3: DELETE EMPLOYEE ROLE");
            Console.WriteLine("4: EMPLOYEE APPRAISAL REPORT");

           

            bool loopContinue = true;
            while (loopContinue)
            {
                Console.WriteLine("Enter The Number as per your Choice.........(1/2/3/4)");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    // Providing choice for user for Which Module Run first
                    case 1:
                        DAL obj = new DAL();
                        Console.WriteLine("---------------------- INSERT NEW EMPLOYEE DETAILS -----------------------");
                        Employee emp = obj.GetInputFromUser();
                        obj.AddNewEmployee(emp);
                        obj.DisplayEmployee();
                        loopContinue = false;
                        goto default;

                    case 2:
                        DAL obj1 = new DAL();
                        Console.WriteLine("------------------UPDATE THE EMPLOYEE ROLE AFTER GETTING APPRAISAL---------");
                        Employee editemp = obj1.GetEditDetailFromUser();
                        obj1.EditEmployee(editemp);
                        obj1.DisplayEmployee();
                        //Console.WriteLine("=========================================================================");
                        loopContinue = false;
                        goto default;

                    case 3:
                        DAL obj3 = new DAL();
                        Console.WriteLine("----------------------- DELETE EMPLOYEE ROLE ------------------------------");
                        obj3.DisplayEmployee();
                        obj3.DeleteEmployeeRole();
                        //obj3.DisplayEmployee();

                        loopContinue = false;
                        goto default;


                    case 4:
                        DAL obj2 = new DAL();
                        Console.WriteLine("-------------------------- EMPLOYEE APPRAISAL REPORT ----------------------");
                        obj2.AppraisalReport_1();
                        obj2.AppraisalReport_2();
                        obj2.AppraisalReport_3();
                        obj2.AppraisalReport_4();
                        //obj2.DisplayEmployee();
                        loopContinue = false;
                        goto default;



                    default:
                        loopContinue = true;
                        Console.WriteLine("---------------------- Do You Want Continue Please Enter 'YES' Otherwise Press Any Key to Exit-----------------------");
                        Console.WriteLine("");
                        string choice1 = Convert.ToString(Console.ReadLine());
                      

                        if (choice1 == "YES")
                        {
                            Choices();
                        }
                        else
                        {
                            loopContinue = false;
                        }
                        break;
                        

                }
                if (loopContinue)
                    Console.WriteLine("--------------------------------------- Please Enter Correct Choice Once Again --------------------------------");

            }
        }
    }

}

