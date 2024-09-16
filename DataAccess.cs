using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Configuration;

namespace POSBarasaki

{
    public class DataAccess
    {
        public static DataTable ExecuteSelectCommand(DbCommand command)
        {
            //The DataTable to be returned
            DataTable table;
            //Execute the command making sure the connection gets closed in the end
            try
            {
                //Open the data connection
                command.Connection.Open();
                //Execute the command and save the results in a DataTable
                DbDataReader reader = command.ExecuteReader();
                table = new DataTable();
                table.Load(reader);

                //Close the reader
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }
        public static SqlCommand CreateCommand()
        {
            //string Pass = "";

            //UserID = HttpContext.Current.Session["userName"].ToString();
            //Pass = HttpContext.Current.Session["pass"].ToString();

            //string connectionString = "Data Source=.;Initial Catalog=DYN_BPK;Integrated Security=True";
            string connectionString = "";
            try
            {
                connectionString = "Data Source = 192.168.1.247; Initial Catalog = sai_db; User ID = sa; Password = 1nd()n3514572; Encrypt = False";

                //connectionString += "User ID=" + UserID + ";Password=" + Pass;
            }
            catch (Exception ex)
            {
                throw ex;

            }
            SqlConnection conn = new SqlConnection(connectionString);
            //Set the connection string
            conn.ConnectionString = connectionString;
            SqlCommand comm = conn.CreateCommand();
            //Set the command type to stored procedure
            comm.CommandType = CommandType.StoredProcedure;
            return comm;
        }
        public static string ConvertCurrencyToTextEnglish(decimal amount)
        {
            if (amount == 0)
            {
                return "Zero Rupiah";
            }

            string[] units = {  "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = {  "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string result = "";

            // Process trillions
            long trillions = (long)amount / 1_000_000_000_000;
            if (trillions > 0)
            {
                result += ConvertThreeDigitsToTextEnglish(trillions) + " Trillion ";
            }

            // Process billions
            long billions = ((long)amount / 1_000_000_000) % 1_000;
            if (billions > 0)
            {
                result += ConvertThreeDigitsToTextEnglish(billions) + " Billion ";
            }

            // Process millions
            long millions = ((long)amount / 1_000_000) % 1_000;
            if (millions > 0)
            {
                result += ConvertThreeDigitsToTextEnglish(millions) + " Million ";
            }

            // Process thousands
            int thousands = (int)((amount / 1_000) % 1_000);
            if (thousands > 0)
            {
                result += ConvertThreeDigitsToTextEnglish(thousands) + " Thousand ";
            }

            // Process dollars
            int dollarPart = (int)(amount % 1_000);
            if (dollarPart > 0)
            {
                result += ConvertThreeDigitsToTextEnglish(dollarPart) + " Rupiah";
            }

            return result.Trim();
        }
        public static string ConvertThreeDigitsToTextEnglish(long num)
        {
            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
            string[] teens = { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string result = "";

            // Process hundreds
            int hundreds = (int)(num / 100);
            if (hundreds > 0)
            {
                result += units[hundreds] + " Hundred ";
            }

            // Process tens and ones
            int remainder = (int)(num % 100);
            if (remainder > 0)
            {
                if (result != "")
                {
                    result += "and ";
                }

                if (remainder <= 10)
                {
                    result += units[remainder];
                }
                else if (remainder < 20 && remainder >= 11)
                {
                    result += teens[remainder - 11];
                }
                else
                {
                    result += tens[remainder / 10];
                    if (remainder % 10 > 0)
                    {
                        result += " " + units[remainder % 10];
                    }
                }
            }

            return result;
        }
    }
}
