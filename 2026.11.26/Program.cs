using System;
using System.Globalization;
using HomersekletKezeles;

// A hőmérséklet osztály saját névtere
namespace HomersekletKezeles
{
    // A lehetséges mértékegységek felsorolása
    public enum Mertekegyseg
    {
        CELSIUS,
        KELVIN
    }

    public class Homerseklet
    {
        // Adattagok
        private double ertek;
        private Mertekegyseg mertekegyseg;

        // Kétparaméteres konstruktor
        // Mindkét adattagot a kapott paraméterek alapján állítjuk be
        public Homerseklet(double ertek, Mertekegyseg mertekegyseg)
        {
            this.ertek = ertek;
            this.mertekegyseg = mertekegyseg;
        }

        // Egyparaméteres konstruktor
        // A mértékegység automatikusan Celsius lesz
        public Homerseklet(double ertek)
        {
            this.ertek = ertek;
            this.mertekegyseg = Mertekegyseg.CELSIUS;
        }

        // Getter metódus az érték lekérdezéséhez
        public double GetErtek()
        {
            return ertek;
        }

        // Setter metódus az érték módosításához
        public void SetErtek(double ertek)
        {
            this.ertek = ertek;
        }

        // Getter metódus a mértékegység lekérdezéséhez
        public Mertekegyseg GetMertekegyseg()
        {
            return mertekegyseg;
        }

        // Setter metódus a mértékegység módosításához
        public void SetMertekegyseg(Mertekegyseg mertekegyseg)
        {
            this.mertekegyseg = mertekegyseg;
        }

        // A ToString metódus felüldefiniálása
        // Az objektum összes adatát szövegként adja vissza
        public override string ToString()
        {
            return $"Homerseklet [ertek={ertek}, " +
                   $"mertekegyseg={mertekegyseg}]";
        }

        // Osztályszintű, azaz static konverziós metódus
        // A második paraméter a cél mértékegységet jelenti
        public static double Konvertalas(
            double ertek, Mertekegyseg celMertekegyseg)
        {
            if (celMertekegyseg == Mertekegyseg.KELVIN)
            {
                // Celsius átváltása Kelvinre
                return ertek + 273.15;
            }
            else
            {
                // Kelvin átváltása Celsiusra
                return ertek - 273.15;
            }
        }
    }
}

// A futtatható osztály külön névtere
namespace HomersekletTeszt
{
    public class HoProba
    {
        static void Main(string[] args)
        {
            // A tizedesjegyek ponttal jelenjenek meg
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // Négy objektum tárolására alkalmas tömb
            Homerseklet[] homersekletek = new Homerseklet[4];

            // Az első két objektum ellenőrzött beolvasása
            Console.WriteLine("Első hőmérséklet:");
            homersekletek[0] = Beolvasas(Mertekegyseg.CELSIUS);

            Console.WriteLine("\nMásodik hőmérséklet:");
            homersekletek[1] = Beolvasas(Mertekegyseg.KELVIN);

            // Véletlenszám-generátor létrehozása
            Random random = new Random();

            // Két véletlenszerű valós szám generálása
            // A NextDouble() 0 és 1 közötti számot generál
            // 100-zal megszorozva 0 és 100 közötti értéket kapunk
            homersekletek[2] = new Homerseklet(
                random.NextDouble() * 100);

            homersekletek[3] = new Homerseklet(
                random.NextDouble() * 100);

            // A tömb elemeinek listázása
            Console.WriteLine("\nA tömb elemeinek listázása:");
            Listazas(homersekletek);

            Console.WriteLine("\nKonverzió:");

            // Az első objektum értékének átváltása Kelvinre
            double kelvin = Homerseklet.Konvertalas(
                homersekletek[0].GetErtek(),
                Mertekegyseg.KELVIN);

            Console.WriteLine(
                $"{homersekletek[0].GetErtek()} Celsius : " +
                $"{kelvin} Kelvin");

            // A második objektum értékének átváltása Celsiusra
            double celsius = Homerseklet.Konvertalas(
                homersekletek[1].GetErtek(),
                Mertekegyseg.CELSIUS);

            Console.WriteLine(
                $"{homersekletek[1].GetErtek()} Kelvin : " +
                $"{celsius} Celsius");

            // Átlaghőmérséklet kiszámítása
            Console.WriteLine("\nSzámítás:");

            double atlag = AtlagHomerseklet(homersekletek);

            Console.WriteLine(
                $"Az átlaghőmérséklet: {atlag} Kelvin");
        }

