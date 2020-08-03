using System;
using CoWorking.App.Models;
using System.Linq;
using System.Collections.Generic;

namespace CoWorkingApp.Data
{
    public class ReservationData
    {
        private JsonManager<Reservation> jsonManager;

        public ReservationData()
        {
            jsonManager = new JsonManager<Reservation>();
        }

        public bool CreateReservation(Reservation newReservation)
        {
            var reservationCollection = jsonManager.GetCollection();

            reservationCollection.Add(newReservation);

            jsonManager.SaveCollection(reservationCollection);

            return true;
        }

        public bool CancelReservation(Guid reservationId)
        {
            var reservationCollection = jsonManager.GetCollection();

            var indexReservation = reservationCollection.FindIndex(p => p.ReservationId == reservationId);

            reservationCollection.RemoveAt(indexReservation);

            jsonManager.SaveCollection(reservationCollection);

            return true;
        }

        public IEnumerable<Reservation> GetReservationsByUser(Guid userId)
        {
            var reservationCollection = jsonManager.GetCollection();

            return reservationCollection.Where(p => p.UserId == userId && p.ReservationDate > DateTime.Now);
        }

        public IEnumerable<Reservation> GetReservationsHistoryByUser(Guid userId)
        {
            var reservationCollection = jsonManager.GetCollection();

            return reservationCollection.Where(p => p.UserId == userId);
        }
    }
}