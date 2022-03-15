using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.CategoryLogic
{
    public class CategoryDelete
    {
        public class CategoryDeleteCommand : IRequest<bool>
        {
            public string id { get; set; }
        }

        public class CategoryDeleteValidator : AbstractValidator<CategoryDeleteCommand>
        {
            public CategoryDeleteValidator()
            {
                RuleFor(x => x.id).NotNull().WithMessage("La categoría se eliminó exitosamente"); ;
            }
        }

        public class CategoryDeleteHandler : IRequestHandler<CategoryDeleteCommand, bool>
        {
            public readonly IMongoRepository<Category> dbProduct;

            public CategoryDeleteHandler(IMongoRepository<Category> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<bool> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.DeleteById(request.id);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
