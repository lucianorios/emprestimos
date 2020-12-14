using Loan.WebApi.ApplicationServices.Handlers;
using Loan.WebApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Loan.WebApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        private readonly DomainNotificationHandler domainNotification;

        public ApiControllerBase(INotificationHandler<DomainNotification> domainNotification)
        {
            this.domainNotification = (DomainNotificationHandler)domainNotification;
        }

        #region [IActionResult] sync

        protected IActionResult Result<TData>(TData data)
        {
            if (this.IsValidOperation)
            {
                return ResultOk(data);
            }

            return ResultErro(data, this.domainNotification.GetNotifications().Select(s => s.Value));
        }

        protected IActionResult Result()
        {
            if (this.IsValidOperation)
            {
                return ResultOk();
            }

            return ResultErro(this.domainNotification.GetNotifications().Select(s => s.Value));
        }

        private IActionResult ResultOk()
        {
            return ResultOk("Sucesso!");
        }

        private IActionResult ResultOk<TData>(TData data, IEnumerable<string> messages)
            where TData : class
        {
            return new JsonResult(new Response<TData>(data, true, HttpStatusCode.OK, messages));
        }

        private IActionResult ResultOk<TData>(TData data)
        {
            return new JsonResult(new Response<TData>(data, true, HttpStatusCode.OK));
        }

        private IActionResult ResultErro(IEnumerable<string> messages)
        {
            return ResultErro("Erro!", messages);
        }

        private IActionResult ResultErro<TData>(TData data, IEnumerable<string> messages)
        {
            return new JsonResult(new Response<TData>(data, false, HttpStatusCode.OK, messages));
        }

        #endregion

        #region [IActionResult] async 

        protected Task<IActionResult> ResultOkAsync()
        {
            return Task.FromResult(ResultOk());
        }

        protected Task<IActionResult> ResultOkAsync<TData>(TData data, IEnumerable<string> messages)
            where TData : class
        {
            return Task.FromResult(ResultOk(data, messages));
        }

        protected Task<IActionResult> ResultOkAsync<TData>(TData data)
            where TData : class
        {
            return Task.FromResult(ResultOk(data));
        }

        protected Task<IActionResult> ResultErroAsync(IEnumerable<string> messages)
        {
            return Task.FromResult(ResultErro(messages));
        }

        protected Task<IActionResult> ResultErroAsync<TData>(TData data, IEnumerable<string> messages)
            where TData : class
        {
            return Task.FromResult(ResultErro(data, messages));
        }

        #endregion

        #region protected

        protected IEnumerable<DomainNotification> Notifications => this.domainNotification.GetNotifications();

        protected bool IsValidOperation => !this.domainNotification.HasNotification();

        //protected void NotifyError(string code, string message)
        //{
        //    this.mediator.RaiseEventAsync(new DomainNotification(code, message));
        //}

        #endregion
    }
}