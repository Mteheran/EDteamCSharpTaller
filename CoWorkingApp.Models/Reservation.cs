using System;
namespace CoWorking.App.Models
{
    public class Reservation
    {
        public Guid ReservationId {get;set;} = Guid.NewGuid();
        public DateTime ReservationDate {get;set;}
        public Guid DeskId {get;set;}
        public Guid UserId {get;set;}
    }
}