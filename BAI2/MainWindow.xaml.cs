using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BAI
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private uint[] pixeldata;
        private BitmapImage image;
        private int width, height, bitmapscale;
        private WriteableBitmap writeableBitmap;

        public MainWindow()
        {
            InitializeComponent();

            Uri uri = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "logo-HBOICT.png"));
            image = new BitmapImage(uri);

            width = image.PixelWidth;
            height = image.PixelHeight;
            bitmapscale = (int)(image.PixelWidth / imgBitmap.Width);
            pixeldata = new uint[width * height];
            image.CopyPixels(pixeldata, 4 * width, 0);
            // pixeldata bevat nu 1 32-bit uint-waarde per pixel: vanaf links (most significant bit = positie 31) geteld
            // bits 31-24 = alpha-kanaal (transparantie, 0 = geheel doorzichtig, 255 = geheel zichtbaar)
            // bits 23-16 = rood-kanaal
            // bits 15-8 = groen-kanaal
            // bits 7-0 = blauw-kanaal
            // Voorbeeld:
            //    0b11000000_11111111_11111111_00000000 = iets transparant geel,
            //    0x3F_00_00_FF = grotendeels transparant blauw

            writeableBitmap = new WriteableBitmap(width, height, image.DpiX, image.DpiY, PixelFormats.Bgra32, null);
            RenderPixelData(PixelFuncs.FilterNiks);

            imgBitmap.Source = writeableBitmap;
        }


        private void btnFilterKleur_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnFilterRood":
                    RenderPixelData(PixelFuncs.FilterRood);
                    break;
                case "btnFilterGroen":
                    RenderPixelData(PixelFuncs.FilterGroen);
                    break;
                case "btnFilterBlauw":
                    RenderPixelData(PixelFuncs.FilterBlauw);
                    break;
                case "btnOrigineleKleuren":
                    RenderPixelData(PixelFuncs.FilterNiks);
                    break;
                default:
                    break;
            }
        }


        private void imgBitmap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int Xpos = (int)e.GetPosition(sender as Image).X;
            int Ypos = (int)e.GetPosition(sender as Image).Y;
            uint pixelvalue = pixeldata[Ypos * image.PixelWidth * bitmapscale + Xpos * bitmapscale];

            lblCursorWaardes.Content =
                String.Format("Positie: ({0}, {1}) - rood: {2}, groen: {3}, blauw: {4}",
                Xpos, Ypos, PixelFuncs.RoodWaarde(pixelvalue),
                PixelFuncs.GroenWaarde(pixelvalue),
                PixelFuncs.BlauwWaarde(pixelvalue));
        }


        private void btnKleurenSet_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnAlleKleuren":
                    RenderHashSet(SetFuncs.AlleKleuren(pixeldata));
                    break;
                case "btnBlauwtinten":
                    RenderHashSet(SetFuncs.BlauwTinten(pixeldata));
                    break;
                case "btnDonkereKleuren":
                    RenderHashSet(SetFuncs.DonkereKleuren(pixeldata));
                    break;
                case "btnNietBlauw":
                    RenderHashSet(SetFuncs.NietBlauwTinten(pixeldata));
                    break;
                case "btnDonkerblauw":
                    RenderHashSet(SetFuncs.DonkerBlauwTinten(pixeldata));
                    break;
                default:
                    break;
            }
        }

        private void btnStegaPlaatje_Click(object sender, RoutedEventArgs e)
        {
            RenderPixelData(PixelFuncs.Steganografie);
        }

        private void btnStegaPlaatje2_Click(object sender, RoutedEventArgs e)
        {
            RenderPixelData(PixelFuncs.Steganografie2);
        }

        private void RenderPixelData(Func<uint, uint> bitwiseTransform)
        {
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixeldata.Select(bitwiseTransform).ToArray(), 4 * width, 0);
        }


        private void RenderHashSet(HashSet<uint> kleurenData)
        {
            uint[] hashsetData = new uint[pixeldata.Length];
            int pos = 0;

            //
            // Bouw uint[] op
            //
            // Let op: het is niet gebruikelijk te loopen over een hashset,
            // maar voor deze opdracht moet het even ;)
            //
            foreach (uint kleur in kleurenData.OrderBy(waarde => waarde))
            {
                for (int i = 0; i < Math.Floor((double)(pixeldata.Length / kleurenData.Count)); i++)
                {
                    hashsetData[pos] = kleur;
                    pos++;
                }
            }
            writeableBitmap.WritePixels(new Int32Rect(0, 0, width, height), hashsetData, 4 * width, 0);
        }
    }
}