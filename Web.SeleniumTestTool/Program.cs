using System;
using System.Threading;
using Entities.Concrete;
using NUnit;
using Entities.Dtos;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using Web.SeleniumTestTool.Models;

namespace Web.SeleniumTestTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string passwordError = "Şifre Hatalı";
            string jwtTitle = ".";


            UserForLoginDto loginDto = new UserForLoginDto();

            loginDto.Email = "alisari41@outlook.com";
            loginDto.Password = "123456"; //*


            Address address = new Address();
            address.Mahalle = "";//Hatalı giriyorum önce sonra değişcem
            address.Cadde = "";
            address.Sokak = "Acar";
            address.BinaNo = "23";
            address.Kat = 2;
            address.İlce = "Körfez";
            address.İl = "KOCAELİ";
            address.PostaKodu = 41900;

            TelephoneDirectories telephoneDirectories = new TelephoneDirectories();
            telephoneDirectories.AddressId = 1;


            Elements elements = new Elements();

            elements.Link = @"https://localhost:44375/";
            elements.BtnGuvenlikDegil = "details-button";
            elements.BtnGuvenlikDegilKabulEt = "proceed-link";
            elements.BtnAdmin = "btnAdmin";


            elements.TxtGirisEmail = "txtGirisEmail";
            elements.TxtGirisPassword = "txtGirisPassword";
            elements.TxtMahalle = "txtMahalle";
            elements.TxtCadde = "txtCadde";
            elements.TxtSokak = "txtSokak";
            elements.TxtBinaNo = "txtBinaNo";
            elements.TxtKat = "txtKat";
            elements.TxtIlce = "txtIlce";
            elements.TxtIl = "txtIl";
            elements.TxtPostaKodu = "txtPostakodu";


            elements.TxtKontrol = "txtKotrol";// Validation kotrol





            elements.BtnSingIn = "btnSingIn";
            elements.BtnTables = "btnTables";
            elements.BtnKullancilar = "btnKullancilar";
            elements.BtnOperationClaims = "btnOperationClaims";
            elements.BtnUserOperationClaims = "btnUserOperationClaims";
            elements.BtnAdres = "btnAdres";
            elements.BtnTelefonRehberi = "btnTelefonRehberi";
            elements.BtnAdd = "btnAdd";
            elements.BtnEdit = "btnEdit";
            elements.BtnDelete = "btnDelete";




            elements.XPathTableKolon = "/html/body/div/main/section/div/div[2]/div/div/table/tbody/tr[3]";
            elements.XPathAddressEdit = "/html/body/div[1]/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[7]/td[10]/a";
            elements.XPathAddressDelete = "/html/body/div[1]/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[13]/td[11]/a";




            elements.IdJwt = "jwt";
            elements.IdPasswordError = "passwordError";



            IWebDriver driver = new ChromeDriver(); //Hangi tarayıcı üzerinden işlem yapacağımı belirtiyorum.
            driver.Navigate().GoToUrl(elements.Link); //Url'e gitme işlemi

            Console.WriteLine("Güvenli değil! Uyarsını zorla geçiliyor...");

            //Güvenli Değil
            driver.FindElement(By.Id(elements.BtnGuvenlikDegil)).Click();

            // Güvenli Değil fakat Yinede kabul et butonu
            driver.FindElement(By.Id(elements.BtnGuvenlikDegilKabulEt)).Click();
            Thread.Sleep(3000);



            Console.WriteLine("Tabloda diğer bir öğeyi seçme işlemi yapılıyor.");
            // Tabloda diğer bir öğeyi seçme işlemi
            driver.FindElement(By.XPath(elements.XPathTableKolon)).Click();
            Thread.Sleep(2000);



            // Sayfa Aşşağı kaydırma kullan

            Console.WriteLine("Admin Sayfasına Gidiliyor");
            // Admin Sayfasına Git
            driver.FindElement(By.Id(elements.BtnAdmin)).Click();

            Console.WriteLine("Email ve şifre bilgileri giriliyor");
            // Email id..
            driver.FindElement(By.Id(elements.TxtGirisEmail)).SendKeys(loginDto.Email);

            // Password Id..
            driver.FindElement(By.Id(elements.TxtGirisPassword)).SendKeys(loginDto.Password);

            Console.WriteLine("Giriş yap butonuna basılıyor.");
            // Giriş yap
            driver.FindElement(By.Id(elements.BtnSingIn)).Click();



            // Şifre hatalı
            try
            {
                Assert.IsTrue(driver.FindElement(By.Id(elements.IdPasswordError)).Text.Contains(passwordError));
                Console.WriteLine("\n\nŞifre Hatalı Bilgisi alınmıştır\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\nYanlış Girilen Şifre Hatalı Bilgisi ALINAMAMIŞTIR.\n\n");
            }

            loginDto.Password = "123456*";

            Thread.Sleep(3000);

            Console.WriteLine("Şifre Yanlış olduğu için tekrar işlemler yapılıyor.");
            // Email id..
            driver.FindElement(By.Id(elements.TxtGirisEmail)).SendKeys(loginDto.Email);

            // Password Id..
            driver.FindElement(By.Id(elements.TxtGirisPassword)).SendKeys(loginDto.Password);

            // Giriş yap
            driver.FindElement(By.Id(elements.BtnSingIn)).Click();

            Thread.Sleep(2000);


            // Giriş Başarılımı test ediliyor
            try
            {

                Assert.IsTrue(driver.FindElement(By.Id(elements.IdJwt)).Text.Contains(jwtTitle));
                Console.WriteLine("\n\nGiriş Başarılı\n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Adres tablosuna gitmeye çalışılıyor");
            // Tables dropdown aç
            driver.FindElement(By.Id(elements.BtnTables)).Click();
            Thread.Sleep(1000);

            // Adres tablosuna git id..
            driver.FindElement(By.Id(elements.BtnAdres)).Click();

            Thread.Sleep(2000);

            Console.WriteLine("Adres ekleme sayfasına gidiliyor.");
            // Adres Ekleme sayfasına git
            driver.FindElement(By.Id(elements.BtnAdd)).Click();

            Console.WriteLine("Adres değerleri giriliyor..");
            //Adress Tablosunun Bilgilerini doldour
            driver.FindElement(By.Id(elements.TxtMahalle)).SendKeys(address.Mahalle);
            driver.FindElement(By.Id(elements.TxtCadde)).SendKeys(address.Cadde);
            driver.FindElement(By.Id(elements.TxtSokak)).SendKeys(address.Sokak);
            driver.FindElement(By.Id(elements.TxtBinaNo)).SendKeys(address.BinaNo);
            driver.FindElement(By.Id(elements.TxtKat)).SendKeys(address.Kat.ToString());
            driver.FindElement(By.Id(elements.TxtIlce)).SendKeys(address.İlce);
            driver.FindElement(By.Id(elements.TxtIl)).SendKeys(address.İl);
            driver.FindElement(By.Id(elements.TxtPostaKodu)).SendKeys(address.PostaKodu.ToString());

            Console.WriteLine("Kayıt ediliyor");
            //Bilgiler Girildikten sonra ekle butonuna bas
            driver.FindElement(By.Id(elements.BtnAdd)).Click();


            Thread.Sleep(2000);

            Console.WriteLine("Sayfada aşşağı yukarı scrol gidiliyor..");
            // sayfayı aşşağı doğru kaydır
            driver.ExecuteJavaScript("window.scrollTo (0, document.body.scrollHeight)");
            Thread.Sleep(3000);

            //Syafayı yukarı doğru kaydırma
            driver.ExecuteJavaScript("window.scrollTo (0, -document.body.scrollHeight)");
            Thread.Sleep(2000);



            // Validation Kontrol İşlemleri
            try
            {
                Assert.IsNotNull(elements.TxtKontrol);
                Console.WriteLine("\n\n Girilen değerler validation kurallarına uymamaktadır. \n\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("\n\n Doğrulamalar gözükmüyor. \n\n");
            }


            // Adres Bilgisini tekrar girip Tekrar Kaydetmeye çalışılıyor
            address.Mahalle = "Denizciler";
            driver.FindElement(By.Id(elements.TxtMahalle)).SendKeys(address.Mahalle);
            Thread.Sleep(1000);

            Console.WriteLine("Eksik girilen adres bilgileri girilerek tekrar kayıt ediliyor.");
            //Bilgiler Girildikten sonra ekle butonuna bas
            driver.FindElement(By.Id(elements.BtnAdd)).Click();


            Thread.Sleep(2000);

            Console.WriteLine("Bir ürün seçerek o üründe güncelleme işlemi yapılıyor..");
            // 9 Numaralı Adresin Bilgilerini güncellemek için sayfasına yönlendiriliyor.full xpath
            driver.FindElement(By.XPath(elements.XPathAddressEdit)).Click();

            // Mahalle, sokak, ilçe ve il alanlarını güncelleme testi yapılıyor
            address.Mahalle = "Yörükler";
            address.Sokak = "Gerikler";
            address.İlce = "Aşağı Yörükler";
            address.İl = "Tam Yörükler";
            
            // sayfayı aşşağı doğru kaydır
            driver.ExecuteJavaScript("window.scrollTo (0, document.body.scrollHeight)");
            Thread.Sleep(3000);

            //Syafayı yukarı doğru kaydırma
            driver.ExecuteJavaScript("window.scrollTo (0, -document.body.scrollHeight)");
            Thread.Sleep(2000);

            Console.WriteLine("Güncellenecek alanların textbox içerisi temizleniyor.");
            // Kolonları temizleyelim
            driver.FindElement(By.Id(elements.TxtMahalle)).Clear();
            driver.FindElement(By.Id(elements.TxtSokak)).Clear();
            driver.FindElement(By.Id(elements.TxtIlce)).Clear();
            driver.FindElement(By.Id(elements.TxtIl)).Clear();
            
            Thread.Sleep(2000);

            Console.WriteLine("Veriler girildikten sonra güncelleme işlemi yapılıyor..");
            driver.FindElement(By.Id(elements.TxtMahalle)).SendKeys(address.Mahalle);
            driver.FindElement(By.Id(elements.TxtSokak)).SendKeys(address.Sokak);
            driver.FindElement(By.Id(elements.TxtIlce)).SendKeys(address.İlce);
            driver.FindElement(By.Id(elements.TxtIl)).SendKeys(address.İl);

            Thread.Sleep(2000);


            //Bilgiler Girildikten sonra güncelle butonuna bas
            driver.FindElement(By.Id(elements.BtnEdit)).Click();
            Thread.Sleep(2000);

            Console.WriteLine("Seçilen adresin bilgileri siliniyor");
            // Seçtiğim Adresi silmek için yönelendir. full xpath
            driver.FindElement(By.XPath(elements.XPathAddressDelete)).Click();

            Thread.Sleep(2000);

            // sayfayı aşşağı doğru kaydır
            driver.ExecuteJavaScript("window.scrollTo (0, document.body.scrollHeight)");
            Thread.Sleep(3000);

            //Syafayı yukarı doğru kaydırma
            driver.ExecuteJavaScript("window.scrollTo (0, -document.body.scrollHeight)");
            Thread.Sleep(2000);


            // Adresi sil
            driver.FindElement(By.Id(elements.BtnDelete)).Click();


            // Tables dropdown aç
            driver.FindElement(By.Id(elements.BtnTables)).Click();
            Thread.Sleep(1000);


            Console.WriteLine("Telefon Rehberi tablosuna gidiliyor...");
            // Telefon Rehberi tablosuna git id..
            driver.FindElement(By.Id(elements.BtnTelefonRehberi)).Click();

            Thread.Sleep(2000);

            Console.WriteLine("Rehbere kişi eklemek için sayfaya yönlendiriliyor..");
            // Telefon rehberine kişi Ekleme sayfasına git
            driver.FindElement(By.Id(elements.BtnAdd)).Click();






            Console.Read();
        }
    }
}