        // Ellenőrzött beolvasást végző metódus
        static Homerseklet Beolvasas(Mertekegyseg elvartMertekegyseg)
        {
            double ertek;

            // A fizikailag megengedett legkisebb hőmérséklet
            double minimum;

            if (elvartMertekegyseg == Mertekegyseg.CELSIUS)
            {
                minimum = -273.15;
            }
            else
            {
                minimum = 0;
            }

            // Addig kérjük be az értéket, amíg az nem megfelelő
            while (true)
            {
                Console.Write("Hőmérséklet értéke: ");

                string bemenet = Console.ReadLine() ?? "";

                // A vesszőt is elfogadjuk tizedeselválasztóként
                bemenet = bemenet.Replace(',', '.');

                // Ellenőrizzük, hogy valóban számot kaptunk-e,
                // és az érték fizikailag lehetséges-e
                if (double.TryParse(bemenet, out ertek) &&
                    !double.IsNaN(ertek) &&
                    !double.IsInfinity(ertek) &&
                    ertek >= minimum)
                {
                    break;
                }

                Console.WriteLine("Hibás hőmérséklet!");
            }

            Mertekegyseg mertekegyseg;

            // A mértékegység ellenőrzött beolvasása
            while (true)
            {
                Console.Write(
                    $"Mértékegység ({elvartMertekegyseg}): ");

                string bemenet = Console.ReadLine() ?? "";

                // A kis- és nagybetűket azonos módon kezeljük
                bemenet = bemenet.Trim().ToUpper();

                // Megvizsgáljuk, hogy a megfelelő
                // mértékegységet adta-e meg a felhasználó
                if (bemenet == elvartMertekegyseg.ToString())
                {
                    mertekegyseg = elvartMertekegyseg;
                    break;
                }

                Console.WriteLine("Hibás mértékegység!");
            }

            // Létrehozzuk és visszaadjuk az objektumot
            return new Homerseklet(ertek, mertekegyseg);
        }

        // A tömb elemeinek kiírását végző metódus
        static void Listazas(Homerseklet[] tomb)
        {
            // Egyesével végigmegyünk a tömb objektumain
            foreach (Homerseklet homerseklet in tomb)
            {
                // Automatikusan meghívódik a ToString metódus
                Console.WriteLine(homerseklet);
            }
        }

        // Átlaghőmérséklet kiszámítása Kelvinben
        static double AtlagHomerseklet(Homerseklet[] tomb)
        {
            double osszeg = 0;

            // Végigmegyünk a tömb összes objektumán
            foreach (Homerseklet homerseklet in tomb)
            {
                // Ha Celsiusban van megadva a hőmérséklet
                if (homerseklet.GetMertekegyseg() ==
                    Mertekegyseg.CELSIUS)
                {
                    // Először Kelvinre konvertáljuk,
                    // majd hozzáadjuk az összeghez
                    osszeg += Homerseklet.Konvertalas(
                        homerseklet.GetErtek(),
                        Mertekegyseg.KELVIN);
                }
                else
                {
                    // Ha már Kelvinben van, nem kell konvertálni
                    osszeg += homerseklet.GetErtek();
                }
            }

            // Átlag = összeg / elemszám
            return osszeg / tomb.Length;
        }
    }
}