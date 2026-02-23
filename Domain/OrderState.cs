using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public interface OrderState
    {

        public void submit();
        public void cancel();
        public void pay();
        public void change(List<MovieTicket> movieTickets);
        public void checkDeadline();
    }

    public class CancelledState : OrderState
    {
        public void addSeatReservation()
        {
            throw new NotImplementedException();
        }

        public void cancel()
        {
            throw new NotImplementedException();
        }

        public void change(List<MovieTicket> movieTickets)
        {
            throw new NotImplementedException();
        }

        public void checkDeadline()
        {
            throw new NotImplementedException();
        }

        public void pay()
        {
            throw new NotImplementedException();
        }

        public void submit()
        {
            throw new NotImplementedException();
        }
    }

    public class SubmittedState : OrderState
    {
        private Order order;
        public SubmittedState(Order order)
        {
            this.order = order;
        }

        public void cancel()
        {
            order.orderObservable.notifyObserver("Cancelled");
            this.order.setState(new CancelledState());
        }

        public void change(List<MovieTicket> movieTickets)
        {
            order.removeAllSeatReservation();
            foreach (var ticket in movieTickets)
            {
                order.addSeatReservation(ticket);
            }
        }

        public void checkDeadline()
        {
            var currentDate = DateTime.Now;
            var orderDate = order.orderDate();
            var hoursRemaining = (orderDate - currentDate).TotalHours;

            if (hoursRemaining < 12)
            {
                order.orderObservable.notifyObserver("Cancelled");
                order.setState(new CancelledState());
            }
            else if (hoursRemaining < 24)
            {
                order.orderObservable.notifyObserver("Message of provisional order");
                order.setState(new ProvisionalState(order));
            }
        }

        public void pay()
        {
            order.orderObservable.notifyObserver("Payed");
            order.setState(new PaidState());
        }

        public void submit()
        {
            throw new NotImplementedException();
        }
    }

    public class ProvisionalState : OrderState
    {
        private Order order;
        public ProvisionalState(Order order)
        {
            this.order = order;
        }

        public void cancel()
        {
            order.orderObservable.notifyObserver("Cancelled");
            order.setState(new CancelledState());
        }

        public void change(List<MovieTicket> movieTickets)
        {
            order.removeAllSeatReservation();
            foreach (var ticket in movieTickets)
            {
                order.addSeatReservation(ticket);
            }
        }

        public void checkDeadline()
        {
            var currentDate = DateTime.Now;
            var orderDate = order.orderDate();
            var hoursRemaining = (orderDate - currentDate).TotalHours;

            if (hoursRemaining < 12)
            {
                order.orderObservable.notifyObserver("Cancelled");
                order.setState(new CancelledState());
            }
            else if (hoursRemaining < 24)
            {
                order.orderObservable.notifyObserver("Message of provisional order");
                order.setState(new ProvisionalState(order));
            }
        }

        public void pay()
        {
            order.orderObservable.notifyObserver("Payed");
            order.setState(new PaidState());
        }

        public void submit()
        {
            throw new NotImplementedException();
        }
    }

    public class PaidState : OrderState
    {
        public void cancel()
        {
            throw new NotImplementedException();
        }

        public void change(List<MovieTicket> movieTickets)
        {
            throw new NotImplementedException();
        }

        public void checkDeadline()
        {
            throw new NotImplementedException();
        }

        public void pay()
        {
            throw new NotImplementedException();
        }

        public void submit()
        {
            throw new NotImplementedException();
        }
    }

    public class CreatedState : OrderState
    {
        public Order order;
        public CreatedState(Order order)
        {
            this.order = order;
        }

        public void cancel()
        {
            throw new NotImplementedException();
        }

        public void change(List<MovieTicket> movieTickets)
        {
            order.removeAllSeatReservation();
            foreach (var ticket in movieTickets)
            {
                order.addSeatReservation(ticket);
            }
        }

        public void checkDeadline()
        {
            throw new NotImplementedException();
        }


        public void pay()
        {
            throw new NotImplementedException();
        }

        public void submit()
        {
            order.orderObservable.notifyObserver("Submitted");
            order.setState(new SubmittedState(order));
        }
    }
}