

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace Business.Constants
{
    public static class Messages
    {//static yapmamın sebebi her seferinde new'lememek için
        //Message lar sabit olduğu için buradan static kullanıyorum.
        

        

        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre Hatalı.";
        public static string SuccessfulLogin="Sisteme Giriş başarılı.";
        public static string UserAlreadyExits = "Bu kullanıcı zaten mevcut.";
        public static string UserRegisterd = "Kullanıcı başarıyla kaydedildi.";
        public static string AccessTokenCreated ="Access token başarıyla oluşturuldu." ;


        public static string AuthorizationDenied = "Yetkiniz yok.";
    }
}
