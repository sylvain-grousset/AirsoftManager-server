using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;

namespace AirsoftManager_server.Utils
{
    public class QR
    {
        public static string GenerateQR()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("www.google.fr", QRCodeGenerator.ECCLevel.Q);
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            return "data:image/png;base64," + qrCode.GetGraphic(20);
        }

    }
}
