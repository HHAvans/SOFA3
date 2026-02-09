using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SOFA3.Domain
{
    public interface ExportBehavior
    {
        public void export(Order order);
    }

    public class JsonExportBehavior : ExportBehavior
    {
        public void export(Order order)
        {
            string json = JsonSerializer.Serialize(order, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(@"C:\Users\homer\Downloads\movie.json", json);
            return;
        }
    }

    public class TextExportBehavior : ExportBehavior
    {
        public void export(Order order)
        {
            StringBuilder sb = new StringBuilder($"Export of {order.getOrderNr()}", 1000);
            sb.AppendLine();
            sb.AppendLine();

            foreach (var ticket in order.movieTickets)
            {
                sb.AppendLine(ticket.toString());
            }

            File.WriteAllText(@"C:\Users\homer\Downloads\movie.txt", sb.ToString());
        }
    }
}
