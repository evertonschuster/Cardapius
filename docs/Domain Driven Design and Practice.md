# Introduction

Domain Driven Design (DDD) is about mapping business domain concepts into software artifacts. Most of the writings and articles on this topic have been based on Eric Evans' book "Domain Driven Design", covering the domain modeling and design aspects mainly from a conceptual and design stand-point. These writings discuss the main elements of DDD such as Entity, Value Object, Service etc or they talk about concepts like Ubiquitous Language, Bounded Context and Anti-Corruption Layer.
Domain Driven Design and Development is also influenced by several architectural, design, and implementation aspects such as:
- Business Rules
- Persistence
- Caching
- Transaction Management
- Security
- Code Generation
- Test Driven Development
- Refactoring

## A domain model offers several benefits some of which are:
- It helps the team create a common model, between the business and IT stakeholders in the company, that the team can use to communicate about the business requirements, data entities, and process models.
- The model is modular, extensible and easy to maintain as the design reflects the business model.
- It improves the reusability and testability of the business domain objects.

On the flip side, let's see what happens when IT teams don't follow a domain model approach for developing medium to large size enterprise software applications.

Not investing in a domain model and development effort leads to an application architecture with a "Fat Service Layer" and an "Anemic Domain Model" where facade classes (usually Stateless Session Beans) start accumulating more and more business logic and domain objects become mere data carriers with getters and setters. This approach also leads to domain specific business logic and rules being scattered (and duplicated in some cases) in several different facade classes.

Anemic domain models, in most cases, are not cost-effective; they don't give the company a competitive advantage over other companies because implementing business requirement changes in this architecture take too long to develop and deploy to production environment.

Before we look at different architectural and design considerations in a DDD implementation project, let's take a look at the characteristics of a rich domain model.

- The domain model should focus on a specific business operational domain. It should align with the business model, strategies and business processes.
- It should be isolated from other domains in the business as well as other layers in the application architecture.
- It should be reusable to avoid any duplicate models and implementations of the same core business domain elements.
- The model should be designed loosely coupled with other layers in the application, meaning no dependencies on the layers on either side of domain layer (i.e. database and facade layers).
- It should be an abstract and cleanly separated layer enabling easier maintenance, testing, and versioning. The domain classes should be unit testable outside the container (and from inside the IDE).
- It should be designed using a POJO programming model without any technology or framework dependencies (I always tell the project teams I work with in my company, that the technology we use for software development is Java).
- The domain model should be independent of persistence implementation details (although the technology does place some constraints on the model).
- It should have minimum dependencies on any infrastructure frameworks because it will outlive these frameworks and we don't want any tight coupling on any external framework.



## References
https://www.infoq.com/articles/ddd-in-practice/
https://khalilstemmler.com/articles/typescript-domain-driven-design/entities/
https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/