using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Passageiro.Domain.Entities;
using Passageiro.Domain.Enums;
using Passageiro.Repository.Data;
using Passageiro.Repository.Interfaces;
using Passageiro.Repository.Models;
using Passageiro.Service.Interfaces;

namespace Passageiro.Service.Services {
    public class ControleAcessoService : ServiceBase<ControleAcessoDomain>, IControleAcessoService {
        private readonly PassageiroContext _dbContext;
        private readonly IControleAcessoRepository _controle;
        private IConfiguration _configuration { get; }
        private readonly IMapper _mapper;
        public ControleAcessoService (
            PassageiroContext dbContext,
            IControleAcessoRepository controle,
            IConfiguration configuration,
            IMapper mapper) : base (dbContext) {
            this._dbContext = dbContext;
            this._controle = controle;
            this._configuration = configuration;
            this._mapper = mapper;
        }

        public UsuarioLogadoModel Authenticate (string login, string senha) {

            var usuarioLogadoModel = new UsuarioLogadoModel ();
            var controleAcessoEncontrado = _dbContext.ControleAcesso.Where (x => x.Login == login && x.Senha == senha).FirstOrDefault ();

            if (controleAcessoEncontrado == null)
                return null;

            usuarioLogadoModel = _mapper.Map<UsuarioLogadoModel> (controleAcessoEncontrado);

            switch (controleAcessoEncontrado.TipoUsuario) {
                case TipoUsuarioEnum.Sistema:
                    usuarioLogadoModel.Rules = "Sistema";
                    break;
                case TipoUsuarioEnum.Usuario:
                    usuarioLogadoModel.Rules = "Usuario";
                    break;
            }

            return usuarioLogadoModel;
        }
    }
}