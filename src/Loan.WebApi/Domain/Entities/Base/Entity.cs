using Loan.WebApi.Domain.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Loan.WebApi.Domain.Entities
{
    public abstract class Entity : IEntity<long>
    {
        public virtual long Id { get; protected set; }
        public virtual DateTime DataCriacao { get; protected set; }

        public Entity()
        {
            this.DataCriacao = DateTime.Now;
        }

        public Entity(DateTime dataCriacao)
        {
            this.DataCriacao = DateTime.Now;
        }

        #region Notifications

        [NotMapped]
        private List<DomainNotification> _notifications;

        [NotMapped, JsonIgnore]
        public IEnumerable<DomainNotification> Notifications { get => this._notifications?.AsReadOnly(); }

        [NotMapped, JsonIgnore]
        public bool Invalido { get => this._notifications != null ? this._notifications.Any() : false; }

        [NotMapped, JsonIgnore]
        public bool Valido { get => this._notifications != null ? !this._notifications.Any() : true; }

        public void AddNotification(string mensagem)
        {
            if (this._notifications == null)
                this._notifications = new List<DomainNotification>();

            this._notifications.Add(new DomainNotification(GetType().Name, mensagem));
        }

        #endregion
    }
}
