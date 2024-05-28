using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RailwayReservationApp.Exceptions.SeatExcepions;
using RailwayReservationApp.Exceptions.StationExceptions;
using RailwayReservationApp.Exceptions.TrackExceptions;
using RailwayReservationApp.Exceptions.TrainExceptions;
using RailwayReservationApp.Interfaces;
using RailwayReservationApp.Models;
using RailwayReservationApp.Models.AdminDTOs;

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

        [Route("AddTrainByAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTrainReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddTrainReturnDTO>> Get([FromBody] AddTrainDTO addTrainDTO)
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

        [Route("AddTrainRouteByAdmin")]
        [HttpPost]
        [ProducesResponseType(typeof(AddTrainRouteReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        [ProducesErrorResponseType(typeof(ErrorModel))]
        public async Task<ActionResult<AddTrainRouteReturnDTO>> Get([FromBody] AddTrainRouteDTO addTrainRouteDTO)
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

        [Route("UpdatePricePerKMByAdmin")]
        [HttpPost]
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


    }
}
