using System.ComponentModel.DataAnnotations;

namespace RentalBooking.Model
{
    public class Booking
    {
        public string BookingId { get; set; }
        public string ClientId { get; set; }
        public string CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string LocationPickup { get; set; }
        public string LocationDropoff { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentTransactionId { get; set; }
        public string VehicleCondition { get; set; }
        public string BookingSource { get; set; }
        public int? MileageLimit { get; set; }
        public string InsuranceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    } 

    public class BookingHistory
    {
        public string HistoryId { get; set; }
        public string BookingId { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
        public string Notes { get; set; }
    }

    public class BookingAdditionalService
    {
        public string ServiceId { get; set; }
        public string BookingId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
    }

    public class ChangeStatus
    {
        public string id { get; set; }
        public string status { get; set; }

    }

}
