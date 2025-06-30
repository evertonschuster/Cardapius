using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Administration.Application.Products.Commands.CreateProduct
{
    internal class CreateProductHandler(
        IProductRepository productRepository,
        IValidator<CreateProductCommand> validator
        ) : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            validator.ValidateAndThrow(request);


            var model = request.ToModel();
            var result = productRepository.Create(model);

            throw new NotImplementedException();
        }
    }
}
