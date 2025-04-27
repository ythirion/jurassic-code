## Tests

Whenever you need to write test, ensure that they are as small and descriptive as possible.
Use patterns such as test data builder to ensure that it's easy for a human to understand what the test is doing.
Also, uses libraries such as Bogus to represent human readeable data instead of making it up yourself.
Try to be the most expressive possible and split the test files so that they are logical for the user to find.
If you act on a test project, don't prefix or suffix folders with `test`, the context is self-explicit.
For the backend, the test stack is 
- NSubstitute for mocking
- FluentAssertions for assertions (but freeze it to the latest 7.x version)
- xUnit for testing
- Bogus for generating human-like data
