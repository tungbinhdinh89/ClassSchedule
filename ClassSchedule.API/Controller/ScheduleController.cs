using ClassSchedule.Application.DTOs;
using ClassSchedule.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClassSchedule.API.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController(IScheduleService scheduleService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            var scheduleDTOs = await scheduleService.GetAllSchedulesAsync();
            return Ok(scheduleDTOs);
        }


        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleDTO scheduleDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await scheduleService.CreateScheduleAsync(scheduleDTO);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleDTO scheduleDTO)
        {
            if (id != scheduleDTO.Id)
                return BadRequest("ID mismatch.");

            var updated = await scheduleService.UpdateScheduleAsync(scheduleDTO);

            if (!updated)
                return NotFound($"Schedule with ID {id} not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var deleted = await scheduleService.DeleteScheduleAsync(id);
            if (!deleted)
                return NotFound($"Schedule with ID {id} not found.");

            return NoContent();
        }
    }
}
