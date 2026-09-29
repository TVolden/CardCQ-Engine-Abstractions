# CardCQ.Engine.Abstractions

The contracts for building content packages for the **CardCQ** card game engine.

CardCQ uses a command/query split: **commands** change the game state, **queries** read it. Commands record what happened as **events** and can raise **control signals** that other parts of the game react to. Every concept carries a description that the engine shows to the card game designer in the editor.

Reference this package to build your own commands, queries and handlers against the engine without depending on the engine itself.

## Installation

```shell
dotnet add package CardCQ.Engine.Abstractions
```

Targets **.NET 10**. The interfaces use `static abstract` members, so they need C# 11 or later.

## Concepts

| Interface | Purpose |
|---|---|
| `ICardCommand` | An action that changes the game state. It returns no data. |
| `ICardCommandHandler<TCommand>` | Runs a command. It records events and raises control signals through `ICardEventDispatcher`. |
| `ICardQuery<TResult>` | A read-only request that returns a `TResult`. It must not change the game state. |
| `ICardQueryHandler<TQuery, TResult>` | Answers a query. It gets no event dispatcher, because queries don't change state. |
| `ICardEvent` | Base class for events. Events record what happened, and the engine replays them to rebuild a game state. |
| `ICardEventObserver<TEvent>` | Applies a recorded event, for example by updating the package's state. It also runs when events are replayed. |
| `IControlSignal` | A signal that triggers reactions in the game. Signals are not recorded or replayed. |
| `IControlSignalObserver<TSignal>` | Reacts when a specific control signal is raised. |
| `ICardEventDispatcher` | Records events and raises control signals inside the engine. You can mock it in unit tests. |
| `ICardCQDispatcher` | Dispatches commands and queries, so a handler can use other commands. |
| `ICardQueryDispatcher` | Dispatches queries only. |
| `IBlockValueType` | Marks a custom type that code blocks can take as a parameter or return from a query, such as a card or a collection. Built-in types (`string`, `int`, `bool`, ...) need no marker. |
| `ICardCQService` | Registers command handlers, query handlers, event observers and signal observers with the engine. |
| `ICardCQPackageSetup` | The entry point of a package. The engine calls it to register the package's handlers and observers. |

The designer-facing text comes from static members:

- `Concept` on commands, queries, control signals and value types says **what** the concept is.
- `Description` on handlers says **how** it is handled.

### Events or signals?

| | Events (`ICardEvent`) | Control signals (`IControlSignal`) |
|---|---|---|
| Purpose | Record a change to the game state | Let the game react to something as it happens |
| Recorded and replayed | Yes | No |
| Sent with | `DispatchAsync` | `RaiseSignal` |
| Observed by | `ICardEventObserver<TEvent>` | `IControlSignalObserver<TSignal>` |

## Example

This example is a small package that adds dice to the engine:

- `CreateDie` creates a die with a unique id, then dispatches `RollDie` to give it its first value.
- `RollDie` rolls an existing die.
- `GetDieValue` returns the value a die is currently showing.

Creating a die raises `DieCreatedSignal` and records `DieCreatedEvent`. Every roll, including the first one, raises `DieRolledSignal` and records `DieRolledEvent`. Event observers apply the events to the dice repository, so the state is rebuilt when the events are replayed.

### Shared state

The handlers and observers share the state of the dice.

```csharp
using CardCQ.Engine.Abstractions;

public class DiceRepository
{
    Dictionary<Guid, Die> repository = new();
    public Die GetDie(Guid Id) => repository[Id];
    public void StoreDie(Guid Id, Die die) => repository[Id] = die;
}

public record Die(int Sides, int Value);
```

### Creating a die

```csharp
public record CreateDie(Guid DieId, int Sides) : ICardCommand
{
    public static string Concept => "Create a new die with a given number of sides.";
}

public record DieCreatedSignal(Guid DieId, int Sides) : IControlSignal
{
    public static string Concept => "A die has been created.";
}

public class DieCreatedEvent(Guid dieId, int sides) : ICardEvent
{
    public Guid DieId { get; } = dieId;
    public int Sides { get; } = sides;
}

public class CreateDieHandler(ICardCQDispatcher dispatcher) : ICardCommandHandler<CreateDie>
{
    public static string Description => "Adds the die to the game and rolls it for its first value.";

    public async Task HandleAsync(CreateDie command, ICardEventDispatcher eventDispatcher, CancellationToken ct)
    {
        await eventDispatcher.RaiseSignal(new DieCreatedSignal(command.DieId, command.Sides), ct);
        await eventDispatcher.DispatchAsync(new DieCreatedEvent(command.DieId, command.Sides), ct);
        await dispatcher.DispatchAsync(new RollDie(command.DieId), ct);
    }
}

public class DieCreatedEventObserver(DiceRepository repository) : ICardEventObserver<DieCreatedEvent>
{
    public Task Invoke(DieCreatedEvent cardEvent, CancellationToken ct)
    {
        repository.StoreDie(cardEvent.DieId, new Die(cardEvent.Sides, 1));
        return Task.CompletedTask;
    }
}
```

### Rolling a die

