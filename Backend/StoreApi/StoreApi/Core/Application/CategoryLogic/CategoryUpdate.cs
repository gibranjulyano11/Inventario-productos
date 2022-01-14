using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using MongoDB.Driver;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.ProductLogic
{
    public class CategoryUpdate
    {
        public class CategoryUpdateCommand : IRequest<string>
        {
            public string CategoryId { get; set; }
            public string CategoryName { get; set; }

        }
        public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateCommand>
        {
            public CategoryUpdateValidator()
            {
                RuleFor(x => x.CategoryId).NotEmpty().NotNull();
                RuleFor(x => x.CategoryName).NotEmpty().NotNull().WithMessage("Error, el producto necesita un código");
            }
        }
        public class CategoryUpdateHandler : IRequestHandler<CategoryUpdateCommand, string>
        {
            private readonly IMongoRepository<Category> dbProduct;

            public CategoryUpdateHandler(IMongoRepository<Category> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.UpdateReplaceDocument(new Category
                    {
                        Id = request.CategoryId,
                        Name = request.CategoryName,
                    });


                    return $"La Categoria {request.CategoryName} fue actualizada exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}
