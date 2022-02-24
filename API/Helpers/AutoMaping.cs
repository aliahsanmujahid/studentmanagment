using System;
using System.Collections.Generic;
using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMaping : Profile
    {
        public AutoMaping(){ 

        CreateMap<Class, ClassDto>();

        }
        
    }
}