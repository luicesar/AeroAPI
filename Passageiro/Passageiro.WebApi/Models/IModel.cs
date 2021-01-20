using Passageiro.Shared.Entities;

namespace Passageiro.WebApi.Models {
    public interface IModel<D> where D : Entity {
        D MapForDomain ();
    }
}