using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResnponse> :
        IPipelineBehavior<TRequest, TResnponse> where TRequest : IRequest<TResnponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }
        public async Task<TResnponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResnponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                /// nos traemos el contexto actual
                var context = new ValidationContext<TRequest>(request);

                //ejecutamos todas las validaciones
                var validationResults =
                    await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));


                // vamos a ver si hubo problemas de validacion
                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f != null).ToList();

                // tronamos los errores
                if (failures.Any())
                    throw new ValidationException(failures);

            }

            return await next();
        }
    }
}
