using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace SOFA3.Domain
{
    public class Order
    {
        // Strategy pattern stuff
        private PriceBehavior priceBehavior;
        private ExportBehavior exportBehavior;

        private int orderNr { get; set; }
        private bool isStudentOrder { get; set; }
        public List<MovieTicket> movieTickets = new List<MovieTicket>();

        public Order(int orderNr, bool isStudentOrder)
        {
            this.orderNr = orderNr;
            this.isStudentOrder = isStudentOrder;

            // default export is text
            this.exportBehavior = new TextExportBehavior();
            this.priceBehavior = isStudentOrder ? new StudentPreiumPriceBehavior() : new RegularPriceBehavior();
        }

        public int getOrderNr()
        {
            return this.orderNr;
        }

        public void addSeatReservation(MovieTicket ticket)
        {
            this.movieTickets.Add(ticket);

        }

        public double calculatePrice()
        {
            if (movieTickets.Count == 0)
            {
                return 0.0;
            }

            var ticketsToCalculate = new List<MovieTicket>(this.movieTickets);
            var currentDay = this.movieTickets.First().movieScreening.dateAndTime.DayOfWeek;
            var isWeekend = (currentDay == DayOfWeek.Friday || currentDay == DayOfWeek.Saturday || currentDay == DayOfWeek.Sunday);

            if (!isWeekend || this.isStudentOrder)
            {

                for(int i = ticketsToCalculate.Count - 1; i >= 0; i--)
                {
                    if(i%2 == 1)
                    {
                        ticketsToCalculate.RemoveAt(i);
                    }
                }
            }

            var totalPrice = 0.0;
            foreach (var ticket in ticketsToCalculate)
            {
                totalPrice += ticket.getPrice() + this.priceBehavior.extraPrice(ticket);
            }

            if (this.movieTickets.Count >= 6)
            {
                totalPrice *= 1.1;
            }
            return totalPrice;
        }

        public void setExportFormat(ExportBehavior exportBehavior)
        {
            this.exportBehavior = exportBehavior;
        }

        public void export()
        {
            this.exportBehavior.export(this);
        }
    }
}
