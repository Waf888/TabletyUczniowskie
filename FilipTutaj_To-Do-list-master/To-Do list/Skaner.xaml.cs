using ZXing.Net.Maui;

namespace To_Do_list
{
    public partial class ScannerPage : ContentPage
    {
        private Entry taskEntry;

        public ScannerPage(Entry taskEntry)
        {
            InitializeComponent();

            this.taskEntry = taskEntry;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            cameraView.Options = new BarcodeReaderOptions
            {
                Formats =
                    BarcodeFormats.OneDimensional |
                    BarcodeFormats.TwoDimensional,

                AutoRotate = true,
                Multiple = false,
                TryHarder = true
            };

            cameraView.IsDetecting = true;
        }

        protected override void OnDisappearing()
        {
            cameraView.IsDetecting = false;

            base.OnDisappearing();
        }

        private void CameraView_BarcodeDetected(
            object sender,
            BarcodeDetectionEventArgs e)
        {
            var result = e?.Results?.FirstOrDefault();

            if (result == null)
            {
                return;
            }

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                
                codeValue.Text = result.Value;

                
                taskEntry.Text = result.Value;

                
                cameraView.IsDetecting = false;

                
                await Navigation.PopAsync();
            });
        }
    }
}
