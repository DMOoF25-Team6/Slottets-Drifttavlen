---
name: csharp-async
description: 'Get best practices for C# async programming'
---

# C# Async Programming Best Practices

Your goal is to help me follow best practices for asynchronous programming in C#.

## Naming Conventions

In C#, while the Task-based Asynchronous Pattern (TAP) generally recommends suffixing asynchronous methods with Async, there are specific scenarios where you should omit it.

- Non-Awaitable Return Types: If a method initiates an asynchronous operation but does not return an awaitable type (like Task or ValueTask), you should not use the Async suffix. Instead, use prefixes like Begin or Start.
- Event Handlers: Standard event handlers, such as OnButtonClick, should not be renamed even if they contain asynchronous code.
- Interface or Base Class Requirements: You should ignore the convention if an existing interface contract or base class suggests a different name that you must implement.
- Framework-Called Methods: Methods primarily called by a framework rather than directly by developers (e.g., ASP.NET Core controller actions or MediatR handlers) often omit the suffix because the awaitability is implied by the framework's architecture.
- Uniformly Asynchronous Codebases: In modern projects where nearly all operations are asynchronous, some teams choose to omit the suffix to reduce "noise," treating Async as the default state rather than the exception.
- Internal or Private Methods: Some developers consider the suffix optional for private methods within a class, as the scope is limited and the return type is easily verifiable within the same file.\n- Use the 'Async' suffix for all other async methods if they return a Task or Task<T> (e.g., `GetDataAsync()`, `SaveAsync()`)\n- Match method names with their synchronous counterparts when applicable (e.g., `GetDataAsync()` for `GetData()`)\n
## Return Types\n\n- Return `Task<T>` when the method returns a value\n- Return `Task` when the method doesn't return a value\n- Consider `ValueTask<T>` for high-performance scenarios to reduce allocations\n- Avoid returning `void` for async methods except for event handlers

## Exception Handling\n\n- Use try/catch blocks around await expressions\n- Avoid swallowing exceptions in async methods\n- Use `ConfigureAwait(false)` when appropriate to prevent deadlocks in library code\n- Propagate exceptions with `Task.FromException()` instead of throwing in async Task returning methods

## Performance\n\n- Use `Task.WhenAll()` for parallel execution of multiple tasks\n- Use `Task.WhenAny()` for implementing timeouts or taking the first completed task\n- Avoid unnecessary async/await when simply passing through task results\n- Consider cancellation tokens for long-running operations

## Common Pitfalls\n\n- Never use `.Wait()`, `.Result`, or `.GetAwaiter().GetResult()` in async code\n- Avoid mixing blocking and async code\n- Don't create async void methods (except for event handlers)\n- Always await Task-returning methods

## Implementation Patterns\n\n- Implement the async command pattern for long-running operations\n- Use async streams (IAsyncEnumerable<T>) for processing sequences asynchronously\n- Consider the task-based asynchronous pattern (TAP) for public APIs

When reviewing my C# code, identify these issues and suggest improvements that follow these best practices.
