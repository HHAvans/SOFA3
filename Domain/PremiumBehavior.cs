using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public interface PremiumBehavior
    {
        double GetPremiumExtra();
    }

    public class StudentPremiumPriceBehavior : PremiumBehavior
    {
        public double GetPremiumExtra()
        {
            return 2.0;
        }
    }

    public class RegularPriceBehavior : PremiumBehavior
    {
        public double GetPremiumExtra()
        {
            return 3.0;
        }
    }
}