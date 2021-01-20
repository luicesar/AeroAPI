using Passageiro.Domain.Entities;
using Passageiro.Repository.Models;

namespace Passageiro.Service.Interfaces {
    public interface IControleAcessoService : IServiceBase<ControleAcessoDomain> {
        UsuarioLogadoModel Authenticate (string login, string senha);
    }
}