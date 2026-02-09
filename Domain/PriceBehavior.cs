using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public interface PriceBehavior
    {
        public double extraPrice(MovieTicket ticket);
    }

    public class StudentPreiumPriceBehavior : PriceBehavior
    {
        public double extraPrice(MovieTicket ticket)
        {
            return ticket.isPremiumTicket() ? 2.0 : 0.0;
        }
    }

    public class RegularPriceBehavior : PriceBehavior
    {
        public double extraPrice(MovieTicket ticket)
        {
            return ticket.isPremiumTicket() ? 3.0 : 0.0;
        }
    }
}