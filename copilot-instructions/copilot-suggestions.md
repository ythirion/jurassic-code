When enhancing test coverage or fixing test issues in the JurassicCode project:

Follow proper C# naming conventions with PascalCase for classes, interfaces, methods, properties, and _camelCase for private fields.

Use test isolation patterns with proper setup and teardown in each test class:

Reset the database state before/after each test
Use TestInitializer helpers to create clean service instances
Don't rely on default initialization data
Respect the existing domain rules (e.g., dinosaur coexistence rules where T-Rex cannot coexist with other species, Velociraptor can coexist with herbivores, etc.)

Structure tests using the Arrange-Act-Assert pattern and make them small and descriptive.

Utilize the established testing stack:

NSubstitute for mocking
FluentAssertions for assertions
xUnit for test framework
Bogus for generating realistic test data
Test data builders
When fixing test issues, ensure tests reflect the actual intended behavior rather than assumptions about how the code should work.

Maintain test isolation by using independent data sets for each test to prevent interference.

Add XML documentation comments to explain complex test setups or assertions.
