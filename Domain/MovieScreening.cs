using System;
using System.Collections.Generic;

namespace SOFA3.Domain
{
    public class MovieScreening
    {
        public DateTime dateAndTime { get; set; }
        private double pricePerSeat { get; set; }
        private List<MovieTicket> ticketList { get; set; }
        private Movie movie { get; set; }

        public MovieScreening(Movie movie, DateTime dateAndTime, double pricePerSeat)
        {
            this.movie = movie;
            this.dateAndTime = dateAndTime;
            this.pricePerSeat = pricePerSeat;
        }

        public Double getPricePerSeat()
        {
            return this.pricePerSeat;
        }

        public String toString()
        {
            return this.dateAndTime.ToString() + " - " + this.pricePerSeat.ToString("C") + " - " + this.movie.ToString();
        }
    }
}