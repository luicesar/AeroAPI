using System.Collections.Generic;
using Flunt.Validations;
using Passageiro.Shared.Entities;

namespace Passageiro.Domain.Entities {
  public class PassageiroDomain : Entity {
    protected PassageiroDomain () {

    }

    public PassageiroDomain (string nome, int idade, string celular) {
      Nome = nome;
      Idade = idade;
      Celular = celular;

      this.Validate ();
    }

    public void Validate () {
      AddNotifications (new Contract ()
        .IsNotNullOrEmpty (Nome, "Passageiro.Nome", "O Nome é obrigatório")
        .IsNotNullOrEmpty (Celular, "Passageiro.Celular", "O Celular é obrigatório")
      );

    }

    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Celular { get; set; }

  }
}