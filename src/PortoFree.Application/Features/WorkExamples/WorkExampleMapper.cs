using AutoMapper;
using PortoFree.Application.Features.WorkExamples.Commands.AddWorkExample;
using PortoFree.Application.Features.WorkExamples.Commands.UpdateWorkExample;
using PortoFree.Domain.Entities;

namespace PortoFree.Application.Features.WorkExamples;

public class WorkExampleMapper : Profile
{
    public WorkExampleMapper()
    {
        CreateMap<WorkExample, WorkExampleDto>();
        
        CreateMap<AddWorkExampleCommand, WorkExample>();
        CreateMap<UpdateWorkExampleCommand, WorkExample>();
    }
}