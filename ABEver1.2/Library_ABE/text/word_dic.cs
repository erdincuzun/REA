using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Library_ABE.text
{
    public class word_dic
    {
        public Hashtable _word_dic;
        public List<Hashtable> _sentence_word;
        public List<int> _sentence_type;
        public Hashtable _words_in_sentence;
        public Hashtable _words_in_sentence_temp;
        public List<string> _sentence_data;

        db.sozluk _sozluk;

        string[] addfeat = { "_OLUMSUZLUK_", "_GELECEKZAMAN_", "_YETENEK_",
                             "FIIL_GECMISZAMAN_DI", "FIIL_GECMISZAMAN_MIS", "HIKAYE",
                             "FIIL_YETENEK_EBIL", 
                             "_FIIL_SART_SE_", "IMEK_SART_SE" };

        //"_GECMISZAMAN_", "_HIKAYE_",

        string[] stopword_turkce = {"ve", "ya", "da", "de", "ki", "ile", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz", 
                            "bu", "ben", "o", "şu", "sen", "biz", "siz", "bazı", "bana", "veya", "her", "ötürü", "şey", "diğer", 
                            "hiç",  "hem", "tek", "gibi", "çok", "en", "için", "kendi", "yani", "kadar", "en", "merhaba", "ile"};
        string[] notr_turkce = {"ama", "iken", "ancak", "fakat", "keşke", "rağmen", "yerine","bile", "göre", "san_V" };
        string[] olumsuz_turkce = { "değil", "yok" };
        string[] soru_turkce = { "mi","mu", "mı", "mü" };
        string[] ozel_turkce = { "şikayet", "avantaj", "güvence", "tavsiye", "hız", "kalite", "performans", "leke", "çizik", "problem", "sorun", "gürültü", "rezalet", "eski", "teşekkür", "garanti"};


        string[] stopword_english = {"ve", "ya", "da", "de", "ki", "ile", "bir", "iki", "üç", "dört", "beş", "altı", "yedi", "sekiz", "dokuz", 
                            "bu", "ben", "o", "şu", "sen", "biz", "siz", "bazı", "bana", "veya", "her", "ötürü", "şey", "diğer", 
                            "hiç",  "hem", "tek", "gibi", "çok", "en", "için", "kendi", "yani", "kadar", "en"};
        string[] notr_english = { "ama", "iken", "ancak", "fakat", "keşke", "rağmen", "yerine", "bile", "göre" };
        string[] olumsuz_english = { "değil", "yok" };
        string[] soru_english = { "mi", "mu", "mı", "mü" };
        string[] ozel_english = { };

        public word_dic()
        {
            _sozluk = new db.sozluk();
            _word_dic = new Hashtable();
            _sentence_word = new List<Hashtable>();
            _sentence_type = new List<int>();
            _sentence_data = new List<string>();
        }

        public void update_hashtables(string _sentence, int _type, string language, string stemming, bool addtur, int n, bool punctuation, int novelty)
        {
            _words_in_sentence = new Hashtable();
            _words_in_sentence_temp = new Hashtable();
            //bir kere hazırlansın defalarca açılmasın.
            wtn _wtn = new wtn(n);//Word Trucation için
            zemberek_id _zid = new zemberek_id();//Türkçe Zemberek için
            Stemmer_Porter _sp = new Stemmer_Porter();//ingilizce için

            string temp_sentence = _sentence.Trim();
            temp_sentence = CaseFunctions.LowerCase(temp_sentence, language);
            
            bool p_soru = false;

            if (punctuation)
            {
                if (temp_sentence != "")
                {
                    int boyut = temp_sentence.Length;
                    string noktalama = "";
                    if (boyut > 5)
                    {
                        if (temp_sentence[boyut - 1].ToString() + temp_sentence[boyut - 2].ToString() + temp_sentence[boyut - 3].ToString() == "...")
                            noktalama = "p___uc_nokta";
                        else if (temp_sentence[boyut - 1].ToString() == ".")
                            noktalama = "p___nokta";
                        else if (temp_sentence[boyut - 1].ToString() == "!")
                            noktalama = "p___unlem";
                        else if (temp_sentence[boyut - 1].ToString() == "?")
                        {
                            noktalama = "p___soru";
                            p_soru = true;
                        }
                        else
                            noktalama = "";
                    }

                    if (noktalama != "")
                    {
                        //sozluk_cumle_kontrol(noktalama, 2);
                    }
                }
            }

            if (p_soru)//soru karakteri varsa 
            {
                sozluk_cumle_kontrol("soru_eki", 1);
            }
            else
            {

                temp_sentence = character_filter(temp_sentence); //clear illegal characters
                string[] aword = temp_sentence.Split(' ');

                for (int i = 0; i < aword.Length; i++)
                {
                    string kelime = aword[i].Trim();
                    bool tumu_kelime = kelime.All(Char.IsLetter);
                    if (tumu_kelime)
                    {
                        if (kelime != "")
                        {
                            if (stemming != "No Stemming")
                            {
                                if (stemming == "Word Truncation")
                                {
                                    kelime = _wtn.StemWord(kelime);
                                }
                                else
                                {
                                    if (language == "Turkish")
                                    {
                                        if (stemming == "Zemberek")
                                        {
                                            if (addtur == false)
                                            {
                                                string _cat = "";
                                                kelime = _zid.StemWord(kelime, ref _cat)[0];

                                                if (_cat == "FIIL")
                                                    kelime += "_V";
                                            }
                                            else
                                            {
                                                string _cat = "";
                                                string[] sonuclar = _zid.StemWord(kelime, ref _cat);
                                                //kelime kökü
                                                kelime = sonuclar[0];
                                                if (_cat == "FIIL")
                                                    kelime += "_V";
                                                //bu kısım sonra yazılacak, gelişmiş kısım.
                                                //kelimeye ait bilgiler yazılıyor, extra durum, orjinallik

                                                for (int x = 1; x < sonuclar.Length; x++)
                                                    for (int y = 0; y < addfeat.Length; y++)
                                                        if (sonuclar[x].Contains(addfeat[y]))
                                                        {
                                                            sozluk_cumle_kontrol(addfeat[y], 1);
                                                            //"_OLUMSUZLUK_" durumu fiile ekli kalsın. cümlenin durumunu etkiliyor.
                                                            if (addfeat[y] == "_OLUMSUZLUK_")
                                                                kelime += "_NOT";
                                                        }
                                            }
                                        }
                                    }

                                    if (language == "English")
                                    {
                                        if (stemming == "Porter")
                                        {
                                            string __temp_kelime = _sp.stem(kelime);
                                            if (__temp_kelime.Trim() != "")
                                                kelime = __temp_kelime;
                                        }
                                    }
                                }
                            }

                            aword[i] = kelime;//kelime listesi de güncelleniyor.
                            cumle_kontrol(kelime);
                        }//kelime_boş_olmasın
                    }//tümü kelime
                }//kelimeler

                //weka için özel
                foreach (DictionaryEntry item in _words_in_sentence_temp)
                {
                    if (novelty == 0)
                        sozluk_cumle_kontrol((string)item.Key, 1);
                    else
                    {
                        bool isFound_stopWord = false;
                        if (language == "Turkish")
                            isFound_stopWord = Array.IndexOf(stopword_turkce, (string)item.Key) >= 0;
                        else
                            isFound_stopWord = Array.IndexOf(stopword_english, (string)item.Key) >= 0;

                        if (!isFound_stopWord)
                        {
                            if (novelty == 1)
                                sozluk_cumle_kontrol((string)item.Key, 1);
                            else if (novelty == 2 || novelty==3)
                            {
                                bool isFound_soru = false;
                                if (language == "Turkish")
                                    isFound_soru = Array.IndexOf(soru_turkce, (string)item.Key) >= 0;
                                else
                                    isFound_soru = Array.IndexOf(soru_english, (string)item.Key) >= 0;

                                if (isFound_soru)
                                {
                                    _words_in_sentence = new Hashtable();
                                    sozluk_cumle_kontrol("soru_eki", 1);
                                    break;
                                }
                                else
                                {
                                    bool isFound_notr = false;
                                    if (language == "Turkish")
                                        isFound_notr = Array.IndexOf(notr_turkce, (string)item.Key) >= 0;
                                    else
                                        isFound_notr = Array.IndexOf(notr_english, (string)item.Key) >= 0;

                                    if (isFound_notr)
                                    {
                                        _words_in_sentence = new Hashtable();
                                        sozluk_cumle_kontrol("onemli_baglac", 1);//sadece bağlaç yazıldı
                                        break;
                                    }
                                    else
                                    {
                                        bool isFound_olumsuz = false;
                                        if (language == "Turkish")
                                            isFound_olumsuz = Array.IndexOf(olumsuz_turkce, (string)item.Key) >= 0;
                                        else
                                            isFound_olumsuz = Array.IndexOf(olumsuz_english, (string)item.Key) >= 0;

                                        if (isFound_olumsuz)
                                        {
                                            sozluk_cumle_kontrol((string)item.Key + "_V_NOT", 1);
                                        }
                                        else
                                        {
                                            bool isFound_ozel = false;
                                            if (language == "Turkish")
                                                isFound_ozel = Array.IndexOf(ozel_turkce, (string)item.Key) >= 0;
                                            else
                                                isFound_ozel = Array.IndexOf(ozel_english, (string)item.Key) >= 0;

                                            if (isFound_ozel)
                                                sozluk_cumle_kontrol((string)item.Key + "_A", 1);
                                            else
                                            {
                                                string _cat = "";
                                                if (((string)item.Key).Contains("_V"))
                                                {
                                                    _cat = "V"; //verb oldu mu bakma!!!
                                                    sozluk_cumle_kontrol((string)item.Key, 1);
                                                }
                                                else
                                                {
                                                    _cat = _sozluk.tur_ara(CaseFunctions.UpperCase(((string)item.Key), language), language.ToLower());

                                                    if (_cat == "{A}")
                                                        sozluk_cumle_kontrol((string)item.Key + "_A", 1);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }//novelty 2
                    }
                }

                //adjective'ler not yapılıyor.
                if (novelty == 3)//novelty 2'in üzerine ek işlemler
                {
                    _words_in_sentence_temp = new Hashtable();
                    bool start_not = false;
                    for (int i = aword.Length-1; i >= 0; i--)
                    {
                        if (start_not)
                            if(_words_in_sentence.ContainsKey(aword[i] + "_A"))
                                if (!_words_in_sentence_temp.ContainsKey(aword[i] + "_A_NOT"))
                                _words_in_sentence_temp.Add(aword[i] + "_A_NOT",1);

                        if (aword[i].Contains("_V"))
                        {
                            if (_words_in_sentence.ContainsKey(aword[i]) && !aword[i].Contains("_V_NOT"))
                                start_not = false;
                            if (_words_in_sentence.ContainsKey(aword[i]) && aword[i].Contains("_V_NOT"))
                                start_not = true;
                        }
                    }

                    foreach (DictionaryEntry item in _words_in_sentence)//kalan elemanlar ekleniyor.
                    {
                        if (!_words_in_sentence_temp.ContainsKey(((string)item.Key) + "_NOT"))
                           // if (!((string)item.Key).Contains("_V"))
                                _words_in_sentence_temp.Add((string)item.Key, 1);
                    }

                    _words_in_sentence = new Hashtable();
                }
            }//soru eki yoksa

            string temp_sent="";
            if(_words_in_sentence.Count > 0)
                foreach (DictionaryEntry item in _words_in_sentence)
                    temp_sent += " " + (string)item.Key;
            else
                foreach (DictionaryEntry item in _words_in_sentence_temp)
                    temp_sent += " " + (string)item.Key;

            _sentence_word.Add(_words_in_sentence);
            _sentence_type.Add(_type);
            _sentence_data.Add("'" + temp_sent + "'," + _type.ToString());//for arff
            //_sentence_data.Add("'" + temp_sent + " - " + _sentence + "'," + _type.ToString());//for arff
        }

        string[] n_chars = new string[] {".",",","?",",","!","&","<", ">", "*", "+","-","/","(",")","[","]", "&", "%", "$", "#", "'", "\"", "{", "}", "=", "_",";",":","|" }; 
        
        private string character_filter(string _input)
        {
            foreach (string item in n_chars)
                _input = _input.Replace(item, " ");

            return _input;
        }

        private void sozluk_cumle_kontrol(string kelime, int tur)
        {
            int score = 1;
            //if (tur == 2) score = weightoffeat1;
            //if (tur == 3) score = weightoffeat2;

            if (!_words_in_sentence.ContainsKey(kelime))//cümle içindeki kelimeler
            {
                if (kelime.Trim() != "")
                    _words_in_sentence.Add(kelime, score);
            }
            else
                _words_in_sentence[kelime] = ((int)_words_in_sentence[kelime]) + 1;

            if (!_word_dic.ContainsKey(kelime))//sözlük oluştur
            {
                if (kelime.Trim() != "")
                    _word_dic.Add(kelime, score);
            }
            else
                _word_dic[kelime] = ((int)_word_dic[kelime]) + 1;
        }

        private void cumle_kontrol(string kelime)
        {
            if (!_words_in_sentence_temp.ContainsKey(kelime))//cümle içindeki kelimeler
            {
                if (kelime.Trim() != "")
                    _words_in_sentence_temp.Add(kelime, 1);
            }
            else
                _words_in_sentence_temp[kelime] = ((int)_words_in_sentence_temp[kelime]) + 1;
        }
    }
}
