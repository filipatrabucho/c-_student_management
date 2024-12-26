using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebMySQL01
{
    public class DBconnect
    {
        private MySqlConnection connection;

        private string server;
        private string database;
        private string uid;
        private string password;
        private string port;

        public DBconnect()
        {
            Initialize();
        }


        //Initialize values 
        private void Initialize()
        {

            server = "grandeporto.ddns.net";
            //server = "127.0.0.1";
            database = "Prog22023";
            uid = "programador";
            password = "Dados@2023";
            port = "3306";

            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Port=" + port + ";";

            connection = new MySqlConnection(connectionString);

        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
            }
        }

        public bool Insert(string nome, char genero, string idade)
        {
            bool flag = true;

            try
            {
                string query = "Insert into formando (Nome, Genero, Idade) values ('" +
                    nome + "','" + genero + "'," + idade + ")";
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();                    
                }
            }   
            catch (MySqlException ex) {
                flag = false;
            }
            finally
            {
                CloseConnection();
            }
            return flag;
        }

        public void CarregarFormandos(ref DropDownList lista)
        {
            try
            {
                string query = "Select ID, Nome, Genero, Idade From formando order by ID";

                if (this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    lista.Items.Clear();
                    lista.Items.Add("");
                    while (reader.Read())
                    {
                        lista.Items.Add(reader.GetValue(0).ToString() + " - "
                            + reader.GetValue(1));
                    }
                    this.CloseConnection();
                }
            }   
            catch (MySqlException ex)
            {

            }
        }

        public bool DevolverFormando(string id, ref string nome, ref char genero, 
            ref int idade)
        {
            try
            {
                string query = "Select ID, Nome, Genero, Idade From formando where ID = " + id;

                if (this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        nome = reader.GetString(1);
                        genero = reader.GetChar(2);
                        //genero = reader.GetString(2)[0];
                        idade = reader.GetInt16(3);
                        //idade = int.Parse(reader.GetValue(3).ToString());
                    }
                    this.CloseConnection();

                }
            } 
            catch (MySqlException ex)
            {
                this.CloseConnection();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public bool Delete(string id)
        {
            try
            {
                string query = "Delete From formando where ID = " + id;
                if (this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();

                    this.CloseConnection();
                }
            }
            catch (MySqlException ex)
            {
                return false;
            }
            return true;
        }

        public int Count()
        {
            int count = -1;
            try
            {
                if (this.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand("Select Count(*) From formando", connection);
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                    this.CloseConnection();
                }

            }
            catch (MySqlException ex)
            {

            }
            return count;
        }

        //GridView
        public void Blind(ref GridView gv1)
        {
            try
            {
                string query = "SELECY * FROM formando;";

                //Open Connection
                if (this.OpenConnection() == true)
                {
                    //Create Mysql Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    da.Fill(ds, "formando");

                    //close Connection
                    this.CloseConnection();
                    gv1.DataSource = ds.Tables[0].DefaultView;
                }
            }
            catch (MySqlException ex){ }
        }



    }


}