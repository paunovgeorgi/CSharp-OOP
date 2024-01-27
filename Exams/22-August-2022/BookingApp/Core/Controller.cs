using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Core.Contracts;
using BookingApp.Models;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IHotel> hotels = new HotelRepository();
        public string AddHotel(string hotelName, int category)
        {
            if (hotels.All().Any(h=>h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }
                IHotel hotel = new Hotel(hotelName, category);
                hotels.AddNew(hotel);
                return String.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);

            if (hotel.Rooms.All().Any(r=>r.GetType().Name == roomTypeName))
            {
                return String.Format(OutputMessages.RoomTypeAlreadyCreated);
            }

            if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            IRoom room = null;

            if (roomTypeName == nameof(Apartment))
            {
                room = new Apartment();
            }
            else if (roomTypeName == nameof(DoubleBed))
            {
                room = new DoubleBed();
            }
            else
            {
                room = new Studio();
            }

            hotel.Rooms.AddNew(room);

            return String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);

            if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            if (!hotel.Rooms.All().Any(r=>r.GetType().Name == roomTypeName))
            {
                return String.Format(OutputMessages.RoomTypeNotCreated);
            }

            IRoom room = hotel.Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName);

            if (room.PricePerNight > 0)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotResetInitialPrice);
            }

            room.SetPrice(price);

            return String.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(h=>h.Category == category))
            {
                return String.Format(OutputMessages.CategoryInvalid, category);
            }

            bool isFound = false;

            List<IHotel> currentHotels = hotels.All().Where(h => h.Category == category).ToList();
            IHotel hotelToBook = null;
            IRoom roomToBook = null;
            foreach (var hotel in currentHotels.OrderBy(h=>h.FullName))
            {
                List<IRoom> currentRooms = hotel.Rooms.All().Where(r => r.PricePerNight > 0).ToList();
                foreach (IRoom room in currentRooms.OrderBy(r=>r.BedCapacity))
                {
                    if (room.BedCapacity >= adults + children)
                    {
                        roomToBook = room;
                        hotelToBook = hotel;
                        isFound = true;
                        break;
                    }
                }
                if (isFound)
                {
                    break;
                }
            }

            if (roomToBook == null)
            {
                return String.Format(OutputMessages.RoomNotAppropriate);
            }

            int bookingNumber = hotelToBook.Bookings.All().Count + 1;
            IBooking booking = new Booking(roomToBook, duration, adults, children,bookingNumber);
            hotelToBook.Bookings.AddNew(booking);
            return String.Format(OutputMessages.BookingSuccessful, bookingNumber, hotelToBook.FullName);
        }

        public string HotelReport(string hotelName)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {hotelName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine("--Bookings:");
            sb.AppendLine();
            if (!hotel.Bookings.All().Any())
            {
                sb.AppendLine("none");
            }
            else
            { 
                foreach (IBooking booking in hotel.Bookings.All())
                {
                   sb.AppendLine(booking.BookingSummary());
                   sb.AppendLine();
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
