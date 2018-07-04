using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data;

namespace Library_ABE.db
{
    public class comment
    {
        public MySqlDataAdapter CreateSQL_Commands(string URL, string _Type, string language_id, string category_id, string template_id)
        {
            try
            {
                MySqlConnection _connect = new MySqlConnection(global._cnt_str);
                _connect.Open();
                string _sql = "SELECT c.Comment_ID, c.template_ID, c.Type, c.Sentence, c.CommentNo, c.SentenceNo, c.URL FROM comments c ";
                if (URL == "")
                {
                    if(template_id.Trim()=="" || template_id == "0")
                    {
                    _sql += " INNER JOIN web_sites_templates ws ON c.template_id=ws.template_id";
                    _sql += " WHERE c.Type='" + @_Type + "'";
                    _sql += " AND ws.language_id=" + language_id;
                    if (Convert.ToInt32(category_id) < 4)
                        _sql += " AND ws.category_id=" + category_id;
                    }
                    else
                        _sql += " WHERE c.template_id=" + template_id;               
                }
                else
                {
                    _sql += " WHERE c.URL='" + @URL + "'"; 
                }


                MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(_sql, _connect);
                MySqlCommandBuilder sqlCommandBuilder = new MySqlCommandBuilder(sqlDataAdapter);

                return sqlDataAdapter;
            }
            catch
            {
                return null;
            }
        }

        public DataTable select_comment(MySqlDataAdapter sqlDataAdapter)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            DataTable t = new DataTable();
            sqlDataAdapter.Fill(t);
            _connect.Close();
            return t;
        }

        public DataTable select_comment(string language_id, string category_id)
        {
            string _sql = "SELECT Type, Sentence FROM comments c ";
            _sql += " INNER JOIN web_sites_templates ws ON c.template_id=ws.template_id";
            _sql += " WHERE ws.language_id=" + language_id;
            if(Convert.ToInt32(category_id) < 4)
                _sql += " AND ws.category_id=" + category_id;

            _sql += " AND c.Type>-2 AND c.Type <=2";
 
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(_sql, _connect);

            DataSet ds = new DataSet();
            da.Fill(ds, "d");
            DataTable dt = ds.Tables[0];

            _connect.Close();

            return dt;
        }

        public int Insert_Comment(string URL, string commentno, string sentenceno, string sentence, string _type,  string template_id)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            int _r = 0;
            string _sql = "INSERT INTO comments (URL, CommentNo, SentenceNo, Sentence, Type, template_id) ";
            _sql += " VALUES ('" + @URL + "'," + commentno + "," + sentenceno + ",'" + @sentence + "'," +_type + "," + template_id+")";
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
