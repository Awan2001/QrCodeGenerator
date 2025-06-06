using QRCoder;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string saveDirectory = @"C:\Users\arife\OneDrive\Desktop\GeneratedQrCode";

        // Create the directory if it doesn't exist
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        while (true)
        {
            Console.WriteLine("Enter URL to generate QR code:");
            string url = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(url))
            {
                Console.WriteLine("Invalid input. Please enter a non-empty URL.");
                continue;
            }

            string fileName;
            string fullPath;
            while (true)
            {
                Console.WriteLine("Enter a name for the QR code image (without extension):");
                fileName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Console.WriteLine("Invalid name. Please try again.");
                    continue;
                }

                fileName = fileName.Trim() + ".png";
                fullPath = Path.Combine(saveDirectory, fileName);

                if (File.Exists(fullPath))
                {
                    Console.WriteLine($"File \"{fileName}\" already exists. Please choose a different name.");
                }
                else
                {
                    break;
                }
            }

            try
            {
                using var qrGenerator = new QRCodeGenerator();
                using QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

                var pngQrCode = new PngByteQRCode(qrCodeData);
                byte[] qrCodeBytes = pngQrCode.GetGraphic(20);

                File.WriteAllBytes(fullPath, qrCodeBytes);

                Console.WriteLine($"QR code saved to: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.Write("Do you want to generate another QR code? (y/n): ");
            string choice = Console.ReadLine();
            if (choice.Trim().ToLower() != "y")
            {
                break;
            }
        }
    }
}
