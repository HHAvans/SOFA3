using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public interface DiscountBehavior
    {
        List<MovieTicket> getTicketsToCharge(List<MovieTicket> tickets);
        double applyAdditionalDiscount(List<MovieTicket> tickets, double currentTotal);
    }
    public class StudentDiscountBehavior : DiscountBehavior
    {
        public List<MovieTicket> getTicketsToCharge(List<MovieTicket> tickets)
        {
            var result = new List<MovieTicket>();
            for(int i = 0; i < tickets.Count; i += 2)
            {
                //only charge every other ticket
                result.Add(tickets[i]);
            }
            return result;
        }

        public double applyAdditionalDiscount(List<MovieTicket> tickets, double currentTotal)
        {
            return currentTotal;
        }
    }
    public class RegularDiscountBehavior : DiscountBehavior
    {
        public List<MovieTicket> getTicketsToCharge(List<MovieTicket> tickets)
        {
            var day = tickets.First().movieScreening.dateAndTime.DayOfWeek;
            bool isWeekend = (day == DayOfWeek.Friday || day == DayOfWeek.Saturday || day == DayOfWeek.Sunday);
            if (!isWeekend)
            {
                //only charge every other ticket on non-weekend days
                var result = new List<MovieTicket>();
                for (int i = 0; i < tickets.Count; i += 2)
                {
                    result.Add(tickets[i]);
                }
                return result;

            }
            return tickets;
        }

        public double applyAdditionalDiscount(List<MovieTicket> tickets, double currentTotal)
        {
            if(tickets.Count >= 6)
            {
                return currentTotal * 0.9; //10% discount for orders of 6 or more tickets
            };
            return currentTotal;
        }
    }
}
