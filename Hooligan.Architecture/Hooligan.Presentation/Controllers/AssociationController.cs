using Hooligan.Application.Usages;
using Hooligan.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hooligan.Presentation.Controllers;

[Route("api/[controller]s")]
public class AssociationController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Association>> Post([FromBody] CreateAssociation association)
    {
        return await mediator.Send(association);
    }
}