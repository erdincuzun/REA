using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using net.zemberek.erisim;
using net.zemberek.yapi;
using net.zemberek.tr.yapi;

namespace Library_ABE.text
{
    public class zemberek_id
    {
        DilAyarlari tr = new TurkiyeTurkcesi();
        Zemberek zemberek;

        public zemberek_id()
        {
            zemberek = new Zemberek(tr);
        }

        public void yapim_eklerini_sozluge_ekle(string word)
        {
            Kelime[] cozumler = zemberek.kelimeCozumle(word);

            foreach (Kelime kelime_ in cozumler)
            {
                //Mevcut Kelimenin Kökü
                Kok kok = kelime_.kok();

                List<net.zemberek.yapi.ek.Ek> eklerlistesi = new List<net.zemberek.yapi.ek.Ek>();


                for (int i = 0; i < kelime_.ekSayisi(); i++)
                {
                    //Kelime kelime = cozumler[0];            
                    //Kok kok = (Kok)zemberek.dilBilgisi().kokler().kokBul("koyun")[0];            
                    //String yeni = zemberek.kelimeUret(kok, kelime.ekler()); 

                    //             
                    eklerlistesi.Add(kelime_.ekDizisi()[i]);
                    // Response.Write("<br>" + kelime_.ekDizisi()[i]);
                    if (kelime_.ekDizisi()[i].ToString().Contains("BULUNMA_LIK") || kelime_.ekDizisi()[i].ToString().Contains("BULUNMA_LI") ||
                        kelime_.ekDizisi()[i].ToString().Contains("DURUM_LIK") || kelime_.ekDizisi()[i].ToString().Contains("ILGI_CI")
                        || kelime_.ekDizisi()[i].ToString().Contains("YOKLUK_SIZ"))
                    {
                        string yeni_kelime = zemberek.kelimeUret(kok, eklerlistesi);
                        //Response.Write("<br>" + yeni_kelime);
                        //if (zemberek.dilBilgisi().kokler().kokBul(yeni_kelime, KelimeTipi.ISIM).ToString() != "")
                        Kelime[] cozumler2 = zemberek.kelimeCozumle(yeni_kelime);
                        //for döngüsü açıp çözümler arasında içerik kök ile yeni_kelimenin uyumunu kontrol etmek
                        bool varmi = false;
                        foreach (Kelime kelime2_ in cozumler2)
                        {
                            //Mevcut Kelimenin Kökü
                            Kok kok2 = kelime2_.kok();
                            if (kok2.icerik() == yeni_kelime) varmi = true;

                        }
                        //Yoksa eklemeyi yap varsa eklemeden devam et
                        if (!varmi)
                            zemberek.dilBilgisi().kokler().ekle(new Kok(yeni_kelime, KelimeTipi.ISIM));//yeni kelime sistemde
                    }

                }
            }
        }

        public string[] StemWord(string word, ref string _cat)
        {
            string[] _sonuc = new string[1];

            word = word.Trim();
            yapim_eklerini_sozluge_ekle(word);//özel durum

            Kelime[] cozumler = zemberek.kelimeCozumle(word);

            string temp = "";
            int i = 0;
            foreach (Kelime kelime_ in cozumler)
            {
                if (i == 0)
                {
                    _sonuc = new string[kelime_.ekSayisi() + 1];
                    _sonuc[0] = kelime_.kok().icerik();
                    for (int x = 0; x < kelime_.ekSayisi(); x++)
                    {
                        _sonuc[x + 1] = kelime_.ekler()[x].ad();
                    }
                    //sonuçları ekle
                    _cat = kelime_.kok().tip().ToString();
                    temp = _sonuc[0];
                }
                else
                {
                    if (kelime_.kok().icerik().Length > temp.Length)
                    {
                        _sonuc = new string[kelime_.ekSayisi() + 1];
                        _sonuc[0] = kelime_.kok().icerik();
                        for (int x = 0; x < kelime_.ekSayisi(); x++)
                        {
                            _sonuc[x + 1] = kelime_.ekler()[x].ad();
                        }

                        _cat = kelime_.kok().tip().ToString();
                        temp = _sonuc[0];
                    }
                }
                i++;
            }

            if (temp.Trim() == "")
            {
                _sonuc = new string[1];
                _sonuc[0] = word;
            }

            return _sonuc;
        }


        /*
         * 
         *         public string StemWordMax(string word)
        {
            word = word.Trim();
            Kelime[] cozumler = zemberek.kelimeCozumle(word);

            string temp = "";

            int i = 0;
            foreach (Kelime kelime_ in cozumler)
            {
                if (i == 0)
                {
                    temp = kelime_.kok().icerik();
                }
                else
                {
                    if (kelime_.kok().icerik().Length > temp.Length)
                        temp = kelime_.kok().icerik();
                }
                i++;
            }
            if (temp.Trim() == "") temp = word;

            return temp;
        }
         * 
         * public string StemWordMin(string word)
{
    word = word.Trim();
    Kelime[] cozumler = zemberek.kelimeCozumle(word);
            
    string temp = "";

    int i = 0;
    foreach (Kelime kelime_ in cozumler)
    {
        if (i == 0)
        {
            temp = kelime_.kok().icerik();
        }
        else
        {
            if (kelime_.kok().icerik().Length < temp.Length)
                temp = kelime_.kok().icerik();
        }
        i++;
    }
    if (temp.Trim() == "") temp = word;

    return temp.Trim();
}*/

    }
}
