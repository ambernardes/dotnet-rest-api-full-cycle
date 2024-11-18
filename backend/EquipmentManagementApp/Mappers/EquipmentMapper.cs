using AutoMapper;
using EquipmentManagementApp.Dtos;
using EquipmentManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagementApp.Mappers
{
    public class EquipmentMapper : Profile
    {
        public EquipmentMapper()
        {
            CreateMap<Equipment, EquipmentDto>().ReverseMap();
        }
    }
}
