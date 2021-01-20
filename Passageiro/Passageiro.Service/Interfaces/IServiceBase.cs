using Passageiro.Repository.Interfaces;
using Passageiro.Shared.Entities;

namespace Passageiro.Service.Interfaces {
    public interface IServiceBase<T> : IRepositoryBase<T> where T : Entity {

    }
}