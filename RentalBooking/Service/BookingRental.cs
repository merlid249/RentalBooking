using Microsoft.Data.SqlClient;
using RentalBooking.Model;
using RentalBooking.Tools;
using System.Collections.Generic;
using System.Data;

namespace RentalBooking.Service
{
    public class BookingRental
    {
        internal static async Task<List<Booking>> GetRentalBookingsAsync()
        {
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(Connection.RentalBookingConnection))
            {
                await conn.OpenAsync();
                string response = "SELECT * FROM RentalBooking.dbo.Bookings";

                using (SqlCommand cmd = new SqlCommand(response, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Map the data from the reader to a Booking object
                            Booking bookCar = new Booking
                            {
                            };
                            try
                            {
                                bookCar.BookingId = reader["booking_id"].ToString();
                                bookCar.ClientId = reader["client_id"].ToString();
                                bookCar.CarId = reader["car_id"].ToString();
                                bookCar.StartDate = DateTime.Parse(reader["start_date"].ToString());
                                bookCar.EndDate = DateTime.Parse(reader["end_date"].ToString());
                                bookCar.Status = reader["status"].ToString();
                                bookCar.LocationPickup = reader["location_pickup"].ToString();
                                bookCar.LocationDropoff = reader["location_dropoff"].ToString();
                                bookCar.Price = Decimal.Parse(reader["price"].ToString());
                                bookCar.PaymentMethod = reader["payment_method"].ToString();
                                bookCar.PaymentStatus = reader["payment_status"].ToString();
                                bookCar.PaymentTransactionId = reader["payment_transaction_id"] as string;
                                bookCar.VehicleCondition = reader["vehicle_condition"] as string;
                                bookCar.BookingSource = reader["booking_source"] as string;
                                bookCar.MileageLimit = reader["mileage_limit"] == DBNull.Value ? (int?)null : Int32.Parse(reader["mileage_limit"].ToString());
                                bookCar.InsuranceType = reader["insurance_type"] as string;
                                bookCar.CreatedAt = DateTime.Parse(reader["created_at"].ToString());
                                bookCar.UpdatedAt = DateTime.Parse(reader["updated_at"].ToString());
                            }
                            catch (FormatException fe)
                            {
                                // Log or handle the format exception as needed
                                Console.WriteLine(fe.Message);
                                continue; // Skip this record and continue with the next one
                            }
                            bookings.Add(bookCar);
                        }
                    }
                }
            }
            return bookings;
        }

        internal static async Task<Booking> getBookingById(string id)
        {

            Booking booked = new Booking();
            using (SqlConnection conn = new SqlConnection(Connection.RentalBookingConnection))
            {
                await conn.OpenAsync();
                string response = "SELECT * FROM RentalBooking.dbo.Bookings WHERE " +
                    " Bookings.booking_id ='" + id.Trim() + "' ";

                using (SqlCommand cmd = new SqlCommand(response, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            // Map the data from the reader to a Booking object
                            
                            try
                            {
                                booked.BookingId = reader["booking_id"].ToString();
                                booked.ClientId = reader["client_id"].ToString();
                                booked.CarId = reader["car_id"].ToString();
                                booked.StartDate = DateTime.Parse(reader["start_date"].ToString());
                                booked.EndDate = DateTime.Parse(reader["end_date"].ToString());
                                booked.Status = reader["status"].ToString();
                                booked.LocationPickup = reader["location_pickup"].ToString();
                                booked.LocationDropoff = reader["location_dropoff"].ToString();
                                booked.Price = Decimal.Parse(reader["price"].ToString());
                                booked.PaymentMethod = reader["payment_method"].ToString();
                                booked.PaymentStatus = reader["payment_status"].ToString();
                                booked.PaymentTransactionId = reader["payment_transaction_id"] as string;
                                booked.VehicleCondition = reader["vehicle_condition"] as string;
                                booked.BookingSource = reader["booking_source"] as string;
                                booked.MileageLimit = reader["mileage_limit"] == DBNull.Value ? (int?)null : Int32.Parse(reader["mileage_limit"].ToString());
                                booked.InsuranceType = reader["insurance_type"] as string;
                                booked.CreatedAt = DateTime.Parse(reader["created_at"].ToString());
                                booked.UpdatedAt = DateTime.Parse(reader["updated_at"].ToString());
                            }
                            catch (FormatException fe)
                            {
                                // Log or handle the format exception as needed
                                Console.WriteLine(fe.Message);
                                continue; // Skip this record and continue with the next one
                            }
                        }
                    }
                }
            }
            return booked;
        }
    }
}

