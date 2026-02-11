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
        private PremiumBehavior premiumBehavior;
        private DiscountBehavior discountBehavior;
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
            if(isStudentOrder)
            {
                this.discountBehavior = new StudentDiscountBehavior();
                this.premiumBehavior = new StudentPremiumPriceBehavior();
            }
            else
            {
                this.discountBehavior = new RegularDiscountBehavior();
                this.premiumBehavior = new RegularPriceBehavior();
            }
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

            var ticketsToCharge = discountBehavior.getTicketsToCharge(this.movieTickets);
            double total = 0;
            foreach(var ticket in ticketsToCharge)
            {
                double basePrice = ticket.getPrice();
                double premium = ticket.isPremiumTicket() ? premiumBehavior.GetPremiumExtra() : 0.0;
                total += basePrice + premium;
            }
            return discountBehavior.applyAdditionalDiscount(movieTickets, total);
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
