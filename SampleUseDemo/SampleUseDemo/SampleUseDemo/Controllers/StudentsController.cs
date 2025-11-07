using Microsoft.AspNetCore.Mvc;
using SampleUseDemo.Models;
using UowRepositoryKit.Interfaces;

namespace SampleUseDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public StudentsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _uow.Repository<Student>().GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var student = await _uow.Repository<Student>().GetByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student model)
        {
            model.Id = 0; // Ensure Id is not set for new entities to allow identity column to generate it
            await _uow.Repository<Student>().AddAsync(model);
            await _uow.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student model)
        {
            var existing = await _uow.Repository<Student>().GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = model.Name;
            existing.Age = model.Age;
            existing.Class = model.Class;
            existing.Address = model.Address;

            _uow.Repository<Student>().Update(existing);
            await _uow.SaveChangesAsync();

            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _uow.Repository<Student>().GetByIdAsync(id);
            if (student == null) return NotFound();

            _uow.Repository<Student>().Delete(student);
            await _uow.SaveChangesAsync();

            return Ok();
        }
    }
}
