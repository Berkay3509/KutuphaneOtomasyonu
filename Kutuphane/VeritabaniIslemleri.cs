using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane
{
    internal class VeritabaniIslemleri
    {
        SqlConnection baglanti = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\KutuphaneOtomasyonu.mdf; Integrated Security=True;");
        /// <summary>
        /// Kullanıcı adı ve şifre bilgilerini kontrol ederek geriye string türunde 4 elemanlı dizi geri dondürür
        /// dizinin 0. indeksi kullanıcı adı ve şifrenin geçerli olduğu(eğer 1 ise doğru 0 ise yanlış), 1. indeksi kullanıcı adını,
        /// 2. indeksi yetkisini (yönetici için 1, kullanıcı için 0), 3. indeksi üye numarası bilgisini içerir
        /// </summary>
        /// <param name="KullaniciAdi">Kontrol edilecek kullanıcı adı</param>
        /// <param name="Sifre">Kotrol edilecek sifre</param>
        /// <returns>dizinin 0. indeksi kullanıcı adı ve şifrenin geçerli olduğu(eğer 1 ise doğru 0 ise yanlış), 1. indeksi kullanıcı adını,
        /// 2. indeksi yetkisini (yönetici için 1, kullanıcı için 0), 3. indeksi üye numarası bilgisini içerir. </returns>
        /// <returns></returns>
        /// 
        /// <summary>
        /// Üye ekleme işlemi aynı zamanda da kullanıcı ekleme işlemidir
        /// </summary>
        /// <param name="uye_adi">Üyenin adi</param>
        /// <param name="uye_soyadi">Üyenin soyadı</param>
        /// <param name="tel">Üyenin telefonu</param>
        /// <param name="adres">Üyenin adresi</param>
        /// <param name="eposta">Üyenin eposta adresi</param>
        /// <param name="uyelikTarihi">Üyenin üyelik tarihi</param>
        /// <param name="dogumTarihi">Üyenin doğum tarihi</param>
        /// <param name="kullaniciAdi">Üyenin kullanıc1 adı</param>
        /// <param name="sifre">Üyenin kullanıc1 şifresi</param>
        /// <param name="yonetici">Üyenin yönetici olup olmadığı</param>
        /// <returns>Üye ekleme işlemi başarılı olursa true, başarısız olursa false değeri geri döndürür</returns>
        public string[] kullaniciGirisKontrolu(string KullaniciAdi, string Sifre)
        {
            string[] bilgiler = new string[4];
            SqlCommand komut = new SqlCommand("SELECT KullaniciAdi, Yonetici, Uye_No FROM Kullanicilar WHERE KullaniciAdi=\'" + KullaniciAdi + "\' AND Sifre = \'" + Sifre + "\';", baglanti);
            baglanti.Open();

            SqlDataReader okuyucu = komut.ExecuteReader();

            if (okuyucu.HasRows)
            {
                okuyucu.Read();
                bilgiler[0] = "1";
                bilgiler[1] = okuyucu.GetString(0);
                bilgiler[2] = okuyucu.GetString(1);
                bilgiler[3] = okuyucu.GetInt32(2).ToString();
            }
            else
            {
                bilgiler[0] = "0";
                bilgiler[1] = "0";
                bilgiler[2] = "0";
                bilgiler[3] = "0";
            }
            baglanti.Close();
            return bilgiler;
        }
        public bool YoneticiUyeEkleme(string uye_adi, string uye_soyadi, string tel, string adres, string eposta, string uyelikTarihi, string dogumTarihi, string kullaniciAdi, string sifre, string yonetici)
        {
            try
            {
                SqlCommand uyeEklemeKomutu = new SqlCommand("INSERT INTO Uyeler(Adi,Soyadi,Telefon,Adres,Eposta,UyelikTarihi,DogumTarihi) VALUES('" + uye_adi + "','" + uye_soyadi + "','" + tel + "','" + adres + "','" + eposta + "','" + uyelikTarihi + "','" + dogumTarihi + "');", baglanti);
                SqlCommand uyeNoGetir = new SqlCommand("SELECT Uye_No FROM Uyeler WHERE Adi='" + uye_adi + "' AND Soyadi = '" + uye_soyadi + "' AND Telefon = '" + tel + "';", baglanti);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(uyeNoGetir); 
                baglanti.Open();
                uyeEklemeKomutu.ExecuteNonQuery(); 
                da.Fill(dt);
                DataRow dr = dt.Rows[0]; 
                string uye_no = dr[0].ToString(); 

                SqlCommand kullaniciEkleKomutu = new SqlCommand("INSERT INTO Kullanicilar(KullaniciAdi,Sifre,Uye_No,Yonetici) VALUES('" + kullaniciAdi + "','" + sifre + "','" + uye_no + "','" + yonetici + "');", baglanti);
                kullaniciEkleKomutu.ExecuteNonQuery();
                baglanti.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Üye güncelleme ve silme formlarındaki combobox için veritablosu oluşturur.
        /// </summary>
        /// <returns>DataTable türünde veri geri döndürür.</returns>
        public DataTable YoneticiComboboxDataTable()
        {
            SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No, Adi AS [Üye Adı],Soyadi AS [Üye Soyadı], Eposta AS [Üye E-Posta Adresi], " +
                "Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü],UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler;", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            return dt;
        }
        /// <summary>
        /// Üyeye ait bilgileri string türünde 7 elemanlı(0. indeks üye numarası,1. indeks adi, 2. indeksi soyadi, 3. indeks eposta,
        /// 4. indeks telefon, 5. indeks doğum tarihi, 6. indeks üyelik tarihi) dizi geri döndürür.
        /// </summary>
        /// <param name="uye_no">Bilgileri geri döndürülecek üyenin üye nosu</param>
        /// <returns>dizinin 0. indeksi üye numarası,1. indeksi adi, 2. indeksi soyadi, 3. indeksi eposta,
        /// 4. indeksi telefon, 5. indeksi doğum tarihi, 6. indeks üyelik tarihi bilgilerini içerir</returns>
        public string[] YoneticiUyeNoyaGoreUyeBilgisi(string uye_no)
        {
            string[] bilgiler = new string[8];
            try
            {
                SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No, Adi AS [Üye Adı],Soyadi AS [Üye Soyadı], Eposta AS [Üye E-Posta Adresi]," +
                    "Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü],UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler WHERE Uye_No='" +
                    uye_no + "';", baglanti);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
                baglanti.Open();
                da.Fill(dt);
                baglanti.Close();
                DataRow dr = dt.Rows[0];
                bilgiler[0] = dr[0].ToString();
                bilgiler[1] = dr[1].ToString();
                bilgiler[2] = dr[2].ToString();
                bilgiler[3] = dr[3].ToString();
                bilgiler[4] = dr[4].ToString();
                bilgiler[5] = dr[5].ToString();
                bilgiler[6] = dr[6].ToString();
                bilgiler[7] = dr[7].ToString();
            }
            catch (Exception)
            {
                bilgiler[0] = "-1";
                if (baglanti.State == ConnectionState.Open)
                {
                    baglanti.Close();
                }
            }
            return bilgiler;
        }
        /// <summary>
        /// Verilen uye_nosuna ait üyenin bilgilerini günceller
        /// </summary>
        /// <param name="uye_no">Üyenin nosu</param>
        /// <param name="uye_adi">Üyenin adı</param>
        /// <param name="uye_soyadi">Üyenin soyadı</param>
        /// <param name="tel">Üyenin telefonu</param>
        /// <param name="adres">Üyenin adresi</param>
        /// <param name="eposta">Üyenin eposta adresi</param>
        /// <param name="uyelikTarihi">Üyenin üyelik tarihi</param>
        /// <param name="dogumTarihi">Üyenin doğum tarihi</param>
        /// <returns>Üye güncelleme işlemi başarılı olursa true, başarısız olursa false değeri geri döndürür</returns>
        public bool YoneticiUyeGuncelleme(string uye_no, string uye_adi, string uye_soyadi, string tel, string adres, string eposta, string uyelikTarihi,
            string dogumTarihi)
        {
            try
            {
                SqlCommand uyeGuncellemeKomutu = new SqlCommand("UPDATE Uyeler SET Adi='" + uye_adi + "',Soyadi='" + uye_soyadi + "',Telefon='" +
                    tel + "',Adres='" + adres + "',Eposta='" + eposta + "',UyelikTarihi='" + uyelikTarihi + "',DogumTarihi='" + dogumTarihi +
                    "' WHERE Uye_No ='" + uye_no + "';", baglanti);
                baglanti.Open();
                uyeGuncellemeKomutu.ExecuteNonQuery();
                baglanti.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Verilen uye_nosuna ait üyeyi siler. Üyenin daha önceden aldığı kitap kayıtları ve kullanıcı bilgileri de silinir
        /// </summary>
        /// <param name="uye_no">Üye no</param>
        /// <returns>Üye silme işlemi başarılı olursa true, başarısız olursa false değeri geri döndürür</returns>
        public bool YoneticiUyeSilme(string uye_no)
        {
            try
            {
                SqlCommand AlinanKitaplardanSil = new SqlCommand("DELETE FROM AlinanKitaplar WHERE Uye_No='" + uye_no + "';", baglanti);
                SqlCommand kullaniciyiSil = new SqlCommand("DELETE FROM Kullanicilar WHERE Uye_No='" + uye_no + "';", baglanti);
                SqlCommand uyeyiSil = new SqlCommand("DELETE FROM Uyeler WHERE Uye_No='" + uye_no + "';", baglanti);
                baglanti.Open();
                kullaniciyiSil.ExecuteNonQuery();
                AlinanKitaplardanSil.ExecuteNonQuery();
                uyeyiSil.ExecuteNonQuery();
                baglanti.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Üye Listesini Datatable türünde geri gönderir
        /// </summary>
        /// <returns>Veritablosunun içerisinde sırasıyla üyenin numarası, adı, soyadı, eposta adresi, telefonu, doğum tarihi, üyelik tarihi ve adres bilgileri
        /// vardır</returns>
        public DataTable YoneticiUyeListesi()
        {
            SqlCommand uyeleriGetir = new SqlCommand("SELECT Uye_No, Adi AS [Üye Adı],Soyadi AS [Üye Soyadı], Eposta AS [Üye E-Posta Adresi], Telefon AS [Üye Telefonu], DogumTarihi AS [Üye Doğum Günü],UyelikTarihi AS [Üyelik Tarihi],Adres AS [Üye Adresi] FROM Uyeler;", baglanti);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(uyeleriGetir);
            baglanti.Open();
            da.Fill(dt);
            baglanti.Close();
            return dt;
        }
        public DataTable KullanicininVerilenKitaplariListesi(int uyeNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                U.Adi AS [Üye Adı],
                U.Soyadi AS [Üye Soyadı],
                U.Eposta AS [Üye E-Posta Adresi],
                K.Adi AS [Kitap Adı],
                K.Yazari AS [Kitabın Yazarı],
                K.BasimEvi AS [Kitabın Basım Evi],
                K.BasimYili AS [Kitabın Basım Yılı],
                AK.AlisTarihi AS [Ödünç Alma Tarihi]
            FROM
                dbo.AlinanKitaplar AK
            INNER JOIN
                dbo.Uyeler U ON AK.Uye_No = U.Uye_No
            INNER JOIN
                dbo.Kitaplar K ON AK.k_id = K.k_id
            WHERE
                AK.TeslimTarihi IS NULL
                AND AK.Uye_No = @UyeNoParam
            ORDER BY
                AK.AlisTarihi DESC;"; 

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    komut.Parameters.AddWithValue("@UyeNoParam", uyeNo);
                    SqlDataAdapter da = new SqlDataAdapter(komut);

                    
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
               
                Console.WriteLine("Veritabanı Hatası (KullanicininVerilenKitaplariListesi): " + sqlEx.Message);
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KullanicininVerilenKitaplariListesi): " + ex.Message);
            }
            finally
            {
               
                
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
                
            }
            return dt;
        }
        public DataTable SistemKullanicilariListesi()
        {
            DataTable dt = new DataTable();
            try
            {
               
                string sorgu = @"
            SELECT
                KullaniciAdi AS [Kullanıcı Adı],
                Sifre,
                CASE Yonetici
                    WHEN '1' THEN 'Evet'
                    ELSE 'Hayır'
                END AS [Yönetici Mi],
                Uye_No AS [İlişkili Üye No] 
            FROM
                dbo.Kullanicilar
            ORDER BY
                KullaniciAdi;";

                
                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (SistemKullanicilariListesi): " + sqlEx.Message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (SistemKullanicilariListesi): " + ex.Message);
              
            }
     
            return dt;
        }
        public DataTable SistemKullanicilariComboBoxListesi()
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                KullaniciAdi,
                Sifre,
                Yonetici 
            FROM
                dbo.Kullanicilar
            ORDER BY
                KullaniciAdi;";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (SistemKullanicilariComboBoxListesi): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (SistemKullanicilariComboBoxListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                     this.baglanti.Close();
                }
            }
            return dt;
        }
        public bool SistemKullanicisiGuncelle(string eskiKullaniciAdi, string yeniKullaniciAdi, string sifre, string yonetici)
        {
            try
            {
                
                string sorgu = @"
            UPDATE Kullanicilar SET
                KullaniciAdi = @YeniKullaniciAdi,
                Sifre = @Sifre,
                Yonetici = @Yonetici
            WHERE
                KullaniciAdi = @EskiKullaniciAdi";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    komut.Parameters.AddWithValue("@YeniKullaniciAdi", yeniKullaniciAdi.Trim());
                    komut.Parameters.AddWithValue("@Sifre", sifre); 
                    komut.Parameters.AddWithValue("@Yonetici", yonetici); 
                    komut.Parameters.AddWithValue("@EskiKullaniciAdi", eskiKullaniciAdi);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    int etkilenenSatir = komut.ExecuteNonQuery();
                    return etkilenenSatir > 0; 
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (SistemKullanicisiGuncelle): " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (SistemKullanicisiGuncelle): " + ex.Message);
                return false;
            }
            finally
            {
                 if (this.baglanti.State == ConnectionState.Open)
                 {
                 this.baglanti.Close();
                 }
            }
        }
        public DataTable KullanicininTeslimEttigiKitaplarListesi(int uyeNo)
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                U.Adi AS [Üye Adı],
                U.Soyadi AS [Üye Soyadı],
                K.Adi AS [Kitap Adı],
                K.Yazari AS [Yazarı],
                AK.AlisTarihi AS [Alındığı Tarih],
                AK.TeslimTarihi AS [İade Tarihi]
            FROM
                dbo.AlinanKitaplar AK
            INNER JOIN
                dbo.Uyeler U ON AK.Uye_No = U.Uye_No
            INNER JOIN
                dbo.Kitaplar K ON AK.k_id = K.k_id
            WHERE
                AK.TeslimTarihi IS NOT NULL 
                AND AK.Uye_No = @UyeNoParam
            ORDER BY
                AK.TeslimTarihi DESC, AK.AlisTarihi DESC;";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    komut.Parameters.AddWithValue("@UyeNoParam", uyeNo);
                    SqlDataAdapter da = new SqlDataAdapter(komut);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (KullanicininTeslimEttigiKitaplarListesi): " + sqlEx.Message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KullanicininTeslimEttigiKitaplarListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close(); 
                }
            }
            return dt;
        }
        public DataTable UyelerComboBoxListesi()
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                Uye_No,
                Adi AS UyeAdi,
                Soyadi AS UyeSoyadi,
                Eposta
            FROM
                dbo.Uyeler
            ORDER BY
                Eposta;"; 
                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (UyelerComboBoxListesi): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (UyelerComboBoxListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return dt;
        }
        public int KitapIdGetir(string adi, string yazari, string basimEvi, string basimYili)
        {
            int kitapId = 0;
            string sorgu = "SELECT k_id FROM Kitaplar WHERE Adi = @Adi AND Yazari = @Yazari AND BasimEvi = @BasimEvi AND BasimYili = @BasimYili";
            try
            {
                using (SqlCommand cmd = new SqlCommand(sorgu, this.baglanti))
                {
                    cmd.Parameters.AddWithValue("@Adi", adi);
                    cmd.Parameters.AddWithValue("@Yazari", yazari);
                    cmd.Parameters.AddWithValue("@BasimEvi", basimEvi);
                    cmd.Parameters.AddWithValue("@BasimYili", basimYili);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        kitapId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata (KitapIdGetir): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return kitapId;
        }
        public int KitapOduncVerTransactionYok(int kitapId, int uyeNo, string alisTarihi, string kitapAdi, string kitapYazari, string kitapBasimEvi, string kitapBasimYili)
        {
            try
            {
                string sqlInsert = "INSERT INTO AlinanKitaplar (k_id, Uye_No, AlisTarihi) VALUES (@k_id, @Uye_No, @AlisTarihi)";
                using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, this.baglanti))
                {
                    cmdInsert.Parameters.AddWithValue("@k_id", kitapId);
                    cmdInsert.Parameters.AddWithValue("@Uye_No", uyeNo);
                    cmdInsert.Parameters.AddWithValue("@AlisTarihi", alisTarihi);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    cmdInsert.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Hata (KitapOduncVer - Insert AlinanKitaplar): " + sqlEx.Message);
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601) 
                {
                    return 1; 
                }
                return 3; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KitapOduncVer - Insert AlinanKitaplar): " + ex.Message);
                return 3; 
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            try
            {
                string sqlUpdate = @"UPDATE Kitaplar SET KacTane = KacTane - 1
                             WHERE Adi = @Adi AND Yazari = @Yazari AND BasimEvi = @BasimEvi AND BasimYili = @BasimYili AND KacTane > 0";

                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, this.baglanti))
                {
                    cmdUpdate.Parameters.AddWithValue("@Adi", kitapAdi);
                    cmdUpdate.Parameters.AddWithValue("@Yazari", kitapYazari);
                    cmdUpdate.Parameters.AddWithValue("@BasimEvi", kitapBasimEvi);
                    cmdUpdate.Parameters.AddWithValue("@BasimYili", kitapBasimYili);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    int affectedRows = cmdUpdate.ExecuteNonQuery();

                    if (affectedRows == 0)
                    {
                        Console.WriteLine("Stok azaltma hatası: Kitap stoku işlem sırasında tükendi, zaten sıfırdı veya belirtilen kriterlere uyan kitap bulunamadı.");
                        return 2; 
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Hata (KitapOduncVer - Update Kitaplar): " + sqlEx.Message);
                return 3; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KitapOduncVer - Update Kitaplar): " + ex.Message);
                return 3; 
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return 0; 
        }
        public DataTable StoktakiKitaplarListesi()
        {
            DataTable dt = new DataTable();
            try
            {
                
                string sorgu = @"
            SELECT
                Adi,
                Yazari,
                BasimEvi,
                BasimYili,
                KacTane
            FROM
                dbo.Kitaplar
            WHERE
                KacTane > 0
            ORDER BY
                Adi;"; 

                
                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (StoktakiKitaplarListesi): " + sqlEx.Message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (StoktakiKitaplarListesi): " + ex.Message);
            }
            finally
            {
                
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return dt;
        }
        public DataTable KitaplarComboBoxListesi()
        {
            DataTable dt = new DataTable();
            try
            {
                
                string sorgu = @"
            SELECT
                k_id,
                Adi,
                Yazari,      
                BasimEvi,    
                BasimYili,
                KacTane
            FROM
                dbo.Kitaplar
            ORDER BY
                Adi;";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (KitaplarComboBoxListesi): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KitaplarComboBoxListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return dt;
        }
        public DataRow KitapBilgileriniGetir(int kitapId)
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                Adi,
                Yazari,
                BasimEvi,
                BasimYili,
                KacTane
            FROM
                dbo.Kitaplar
            WHERE
                k_id = @KitapId;";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    komut.Parameters.AddWithValue("@KitapId", kitapId);
                    SqlDataAdapter da = new SqlDataAdapter(komut);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0];
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (KitapBilgileriniGetir): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KitapBilgileriniGetir): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return null; 
        }
        public bool KitapSil(int kitapId)
        {
            try
            {
               

                string sorguKitap = "DELETE FROM Kitaplar WHERE k_id = @kitapId_param"; 
                using (SqlCommand komut = new SqlCommand(sorguKitap, this.baglanti))
                {
                    komut.Parameters.AddWithValue("@kitapId_param", kitapId); 
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    int etkilenenSatir = komut.ExecuteNonQuery();
                    return etkilenenSatir > 0; 
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (KitapSil): " + sqlEx.Message);
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KitapSil): " + ex.Message);
                return false;
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
        }
        public DataTable TumKitaplarListesi()
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                k_id AS [İD],
                Adi AS [Kitabın Adı],
                Yazari AS [Yazarı],
                BasimEvi AS [Basım Evi],
                BasimYili AS [Basım Yılı],
                KacTane AS [Adet]
            FROM
                dbo.Kitaplar
            WHERE
                KacTane > 0 
            ORDER BY
                [Kitabın Adı];";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    this.baglanti.Open(); 
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (TumKitaplarListesi): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (TumKitaplarListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return dt;
        }
        public int KitapStokAzalt(int kitapId, int azaltilacakAdet, bool stokSifirsaSil)
        {
            int mevcutStok = -1;
            try
            {
                string sorguStokGetir = "SELECT KacTane FROM Kitaplar WHERE k_id = @k_id_stok";
                using (SqlCommand cmdStokGetir = new SqlCommand(sorguStokGetir, this.baglanti))
                {
                    cmdStokGetir.Parameters.AddWithValue("@k_id_stok", kitapId);
                    this.baglanti.Open();
                    object result = cmdStokGetir.ExecuteScalar();
                    this.baglanti.Close();

                    if (result != null && result != DBNull.Value)
                    {
                        mevcutStok = Convert.ToInt32(result);
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata (KitapStokAzalt - Stok Getir): " + ex.Message);
                this.baglanti.Close();
                return 2;
            }
            if (mevcutStok < azaltilacakAdet)
            {
                return 1;
            }
            int yeniStok = mevcutStok - azaltilacakAdet;
            try
            {
                string sorguUpdate = "UPDATE Kitaplar SET KacTane = @YeniKacTane WHERE k_id = @k_id_update";
                using (SqlCommand cmdUpdate = new SqlCommand(sorguUpdate, this.baglanti))
                {
                    cmdUpdate.Parameters.AddWithValue("@YeniKacTane", yeniStok);
                    cmdUpdate.Parameters.AddWithValue("@k_id_update", kitapId);
                    this.baglanti.Open();
                    int etkilenen = cmdUpdate.ExecuteNonQuery();
                    this.baglanti.Close();

                    if (etkilenen == 0)
                    {
                        return 2;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata (KitapStokAzalt - Stok Güncelle): " + ex.Message);
                this.baglanti.Close(); 
                return 2; 
            }
            if (yeniStok <= 0 && stokSifirsaSil)
            {
                if (!KitapSil(kitapId)) 
                {
                    Console.WriteLine("Uyarı (KitapStokAzalt): Stok sıfırlandı ama kitap silinemedi. Kitap ID: " + kitapId);
                    return 3;
                }
            }

            return 0; 
        }
        public bool KitapGuncelle(int kitapId, string adi, string yazari, string basimEvi, string basimYili, int kacTane) 
        {
            try
            {
                string sorgu = @"
            UPDATE Kitaplar SET
                Adi = @Adi,
                Yazari = @Yazari,
                BasimEvi = @BasimEvi,
                BasimYili = @BasimYili,
                KacTane = @KacTane
            WHERE
                k_id = @k_id_param;";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    komut.Parameters.AddWithValue("@Adi", adi.Trim());
                    komut.Parameters.AddWithValue("@Yazari", yazari.Trim());
                    komut.Parameters.AddWithValue("@BasimEvi", basimEvi.Trim());
                    komut.Parameters.AddWithValue("@BasimYili", basimYili.Trim());
                    komut.Parameters.AddWithValue("@KacTane", kacTane);
                    komut.Parameters.AddWithValue("@k_id_param", kitapId);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    int etkilenenSatir = komut.ExecuteNonQuery();
                    return etkilenenSatir > 0;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (KitapGuncelle): " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KitapGuncelle): " + ex.Message);
                return false;
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
        }
        public int KitapEkle(string adi, string yazari, string basimEvi, string basimYili, int kacTane)
        {
            try
            {
                string sorgu = @"
            INSERT INTO dbo.Kitaplar
                (Adi, Yazari, BasimEvi, BasimYili, KacTane)
            VALUES
                (@Adi, @Yazari, @BasimEvi, @BasimYili, @KacTane);";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    komut.Parameters.AddWithValue("@Adi", adi.Trim());
                    komut.Parameters.AddWithValue("@Yazari", yazari.Trim());
                    komut.Parameters.AddWithValue("@BasimEvi", basimEvi.Trim());
                    komut.Parameters.AddWithValue("@BasimYili", basimYili.Trim()); 
                    komut.Parameters.AddWithValue("@KacTane", kacTane);

                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    int etkilenenSatir = komut.ExecuteNonQuery();

                    if (etkilenenSatir > 0)
                    {
                        return 0; 
                    }
                    else
                    {
                        return 2; 
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (KitapEkle): " + sqlEx.Message);
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    return 1; 
                }
                return 1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (KitapEkle): " + ex.Message);
                return 2; 
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
        }
        public DataTable OduncAlinmisKitaplarListesi()
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                AK.k_id,        
                AK.Uye_No,      
                U.Adi AS [Üye Adı],
                U.Soyadi AS [Üye Soyadı],
                U.Eposta AS [Üye E-Posta Adresi],
                K.Adi AS [Kitap Adı],
                K.Yazari AS [Kitabın Yazarı],
                K.BasimEvi AS [Kitabın Basım Evi],
                K.BasimYili AS [Kitabın Basım Yılı],
                AK.AlisTarihi AS [Ödünç Alma Tarihi]
            FROM
                dbo.AlinanKitaplar AK
            INNER JOIN
                dbo.Uyeler U ON AK.Uye_No = U.Uye_No
            INNER JOIN
                dbo.Kitaplar K ON AK.k_id = K.k_id
            WHERE
                AK.TeslimTarihi IS NULL
            ORDER BY
                U.Adi, U.Soyadi, AK.AlisTarihi;";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (OduncAlinmisKitaplarListesi): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (OduncAlinmisKitaplarListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return dt;
        }
        public int KitapIadeEt(int kitapId, int uyeNo, string teslimTarihi)
        {
            SqlTransaction transaction = null;
            try
            {
                if (this.baglanti.State == ConnectionState.Closed)
                {
                    this.baglanti.Open();
                }
                transaction = this.baglanti.BeginTransaction();

                
                string sqlUpdateAlinan = @"
            UPDATE AlinanKitaplar SET
                TeslimTarihi = @TeslimTarihi
            WHERE
                k_id = @k_id
                AND Uye_No = @Uye_No
                AND TeslimTarihi IS NULL;"; 

                int alinanEtkilenen;
                using (SqlCommand cmdUpdateAlinan = new SqlCommand(sqlUpdateAlinan, this.baglanti, transaction))
                {
                    cmdUpdateAlinan.Parameters.AddWithValue("@TeslimTarihi", teslimTarihi);
                    cmdUpdateAlinan.Parameters.AddWithValue("@k_id", kitapId);
                    cmdUpdateAlinan.Parameters.AddWithValue("@Uye_No", uyeNo);
                    alinanEtkilenen = cmdUpdateAlinan.ExecuteNonQuery();
                }

                if (alinanEtkilenen == 0)
                {
                    transaction.Rollback();
                    Console.WriteLine("İade Hatası: AlinanKitaplar tablosunda eşleşen ödünç kaydı bulunamadı veya zaten iade edilmiş.");
                    return 1;
                }

               
                string sqlUpdateKitapStok = @"
            UPDATE Kitaplar SET
                KacTane = KacTane + 1
            WHERE
                k_id = @k_id_stok;";

                int kitapStokEtkilenen;
                using (SqlCommand cmdUpdateKitapStok = new SqlCommand(sqlUpdateKitapStok, this.baglanti, transaction))
                {
                    cmdUpdateKitapStok.Parameters.AddWithValue("@k_id_stok", kitapId);
                    kitapStokEtkilenen = cmdUpdateKitapStok.ExecuteNonQuery();
                }

                if (kitapStokEtkilenen == 0)
                {
                    transaction.Rollback();
                    Console.WriteLine("İade Hatası: Kitaplar tablosunda stok artırılamadı (kitap bulunamadı).");
                   
                    return 2; 
                }

                transaction.Commit();
                return 0;
            }
            catch (SqlException sqlEx)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Veritabanı Hatası (KitapIadeEt): " + sqlEx.Message);
                return 3; 
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                Console.WriteLine("Genel Hata (KitapIadeEt): " + ex.Message);
                return 3;
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
        }
        public DataTable TumTeslimEdilmisKitaplarListesi()
        {
            DataTable dt = new DataTable();
            try
            {
                string sorgu = @"
            SELECT
                U.Uye_No,      
                K.k_id AS KitapID, 
                K.KacTane,     
                U.Adi AS [Üye Adı],
                U.Soyadi AS [Üye Soyadı],
                U.Eposta AS [Üye E-Posta Adresi],
                K.Adi AS [Kitap Adı],
                K.Yazari AS [Kitabın Yazarı],
                K.BasimEvi AS [Kitabın Basım Evi],
                K.BasimYili AS [Kitabın Basım Yılı],
                AK.AlisTarihi AS [Ödünç Alma Tarihi],
                AK.TeslimTarihi AS [Teslim Tarihi]
            FROM
                dbo.AlinanKitaplar AK
            INNER JOIN
                dbo.Uyeler U ON AK.Uye_No = U.Uye_No
            INNER JOIN
                dbo.Kitaplar K ON AK.k_id = K.k_id
            WHERE
                AK.TeslimTarihi IS NOT NULL  
            ORDER BY
                AK.TeslimTarihi DESC, U.Adi, K.Adi;";

                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti))
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (TumTeslimEdilmisKitaplarListesi): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (TumTeslimEdilmisKitaplarListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return dt;
        }
        public DataTable OduncAlinmisKitaplarListesii()
        {
            DataTable dt = new DataTable();
            try
            {
                // Formda k_id veya Uye_No doğrudan gösterilmese de,
                // gelecekte bir işlev için (örn: çift tıklama ile detay açma)
                // bu ID'leri sorguya dahil etmek isteyebilirsiniz. Şimdilik formdaki sorguya sadık kalalım.
                string sorgu = @"
            SELECT
                U.Adi AS [Üye Adı],
                U.Soyadi AS [Üye Soyadı],
                U.Eposta AS [Üye E-Posta Adresi],
                K.Adi AS [Kitap Adı],
                K.Yazari AS [Kitabın Yazarı],
                K.BasimEvi AS [Kitabın Basım Evi],
                K.BasimYili AS [Kitabın Basım Yılı],
                AK.AlisTarihi AS [Ödünç Alma Tarihi]
            FROM
                dbo.AlinanKitaplar AK
            INNER JOIN
                dbo.Uyeler U ON AK.Uye_No = U.Uye_No
            INNER JOIN
                dbo.Kitaplar K ON AK.k_id = K.k_id
            WHERE
                AK.TeslimTarihi IS NULL
            ORDER BY
                AK.AlisTarihi DESC, U.Adi;";
                using (SqlCommand komut = new SqlCommand(sorgu, this.baglanti)) 
                {
                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    if (this.baglanti.State == ConnectionState.Closed)
                    {
                        this.baglanti.Open();
                    }
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Veritabanı Hatası (OduncAlinmisKitaplarListesi): " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Genel Hata (OduncAlinmisKitaplarListesi): " + ex.Message);
            }
            finally
            {
                if (this.baglanti.State == ConnectionState.Open)
                {
                    this.baglanti.Close();
                }
            }
            return dt;
        }
    }
}
