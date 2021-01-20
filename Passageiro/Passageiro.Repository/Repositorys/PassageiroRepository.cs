using Passageiro.Domain.Entities;
using Passageiro.Repository.Data;
using Passageiro.Repository.Interfaces;

namespace Passageiro.Repository.Repositorys {
    public class PassageiroRepository : RepositoryBase<PassageiroDomain>, IPassageiroRepository {
        PassageiroContext Dbcontext;
        public PassageiroRepository (PassageiroContext dbcontext) : base (dbcontext) {
            this.Dbcontext = dbcontext;
        }
    }
}