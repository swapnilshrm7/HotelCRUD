using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesApp.Models;
namespace MoviesApp.Controllers
{
    public class HotelController : ApiController
    {
        private static int count = 3;
        private static List<Hotel> Hotels = new List<Hotel>()
        {
            new Hotel {id=1,name="Hyatt",Rooms=140,address="pune",PinCode=441001},
            new Hotel {id=2,name="Novotel",Rooms=180,address="mumbai",PinCode=441010},
            new Hotel {id=3,name="IBIS",Rooms=170,address="delhi",PinCode=400100}
        };
        public ApiResponse GetAllHotels()
        {
            try
            {
                return new ApiResponse()
                {
                    hotels = Hotels,
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Success,
                        ErrorMessage = "data fetched successfully",
                        StatusCode = 200
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse()
                {
                    hotels = null,
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failure,
                        StatusCode = 500,
                        ErrorMessage = "Internal Server Error"
                    }
                };
            }
        }
        public ApiResponse GetHotel(int id)
        {
            List<Hotel> Hotel1 = new List<Hotel>();
            var Hotel2 = Hotels.Find((p) => p.id == id);
            Hotel1.Add(Hotel2);
            if(Hotel2== null)
                {
                return new ApiResponse()
                    {
                    hotels = null,
                    Status = new Status()
                        {
                        ApiStatus = ApiStatus.Failure,
                        StatusCode = 404,
                        ErrorMessage = "Not Found"
                        }
                    };
                }
            else
                {
                return new ApiResponse()
                    {
                    hotels = Hotel1,
                    Status = new Status()
                        {
                        ApiStatus = ApiStatus.Success,
                        StatusCode = 500,
                        ErrorMessage = "Hotel found successfully"
                        }
                    };
                }
        }
        [HttpPost]
        public ApiResponse AddHotel(Hotel HotelToBeAdded)
        {
            try
            {
                if (HotelToBeAdded != null)
                {
                    HotelToBeAdded.id = ++count;
                    Hotels.Add(HotelToBeAdded);
                    return new ApiResponse()
                    {
                        hotels = Hotels,
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Success,
                            ErrorMessage = "Hotel has been added successfully!!",
                            StatusCode = 201        //created
                        }
                    };
                }
                throw new Exception(" ");
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    hotels = null,
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failure,
                        ErrorMessage = "Hotel to be added cannot be null",
                        StatusCode = 406        //not acceptable
                    }
                };
            }
        }
        [HttpDelete]
        public ApiResponse DeleteHotel(int id)
        {
            try
            {
                var HotelToBeRemoved = Hotels.Find(x => x.id == id);
                if (HotelToBeRemoved != null)
                {
                    Hotels.Remove(HotelToBeRemoved);
                    return new ApiResponse()
                    {
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Success,
                            ErrorMessage = "Hotel removed successfully",
                            StatusCode = 200
                        }
                    };
                }
                else
                {
                    throw new Exception(" ");
                }
            }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failure,
                        ErrorMessage = "Hotel not Found",
                        StatusCode = 404
                    }
                };
            }
        }
        [HttpPut]
        //routeTemplate: "api/{controller}/{id}";
        public ApiResponse UpdateHotel(int id,[FromBody] int number)
        {
            try
            {
                var HotelToBeUpdated = Hotels.Find(x => x.id == id);
                if (HotelToBeUpdated != null)
                {
                    if(Hotels.Find(x => x.id == id).Rooms - number>=0)
                    HotelToBeUpdated.Rooms = Hotels.Find(x => x.id == id).Rooms - number;
                    else
                    {
                        throw new EntryPointNotFoundException(" ");
                    }
                    return new ApiResponse()
                    {
                        Status = new Status()
                        {
                            ApiStatus = ApiStatus.Success,
                            ErrorMessage = "Data updated successfully",
                            StatusCode = 200
                        }
                    };
                }
                else
                {
                    throw new Exception(" ");
                }
            }
            catch(EntryPointNotFoundException e1)
                {
                return new ApiResponse()
                {
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failure,
                        ErrorMessage = "Not enough rooms available!",
                        StatusCode = 0
                    }
                };
                }
            catch (Exception e)
            {
                return new ApiResponse()
                {
                    Status = new Status()
                    {
                        ApiStatus = ApiStatus.Failure,
                        ErrorMessage = "Hotel not Found",
                        StatusCode = 404
                    }
                };
            }
        }
    }
}
