using AutoMapper;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Models;
using GrupoWebBackend.DomainAdvertisements.Domain.Models;
using GrupoWebBackend.DomainPublications.Domain.Models;
using GrupoWebBackend.DomainAdoptionsRequests.Resources;
using GrupoWebBackend.DomainAdvertisements.Resources;
using GrupoWebBackend.DomainPublications.Resources;
using GrupoWebBackend.Security.Domain.Entities;
using GrupoWebBackend.Security.Domain.Services.Communication;
using GrupoWebBackend.Security.Resources;
namespace GrupoWebBackend.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveAdvertisementResource, Advertisement>();
            CreateMap<SaveAdoptionsRequestsResource, AdoptionsRequests>();
            CreateMap<SavePublicationResource, Publication>();
            CreateMap<RegisterRequest, User>();
            CreateMap<AuthenticateResponse, User>();
            CreateMap<AuthenticateRequest, User>();
            CreateMap<UserResource, User>();
        }
    }
}
