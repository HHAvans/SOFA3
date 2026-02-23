namespace SOFA3.Domain
{
    public class Customer
    {
        private String name { get; set; }
        public MessageMedium messageMedium { get; set; }

        public Customer(String name, MessageMedium messageMedium)
        {
            this.name = name;
            this.messageMedium = messageMedium;
        }
    }

    public enum MessageMedium
    {
        EMAIL,
        SMS,
        WHATSAPP
    }
}