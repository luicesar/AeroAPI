using System;
using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;

namespace Passageiro.Shared.Entities {
    public abstract class Entity : Notifiable {
        [Key]
        public int Id { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Entity () {
            this.DataCriacao = DateTime.Now;
        }

        public void SetId (int Id) {
            this.Id = Id;
        }

    }
}