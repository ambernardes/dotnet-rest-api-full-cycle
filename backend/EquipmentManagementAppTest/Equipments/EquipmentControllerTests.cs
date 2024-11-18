using AutoMapper;
using EquipmentManagementApp.Controllers;
using EquipmentManagementApp.Mappers;
using EquipmentManagementApp.Services;
using EquipmentManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using EquipmentManagementApp.Dtos;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace EquipmentManagementAppTest.Equipments
{
    public class EquipmentControllerTests
    {
        private readonly EquipmentController _controller;
        private readonly Mock<IEquipmentService> _serviceMock;
        private readonly Mock<ILogger<EquipmentController>> _loggerMock;
        private readonly IMapper _mapper;

        public EquipmentControllerTests()
        {
            _serviceMock = new Mock<IEquipmentService>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<EquipmentMapper>());
            _mapper = config.CreateMapper();
            
            _loggerMock = new Mock<ILogger<EquipmentController>>();

            _controller = new EquipmentController(_serviceMock.Object, _mapper, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAll_EmptyList_ReturnsOkObjectResult()
        {
            var equipmentList = new List<Equipment>(); 
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(equipmentList);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<EquipmentDto>>(okResult.Value);
            Assert.Empty(model); // Adjust based on your expected behavior
        }
        
        [Fact]
        public async Task GetById_ExistingId_ReturnsOkObjectResult()
        {
            var equipmentId = 1;
            var equipment = new Equipment
            {
                Id = 1,
                Name = "Pliers",
                Category = "Tools",
                Status = "In Use",
                Location = "Warehouse 2",
                LastMaintenanceDate = new DateTime(),
                RentalRate = 5
            };
            _serviceMock.Setup(s => s.GetByIdAsync(equipmentId)).ReturnsAsync(equipment);

            var result = await _controller.GetById(equipmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<EquipmentDto>(okResult.Value);
            Assert.Equal(equipmentId, model.Id); // Adjust based on your expected behavior
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsNotFoundResult()
        {
            var nonExistingId = 999;
            _serviceMock.Setup(s => s.GetByIdAsync(nonExistingId)).ReturnsAsync(null as Equipment);

            var result = await _controller.GetById(nonExistingId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ValidEquipmentDto_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var equipmentDto = new EquipmentDto {
                Id = 1,
                Name = "Pliers",
                Category = "Tools",
                Status = "In Use",
                Location = "Warehouse 2",
                LastMaintenanceDate = new DateTime(),
                RentalRate = 5
            };
            
            var equipment = _mapper.Map<Equipment>(equipmentDto);
            var createdEquipment = new Equipment {
                Id = 1,
                Name = "Pliers",
                Category = "Tools",
                Status = "In Use",
                Location = "Warehouse 2",
                LastMaintenanceDate = new DateTime(),
                RentalRate = 5
            }; 
            _serviceMock.Setup(s => s.AddAsync(It.IsAny<Equipment>())).ReturnsAsync(createdEquipment);

            var result = await _controller.Create(equipmentDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<EquipmentDto>(createdAtActionResult.Value);
            Assert.Equal(createdEquipment.Id, model.Id); 
        }

        //TODO: generate more tests  
    }
}