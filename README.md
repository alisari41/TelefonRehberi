
# Telefon Rehberi

## Backend with C#

Kurumsal, Katmanlı Mimari yapısı kullanılarak SOLID kuralları dahilinde oluşturulmuş, C# dili ile yazılmış Telefon Rehberi fikri üzerinden ilerlenmiştir.

> *"Her şey mümkün olduğunca sadeleştirilmeli, fakat basitleştirilmemelidir."* 
> Albert Einstein

### Katmanlar
- **Core:** Projenin çekirdek katmanı, evrensel operasyonlar için kullanılmaktadır. Core katmanını diğer herhangi bir projenizde kullanabiliriz.
- **DataAccess:** Projenin, Veritabanı ile bağını kuran katmandır.
- **Entities:** Veritabanındaki tablolarımızın projemizde nesne olarak kullanılması için oluşturulmuştur. DTO nesnelerinide barındırmaktadır.
- **Business:** Projemizin iş katmanıdır. Türlü iş kuralları; Veri kontrolleri, validasyonlar, IoC Container'lar ve yetki kontrolleri
- **WebAPI:** Prjenin Restful API Katmanıdır.
- **WebMVC:** Projenin sunum katmandır.
- **Web.SeleniumTestTool.UserOperationClaim** Projenin rolleri için test katmanıdır.
- **Web.SeleniumTestTool.UserAndTelephone:** Projenin Telefon rehberi işlemleri test katmanıdır.

### Kullanılan Teknolojiler
<ul>
  <li>.Net Core 5</li>
  <li>Selenium</li>
  <li>Result Türleri</li>
  <li>Restful API</li>
  <li>Interceptor</li>
  <li>Autofac
    <ul>
      <li>IoC (Inversion of Control) </li>
      <li>AOP (Aspect Oriented Programing / Yazılım Geliştirme Yaklaşımı) 
        <ul>
            <li>Caching</li>
            <li>Exception</li>
            <li>Logging</li>
            <li>Performance</li>
            <li>Transaction</li>
            <li>Validation</li>
        </ul>      
      </li>
    </ul>
  </li>            
  <li>Fluent Validation</li>
  <li>Cache yönetimi</li>
  <li>JWT Authentication</li>
  <li>Repository Design Pattern</li>
  <li>Cross Cutting Concerns
    <ul>
        <li>Caching</li>
        <li>Logging</li>
        <li>Validation</li>
    </ul>
  </li>
  <li>ORM
    <ul>
        <li>Dapper</li>
        <li>Entity Framework</li>
    </ul>
  </li>
  <li>Extensions
    <ul>
        <li>Claim
            <ul>
                <li>Claim Principal</li>
            </ul>
        </li>
        <li>Exception Middleware</li>
        <li>Service Collection</li>
        <li>Error Handling
            <ul>
                <li>Error Details</li>
                <li>Validation Error Details</li>
            </ul>
        </li>
    </ul>
  </li>
</ul>

### Selenium
![image](https://user-images.githubusercontent.com/81421228/159133429-387b09c6-1b53-42e5-b50f-8e3dfe92bb69.png)

Öncelikle Selenium nedir ve neden tercih edilir bununla başlayalım. Selenium, farklı tarayıcılarda ve platformlarda kullanılabilen, web uygulamaları için açık kaynaklı bir test paketidir. Selenium kullanarak masaüstü uygulamalar veya mobil uygulamalar test edilemez. Amaç web tabanlı uygulamaların testlerini otomatize etmektir.

Selenium RC, Selenium IDE, Selenium Grid, Selenium WebDriver olmak üzere farklı amaçlar için tasarlanmış dört araçtan oluşur. Selenium, sunduğu çeşitli avantajlar nedeniyle, test cihazları arasında çok popüler hale gelmiştir.

#### Neden Selenium ?

![image](https://user-images.githubusercontent.com/81421228/159133477-6137997b-d1fb-4810-825c-2f6454270b77.png)

Aynı görevi yapan pek çok benzer araç var ancak Selenium daha çok tercih ediliyor.Peki Selenium’u bu kadar öne çıkaran ne?
- Selenium açık kaynaklı olduğundan, lisans maliyeti yoktur.
- Daha önce de belirtildiği gibi, Selenium’un bir takım araçları vardır, bu yüzden kullanıcıların her ihtiyacına uygundur. Farklı ihtiyaçlarınızı karşılamak için WebDriver, Grid ve IDE gibi çeşitli araçları kullanabilirsiniz.
- Bir test cihazının veya geliştiricinin bir otomasyon test aracıyla karşılaştığı en büyük zorluk, dil desteğidir.Test komut dosyaları herhangi bir programlama dilinde yazılabilir.(Örneğin Java, Python, .Net)
- Platform bağımsızdır, herhangi bir işletim sisteminde kullanılabilir.
- Testleri yaparken herhangi bir tarayıcı kullanılabilir.Bu kullanımı esnek hale getirir(Örneğin Mozilla Firefox, Google Chrome,Safari).
- Testleri yönetmek ve rapor üretmek için TestNG ve JUnit gibi araçlar ile entegre edilebilir.
- Sürekli test yapmak için Maven, Jenkins ve Docker entegre edilebilir. 
