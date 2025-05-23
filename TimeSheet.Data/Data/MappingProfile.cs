using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TimeSheet.Models;
using TimeSheet.Models.Dto;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TimeSheet.Data.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.ProjectMilestones, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.ProjectMilestones?
                        .Where(m => m.StartDate <= (DateOnly)context.Items["startDate"]
                                 && m.EndDate >= (DateOnly)context.Items["endDate"])
                        .ToList()
                ));

            CreateMap<ProjectMilestone, ProjectMilestoneDto>()
                .ForMember(dest => dest.Timesheets, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.Timesheets?
                        .Where(t => t.Date >= (DateOnly)context.Items["startDate"]
                                 && t.Date <= (DateOnly)context.Items["endDate"])
                        .ToList()
                ));

            CreateMap<Milestone, MilestoneDto>()
                .ForMember(dest => dest.Timesheets, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.Timesheets?
                        .Where(t => t.Date >= (DateOnly)context.Items["startDate"]
                                 && t.Date <= (DateOnly)context.Items["endDate"])
                        .Where(t => t.EmployeeId == (int)context.Items["EmployeeId"])
                        .ToList()
                ));

            CreateMap<Timesheet, TimesheetDto>();

        }
    }
}
