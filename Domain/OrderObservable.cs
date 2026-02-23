using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public class OrderObservable
    {
        private Order order;
        private OrderObserver orderObserver;

        public OrderObservable(Order order)
        {
            this.order = order;

            var customerMessageMedium = order.customer.messageMedium;
            if (customerMessageMedium == MessageMedium.WHATSAPP)
            {
                this.orderObserver = new WhatsappNotification();
            }
            else if (customerMessageMedium == MessageMedium.SMS)
            {
                this.orderObserver = new SmsNotification();
            }
            else
            {
                this.orderObserver = new EmailNotification();
            }
        }

        public void changeObserver(Order order)
        {
            this.order = order;
        }

        public void notifyObserver(String message)
        {
            orderObserver.sendMessage(message);
        }
    }

    public interface OrderObserver
    {
        public void sendMessage(String message);
    }

    public class EmailNotification : OrderObserver
    {
        public void sendMessage(String message)
        {
            Console.WriteLine("Email notification: " + message);
        }
    }

    public class SmsNotification : OrderObserver
    {
        public void sendMessage(String message)
        {
            Console.WriteLine("SMS notification: " + message);
        }
    }

    public class WhatsappNotification : OrderObserver
    {
        public void sendMessage(String message)
        {
            Console.WriteLine("Whatsapp notification: " + message);
        }
    }
}
