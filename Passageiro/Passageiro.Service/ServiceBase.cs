using Passageiro.Repository;
using Passageiro.Repository.Data;
using Passageiro.Service.Interfaces;
using Passageiro.Shared.Entities;

namespace Passageiro.Service {
    public class ServiceBase<T> : RepositoryBase<T>, IServiceBase<T> where T : Entity {
        public ServiceBase (PassageiroContext dbContext) : base (dbContext) { }

    }
}