using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Library_ABE.db
{
    public class sozluk
    {
        string ConnStr = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=babylon_dicts.mdb;";
        OleDbConnection MyConn;

        public sozluk()
        {
            MyConn = new OleDbConnection(ConnStr);
            MyConn.Open();
        }

        public void close_sozluk()
        {
            MyConn.Close();
        }

        /*~sozluk()
        {
            MyConn.Close();
        }*/

        public string tur_ara(string _word, string _language)
        {
            string _sql = "select category from aykutlu_sozluk Where " + _language + "='" + _word + "'";
            OleDbCommand Cmd = new OleDbCommand(_sql, MyConn); 
            OleDbDataReader ObjReader = Cmd.ExecuteReader();
            string _cat = "";
            if (ObjReader.HasRows)
            {
                ObjReader.Read();
                _cat = ObjReader.GetString(0);
            }
            
            ObjReader.Close();

            return _cat;
        }
    }
}
