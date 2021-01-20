using System;
using AutoMapper;
using Passageiro.Shared.Entities;

namespace Passageiro.WebApi.Models {
    public abstract class Model<T> : IModel<T> where T : Entity {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public Model () {
            this.DataCriacao = DateTime.Now;
        }

        public virtual T MapForDomain () {
            return Mapper.Map<T> (this);
        }
    }
}