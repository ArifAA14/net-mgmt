using AutoMapper;
using EmployeeMgmt.Domain.Entities;
using EmployeeMgmt.Application.DTOs;

namespace EmployeeMgmt.API.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Mapping from Employee to EmployeeDTO and vice versa
      CreateMap<Employee, EmployeeDTO>()
          .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.DepartmentName))
          .ReverseMap();
    }
  }
}


