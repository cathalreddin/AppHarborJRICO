﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace JRICO.CodeArea
{
    public class WriteToLog
    {
        public void WriteLog(string LogData, string LogUser)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString);
            try
            {
                conn.Open();
                String MySql = @"INSERT INTO Log(LogData, LogUser) VALUES('" + LogData + "','" + LogUser + "')";
                SqlCommand MyCmd = new SqlCommand(MySql, conn);
                MyCmd.ExecuteScalar();
                conn.Close();                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}