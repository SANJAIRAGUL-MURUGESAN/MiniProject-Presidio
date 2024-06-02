using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.RewardExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.TrainClassExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Services;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RailwayReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IUserSecondaryService _UserSecondaryService;
        public UserController(IUserService userService, IUserSecondaryService userSecondaryService)
        {
            _UserService = userService;
            _UserSecondaryService = userSecondaryService;
        }

        [Route("UserRegistration")]
        [HttpPost]
        [ProducesResponseType(typeof(UserRegisterReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<UserRegisterReturnDTO>> Get([FromBody] UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var User = await _UserService.UserRegistration(userRegisterDTO);
                return Ok(User);
            }
            catch (UnableToRegisterException utre)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, utre.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [HttpPost("UserLogin")]
        [ProducesResponseType(typeof(UserLoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserLoginReturnDTO>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                var login = await _UserService.Login(userLoginDTO);
                return Ok(login);
            }
            catch (Exception ex)
            {
                //_logger.LogCritical("User Not Authenticated");
                return Unauthorized(new ErrorModel(401, ex.Message));
            }
        }


        [Authorize(Roles = "User")]
        [Route("SearchTrainByUser/Location")]
        [HttpPost]
        [ProducesResponseType(typeof(IList<TrainSearchResultDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<TrainSearchResultDTO>>> Get([FromBody] SearchTrainDTO SearchTrainDTO)
        {
            try
            {
                var Trains = await _UserService.SearchTrainByUser(SearchTrainDTO);
                return Ok(Trains);
            }
            catch (NoTrainsAvailableforyourSearchException ntafe)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, ntafe.Message));
            }
            catch(NoTrainsFoundException ntfe)
            {
                return NotFound(new ErrorModel(404, ntfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("BookTrainByUser")]
        [HttpPost]
        [ProducesResponseType(typeof(BookTrainReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<BookTrainReturnDTO>> Get([FromBody] BookTrainDTO BookTrainDTO)
        {
            try
            {
                var Trains = await _UserService.BookTrainByUser(BookTrainDTO);
                return Ok(Trains);
            }
            catch (SeatAlreadyReservedException sare)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, sare.Message));
            }
            catch (NoTrainsFoundException ntfe)
            {
                return NotFound(new ErrorModel(404, ntfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("ReservationPayment")]
        [HttpPost]
        [ProducesResponseType(typeof(AddPaymentReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddPaymentReturnDTO>> Get([FromBody] AddPaymentDTO addPaymentDTO)
        {
            try
            {
                var Trains = await _UserService.ConfirmPayment(addPaymentDTO);
                return Ok(Trains);
            }
            catch (NoSuchReservationFoundException nsrfe)
            {
                return NotFound(new ErrorModel(409, nsrfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("CancelReservation")]
        [HttpPost]
        [ProducesResponseType(typeof(CancelReservationReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<CancelReservationReturnDTO>> Get([FromBody] CancelReservationDTO cancelReservationDTO)
        {
            try
            {
                var ReservationCancel = await _UserService.CancelReservation(cancelReservationDTO);
                return Ok(ReservationCancel);
            }
            catch (NoSuchReservationFoundException nsrfe)
            {
                return NotFound(new ErrorModel(409, nsrfe.Message));
            }
            catch (NoSuchRewardFoundException nsrfe)
            {
                return NotFound(new ErrorModel(409, nsrfe.Message));
            }
            catch (NoSuchSeatFoundException nssfe)
            {
                return NotFound(new ErrorModel(409, nssfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("GetTrainClasses")]
        [HttpPost]
        [ProducesResponseType(typeof(IList<GetTrainClassReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<GetTrainClassReturnDTO>>> GetTrainClass([FromBody]  int TrainId)
        {
            try
            {
                var Classes = await _UserService.GetAllClassofTrain(TrainId);
                return Ok(Classes);
            }
            catch (NoTrainClassFoundException ntcfe)
            {
                return NotFound(new ErrorModel(409, ntcfe.Message));
            }
            catch (NoSuchTrainFoundException nstfe)
            {
                return NotFound(new ErrorModel(409, nstfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("UpcomingReservations")]
        [HttpPost]
        [ProducesResponseType(typeof(IList<UserBookedTrainsReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<UserBookedTrainsReturnDTO>>> GetBookedTrain([FromBody] int UserId)
        {
            try
            {
                var Trains = await _UserSecondaryService.GetBookedTrains(UserId);
                return Ok(Trains);
            }
            catch (NoTrainClassFoundException ntcfe)
            {
                return NotFound(new ErrorModel(409, ntcfe.Message));
            }
            catch (NoBookedTrainsAvailableException nbtfe)
            {
                return NotFound(new ErrorModel(409, nbtfe.Message));
            }
            catch (NoReservationsFoundException nrfe)
            {
                return NotFound(new ErrorModel(409, nrfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("PastBookings")]
        [HttpPost]
        [ProducesResponseType(typeof(IList<UserBookedTrainsReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<UserBookedTrainsReturnDTO>>> GetPastBookings([FromBody] int UserId)
        {
            try
            {
                var Trains = await _UserSecondaryService.GetPastBookings(UserId);
                return Ok(Trains);
            }
            catch (NoTrainClassFoundException ntcfe)
            {
                return NotFound(new ErrorModel(409, ntcfe.Message));
            }
            catch (NoBookedTrainsAvailableException nbtfe)
            {
                return NotFound(new ErrorModel(409, nbtfe.Message));
            }
            catch (NoReservationsFoundException nrfe)
            {
                return NotFound(new ErrorModel(409, nrfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("CheckSeatDetailsofTrain")]
        [HttpPost]
        [ProducesResponseType(typeof(CheckSeatDetailsReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<CheckSeatDetailsReturnDTO>> Get([FromBody] int TrainId)
        {
            try
            {
                var seat = await _UserSecondaryService.CheckSeatsDetailsbyAdmin(TrainId);
                return Ok(seat);
            }
            catch (NoSuchTrainFoundException nstfe)
            {
                Console.Write("Station Error");
                return NotFound(new ErrorModel(404, nstfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("UpdateUser")]
        [HttpPut]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<Users>> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
        {
            try
            {
                var User = await _UserSecondaryService.UpdateUser(updateUserDTO);
                return Ok(User);
            }
            catch (NoSuchUserFoundException nsufe)
            {
                return NotFound(new ErrorModel(409, nsufe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "User")]
        [Route("DeleteUser")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> DeleteUser([FromBody] int UserId)
        {
            try
            {
                var User = await _UserSecondaryService.DeleteUser(UserId);
                return Ok(User);
            }
            catch (NoSuchUserFoundException nsufe)
            {
                return NotFound(new ErrorModel(409, nsufe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }
    }
}