```csharp
public record RollDie(Guid DieId) : ICardCommand
{
    public static string Concept => "Roll an existing die to get a new value.";
}

public record DieRolledSignal(Guid DieId, int Sides, int Result) : IControlSignal
{
    public static string Concept => "A die was rolled, this is the result.";
}

public class DieRolledEvent(Guid dieId, int result) : ICardEvent
{
    public Guid DieId { get; } = dieId;
    public int Result { get; } = result;
}

public class RollDieHandler(DiceRepository repository) : ICardCommandHandler<RollDie>
{
    public static string Description => "Sets the die to a random value between 1 and its number of sides.";

    public async Task HandleAsync(RollDie command, ICardEventDispatcher eventDispatcher, CancellationToken ct)
    {
        var die = repository.GetDie(command.DieId);
        var result = Random.Shared.Next(1, die.Sides + 1);

        await eventDispatcher.RaiseSignal(new DieRolledSignal(command.DieId, die.Sides, result), ct);
        await eventDispatcher.DispatchAsync(new DieRolledEvent(command.DieId, result), ct);
    }
}

public class DieRolledEventObserver(DiceRepository repository) : ICardEventObserver<DieRolledEvent>
{
    public Task Invoke(DieRolledEvent cardEvent, CancellationToken ct)
    {
        var die = repository.GetDie(cardEvent.DieId);
        repository.StoreDie(cardEvent.DieId, die with { Value = cardEvent.Result });
        return Task.CompletedTask;
    }
}
```

### Getting the current value

```csharp
public record GetDieValue(Guid DieId) : ICardQuery<int>
{
    public static string Concept => "The value a die is currently showing.";
}

public class GetDieValueHandler(DiceRepository repository) : ICardQueryHandler<GetDieValue, int>
{
    public static string Description => "Reads the current value of the die.";

    public Task<int> HandleAsync(GetDieValue query, CancellationToken ct)
        => Task.FromResult(repository.GetDie(query.DieId).Value);
}
```

### Reacting to a roll

A signal observer reacts every time a die is rolled, for example to give a bonus on a six.

```csharp
public class SixRolledObserver : IControlSignalObserver<DieRolledSignal>
{
    public Task SignalRaised(DieRolledSignal signal)
    {
        if (signal.Result == 6)
        {
            // React to the six here...
        }

        return Task.CompletedTask;
    }
}
```

### Registering the package

```csharp
using System.Reflection;

public class DicePackage : ICardCQPackageSetup
{
    public static Assembly GetAssembly => typeof(DicePackage).Assembly;
    public static string Color => "#3A7BD5";

    public Task Setup(ICardCQService service)
    {
        service.RegisterCommandHandler<CreateDie, CreateDieHandler>();
        service.RegisterCommandHandler<RollDie, RollDieHandler>();
        service.RegisterQueryHandler<GetDieValue, int, GetDieValueHandler>();
        service.RegisterEventObserver<DieCreatedEvent, DieCreatedEventObserver>();
        service.RegisterEventObserver<DieRolledEvent, DieRolledEventObserver>();
        service.RegisterSignalObserver<DieRolledSignal, SixRolledObserver>();
        return Task.CompletedTask;
    }
}
```

## Testing

Handlers and observers only depend on these abstractions, so you can unit test them without the engine. Mock `ICardEventDispatcher` to check which signals and events a command handler sends, and mock `ICardCQDispatcher` to check which commands it dispatches.

```csharp
var dieId = Guid.NewGuid();
var eventDispatcher = new Mock<ICardEventDispatcher>();

// Creating a die records the creation and dispatches the first roll.
var cqDispatcher = new Mock<ICardCQDispatcher>();
await new CreateDieHandler(cqDispatcher.Object).HandleAsync(new CreateDie(dieId, Sides: 6), eventDispatcher.Object, CancellationToken.None);

eventDispatcher.Verify(d => d.DispatchAsync(
    It.Is<DieCreatedEvent>(e => e.DieId == dieId && e.Sides == 6),
    It.IsAny<CancellationToken>()));
cqDispatcher.Verify(d => d.DispatchAsync(
    It.Is<RollDie>(c => c.DieId == dieId),
    It.IsAny<CancellationToken>()));

// Rolling a die signals and records a result within its number of sides.
var repository = new DiceRepository();
repository.StoreDie(dieId, new Die(Sides: 6, Value: 1));
await new RollDieHandler(repository).HandleAsync(new RollDie(dieId), eventDispatcher.Object, CancellationToken.None);

eventDispatcher.Verify(d => d.RaiseSignal(
    It.Is<DieRolledSignal>(s => s.DieId == dieId && s.Result >= 1 && s.Result <= 6),
    It.IsAny<CancellationToken>()));
eventDispatcher.Verify(d => d.DispatchAsync(
    It.Is<DieRolledEvent>(e => e.DieId == dieId && e.Result >= 1 && e.Result <= 6),
    It.IsAny<CancellationToken>()));

// Applying the recorded event updates the value returned by the query.
await new DieRolledEventObserver(repository).Invoke(new DieRolledEvent(dieId, 4), CancellationToken.None);
var value = await new GetDieValueHandler(repository).HandleAsync(new GetDieValue(dieId), CancellationToken.None);
Assert.Equal(4, value);
```

## Versioning

This package follows [Semantic Versioning](https://semver.org/). A change to an existing interface is a breaking change and bumps the major version.
