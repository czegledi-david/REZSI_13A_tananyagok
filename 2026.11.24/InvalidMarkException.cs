using System;

namespace SajatKivetelek
{
    public class InvalidMarkException : Exception
    {
        private int erdemjegy;

        public InvalidMarkException(int erdemjegy)
            : base("A megadott érdemjegy nem érvényes!")
        {
            this.erdemjegy = erdemjegy;

            Console.WriteLine("Érvénytelen érdemjegy: " + erdemjegy);
        }

        public int GetErdemjegy()
        {
            return erdemjegy;
        }
    }
}