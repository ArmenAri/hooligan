using Hooligan.Application.Usages;
using Hooligan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hooligan.Presentation.Controllers;

[Route("api/[controller]")]
public class AssociationController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Association>> Post(CreateAssociation association)
    {
        return await mediator.Send(association);
    }
}