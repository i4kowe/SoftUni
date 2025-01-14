function tickets(ticketArray, sortingCriteria) {

    class Ticket {
        constructor(destination, price, status) {
            this.destination = destination;
            this.price = price;
            this.status = status;
        }
    }

    let unSortedTickets = [];

    for (let ticket of ticketArray) {
        
        let[destination, price, status] = ticket.split('|');
        price = +price;

        unSortedTickets.push(new Ticket(destination, price, status));
    };

    let sortedTickets = {};

    switch(sortingCriteria) {
        case "destination":
             sortedTickets = unSortedTickets.sort((a, b) => {
            return a.destination.localeCompare(b.destination);
            });
        break;
        case "price":
             sortedTickets = unSortedTickets.sort((a, b) => {
                return a.price - b.price;
            });
        break;
        case "status":
             sortedTickets = unSortedTickets.sort((a, b) => {
                return a.status.localeCompare(b.status);
            });
        break;
        }


    return sortedTickets
};

console.log(tickets(['Philadelphia|94.20|available',
                    'New York City|95.99|available',
                    'New York City|95.99|sold',
                    'Boston|126.20|departed'],
            'destination'
));