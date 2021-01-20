using AutoMapper;
using Microsoft.Extensions.Configuration;
using Passageiro.Domain.Entities;
using Passageiro.Repository.Data;
using Passageiro.Repository.Interfaces;
using Passageiro.Service.Interfaces;

namespace Passageiro.Service.Services {
    public class PassageiroService : ServiceBase<PassageiroDomain>, IPassageiroService {
        private readonly PassageiroContext _dbContext;
        private readonly IPassageiroRepository _passageiro;
        private IConfiguration _configuration { get; }
        private readonly IMapper _mapper;
        public PassageiroService (
            PassageiroContext dbContext,
            IPassageiroRepository passageiro,
            IConfiguration configuration,
            IMapper mapper
        ) : base (dbContext) {
            this._dbContext = dbContext;
            this._passageiro = passageiro;
            this._mapper = mapper;
            this._configuration = configuration;
        }
    }
}