using Microsoft.AspNetCore.Mvc;
using EquipmentManagementApp.Models;
using EquipmentManagementApp.Services;
using EquipmentManagementApp.Dtos;
using AutoMapper;

namespace EquipmentManagementApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(IEquipmentService service, IMapper mapper, ILogger<EquipmentController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipmentDto>>> GetAll()
        {
            try
            {
                var equipment = await _service.GetAllAsync();
                var equipmentDtos = _mapper.Map<IEnumerable<EquipmentDto>>(equipment);
                return Ok(equipmentDtos);
            }
            catch (Exception ex)
            {
                // Return a generic error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDto>> GetById(int id)
        {
            try
            {
                var equipment = await _service.GetByIdAsync(id);
                if (equipment == null)
                {
                    return NotFound();
                }
                var equipmentDto = _mapper.Map<EquipmentDto>(equipment);
                return Ok(equipmentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all equipment.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentDto>> Create(EquipmentDto equipmentDto)
        {
            try
            {
                var equipment = _mapper.Map<Equipment>(equipmentDto);
                var createdEquipment = await _service.AddAsync(equipment);
                var createdEquipmentDto = _mapper.Map<EquipmentDto>(createdEquipment);
                return CreatedAtAction(nameof(GetById), new { id = createdEquipmentDto.Id }, createdEquipmentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the equipment.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EquipmentDto equipmentDto)
        {
            try {
                    if (id != equipmentDto.Id)
                {
                    return BadRequest();
                }

                var equipment = _mapper.Map<Equipment>(equipmentDto);
                await _service.UpdateAsync(equipment);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the equipment.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the equipment.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
