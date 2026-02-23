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

        // State pattern states
        private OrderState createdState;
        private OrderState submittedState;
        private OrderState cancelledState;
        private OrderState provisionalState;
        private OrderState paidState;

        private OrderState state;

        public Order(int orderNr, bool isStudentOrder)
        {
            this.orderNr = orderNr;
            this.isStudentOrder = isStudentOrder;

            this.createdState = new CreatedState(this);
            this.submittedState = new SubmittedState(this);
            this.cancelledState = new CancelledState();
            this.provisionalState = new ProvisionalState(this);
            this.paidState = new PaidState();
            this.state = createdState;
        }

        public void setState(OrderState orderState)
        {
            state = orderState;
        }

        public int getOrderNr()
        {
            return this.orderNr;
        }

        public void addSeatReservation(MovieTicket ticket)
        {
            this.movieTickets.Add(ticket);

        }

        public void removeAllSeatReservation()
        {
            this.movieTickets.Clear();
        }

        public DateTime orderDate()
        {
            return this.movieTickets.First().movieScreening.dateAndTime;
        }

        public double calculatePrice()
        {
            if (movieTickets.Count == 0)
            {
                return 0.0;
            }

            var ticketsToCalculate = new List<MovieTicket>(this.movieTickets);
            var currentDay = this.orderDate().DayOfWeek;
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

                File.WriteAllText(@"C:\Users\homer\Downloads\movie.txt", sb.ToString());
            }
            else if (exportFormat == TicketExportFormat.JSON)
            {
                string json = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(@"C:\Users\homer\Downloads\movie.json", json);
                return;
            }
        }

        public void submitOrder()
        {
            state.submit();
        }

        public void cancelOrder()
        {
            state.cancel();
        }

        public void changeOrder(List<MovieTicket> movieTickets)
        {
            state.change(movieTickets);
        }

        public void checkDeadline()
        {
            state.checkDeadline();
        }

        public void payOrder()
        {
            state.pay();
        }
    }
}
