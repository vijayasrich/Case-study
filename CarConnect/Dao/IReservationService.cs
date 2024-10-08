using CarConnect.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Dao
{
    public interface IReservationService
    {
        Reservation GetReservationById(int reservationId);
        List<Reservation> GetReservationsByCustomerId(int customerId);
        void CreateReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation);
        void CancelReservation(int reservationId);
        void DisplayReservations(List<Reservation>reservations);
    }
}