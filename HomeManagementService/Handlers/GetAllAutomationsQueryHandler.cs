#region

using HomeManagementService.Models;
using HomeManagementService.Repositories;
using MediatR;

#endregion

namespace HomeManagementService.Handlers;

public class GetAllAutomationsQueryHandler : IRequestHandler<GetAllAutomationsQuery, List<Automation>?>
{
    private readonly IAutomationRepository _automationRepository;

    public GetAllAutomationsQueryHandler(
        IAutomationRepository automationRepository
    )
    {
        _automationRepository = automationRepository;
    }

    public async Task<List<Automation>?> Handle(GetAllAutomationsQuery request, CancellationToken cancellationToken)
    {
        return await _automationRepository.GetAll();
    }
}

public record GetAllAutomationsQuery : IRequest<List<Automation>?>;