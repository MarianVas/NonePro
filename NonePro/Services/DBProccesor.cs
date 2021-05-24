using System;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using NonePro.Models;

namespace WPFNotepad.DataBase
{
    static class DBProccessor
    {
        private static readonly MySqlConnection connection = new MySqlConnection("server = localhost; port = 3306; user = root; database = nonepro; password = root");
        private static int userID;
        static public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch 
            {

            }
        }

        static public void CloseConnection()
        {
            connection.Close();
        }

        static public bool CheckConnection()
        {
            if (connection.State == ConnectionState.Open)
                return true;
            return false;
        }

        static public bool CheckUser()
        {
            if (userID != 0)
                return true;
            return false;
        }

        static public bool VerificateUser(string email, string password)
        {
            if (connection.State == ConnectionState.Closed)
                OpenConnection();

            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            var validateComm = new MySqlCommand("SELECT user_id FROM user_data WHERE user_mail = @Mail AND password = @Pass", connection);
            validateComm.Parameters.Add("@Mail", MySqlDbType.VarChar).Value = email;
            validateComm.Parameters.Add("@Pass", MySqlDbType.VarChar).Value = password;

            sqlAdapter.SelectCommand = validateComm;
            sqlAdapter.Fill(table);

            if (table.Rows.Count != 0)
            {
                userID = Convert.ToInt32(table.Rows[0].ItemArray[0]);
                return true;
            }
            return false;
        }

        static public BindingList<NoteModel> GetNote()
        {
            if (connection.State == ConnectionState.Closed)
                OpenConnection();

            var sqlAdapter = new MySqlDataAdapter();
            var table = new DataTable();
            string command = "SELECT * FROM note_data WHERE user_id = @I AND list_id = 1";

            var getNoteComm = new MySqlCommand(command, connection);
            getNoteComm.Parameters.Add("@I", MySqlDbType.Int32).Value = userID;

            sqlAdapter.SelectCommand = getNoteComm;
            sqlAdapter.Fill(table);
            var result = new BindingList<NoteModel>();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var temp = new NoteModel
                {
                    NoteID = Convert.ToInt32(table.Rows[i].ItemArray[0]),
                    Header = table.Rows[i].ItemArray[3].ToString(),
                    CreationTime = table.Rows[i].ItemArray[4].ToString(),
                    Text = table.Rows[i].ItemArray[5].ToString()
                };
                result.Add(temp);
            }
            return result;
        }

        static public void InsertNote()
        {
            if (connection.State == ConnectionState.Closed)
                OpenConnection();

            string command = "INSERT INTO note_data (user_id, list_id, header, creation_date) VALUES (@I, @L, @H, @D)";
            var insertComm = new MySqlCommand(command, connection);
            insertComm.Parameters.Add("@I", MySqlDbType.Int32).Value = userID;
            insertComm.Parameters.Add("@L", MySqlDbType.Int32).Value = 1;
            insertComm.Parameters.Add("@H", MySqlDbType.VarChar).Value = " ";
            insertComm.Parameters.Add("@D", MySqlDbType.VarChar).Value = DateTime.Now.ToString();
            insertComm.ExecuteNonQuery();
        }

        static public void UpdateNote(NoteModel inputModel)
        {
            if (connection.State == ConnectionState.Closed)
                OpenConnection();

            string command = "UPDATE note_data SET header = @H WHERE user_id = @I AND note_id = @N AND list_id = @L";
            var sqlAdapter = new MySqlDataAdapter();
            
            var updateComm = new MySqlCommand(command, connection);
            updateComm.Parameters.Add("@H", MySqlDbType.VarChar).Value = inputModel.Header;
            updateComm.Parameters.Add("@I", MySqlDbType.Int32).Value = userID;
            updateComm.Parameters.Add("@N", MySqlDbType.Int32).Value = inputModel.NoteID;
            updateComm.Parameters.Add("@L", MySqlDbType.Int32).Value = 1;

            sqlAdapter.SelectCommand = updateComm;
            updateComm.ExecuteNonQuery();
        }

        static public void DeleteNote(NoteModel inputModel)
        {
            if (connection.State == ConnectionState.Closed)
                OpenConnection();

            string command = "DELETE FROM note_data WHERE user_id = @I AND note_id = @N AND list_id = @L";
            var sqlAdapter = new MySqlDataAdapter();

            var deleteComm = new MySqlCommand(command, connection);
            deleteComm.Parameters.Add("@I", MySqlDbType.Int32).Value = userID;
            deleteComm.Parameters.Add("@N", MySqlDbType.Int32).Value = inputModel.NoteID;
            deleteComm.Parameters.Add("@L", MySqlDbType.Int32).Value = 1;

            sqlAdapter.SelectCommand = deleteComm;
            deleteComm.ExecuteNonQuery();
        }

        static public void UpdateText(NoteModel inputModel, string text)
        {
            if (connection.State == ConnectionState.Closed)
                OpenConnection();

            string command = "UPDATE note_data SET text = @text WHERE user_id = @I AND note_id = @N AND list_id = @L";
            var sqlAdapter = new MySqlDataAdapter();

            var upTextComm = new MySqlCommand(command, connection);
            upTextComm.Parameters.Add("@text", MySqlDbType.VarString).Value = text;
            upTextComm.Parameters.Add("@I", MySqlDbType.Int32).Value = userID;
            upTextComm.Parameters.Add("@N", MySqlDbType.Int32).Value = inputModel.NoteID;
            upTextComm.Parameters.Add("@L", MySqlDbType.Int32).Value = 1;

            sqlAdapter.SelectCommand = upTextComm;
            upTextComm.ExecuteNonQuery();
        }
    }
}
