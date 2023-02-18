using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using SolutionTemplate.Application.Abstractions;
using SolutionTemplate.Protos.Domain;
using SolutionTemplate.Protos.Features;
using static SolutionTemplate.Protos.SolutionTemplateService;
using Item = SolutionTemplate.Domain.Item;
using Point = SolutionTemplate.Domain.Point;

namespace SolutionTemplate.Grpc;

public sealed class SolutionTemplateService : SolutionTemplateServiceBase
{
    private readonly ICreateOrderService _createOrderService;

    public SolutionTemplateService(ICreateOrderService createOrderService)
    {
        _createOrderService = createOrderService;
    }

    public override async Task<AddOrderResponse> AddOrder(AddOrderRequest request, ServerCallContext context)
    {
        var items = request.Items
            .Select(x => Item.Create(x.Note))
            .ToList();
        var points = request.Points
            .Select(x => Point.Create(x.Note))
            .ToList();

        var order = await _createOrderService.Create(request.Note, items, points, context.CancellationToken);


        return new AddOrderResponse
        {
            Order = new Order
            {
                Id = order.Id,
                Note = order.Note,
                Items =
                {
                    order.Items.Select(x => new Protos.Domain.Item
                    {
                        Id = x.Id,
                        Note = x.Note
                    }).ToList()
                },
                Points =
                {
                    order.Points
                        .Select(x => new Protos.Domain.Point
                        {
                            Id = x.Id,
                            Note = x.Note
                        })
                }
            }
        };
    }
}
