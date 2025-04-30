## ✅ Improve Existing Tests  
**Improve tests maintainability by making them more human readable**

### 🎯 Intent  
Make tests easier to read, understand, and maintain. 
Clean, expressive tests act as executable specifications and living documentation, especially in legacy contexts where understanding the system behavior is critical.

### 🔍 When to Use  
- When tests are hard to read or brittle  
- Before refactoring sessions on legacy test suites  
- As a preparatory step before introducing new features or fixing bugs  
- To onboard developers unfamiliar with the domain logic

### 🤖 How to Use with AI Assistants  

AI can help rephrase, refactor, and restructure tests to improve readability and alignment with domain language. It can also suggest better patterns and point out anti-patterns.

#### Prompt Examples  
- *"Can you rewrite this test using the Arrange-Act-Assert pattern?"*  
- *"Rewrite these tests to use a Test Data Builder pattern for clarity."*  
- *"Can you add fluent assertions to this test to make it read like a business rule?"*  
- *"What smells do you see in this test suite? How can we reduce duplication or improve naming?"*  
- *"Can you group these tests by scenario to improve readability and maintenance?"*

💡 You can also provide a domain glossary to help the assistant generate assertions that reflect the business vocabulary.

### 🧱 Patterns to Apply  
- **3A Pattern (Arrange-Act-Assert)**  
Brings clarity and structure to test methods.
```csharp
// Arrange
var account = new Account(100);
// Act
account.withdraw(40);
// Assert
Assert.AreEqual(60, account.Balance);
```
- **Test Data Builder**  
Avoids copy/paste setup, improves test reuse.
```csharp
User user = AUser()
  .WithRole("admin")
  .WithEmail("john@example.com")
  .Build();
```
- **Fluent Business Assertions**
```csharp
order.Should()
    .BeConfirmed()
    .And.HasTotal(100)
    .And.HasItemsCount(2);
```

### 🛠️ Related Craft/Agile Practices
- Clean Tests (from Clean Code)
- Test Naming & Structure Conventions
- BDD (Behavior-Driven Development): Writing tests as business rules
- Living Documentation: Making tests readable by non-dev stakeholders
- Outside-In TDD: From domain intent to implementation

### 📚 Go Deeper
- [Expression Builder – Martin Fowler](https://martinfowler.com/bliki/ExpressionBuilder.html)
- [Fluent Assertions in .NET](https://github.com/fluentassertions/fluentassertions)
- [Tests as specification](https://testerstories.com/2012/02/tests-as-specifications/)
- [BDD vs TDD: What’s the Difference?](https://cucumber.io/blog/bdd/bdd-vs-tdd/)

---