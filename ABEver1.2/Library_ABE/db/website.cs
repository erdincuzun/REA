using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data.MySqlClient;

namespace Library_ABE.db
{
    public class website_n
    {
        public string template_id;
        public string web_Site_Name;
        public string its_url;
        public string example_web_page;
        public string language_id;
        public string category_id;
    }

    public class website
    {
        public MySqlDataAdapter CreateSQL_Commands()
        {
            try
            {
                 MySqlConnection _connect = new MySqlConnection(global._cnt_str);
                _connect.Open();
                string _sql = "SELECT * FROM web_sites_templates";
                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(_sql, _connect);
                MySqlCommandBuilder sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);
                
                return sqlDataAdapter;
            }
            catch
            {
                return null;
            }      
        }

        public DataTable website_datatables(MySqlDataAdapter sqlDataAdapter)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            DataTable t = new DataTable();
            sqlDataAdapter.Fill(t);
            _connect.Close();
            return t;
        }

        public website_n Get_WebSite_Info(string template_id)
        {
            website_n _ws = new website_n();

            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            string _sql = "SELECT * FROM web_sites_templates";
            _sql = _sql + " WHERE template_id=" + template_id;

            MySqlCommand _command = new MySqlCommand(_sql, _connect);
            _connect.Open();
            MySqlDataReader _reader = _command.ExecuteReader();

            if (_reader.HasRows)
            {
                _reader.Read();
                _ws.template_id = _reader.GetInt32(0).ToString();
                _ws.web_Site_Name = _reader.GetString(1);
                _ws.its_url = _reader.GetString(2);
                _ws.example_web_page = _reader.GetString(3);
            }

            _connect.Close();
            return _ws;
        }

        public DataTable website_datatables()
        {
            string _sql = "SELECT * FROM web_sites_templates";
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(_sql, _connect);
            _connect.Open();
            DataTable t = new DataTable();
            sqlDataAdapter.Fill(t);
            _connect.Close();
            return t;
        }

        public int Insert_WebSite(string websiteName)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            int _r = 0;
            string _sql = "INSERT INTO  web_sites_templates (Web_Site_Name) ";
            _sql += " VALUES ('" + websiteName + "')";
            MySqlCommand cmd = new MySqlCommand(_sql, _connect);
            try
            {
                _r = cmd.ExecuteNonQuery();
            }
            catch
            {
                _r = -1;
            }

            _connect.Close();

            return _r;
        }

        public int Insert_WebSite2(string csv)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            int _r = 0;
            string _sql = "INSERT INTO  web_sites_templates ";
            _sql += " VALUES (" + csv + ")";
            MySqlCommand cmd = new MySqlCommand(_sql, _connect);
            try
            {
                _r = cmd.ExecuteNonQuery();
            }
            catch
            {
                _r = -1;
            }

            _connect.Close();

            return _r;
        }
    }
}

