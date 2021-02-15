using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

#region 
/*  User Manual
1. Chiqish = exit


*/
#endregion

namespace ConsoleApp4
{
    class Program
    {
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["ConsoleDB"].ConnectionString;

        private static SqlConnection sqlConnection = null;

        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(ConnectionString); // Yangi SQL Malumotlar Omboriga ulanish uchun "sqlConnection" Obyekti yaratildi
            sqlConnection.Open();  // Ulanish uchun PORT Ochildi

            Console.WriteLine("ConsoleApp");

            SqlDataReader sqlDataReader = null;  // "SqlDataReader" Bu obyekt DB dan malumotlarni tanlash uchun ishlatiladi

            string command = string.Empty;  //  Bu O'zgaruvchi User kiritgan kamandani saqlash uchun ishlatiadi


            while (true)
            {
                Console.Write("> ");
                command = Console.ReadLine();
                #region Exit //EXIT kamandasini kiritganda dasturdan chiqish uchun ishlatiladigan Blok Kode
                if (command.ToLower().Equals("exit"))
                {
                    if (sqlConnection.State == ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                    if (sqlDataReader != null)
                    {
                        sqlDataReader.Close();
                    }
                    break;
                }
                #endregion    

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);


                //SELECT * FROM [Database1] WHERE  Id=1

                switch (command.Split(' ')[0].ToLower())
                {
                    case "select":

                        sqlDataReader = sqlCommand.ExecuteReader();

                        while (sqlDataReader.Read())
                        {
                            Console.WriteLine($" {sqlDataReader["Id"]} {sqlDataReader["Name"] }" +
                                $" {sqlDataReader["Age"]}");

                            Console.WriteLine(new string ('-', 30));
                        }
                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }

                        break;
                    case "delete":
                        Console.WriteLine($"O'chirildi: {sqlCommand.ExecuteNonQuery()}");
                        break;
                    case "insert":

                        Console.WriteLine($"Qo'shildi: {sqlCommand.ExecuteNonQuery()}");  //Table

                        break;
                    case "update":
                        Console.WriteLine($"O'zgartirildi: {sqlCommand.ExecuteNonQuery()}");
                        break;

                    default:
                        Console.WriteLine($" {command} Kamandasi toto'g'ri kiritildi");
                        break;
                }



            }
            Console.WriteLine("Davom etish uchun istalgan tugmani bosing");
            Console.ReadLine();
        }

         

    }
}
//INSERT INTO [dbo].[Table] ([Id], [Name], [Age]) VALUES (1, N'Man', N'15')