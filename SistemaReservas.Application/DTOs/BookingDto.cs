using SistemaReservas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaReservas.Application.DTOs
{
    public class BookingDto
    {
        public BookingDto(
            Guid id,
            DateTime checkInDate, 
            DateTime checkOutDate, 
            decimal totalPrice, 
            string propertyTitle, 
            string guestName,
            BookingStatus status)
        {
            Id = id;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            TotalPrice = totalPrice;
            PropertyTitle = propertyTitle;
            GuestName = guestName;
            Status = status;
        }
        public Guid Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string PropertyTitle { get; set; }
        public string GuestName { get; set; }
        public BookingStatus Status { get; set; }
    }
}
