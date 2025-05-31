//using OtpNet;
//using QRCoder;
//using System.Drawing;

//namespace AirlineReservationSystem
//{
//    public static class TotpHelper
//    {
//        public static string GenerateSecretKey()
//        {
//            var key = KeyGeneration.GenerateRandomKey(20);
//            return Base32Encoding.ToString(key);
//        }

//        public static Bitmap GenerateQrCode(string email, string secretKey)
//        {
//            string issuer = "AirlineReservationSystem";
//            string qrContent = $"otpauth://totp/{issuer}:{email}?secret={secretKey}&issuer={issuer}";

//            QRCodeGenerator qrGenerator = new QRCodeGenerator();
//            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
//            QRCode qrCode = new QRCode(qrCodeData);
//            return qrCode.GetGraphic(10);
//        }

//        public static bool VerifyCode(string secretKey, string userCode)
//        {
//            var totp = new Totp(Base32Encoding.ToBytes(secretKey));
//            return totp.VerifyTotp(userCode, out _, window: new VerificationWindow(1, 1));
//        }
//    }
//}