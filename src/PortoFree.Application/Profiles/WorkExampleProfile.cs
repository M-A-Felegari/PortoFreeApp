using AutoMapper;
using PortoFree.Application.Features.WorkExamples.Commands.AddWorkExample;
using PortoFree.Domain.Entities;

namespace PortoFree.Application.Profiles;

public class WorkExampleProfile : Profile
{
    public WorkExampleProfile()
    {
        CreateMap<AddWorkExampleCommand, WorkExample>();
    }
}