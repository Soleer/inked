﻿namespace Inked.Ordering.API.Application.Commands;

/// <summary>
///     Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
///     a requestid sent by client is used to detect duplicate requests.
/// </summary>
/// <typeparam name="T">Type of the command handler that performs the operation if request is not duplicated</typeparam>
/// <typeparam name="R">Return value of the inner command handler</typeparam>
public abstract class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
    where T : IRequest<R>
{
    private readonly ILogger<IdentifiedCommandHandler<T, R>> _logger;
    private readonly IMediator _mediator;
    private readonly IRequestManager _requestManager;

    public IdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<T, R>> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _mediator = mediator;
        _requestManager = requestManager;
        _logger = logger;
    }

    /// <summary>
    ///     This method handles the command. It just ensures that no other request exists with the same ID, and if this is the
    ///     case
    ///     just enqueues the original inner command.
    /// </summary>
    /// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
    /// <returns>Return value of inner command or default value if request same ID was found</returns>
    public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
    {
        var alreadyExists = await _requestManager.ExistAsync(message.Id);
        if (alreadyExists)
        {
            return CreateResultForDuplicateRequest();
        }

        await _requestManager.CreateRequestForCommandAsync<T>(message.Id);
        try
        {
            var command = message.Command;
            var commandName = command.GetGenericTypeName();
            var idProperty = string.Empty;
            var commandId = string.Empty;

            switch (command)
            {
                case CreateOrderCommand createOrderCommand:
                    idProperty = nameof(createOrderCommand.UserId);
                    commandId = createOrderCommand.UserId;
                    break;

                case CancelOrderCommand cancelOrderCommand:
                    idProperty = nameof(cancelOrderCommand.OrderNumber);
                    commandId = $"{cancelOrderCommand.OrderNumber}";
                    break;

                case ShipOrderCommand shipOrderCommand:
                    idProperty = nameof(shipOrderCommand.OrderNumber);
                    commandId = $"{shipOrderCommand.OrderNumber}";
                    break;

                default:
                    idProperty = "Id?";
                    commandId = "n/a";
                    break;
            }

            _logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                commandName,
                idProperty,
                commandId,
                command);

            // Send the embedded business command to mediator so it runs its related CommandHandler 
            var result = await _mediator.Send(command, cancellationToken);

            _logger.LogInformation(
                "Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                result,
                commandName,
                idProperty,
                commandId,
                command);

            return result;
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    ///     Creates the result value to return if a previous request was found
    /// </summary>
    /// <returns></returns>
    protected abstract R CreateResultForDuplicateRequest();
}
