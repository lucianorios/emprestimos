using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loan.WebApi.Domain.Entities
{
    public class Usuario: Entity
    {
        public string Login { get; protected set; }
        public string Password { get; protected set; }

        public Usuario() { }

        public Usuario(long id, string login, string password)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(this.Login))
                this.AddNotification("Informe um login");

            if (string.IsNullOrEmpty(this.Password))
                this.AddNotification("Informe um passworsd");
        }

        public void SetLogin(string login) => this.Login = login;
        public void SetPassword(string password) => this.Password = password;
    }
}
