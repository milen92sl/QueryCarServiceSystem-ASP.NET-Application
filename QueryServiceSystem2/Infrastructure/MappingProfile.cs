using AutoMapper;
using QueryServiceSystem2.Data.Models;
using QueryServiceSystem2.Models.Mechanics;
using QueryServiceSystem2.Models.Queries;
using QueryServiceSystem2.Services.Mechanics.Models;
using QueryServiceSystem2.Services.Queries.Models;

namespace QueryServiceSystem2.Infrastructure
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            this.CreateMap<Car, QueryCarServiceModel>();
            this.CreateMap<Query, QueryServiceModel>()
                .ForMember(m => m.CarModel, cfg => cfg.MapFrom(f => f.Model))
                .ForMember(m => m.CarRegistrationNumber, cfg => cfg.MapFrom(f => f.RegistrationNumber))
                .ForMember(m => m.CarColor, cfg => cfg.MapFrom(f => f.Color))
                .ForMember(c => c.CarName, cfg => cfg.MapFrom(c => c.Car.Name));

            this.CreateMap<Mechanic, QueryServiceModel>()
               .ForMember(m => m.MechanicName, cfg => cfg.MapFrom(f => f.Name));

            this.CreateMap<Mechanic, MechanicServiceModel>()
                .ForMember(f => f.Id, cfg => cfg.MapFrom(f => f.Id))
                .ForMember(f => f.Name, cfg => cfg.MapFrom(f => f.Name))
                .ForMember(f => f.Code, cfg => cfg.MapFrom(f => f.Code))
                .ForMember(f => f.PhoneNumber, cfg => cfg.MapFrom(f => f.PhoneNumber));

            this.CreateMap<Query, LatestQueryServiceModel>()
                .ForMember(m => m.CarModel, cfg => cfg.MapFrom(f => f.Model))
                .ForMember(m => m.CarRegistrationNumber, cfg => cfg.MapFrom(f => f.RegistrationNumber))
                .ForMember(m => m.CarColor, cfg => cfg.MapFrom(f => f.Color));

            this.CreateMap<QueryDetailsServiceModel, QueryFormModel>()
                .ForMember(m => m.CarModel , cfg => cfg.MapFrom(f => f.CarModel));

            this.CreateMap<MechanicDetailsServiceModel, MechanicFormModel>();

            this.CreateMap<Query, QueryDetailsServiceModel>()
                .ForMember(m => m.UserId, cfg => cfg.MapFrom(f => f.Mechanic.UserId))
                .ForMember(m => m.CarModel, cfg => cfg.MapFrom(f => f.Model))
                .ForMember(m => m.CarRegistrationNumber, cfg => cfg.MapFrom(f => f.RegistrationNumber))
                .ForMember(m => m.CarColor, cfg => cfg.MapFrom(f => f.Color))
                .ForMember(m => m.CarName, cfg => cfg.MapFrom(f => f.Car.Name));

            this.CreateMap<Mechanic, MechanicDetailsServiceModel>()
                .ForMember(m => m.MechanicId, cfg => cfg.MapFrom(f => f.Id))
                .ForMember(m => m.MechanicName, cfg => cfg.MapFrom(f => f.Name))
                .ForMember(m => m.MechanicCode, cfg => cfg.MapFrom(f => f.Code))
                .ForMember(m => m.MechanicPhoneNumber, cfg => cfg.MapFrom(f => f.PhoneNumber));
        }
    }
}
