using AutoMapper;
using App.Core.Application.DTOs.User;
using App.Core.Application.ViewModels;
using App.Core.Application.ViewModels.Login;
namespace App.Core.Application.Mappings.DtosAndViewModels
{
    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<UserSummaryDto, UserSummaryViewModel>().ReverseMap();

            CreateMap<LoginDto, LoginViewModel>()
                .ReverseMap();

            //Por agregar los maps de Delete y Update y Create, y Register
        }
    }
}
