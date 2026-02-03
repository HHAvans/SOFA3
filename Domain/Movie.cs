using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public class Movie
    {
        private String title { get; set; }
        private List<MovieScreening> screeningList = new List<MovieScreening>();

        public Movie(String title)
        {
            this.title = title;
        }

        public void addScreening(MovieScreening screening)
        {
            this.screeningList.Add(screening);
        }

        public string toString()
        {
            return this.title;
        }
    }
}
