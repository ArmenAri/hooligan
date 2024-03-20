using Hooligan.Application.Usages;
using Hooligan.Domain;
using Hooligan.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hooligan.Presentation.Controllers;

[Route("api/[controller]s")]
public class AssociationController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Association>> Post([FromBody] CreateAssociation association)
    {
        var result = await mediator.Send(association);
        return result.Match(Ok);
    }
}