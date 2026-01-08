using Microsoft.AspNetCore.Mvc;
using PicpayChal.App.DTO;
using PicpayChal.App.Services.Interfaces;

namespace PicpayChal.App.Controllers;

[ApiController]
[Route("transfer")]
public class TransactionController(
    ITransactionService transactionService,
    INotificationService notificationService
)
    : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;
    private readonly INotificationService _notificationService = notificationService;


    [HttpPost]
    public async Task<ActionResult> Transfer([FromBody] TransactionRequest request)
    {
        var transactionId = await _transactionService.Transfer(request);
        await _notificationService.Notify(transactionId);

        return Created();
    }
}
