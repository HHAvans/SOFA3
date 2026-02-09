using SOFA3.Domain;

Order order = new Order(1, true);
Movie movie = new Movie("batman");
MovieScreening screening = new MovieScreening(movie, DateTime.Now, 10);
MovieTicket ticket = new MovieTicket(screening, true, 1, 1);
MovieTicket ticket2 = new MovieTicket(screening, true, 1, 1);

order.addSeatReservation(ticket);
order.addSeatReservation(ticket2);

Console.WriteLine(order.calculatePrice());
order.setExportFormat(new JsonExportBehavior());
order.export();