using SOFA3.Domain;

Customer customer = new Customer("John Doe", MessageMedium.EMAIL);

Order order = new Order(1, true, customer);
Movie movie = new Movie("batman");
MovieScreening screening = new MovieScreening(movie, DateTime.Now, 10);
MovieTicket ticket = new MovieTicket(screening, true, 1, 1);
MovieTicket ticket2 = new MovieTicket(screening, true, 1, 1);

order.addSeatReservation(ticket);
order.addSeatReservation(ticket2);

order.submitOrder();
order.payOrder();