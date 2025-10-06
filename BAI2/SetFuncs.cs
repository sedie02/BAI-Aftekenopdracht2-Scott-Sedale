using System.Collections.Generic;

namespace BAI
{
    public class SetFuncs
    {
        public static HashSet<uint> AlleKleuren(uint[] pixeldata)
        {
            // *** IMPLEMENTATION HERE *** //
            // Hier mag je loopen over pixeldata

            // HashSet waar alle kleuren in komen
            HashSet<uint> uniekeKleuren = new HashSet<uint>();

            // Loop over alle pixels in de pixeldata array
            foreach (uint pixel in pixeldata)
            {
                // pixels toevoegen aan HashSet. Dubbele kleuren worden automatisch overgeslagen
                uniekeKleuren.Add(pixel);
            }

            return uniekeKleuren;


            
        }

        public static HashSet<uint> BlauwTinten(uint[] pixeldata)
        {
            // *** IMPLEMENTATION HERE *** //
            // Hier mag je loopen over pixeldata

            // hashset voor blauwe tintern
            HashSet<uint> blauwTinten = new HashSet<uint>();

            // Doorloop alle pixels
            foreach (uint pixel in pixeldata)
            {
                // kleurwaardes krijgen
                byte r = PixelFuncs.RoodWaarde(pixel);
                byte g = PixelFuncs.GroenWaarde(pixel);
                byte b = PixelFuncs.BlauwWaarde(pixel);

                // Check of dit een blauwtint is
                if (b > r && b > g)
                {
                    blauwTinten.Add(pixel);
                }
            }

            return blauwTinten;

        }

        public static HashSet<uint> DonkereKleuren(uint[] pixeldata)
        {
            // *** IMPLEMENTATION HERE *** //
            // Hier mag je loopen over pixeldata

            // hashset voor donkere kleuren
            HashSet<uint> donkereKleuren = new HashSet<uint>();

            // Doorloop alle pixels
            foreach (uint pixel in pixeldata)
            {
                // kleurwaardes krijgen
                byte r = PixelFuncs.RoodWaarde(pixel);
                byte g = PixelFuncs.GroenWaarde(pixel);
                byte b = PixelFuncs.BlauwWaarde(pixel);

                // Controleer of de som kleiner is dan 192
                if ((r + g + b) < 192)
                {
                    donkereKleuren.Add(pixel);
                }
            }

            return donkereKleuren;
            
        }

        public static HashSet<uint> NietBlauwTinten(uint[] pixeldata)
        {
            // *** IMPLEMENTATION HERE *** //
            // Gebruik set-operatoren op 1 of meer van de sets 'alle kleuren' /
            // 'blauwtinten' / 'donkere kleuren'
            // Gebruik dus GEEN loop


            // alle kleuren
            HashSet<uint> alleKleuren = AlleKleuren(pixeldata);

            // alle blauewe tinten
            HashSet<uint> blauwTinten = BlauwTinten(pixeldata);

            // nieuwe hashset met alle kleuren
            HashSet<uint> nietBlauw = new HashSet<uint>(alleKleuren);

            // Verwijder alle blauwtinten
            nietBlauw.ExceptWith(blauwTinten);

            
            return nietBlauw;
        }

        public static HashSet<uint> DonkerBlauwTinten(uint[] pixeldata)
        {
            // *** IMPLEMENTATION HERE *** //
            // Gebruik set-operatoren op 1 of meer van de sets 'alle kleuren' /
            // 'blauwtinten' / 'donkere kleuren'
            // Gebruik dus GEEN loop


            // bleuwe tinten en donkere kleuren
            HashSet<uint> blauwTinten = BlauwTinten(pixeldata);
            HashSet<uint> donkereKleuren = DonkereKleuren(pixeldata);

            // hashset voor alle donkerblauwe kleuren
            HashSet<uint> donkerBlauw = new HashSet<uint>(blauwTinten);

            // Intersect met donkere kleuren
            donkerBlauw.IntersectWith(donkereKleuren);

            
            return donkerBlauw;
        }
    }
}
