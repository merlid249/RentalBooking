using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using RentalBooking.Model;
using RentalBooking.Service;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RentalBooking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "CustomBearer")]
    public class BookCarController : ControllerBase
    {


        public void bookCar()
        {

        }
        public async  Task<String>  cancelBooking()
        {
            return "";
        }
        public void changeBooking()
        {

        }



        [HttpGet("getBookedById")]
        public async Task<Booking> getBookedByIdAsync(string id)
        {
            Booking booked=new Booking();

            try
            {
                booked = await BookingRental.getBookingById(id);
                if (booked.BookingId != null )
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;

                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.NoContent;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            return booked;
        }

        [HttpGet("getListBookings")]
        public async Task<List<Booking>> GetListBookedAsync()
        {
            List<Booking> bookedCars = new List<Booking>();
            try
            {
                bookedCars = await BookingRental.GetRentalBookingsAsync();
                if (bookedCars.Count > 0)
                {
                    Response.StatusCode = (int)HttpStatusCode.OK;

                }
                else
                {
                    Response.StatusCode = (int)HttpStatusCode.NoContent;

                }

            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                // For example, you might want to log the error message:
                Console.WriteLine(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

            }
            return bookedCars;
        }
    }
}
