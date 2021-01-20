using Passageiro.Domain.Entities;
using Passageiro.WebApi.Models;

namespace Passageiro.WebApi.ViewModels {
    public class PassageiroViewModel : Model<PassageiroDomain> {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Celular { get; set; }
    }
}