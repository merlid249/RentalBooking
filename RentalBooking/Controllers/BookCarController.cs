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

        [HttpPost("bookCars")]
        public async Task<ActionResult<List<string>>> BookCarsAsync([FromBody] List<Booking> bookings)
        {
            var messages = new List<string>();

            try
            {
                // Start tasks to book each car
                var tasks = bookings.Select(async booking =>
                {
                    var response = await BookingRental.bookCar(booking);
                    if (response)
                    {
                        return "Booking for car ID " + booking.CarId + " is submitted!";
                    }
                    else
                    {
                        return "Submit failed for car ID " + booking.CarId + ". Something went wrong!";
                    }
                }).ToList();

                // Wait for all tasks to complete
                messages = (await Task.WhenAll(tasks)).ToList();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("cancelBookings")]
        public async Task<ActionResult<List<string>>> CancelBookingsAsync([FromBody] List<ChangeStatus> changeStatuses)
        {
            var messages = new List<string>();

            try
            {
                // Start tasks to cancel each booking
                var tasks = changeStatuses.Select(async changeStatus =>
                {
                    var response = await BookingRental.cancelBooking(changeStatus);
                    if (response)
                    {
                        return "Booking with ID " + changeStatus.id + " has been canceled";
                    }
                    else
                    {
                        return "Action failed for booking ID " + changeStatus.id + ". The booking is not found or the status is canceled already!";
                    }
                }).ToList();

                // Wait for all tasks to complete
                messages = (await Task.WhenAll(tasks)).ToList();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("updateBookings")]
        public async Task<ActionResult<List<string>>> UpdateBookingsAsync([FromBody] List<Booking> bookings)
        {
            var messages = new List<string>();

            try
            {
                // Start tasks to update each booking
                var tasks = bookings.Select(async booking =>
                {
                    var response = await BookingRental.updateBookingById(booking);
                    if (response)
                    {
                        return "Booking with ID " + booking.BookingId + " is updated";
                    }
                    else
                    {
                        return "Update fail for booking ID " + booking.BookingId;
                    }
                }).ToList();

                // Wait for all tasks to complete
                messages = (await Task.WhenAll(tasks)).ToList();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("getBookedById")]
        public async Task<Booking> getBookedByIdAsync(string id)
        {
            Booking booked = new Booking();

            try
            {
                booked = await BookingRental.getBookingById(id);
                if (booked.BookingId != null)
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
        public async Task<ActionResult<List<Booking>>> GetListBookedAsync2()
        {
            try
            {
                // Simulate multiple concurrent database operations
                var task1 = BookingRental.GetRentalBookingsAsync();
                var task2 = BookingRental.GetRentalBookingsAsync();
                var task3 = BookingRental.GetRentalBookingsAsync();

                // Wait for all tasks to complete
                var results = await Task.WhenAll(task1, task2, task3);

                // Combine the results from all tasks
                var bookedCars = new List<Booking>();
                foreach (var result in results)
                {
                    bookedCars.AddRange(result);
                }

                if (bookedCars.Count > 0)
                {
                    return Ok(bookedCars);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


        #region WithoutMultithreading 
        /*  [HttpGet("getBookedById")]
          public async Task<Booking> getBookedByIdAsync(string id)
          {
              Booking booked = new Booking();

              try
              {
                  booked = await BookingRental.getBookingById(id);
                  if (booked.BookingId != null)
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
          }*/

        /*  [HttpGet("getListBookings")]
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

          */

        /* [HttpPost("update")]
 public async Task<String> updateBooking( [FromBody] Booking booking)
 {
     Boolean response = false;
     String message = "";
     try
     {
         response = await BookingRental.updateBookingById( booking);
         if (response)
         {
             Response.StatusCode = (int)HttpStatusCode.OK;
             message = "Your booking is updated";
         }
         else
         {
             Response.StatusCode = (int)HttpStatusCode.NotFound;
             message = "Update fail,booking id does not found!";

         }
     }
     catch (Exception ex)
     {
         Console.WriteLine(ex.Message);
         Response.StatusCode = (int)HttpStatusCode.InternalServerError;
         message = "Somthing gone wrong";

     }
     return message;
 }*/

        /* [HttpPost("cancel")]
 public async Task<String> cancelBooking([FromBody] ChangeStatus changeStatus)
 {
     Boolean response = false;
     String message = "";
     try
     {
     response= await BookingRental.cancelBooking(changeStatus);
         if (response)
         {
             Response.StatusCode = (int)HttpStatusCode.OK;
             message = "Your booking has been cancel";
         }
         else
         {
             Response.StatusCode = (int)HttpStatusCode.NotFound;
             message = "Action faild, the booking is not found or the staus is cancelled already! ";

         }
     }
     catch (Exception ex) 
     {
         Console.WriteLine(ex.Message);
         Response.StatusCode = (int)HttpStatusCode.InternalServerError;
         message = "Somthing gone wrong";
     }

     return message;
 }
 */

        /* [HttpPost("bookCar")]
 public async Task<String> bookCar([FromBody] Booking booking)
 {
     Boolean response = false;
     String message = "";
     try
     {
         response = await BookingRental.bookCar(booking);
         if (response)
         {
             Response.StatusCode = (int)HttpStatusCode.OK;
             message = "Your booking  is sumbited!";
         }
         else
         {
             Response.StatusCode = (int)HttpStatusCode.NotFound;
             message = "Submit fail,somthing gone wrong!";

         }
     }
     catch (Exception ex)
     {
         Console.WriteLine(ex.Message);
         Response.StatusCode = (int)HttpStatusCode.InternalServerError;
         message = "Somthing gone wrong";

     }
     return message;
 }*/


        #endregion

    }
}
