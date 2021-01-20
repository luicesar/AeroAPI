using AutoMapper;
using Passageiro.Domain.Entities;
using Passageiro.Repository.Models;
using Passageiro.WebApi.ViewModels;

namespace Passageiro.WebApi {
    public class MappingProfile : Profile {
        public MappingProfile () {
            this.Configure ();
        }

        protected void Configure () {
            CreateMap<PassageiroDomain, PassageiroViewModel> ().ReverseMap ();
            CreateMap<ControleAcessoDomain, UsuarioLogadoModel> ().ReverseMap ();
        }
    }
}