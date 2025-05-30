using QRCoder;
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter URL to generate QR code:");
        string url = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(url))
        {
            Console.WriteLine("Invalid input");
            return;
        }

        try
        {
            using var qrGenerator = new QRCodeGenerator();
            using QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            var pngQrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = pngQrCode.GetGraphic(20);

            System.IO.File.WriteAllBytes("qrcode.png", qrCodeBytes);

            Console.WriteLine("QR code saved to qrcode.png");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
