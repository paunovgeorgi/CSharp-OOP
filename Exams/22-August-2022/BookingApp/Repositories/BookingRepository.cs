using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {

        private readonly List<IBooking> bookings = new List<IBooking>();
        public void AddNew(IBooking model)
        {
            bookings.Add(model);
        }

        public IBooking Select(string criteria)
        {
            return bookings.FirstOrDefault(b => b.BookingNumber == int.Parse(criteria));
        }

        public IReadOnlyCollection<IBooking> All() => bookings.AsReadOnly();

    }
}
