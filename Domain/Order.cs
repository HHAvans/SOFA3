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
        private int orderNr { get; set; }
        private bool isStudentOrder { get; set; }
        public Customer customer { get; set; }
        public List<MovieTicket> movieTickets = new List<MovieTicket>();

        // State pattern states
        private OrderState createdState;
        private OrderState submittedState;
        private OrderState cancelledState;
        private OrderState provisionalState;
        private OrderState paidState;

        private OrderState state;

        // Observable pattern thingie
        public OrderObservable orderObservable;

        public Order(int orderNr, bool isStudentOrder, Customer customer)
        {
            this.orderNr = orderNr;
            this.isStudentOrder = isStudentOrder;
            this.customer = customer;

            this.createdState = new CreatedState(this);
            this.submittedState = new SubmittedState(this);
            this.cancelledState = new CancelledState();
            this.provisionalState = new ProvisionalState(this);
            this.paidState = new PaidState();
            this.state = createdState; 

            this.orderObservable = new OrderObservable(this);
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

                for (int i = ticketsToCalculate.Count - 1; i >= 0; i--)
                {
                    if (i % 2 == 1)
                    {
                        ticketsToCalculate.RemoveAt(i);
                    }
                }
            }

            var totalPrice = 0.0;
            foreach (var ticket in ticketsToCalculate)
            {
                var extraPrice = 0.0;
                if (ticket.isPremiumTicket())
                {
                    if (this.isStudentOrder)
                    {
                        extraPrice = 2.0;
                    }
                    else
                    {
                        extraPrice = 3.0;
                    }
                }
                totalPrice += ticket.getPrice() + extraPrice;
            }

            if (this.movieTickets.Count >= 6)
            {
                totalPrice *= 1.1;
            }
            return totalPrice;
        }

        public void export(TicketExportFormat exportFormat)
        {
            StringBuilder sb = new StringBuilder($"Export of {this.orderNr}", 1000);
            sb.AppendLine();
            sb.AppendLine();

            if (exportFormat == TicketExportFormat.PLAINTEXT)
            {
                foreach (var ticket in this.movieTickets)
                {
                    sb.AppendLine(ticket.toString());
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