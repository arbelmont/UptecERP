using System.Linq;
using System.Threading.Tasks;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Events;
using Definitiva.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Uptec.Erp.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IUnitOfWork _uow;
        private readonly IBus _bus;

        protected BaseController(INotificationHandler<DomainNotification> notifications, 
                                 IUnitOfWork uow, 
                                 IBus bus)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
            _uow = uow;
        }


        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                data = result,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected IActionResult ResponseIgnoreDomainErrors(object result = null)
        {
                return Ok(new
                {
                    success = true,
                    data = result
                });
        }

        protected void StartTransaction()
        {
            _uow.StartTransaction();
        }

        protected bool Commit()
        {
            if (_notifications.HasNotifications())
            {
                _uow.RollbackTransaction();
                return false;
            }

            try
            {
                if (_uow.Commit())
                    return true;

                NotifyError("DataBaseError", "Critical database error while saving data!");
            }
            catch (System.Exception ex)
            {
                NotifyError("DataBaseError", ex.InnerException.Message);
            }
            return false;
        }

        protected void CommitUntracked()
        {
            try
            {
                _uow.Commit();
            }
            catch (System.Exception ex)
            {
                NotifyError("DataBaseError", ex.InnerException.Message);
            }
        }

        protected void CommitTransactionUntracked()
        {
            //if (_notifications.HasNotifications())
            //    _uow.RollbackTransaction();

            try
            {
                _uow.CommitTransaction();
            }
            catch (System.Exception ex)
            {
                _uow.RollbackTransaction();
                NotifyError("DataBaseError", ex.InnerException.Message);
            }
        }

        protected bool IsValidModelState()
        {
            if (ModelState.IsValid) return true;

            NotifyErrorModel();
            return false;
        }

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotifyErrorModel()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string codigo, string mensagem)
        {
            _bus.PublishEvent(new DomainNotification(codigo, mensagem));
        }

        protected void PublishEventAsync(Event theEvent)
        {
            Task.Factory.StartNew(() => _bus.PublishEvent(theEvent));
        }

        protected void PublishEvent(Event theEvent)
        {
            _bus.PublishEvent(theEvent);
        }
    }
}
