using System;
using SajatKivetelek;

namespace IskolaiIndex
{
    public class IndexBejegyzes
    {
        private string tantargy;
        private DateTime datum;
        private int erdemjegy;

        public IndexBejegyzes(string tantargy, DateTime datum, int erdemjegy)
        {
            if (erdemjegy < 1 || erdemjegy > 5)
            {
                throw new InvalidMarkException(erdemjegy);
            }

            this.tantargy = tantargy;
            this.datum = datum;
            this.erdemjegy = erdemjegy;
        }

        public string GetTantargy()
        {
            return tantargy;
        }

        public DateTime GetDatum()
        {
            return datum;
        }

        public int GetErdemjegy()
        {
            return erdemjegy;
        }

        public override string ToString()
        {
            return $"{tantargy};{datum:yyyy.MM.dd};{erdemjegy}";
        }
    }
}