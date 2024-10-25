using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;

namespace Motocycle.Application.Commons.Responses
{
    public class InternalValidationProblemDetails
    {
        public string Title { get; set; }
        public IEnumerable<ErrrorDetail> Errors { get; set; }

        protected InternalValidationProblemDetails()
        {
        }

        public InternalValidationProblemDetails(ModelStateDictionary modelState)
        {
            Title = "One or more validation errors occurred.";
            Errors = modelState.Select(x => new ErrrorDetail { Key = x.Key, Value = x.Value.Errors.Select(x => x.ErrorMessage).FirstOrDefault() });
        }

        public InternalValidationProblemDetails(IDictionary<string, string[]> errors)
        {
            Title = "One or more validation errors occurred.";
            Errors = errors.Select(x => new ErrrorDetail { Key = x.Key, Value = x.Value.FirstOrDefault() });
        }

        public class ErrrorDetail
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public void SetNotifications(IHandler<DomainNotification> notifications)
        {
            foreach (var error in Errors)
                notifications.Handle(DomainNotification.ModelValidation(error.Key, error.Value));
        }
    }
}
