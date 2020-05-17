using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DTBot_Template.Data._MySQL
{
    public class SQL
    {
        public static SQL pubInstance;

        #region Fields

        private readonly MySqlConnection connection;

        #endregion Fields

        #region Constructors

        public SQL(string Username, string Database, string Password, string Server = "134.122.111.200")
        {
            connection = new MySqlConnection(String.Format("SERVER={0};UID={1};DATABASE={2};port=3306;PASSWORD={3};SslMode=Preferred;", Server, Username, Database, Password));
            connection.Open();
        }

        #endregion Constructors

        #region Methods

        public void Execute(string Command, List<Tuple<string, object>> Params = null)
        {
            MySqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = Command;

            Params?.ForEach(x => sqlCommand.Parameters.Add(new MySqlParameter(x.Item1, x.Item2)));

            try { sqlCommand.ExecuteNonQuery(); } catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        public List<object[]> Read(string Command, List<Tuple<string, object>> Params = null)
        {
            MySqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = Command;

            Params?.ForEach(x => sqlCommand.Parameters.Add(new MySqlParameter(x.Item1, x.Item2)));

            MySqlDataReader dataReader = sqlCommand.ExecuteReader();

            List<object[]> Data = new List<object[]>();
            object[] tRow;
            while (dataReader.Read())
            {
                tRow = new object[dataReader.FieldCount];
                for (int i = 0; i < tRow.Length; i++) tRow[i] = dataReader[i];
                Data.Add(tRow);
            }

            dataReader.Close();

            return Data;
        }

        #endregion Methods
    }
}
