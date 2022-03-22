

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace Business.Constants
{
    public static class Messages
    {//static yapmamın sebebi her seferinde new'lememek için
     //Message lar sabit olduğu için buradan static kullanıyorum.
        public static string OperationClaimAdded = "Rol başarıyla eklendi.";
        public static string OperationClaimDeleted = "Rol başarıyla silindi.";
        public static string OperationClaimUpdated = "Rol başarıyla güncellendi.";
        public static string OperationClaimNotNull = "Lütfen Rol alanını boş geçmeyiniz!!";


        public static string UserOperationClaimAdded = "Kullanıcı rolü başarıyla eklendi.";
        public static string UserOperationClaimDeleted = "Kullanıcı rolü başarıyla silindi.";
        public static string UserOperationClaimUpdated = "Kullanıcı rolü başarıyla güncellendi.";
        public static string OperationClaimIdNotNull = "Lütfen Rol alanını boş geçmeyiniz!!";
        public static string UserIddNotNull = "Lütfen kullanıcı alanını boş geçmeyiniz!!";


        public static string AddressAdded = "Adres başarıyla eklendi.";
        public static string AddressDeleted = "Adres başarıyla silindi.";
        public static string AddressUpdated = "Adres başarıyla güncellendi.";
        public static string MahalleNotNull = "Lütfen mahalle alanını boş geçmeyiniz!!";
        public static string SokakNotNull = "Lütfen sokak alanını boş geçmeyiniz!!";
        public static string BinaNoNotNull = "Lütfen Bina numarası alanını boş geçmeyiniz!!";
        public static string KatNotNull = "Lütfen Kat numarası alanını boş geçmeyiniz!!";
        public static string IlceNotNull = "Lütfen İlçe alanını boş geçmeyiniz!!";
        public static string IlNotNull = "Lütfen İl alanını boş geçmeyiniz!!";
        public static string PostaKoduNotNull = "Lütfen Posta Kodu alanını boş geçmeyiniz!!";
        public static string KarakterUzunlugu50 = "Lütfen 50 karakterden fazla karater girmeyiniz!!";
        public static string KarakterUzunlugu10 = "Lütfen 10 karakterden fazla karater girmeyiniz!!"; 
        public static string EnFazla75KatGirebilirsiniz = "En fazla 75 kat girebilirsiniz!!";
        public static string SifirdanBuyukOlmalidir = "Lütfen 0'dan büyük değer giriniz!!";




        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre Hatalı.";
        public static string SuccessfulLogin = "Sisteme Giriş başarılı.";
        public static string UserAlreadyExits = "Bu kullanıcı zaten mevcut.";
        public static string UserRegisterd = "Kullanıcı başarıyla kaydedildi.";
        public static string AccessTokenCreated = "Access token başarıyla oluşturuldu.";


        public static string AuthorizationDenied = "Yetkiniz yok.";

        public static string OperationClaimNameAlreadyExists = "Bu rol zaten mevcut.";


       
    }
}
