using System;
using System.IO;
using IskolaiIndex;
using SajatKivetelek;

namespace TesztProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IndexBejegyzes[] bejegyzesek = new IndexBejegyzes[5];

            // 1. Bejegyzések beolvasása

            int i = 0;

            while (i < bejegyzesek.Length)
            {
                try
                {
                    Console.WriteLine($"\n{i + 1}. bejegyzés");

                    Console.Write("Tantárgy neve: ");
                    string tantargy = Console.ReadLine();

                    Console.Write("Dátum (éééé.hh.nn): ");
                    DateTime datum = DateTime.Parse(Console.ReadLine());

                    Console.Write("Érdemjegy: ");
                    string jegySzoveg = Console.ReadLine();

                    int erdemjegy = int.Parse(jegySzoveg);

                    bejegyzesek[i] = new IndexBejegyzes(
                        tantargy, datum, erdemjegy);

                    i++;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Hiba! Hibás adatformátum!");
                }
                catch (InvalidMarkException ex)
                {
                    Console.WriteLine(
                        "Az érdemjegy nem lehet " + ex.GetErdemjegy());
                }
                catch (OverflowException)
                {
                    Console.WriteLine("A megadott szám túl nagy vagy túl kicsi!");
                }
            }

            
        }
    }
}