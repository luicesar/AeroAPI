using Passageiro.Domain.Enums;
using Passageiro.Shared.Entities;

namespace Passageiro.Domain.Entities {

    public class ControleAcessoDomain : Entity {

        protected ControleAcessoDomain () {

        }

        public ControleAcessoDomain (string login, string senha, TipoUsuarioEnum tipoUsuario) {
            Login = login;
            Senha = senha;
            TipoUsuario = tipoUsuario;
        }

        public string Login { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }
        public string Senha { get; set; }
    }
}