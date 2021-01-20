using Passageiro.Domain.Entities;
using Passageiro.WebApi.Models;

namespace Passageiro.WebApi.ViewModels {
    public class ControleAcessoViewModel : Model<ControleAcessoDomain> {
        public string Login { get; set; }
        public int Sistema { get; set; }
        public string Senha { get; set; }

    }
}