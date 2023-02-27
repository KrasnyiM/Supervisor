using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DelphiSupervisorV6;

namespace SupervisorLogic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProcessInfo, ProcessInfoDto>().ReverseMap();
        }
    }
}
