using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace QRScanTestApp.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            ScanQRCommand = new Command(
                ScanQRCode
            );
        }

        private async void ScanQRCode(object obj)
        {
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                PossibleFormats = new List<ZXing.BarcodeFormat> {
                    ZXing.BarcodeFormat.QR_CODE
                }
            };

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan(options);

            try
            {
                if (result != null)
                {
                    if (result.Text.StartsWith("http://", StringComparison.InvariantCulture) ||
                        result.Text.StartsWith("https://", StringComparison.InvariantCulture))
                    {
                        Device.OpenUri(new Uri(result.Text));
                    }
                }

                Console.WriteLine("Scanned Barcode: " + result.Text);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public ICommand ScanQRCommand { get; }
    }
}
