using Passageiro.Domain.Entities;
using Passageiro.Repository.Data;
using Passageiro.Repository.Interfaces;

namespace Passageiro.Repository.Repositorys {
    public class ControleAcessoRepository : RepositoryBase<ControleAcessoDomain>, IControleAcessoRepository {
        PassageiroContext Dbcontext;
        public ControleAcessoRepository (PassageiroContext dbcontext) : base (dbcontext) {
            this.Dbcontext = dbcontext;
        }
    }
}