namespace BAI
{
    public class PixelFuncs
    {
        public static uint FilterNiks(uint pixel)
        {
            return pixel;
        }

        public static uint FilterRood(uint pixel)
        {
            // *** IMPLEMENTATION HERE *** //

            return pixel & 0xFF00FFFF; // vergelijkt de pixel code met de hexacode die ernaast staat, kijkt welke gelijk zijn en gelijke bits blijven hetzelfde en de andere worden 0
        }

        public static uint FilterGroen(uint pixel)
        {
            // *** IMPLEMENTATION HERE *** //
            return pixel & 0xFFFF00FF;
        }

        public static uint FilterBlauw(uint pixel)
        {
            // *** IMPLEMENTATION HERE *** //
            return pixel & 0xFFFFFF00;
        }


        public static byte RoodWaarde(uint pixelvalue)
        {
            // *** IMPLEMENTATION HERE *** //
            return (byte)((pixelvalue >> 16) & 0xFF); // shift (in dit geval de rode bits) de pixelvalue naar het begin en vergelijkt met de hexacode die ernaast staat, zodat er alleen een rode waarde uit komt (shift 16 keer omdat rood (23-15 bits) 16 bits van 7-0 vandaan is)
        }

        public static byte GroenWaarde(uint pixelvalue)
        {
            // *** IMPLEMENTATION HERE *** //
            return (byte)((pixelvalue >> 8) & 0xFF);
        }

        public static byte BlauwWaarde(uint pixelvalue)
        {
            // *** IMPLEMENTATION HERE *** //
            return (byte)(pixelvalue & 0xFF);
        }

        public static uint Steganografie(uint pixelvalue)
        {
            // *** IMPLEMENTATION HERE *** //

            // plaatje zit verstopt in rode bits, dus shift de rode bits naar 7-0 bits en vergelijkt dat met 1, omdat we alleen de minst-significate bit willen hebben
            uint redBit = (pixelvalue >> 16) & 1;

            // als de bit 1 is, wordt het rood, als de bit 0 is, wordt het zwart
            uint newColor = redBit == 1 ? 0xFFFF0000 : 0xFF000000;

            //returns newRed
            return newColor;
        }


        // ***** Voor de liefhebbers - deze hoef je NIET te maken om een voldoende te krijgen! ***** //
        public static uint Steganografie2(uint pixelvalue)
        {
            // In het originele plaatje zit nog een tweede plaatje verstopt, maar dan op
            // een *nog* ingewikkelder manier.
            // Laat zien dat je echt een expert bent, en decodeer hier het tweede plaatje.
            // (Hint: kijk naar de eerste 4 bytes van de gedecodeerde data.)

            // *** IMPLEMENTATION HERE *** //
            return 0;
        }
    }
}
