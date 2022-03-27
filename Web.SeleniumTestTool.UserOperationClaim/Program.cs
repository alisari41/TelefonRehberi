using System;
using System.Threading;
using Core.Entities.Concrete;
using Entities.Dtos;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Web.SeleniumTestTool.UserOperationClaim.Models;

namespace Web.SeleniumTestTool.UserOperationClaim
{
    class Program
    {
        static void Main(string[] args)
        {

            UserForLoginDto loginDto = new UserForLoginDto();

            loginDto.Email = "alisari41@outlook.com";
            loginDto.Password = "123456*";

            OperationClaim operationClaim = new OperationClaim();
            operationClaim.Name = "";


            Core.Entities.Concrete.UserOperationClaim userOperationClaim = new Core.Entities.Concrete.UserOperationClaim();
            userOperationClaim.UserId = 1;
            userOperationClaim.OperationClaimId = 4;



            Elements elements = new Elements();

            elements.Link = @"https://localhost:44375/";


            elements.TxtGirisEmail = "txtGirisEmail";
            elements.TxtGirisPassword = "txtGirisPassword";
            elements.TxtKontrol = "txtKotrol";// Validation kotrol
            elements.TxtError = "txtEror";
            elements.TxtRol = "txtRol";

            elements.TxtUserId = "txtUserId";
            elements.TxtRolId = "txtRolId";


            elements.BtnGuvenlikDegil = "details-button";
            elements.BtnGuvenlikDegilKabulEt = "proceed-link";
            elements.BtnAdmin = "btnAdmin";
            elements.BtnSignIn = "btnSingIn";
            elements.BtnLogout = "btnLogout";
            elements.BtnTables = "btnTables";
            elements.BtnKullancilar = "btnKullancilar";
            elements.BtnOperationClaims = "btnOperationClaims";
            elements.BtnUserOperationClaims = "btnUserOperationClaims";
            elements.BtnAdd = "btnAdd";
            elements.BtnEdit = "btnEdit";
            elements.BtnDelete = "btnDelete";

            elements.XPathOperationClaimEdit = "/html/body/div[1]/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[8]/td[3]/a";
            elements.XPathOperationClaimDelete = "/html/body/div[1]/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[11]/td[4]/a";

            elements.XPathUserOperationClaimEdit = "/html/body/div[1]/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[3]/td[4]/a";
            elements.XPathUserOperationClaimDelete = "/html/body/div[1]/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[4]/td[5]/a";



            ChromeOptions chromeOptions = new ChromeOptions();
            Console.WriteLine("Sertifika ayarlaması yapılıyor.");
            chromeOptions.AcceptInsecureCertificates = true;


            IWebDriver driver = new ChromeDriver(chromeOptions); //Hangi tarayıcı üzerinden işlem yapacağımı belirtiyorum.
            driver.Navigate().GoToUrl(elements.Link); //Url'e gitme işlemi

            Console.WriteLine("Tarayıcı boyutu büyütülüyor");
            driver.Manage().Window.Maximize();//Tarayıcı boyutu büyütülüyor
            Thread.Sleep(3000);


            //Console.WriteLine("Güvenli değil! Uyarsını zorla geçiliyor...");
            ////Güvenli Değil
            //driver.FindElement(By.Id(elements.BtnGuvenlikDegil)).Click();

            //// Güvenli Değil fakat Yinede kabul et butonu
            //driver.FindElement(By.Id(elements.BtnGuvenlikDegilKabulEt)).Click();
            //Thread.Sleep(3000);


            Console.WriteLine("Admin Sayfasına Gidiliyor");
            // Admin Sayfasına Git
            driver.FindElement(By.Id(elements.BtnAdmin)).Click();

            Console.WriteLine("Email ve şifre bilgileri giriliyor");
            // Email id..
            driver.FindElement(By.Id(elements.TxtGirisEmail)).SendKeys(loginDto.Email);

            // Password Id..
            driver.FindElement(By.Id(elements.TxtGirisPassword)).SendKeys(loginDto.Password);

            Thread.Sleep(3000);

            Console.WriteLine("Giriş yap butonu test ediliyor");
            // Giriş yap
            driver.FindElement(By.Id(elements.BtnSignIn)).Click();


            Thread.Sleep(2000);

            Console.WriteLine("Roller tablosuna gitme işlemi test ediliyor..");
            // Tables dropdown aç
            driver.FindElement(By.Id(elements.BtnTables)).Click();
            Thread.Sleep(1000);

            // Rol tablosuna git id..
            driver.FindElement(By.Id(elements.BtnOperationClaims)).Click();

            Thread.Sleep(2000);

            Console.WriteLine("Rol ekleme sayfasına gidiliyor.");
            // Rol Ekleme sayfasına git
            driver.FindElement(By.Id(elements.BtnAdd)).Click();

            Console.WriteLine("Roller Değerleri giriliyor..");
            driver.FindElement(By.Id(elements.TxtRol)).SendKeys(operationClaim.Name);

            Console.WriteLine("Kayıt ediliyor");
            //Bilgiler Girildikten sonra ekle butonuna bas
            driver.FindElement(By.Id(elements.BtnAdd)).Click();

            Thread.Sleep(2000);


            // Validation Kontrol İşlemleri
            try
            {
                Assert.IsNotNull(elements.TxtKontrol);
                Console.WriteLine("Girilen değerler validation kurallarına uymamaktadır.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Var olan Rolü test ediyoruz..");
            // Var olan Rol deneniyor
            operationClaim.Name = "Admin";
            driver.FindElement(By.Id(elements.TxtRol)).SendKeys(operationClaim.Name);

            driver.FindElement(By.Id(elements.BtnAdd)).Click();

            //  Kontrol İşlemleri
            try
            {
                Assert.IsNotNull(elements.TxtError);
                Console.WriteLine("Daha önce var olan rol kayıt edilmeye çalışınca karşımıza çıkan sonuç kontrol ediliyor.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Thread.Sleep(3000);

            // Txtbox'ı temizle
            driver.FindElement(By.Id(elements.TxtRol)).Clear();

            Console.WriteLine("Var olmayan Rol test ediliyor");
            // Var olan Rol deneniyor
            operationClaim.Name = "Uzman";//Burası her testde değişmesi gerekir.
            driver.FindElement(By.Id(elements.TxtRol)).SendKeys(operationClaim.Name);

            driver.FindElement(By.Id(elements.BtnAdd)).Click();

            Thread.Sleep(2000);

            Console.WriteLine("Seçilen rolün biglilerini güncellemek için sayfasına gidildiği test ediliyor..");
            // x Numaralı Rolün Bilgilerini güncellemek için sayfasına yönlendiriliyor.full xpath
            driver.FindElement(By.XPath(elements.XPathOperationClaimEdit)).Click();
            operationClaim.Name = "Müdür";

            Console.WriteLine("Güncellenecek alanların textbox içerisi temizleniyor.");
            // Kolonları temizleyelim
            driver.FindElement(By.Id(elements.TxtRol)).Clear();


            Thread.Sleep(2000);

            Console.WriteLine("Veriler girildikten sonra güncelleme işlemi yapılıyor..");
            driver.FindElement(By.Id(elements.TxtRol)).SendKeys(operationClaim.Name);

            Thread.Sleep(2000);


            //Bilgiler Girildikten sonra güncelle butonuna bas
            driver.FindElement(By.Id(elements.BtnEdit)).Click();
            Thread.Sleep(2000);

            Console.WriteLine("Seçilen rol bilgileri silinme işlemi test ediliyor..");
            // Seçtiğim rol silmek için yönelendir. full xpath
            driver.FindElement(By.XPath(elements.XPathOperationClaimDelete)).Click();

            Thread.Sleep(2000);


            // Rol Bilgisini sil 
            driver.FindElement(By.Id(elements.BtnDelete)).Click();
            Thread.Sleep(2000);


            Console.WriteLine("Kullanıcı için Roller tablosuna gitme işlemi test ediliyor..");
            // Tables dropdown aç
            driver.FindElement(By.Id(elements.BtnTables)).Click();
            Thread.Sleep(1000);

            // Rol tablosuna git id..
            driver.FindElement(By.Id(elements.BtnUserOperationClaims)).Click();

            Thread.Sleep(2000);

            Console.WriteLine("Kullanıcı Rol ekleme sayfasına gidiliyor.");
            // Rol Ekleme sayfasına git
            driver.FindElement(By.Id(elements.BtnAdd)).Click();

            Console.WriteLine("Kullanıcı Roller Değerleri giriliyor..");
            driver.FindElement(By.Id(elements.TxtUserId)).SendKeys(userOperationClaim.UserId.ToString());
            driver.FindElement(By.Id(elements.TxtRolId)).SendKeys(userOperationClaim.OperationClaimId.ToString());

            Thread.Sleep(2000);
            Console.WriteLine("Kayıt ediliyor");
            //Bilgiler Girildikten sonra ekle butonuna bas
            driver.FindElement(By.Id(elements.BtnAdd)).Click();

            Thread.Sleep(2000);



            Console.WriteLine("Seçilen kullanıcı rol biglilerini güncellemek için sayfasına gidildiği test ediliyor..");
            // x Numaralı Kullanıcı Rolün Bilgilerini güncellemek için sayfasına yönlendiriliyor.full xpath
            driver.FindElement(By.XPath(elements.XPathUserOperationClaimEdit)).Click();
            userOperationClaim.OperationClaimId = 3;

            Console.WriteLine("Güncellenecek alanların textbox içerisi temizleniyor.");
            // Kolonları temizleyelim
            driver.FindElement(By.Id(elements.TxtRolId)).Clear();


            Thread.Sleep(2000);

            Console.WriteLine("Veriler girildikten sonra güncelleme işlemi yapılıyor..");
            driver.FindElement(By.Id(elements.TxtRolId)).SendKeys(userOperationClaim.OperationClaimId.ToString());

            Thread.Sleep(2000);

            //Bilgiler Girildikten sonra güncelle butonuna bas
            driver.FindElement(By.Id(elements.BtnEdit)).Click();
            Thread.Sleep(2000);


            Console.WriteLine("Seçilen kullanıcı rol bilgileri silinme işlemi test ediliyor..");
            // Seçtiğim rol silmek için yönelendir. full xpath
            driver.FindElement(By.XPath(elements.XPathUserOperationClaimDelete)).Click();

            Thread.Sleep(2000);


            // Rol Bilgisini sil 
            driver.FindElement(By.Id(elements.BtnDelete)).Click();
            Thread.Sleep(2000);

            Thread.Sleep(2000);

            //Oturum Kapatılması test ediliyor
            Console.WriteLine("Oturum Kapatılması test ediliyor..");
            driver.FindElement(By.Id(elements.BtnLogout)).Click();

            Thread.Sleep(2000);
            //Tarayıcı kapatılıyor
            Console.WriteLine("Tarayıcı kapatılıyor...");
            driver.Close();


            Console.Read();
        }
    }
}
