using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsumeRest.Consumers;
using ModelLibrary;

namespace ConsumeRest
{
    public static class Worker
    {
        public static async Task Run()
        {
            Consumer<Hotel> hotelConsumer = new Consumer<Hotel>("http://localhost:56897/api/Hotels");
            Consumer<Booking> bookingConsumer = new Consumer<Booking>("http://localhost:56897/api/Bookings");
            Consumer<Guest> guestConsumer = new Consumer<Guest>("http://localhost:56897/api/Guests");
            Consumer<Room> roomConsumer = new Consumer<Room>("http://localhost:56897/api/Rooms");

            Console.WriteLine("\nCalling api/Hotels...\n");

            #region CallingApiHotels
            List<Hotel> hotels = await hotelConsumer.GetAsync();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine(hotel.ToString());
            }

            Hotel oneHotel = await hotelConsumer.GetOneAsync(new[] { 3 });
            Console.WriteLine(oneHotel.ToString());

            bool ok3 = await hotelConsumer.DeleteAsync(new[] { 45 });
            Console.WriteLine("Deleted hotel 45: " + ok3);

            Hotel myHotel = new Hotel(45, "MagHotel", "MagStreet");
            bool ok = await hotelConsumer.PostAsync(myHotel);
            Console.WriteLine("Post Hotel 45 success: " + ok);

            Hotel myOtherHotel = new Hotel(45, "PoulHotel", "PoulStreet");
            bool ok2 = await hotelConsumer.PutAsync(new[] { 45 }, myOtherHotel);
            Console.WriteLine("Hotel 45 updated: " + ok2);
            #endregion

            Console.WriteLine("\nCalling api/Bookings...\n");

            #region CallingApiBookings
            List<Booking> bookings = await bookingConsumer.GetAsync();
            foreach (Booking booking in bookings)
            {
                Console.WriteLine(booking.ToString());
            }

            Booking oneBooking = await bookingConsumer.GetOneAsync(new[] { 42 });
            Console.WriteLine(oneBooking.ToString());


            DateTime date = new DateTime(2011, 2, 10);
            Booking myBooking = new Booking(0, new Guest(1, "Magnus", "SomeRoad"), 7, date, date.AddDays(1), new Room(1, 7, "S", 666.00));
            ok = await bookingConsumer.PostAsync(myBooking);
            Console.WriteLine("Post Booking success: " + ok);

            Booking myOtherBooking = new Booking(0, new Guest(4, "MagMan", "DataBaseRoad"), 7, date, date.AddDays(1), new Room(2, 7, "D", 666.666));
            ok2 = await bookingConsumer.PutAsync(new[] { bookings[bookings.Count - 1].BookingId }, myOtherBooking);
            Console.WriteLine("Booking updated: " + ok2);

            ok3 = await bookingConsumer.DeleteAsync(new[] { bookings[bookings.Count - 1].BookingId });
            Console.WriteLine("Deleted booking: " + ok3); 
            #endregion

            Console.WriteLine("\nCalling api/Guests...\n");

            #region CallingApiGuests
            List<Guest> guests = await guestConsumer.GetAsync();
            foreach (Guest guest in guests)
            {
                Console.WriteLine(guest.ToString());
            }

            Guest oneGuest = await guestConsumer.GetOneAsync(new[] { 1 });
            Console.WriteLine(oneGuest.ToString());

            ok3 = await guestConsumer.DeleteAsync(new[] { 45 });
            Console.WriteLine("Guest 45 deleted: " + ok3);

            Guest myGuest = new Guest(45, "OG MAG", "OGSTREET");
            ok = await guestConsumer.PostAsync(myGuest);
            Console.WriteLine("Post Guest 45 success: " + ok);

            Guest myOtherGuest = new Guest(45, "Charlotte", "Charlotte Street");
            ok2 = await guestConsumer.PutAsync(new[] { 45 }, myOtherGuest);
            Console.WriteLine("Guest 45 updated: " + ok2); 
            #endregion

            Console.WriteLine("\nCalling api/Rooms...\n");

            #region CallingApiRooms
            List<Room> rooms = await roomConsumer.GetAsync();
            foreach (Room room in rooms)
            {
                Console.WriteLine(room.ToString());
            }

            Room oneRoom = await roomConsumer.GetOneAsync(new[] { 1, 1 });
            Console.WriteLine(oneRoom.ToString());

            ok3 = await roomConsumer.DeleteAsync(new[] { 42, 7 });
            Console.WriteLine("Room 42,7 is deleted: " + ok3);

            Room myRoom = new Room(42, 7, "S", 1234.56789);
            ok = await roomConsumer.PostAsync(myRoom);
            Console.WriteLine("Post Room 42,7 success: " + ok);

            Room myOtherRoom = new Room(42, 7, "D", 9876.54321);
            ok2 = await roomConsumer.PutAsync(new[] { 42, 7 }, myOtherRoom);
            Console.WriteLine("Room 42,7 updated: " + ok2); 
            #endregion
        }

        public static async Task Ping()
        {
            await ConsumeHotel.GetOneHotelAsync(1);
        }
    }
}
