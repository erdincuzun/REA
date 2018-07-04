using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Data;

namespace Library_ABE.db
{
    public class rules
    {
        public DataTable select_rules(string template_id)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            MySqlDataAdapter _da = new MySqlDataAdapter("SELECT * from rules  where template_id=" + template_id, _connect);
            DataTable a = new DataTable();
            _da.Fill(a);
            _connect.Close();
            return a;
        }

        public Dictionary<string, string> select_rules_comments(string template_id)
        {
            Dictionary<string, string> _dic = new Dictionary<string, string>();

            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            MySqlDataAdapter _da = new MySqlDataAdapter("SELECT parent_rule_html, child_rule_html from rules  where template_id=" + template_id + " AND rule_Type='Comment_Text'", _connect);
            DataTable a = new DataTable();
            _da.Fill(a);
            _connect.Close();

            foreach (DataRow dtRow in a.Rows)
            {
                string _pt = text.text_process.String_Decimal_Clear((string)dtRow.ItemArray[0]);
                string _ct = text.text_process.String_Decimal_Clear((string)dtRow.ItemArray[1]);
                if (!_dic.ContainsKey(_pt))
                    _dic.Add(_pt, _ct);
            }

            return _dic;
        }

        public int Insert_Rule(string parent_html_rule, string child_html_rule, string rule_type, string recurcing, string template_id)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            int _r = 0;
            string _sql = "INSERT INTO rules (rule_Type, parent_rule_html, child_rule_html, recurring, template_id) ";
            _sql += " VALUES ('" + rule_type + "','" + @parent_html_rule.Replace("'", "''") + "','" + @child_html_rule.Replace("'", "''") + "'," + recurcing + "," + template_id + ")";
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

        public int Update_Rule(string parent_html_rule, string child_html_rule, string recurcing, string rule_id)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            int _r = 0;
            string _sql = "UPDATE rules SET parent_rule_html='" + @parent_html_rule + "', child_rule_html='" + @child_html_rule + "', recurring=" + recurcing;
            _sql += " WHERE rule_id="+rule_id;
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

        public int Delete_aRule(string rule_id)
        {
            MySqlConnection _connect = new MySqlConnection(global._cnt_str);
            _connect.Open();
            int _r = 0;
            MySqlCommand sil;
            sil = new MySqlCommand("delete from rules where rule_id =" + rule_id + "", _connect);

            try
            {
                _r = sil.ExecuteNonQuery();
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
