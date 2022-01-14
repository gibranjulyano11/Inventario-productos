using FluentValidation;
using Lib.Service.Mongo.Interfaces;
using MediatR;
using StoreApi.Core.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace StoreApi.Core.Application.CategoryLogic
{
    public class CategoryCreate
    {
        public class CategoryCreateCommand : IRequest<string>
        {
            public string CategoryId { get; set; }
            public string CategoryName { get; set; }
        }

        public class CategoryCreateValidator : AbstractValidator<CategoryCreateCommand>
        {
            public CategoryCreateValidator()
            {
                RuleFor(x => x.CategoryName).NotEmpty().NotNull().WithMessage("Error, ingrese el nombre de una categoría");
            }
        }

        public class CategoryCreateHandler : IRequestHandler<CategoryCreateCommand, string>
        {
            private readonly IMongoRepository<Category> dbProduct;

            public CategoryCreateHandler(IMongoRepository<Category> dbProduct)
            {
                this.dbProduct = dbProduct;
            }

            public async Task<string> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    await dbProduct.InsertDocument(new Category
                    {
                        Name = request.CategoryName,
                    });


                    return $"La categoría {request.CategoryName} fue insertada exitosamente";
                }
                catch (System.Exception ex)
                {
                    return ex.Message;
                }
            }
        }

    }
}
