using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.StationExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Exceptions.TrainRoutesExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;
using System.Diagnostics.CodeAnalysis;

namespace RailwayReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _AdminService;
        public AdminController(IAdminService adminService)
        {
            _AdminService = adminService;
        }


        [Route("AdminRegister")]
        [HttpPost]
        [ProducesResponseType(typeof(AdminRegisterReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AdminRegisterReturnDTO>> AdminRegister([FromBody] AdminRegisterDTO adminRegisterDTO)
        {
            try
            {
                var train = await _AdminService.AdminRegistration(adminRegisterDTO);
                return Ok(train);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Route("AdminLogin")]
        [HttpPost]
        [ProducesResponseType(typeof(AdminLoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AdminLoginReturnDTO>> AdminLogin([FromBody] AdminLoginDTO adminLoginDTO)
        {
            try
            {
                var admin = await _AdminService.AdminLogin(adminLoginDTO);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddTrainByAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTrainReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddTrainReturnDTO>> AddTrain([FromBody] AddTrainDTO addTrainDTO)
        {
            try
            {
                var train = await _AdminService.AddTrainbyAdmin(addTrainDTO);
                return Ok(train);
            }
            catch (TrainAlreadyAllotedException taae)
            {
                return StatusCode(StatusCodes.Status409Conflict,new ErrorModel(409, taae.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddTrainRouteByAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTrainRouteReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddTrainRouteReturnDTO>> AddTrainRoute([FromBody] AddTrainRouteDTO addTrainRouteDTO)
        {
            try
            {
                var trainRoute = await _AdminService.AddTrainRoutebyAdmin(addTrainRouteDTO);
                return Ok(trainRoute);
            }
            catch (RequiredTrackBusyException rtbe)
            {
                return StatusCode(StatusCodes.Status409Conflict,new ErrorModel(409, rtbe.Message));
            }
            catch (NoTrainTracksFoundException nttfe)
            {
                return NotFound(new ErrorModel(404, nttfe.Message));
            }
            catch (NoSuchStationFoundException nssfe)
            {
                return NotFound(new ErrorModel(404, nssfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddTrainClassByAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTrainClassReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddTrainClassReturnDTO>> Get([FromBody] AddTrainClassDTO addTrainClassDTO)
        {
            try
            {
                var trainClass = await _AdminService.AddTrainClassbyAdmin(addTrainClassDTO);
                return Ok(trainClass);
            }
            catch (InvalidSeatAllocationException isae)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, isae.Message));
            }
            catch (NoSuchStationFoundException nssfe)
            {
                return NotFound(new ErrorModel(404, nssfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddStationByAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(AddStationReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddStationReturnDTO>> Get([FromBody] AddStationDTO addStationDTO)
        {
            try
            {
                var station = await _AdminService.AddStationbyAdmin(addStationDTO);
                return Ok(station);
            }
            catch (StationAlreadyAddedException saae)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, saae.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("AddTrackToStationByAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTrackReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddStationReturnDTO>> Get([FromBody] AddTrackDTO addTrackDTO)
        {
            try
            {
                var station = await _AdminService.AddTrackToStationbyAdmin(addTrackDTO);
                return Ok(station);
            }
            catch (TrackAlreadyAddedException taae)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ErrorModel(409, taae.Message));
            }
            catch(NoSuchStationFoundException nssfe)
            {
                Console.Write("Station Error");
                return NotFound(new ErrorModel(404, nssfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdatePricePerKMByAdmin")]
        [HttpPut]
        [ProducesResponseType(typeof(UpdatePricePerKmReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<UpdatePricePerKmReturnDTO>> Get([FromBody] UpdatePricePerKmDTO updatePricePerKm)
        {
            try
            {
                var train = await _AdminService.UpdatePricePerKmbyAdmin(updatePricePerKm);
                return Ok(train);
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

        [Authorize(Roles = "Admin")]
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
                var seat = await _AdminService.CheckSeatsDetailsbyAdmin(TrainId);
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

        [Authorize(Roles = "Admin")]
        [Route("CheckReservedTracksofStation")]
        [HttpPost]
        [ProducesResponseType(typeof(GetReservedTracksReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<GetReservedTracksReturnDTO>> GetTrack([FromBody] int StationId)
        {
            try
            {
                var track = await _AdminService.GetReservedTracksofStationbyAdmin(StationId);
                return Ok(track);
            }
            catch (NoTrainTracksFoundException nstfe)
            {
                return NotFound(new ErrorModel(404, nstfe.Message));
            }
            catch (NoSuchStationFoundException nstfe)
            {
                return NotFound(new ErrorModel(404, nstfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateTrainStatusAsCompleted")]
        [HttpPut]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<string>> UpdateTrain([FromBody] int TrainId)
        {
            try
            {
                var train = await _AdminService.UpdateTrainStatusCompleted(TrainId);
                return Ok(train);
            }
            catch (NoSuchTrainFoundException nstfe)
            {
                return NotFound(new ErrorModel(404, nstfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateTrainRouteDetailsByAdmin")]
        [HttpPut]
        [ProducesResponseType(typeof(TrainRoutes), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<TrainRoutes>> UpdateTrainRoute([FromBody] UpdateTrainRouteDetailsDTO updateTrainRouteDetailsDTO)
        {
            try
            {
                var trainRoute = await _AdminService.UpdateTrainRouteDetails(updateTrainRouteDetailsDTO);
                return Ok(trainRoute);
            }
            catch (NoSuchTrainRouteFoundException nstfe)
            {
                return NotFound(new ErrorModel(404, nstfe.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("GetAllInlineTrainsByAdmin")]
        [HttpGet]
        [ProducesResponseType(typeof(IList<Train>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<IList<Train>>> GetInlineTrain()
        {
            try
            {
                var InlineTrain = await _AdminService.GetAllInlineTrains();
                return Ok(InlineTrain);
            }
            catch (NoTrainsFoundException nstfe)
            {
                return NotFound(new ErrorModel(404, nstfe.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [Authorize(Roles = "Admin")]
        [Route("GetAllInlineTrainsByAdmin")]
        [HttpGet]
        [ProducesResponseType(typeof(AddRefundReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddRefundReturnDTO>> AddRefund(AddRefundDTO addRefundDTO)
        {
            try
            {
                var refund = await _AdminService.ProcessRefundByAdmin(addRefundDTO);
                return Ok(refund);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }


    }
}
