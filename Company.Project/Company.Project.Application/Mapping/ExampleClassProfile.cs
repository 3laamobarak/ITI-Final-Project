using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.ExampleClass;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Company.Project.Application.Mapping
{
    public class ExampleClassProfile : Profile
    {
        public ExampleClassProfile()
        {
            // Entity -> DTO
            CreateMap<ExampleClass, CreateExampleClassDto>();
            //CreateMap<ExampleClass, ExampleClassDto>();


        }
    }
}
