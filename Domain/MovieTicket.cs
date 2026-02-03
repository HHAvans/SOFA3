using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public class MovieTicket
    {
        private int rowNr { get; set; }
        private int seatNr { get; set; }
        private bool isPremium { get; set; }
        public MovieScreening movieScreening { get; set; }

        public MovieTicket(MovieScreening movieScreening, bool isPremiumReservation, int rowNr, int seatNr)
        {
            this.isPremium = isPremiumReservation;
            this.rowNr = rowNr;
            this.seatNr = seatNr;
            this.movieScreening = movieScreening;
        }

        public bool isPremiumTicket()
        {
            return this.isPremium;
        }

        public double getPrice()
        {
            return movieScreening.getPricePerSeat();
        }

        public String toString()
        {
            return "Row: " + this.rowNr + " Seat: " + this.seatNr + (this.isPremium ? " (Premium)" : "" + " Movie: " + this.movieScreening.toString());
        }
    }
}
