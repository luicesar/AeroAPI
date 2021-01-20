using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passageiro.Domain.Entities;
using Passageiro.Service.Interfaces;
using Passageiro.WebApi.ViewModels;

namespace Passageiro.WebApi.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    [ApiController]
    public class PassageiroController : ControllerBase<PassageiroDomain, PassageiroViewModel> {
        private readonly IPassageiroService _service;
        private readonly IMapper _mapper;
        public PassageiroController (IPassageiroService service, IMapper mapper) : base (service) {
            this._service = service;
            this._mapper = mapper;
        }
    }
}