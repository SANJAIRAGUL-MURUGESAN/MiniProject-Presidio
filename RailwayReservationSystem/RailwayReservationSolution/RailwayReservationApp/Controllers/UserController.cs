using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwayReservationApp.Exceptions.ReservationExceptions;
using RailwayReservationApp.Exceptions.RewardExceptions;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.UserExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using RailwayReservationApp.Models.UserDTOs;
using RailwayReservationApp.Services;
using System.Runtime.CompilerServices;

namespace RailwayReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        public UserController(IUserService userService)
        {
            _UserService = userService;
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



        [Route("SearchTrainByUser/Location")]
        [HttpPost]
        [ProducesResponseType(typeof(IList<Train>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<Train>>> Get([FromBody] SearchTrainDTO SearchTrainDTO)
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
    }
}
