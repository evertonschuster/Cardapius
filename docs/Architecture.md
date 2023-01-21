# Architecture

A typical enterprise application architecture consists of the following four conceptual layers:

- **User Interface** (Presentation Layer): Responsible for presenting information to the user and interpreting user commands.
- **Application Layer**: This layer coordinates the application activity. It doesn't contain any business logic. It does not hold the state of business objects, but it can hold the state of an application task's progress.
- **Domain Layer**: This layer contains information about the business domain. The state of business objects is held here. Persistence of the business objects and possibly their state is delegated to the infrastructure layer.
- **Infrastructure Layer**: This layer acts as a supporting library for all the other layers. It provides communication between layers, implements persistence for business objects, contains supporting libraries for the user interface layer, etc.


## Let's look at the application and domain layers in more detail. The Application layer:

- is responsible for the navigation between the UI screens in the application as well as the interaction with the application layers of other systems.
- can also perform the basic (non-business related) validation on the user input data before transmitting it to the other (lower) layers of the application.
- doesn't contain any business or domain related logic or data access logic.
- doesn't have any state reflecting a business use case but it can manage the state of the user session or the progress of a task.

## The domain layer:
- is responsible for the concepts of business domain, information about the business use case and the business rules. Domain objects encapsulate the state and behavior of business entities. Examples of business entities in a loan processing application are Mortgage, Property, and Borrower.
- can also manage the state (session) of a business use case if the use case spans multiple user requests (e.g. loan registration process which consists of multiple steps: user entering the loan details, system returning the products and rates based on the loan parameters, user selecting a specific product/rate combination, and finally the system locking the loan for that rate).
- contains service objects that only have a defined operational behavior which is not part of any domain object. Services encapsulate behavior of the business domain that doesn't fit in the domain objects themselves.
- is the heart of the business application and should be well isolated from the other layers of the application. Also, it should not be dependent on the application frameworks used in the other layers.


Figure below shows the different architecture layers used in the application and how they relate to DDD.

![This is an image](images/ArchitectureDiagram.gif)

## Aspect Oriented Programming

AOP helps in an even better design (i.e. less cluttering in the domain model) by removing the cross-cutting concerns code like auditing, domain state change tracking etc from the domain objects. It can be used to inject collaborating objects and services into domain objects especially the objects that are not instantiated by the container (such as the persistence objects). Other aspects in the domain layer that could use AOP are caching, transaction management and role based security (authorization).

Loan processing application uses custom Aspects to introduce data caching into the Service objects. Loan product and interest rate information is loaded from the database table once (the first this information is requested by the client) and is then stored in an object cache for subsequent product and rate lookups. Product and rate data is frequently accessed but it's not updated that regularly, so it's a good candidate for caching the data instead of hitting the back-end database every time.

The role of DI and AOP concepts in DDD was the main topic in a recent discussion thread. The discussion was based on a presentation by Ramnivas Laddad where he made the assertion that DDD cannot be implemented without help of AOP and DI. In the presentation, Ramnivas talked about the concept of "fine grained DI" using AOP to make domain objects regain smart behavior. He mentioned that domain objects need access to other fine grained objects to provide rich behavior and a solution to this is to inject Services, Factories, or Repositories into Domain Objects (by using Aspects to inject dependency at constructor or setter invocation time).

Chris Richardson also discussed about using DI, objects, and Aspects to improve the application design by reducing coupling and increasing modularity. Chris talked about "Big Fat Service" anti-pattern which is the result of coupling, tangling and scattering of the application code and how to avoid it using DI and AOP concepts.

## Domain model and Security

Application security in the domain layer ensures only authorized clients (human users or other applications) are calling the domain operations as well as accessing the domain state.

In domain and service classes, authorization is managed at the class method invocation level. For example, the "loan approval" method in an Underwriting domain object can be invoked by any user with an "Underwriter" role for loans up to 1 million dollars whereas the approval method in the same domain object for loan applications with a loan amount greater than 1 million dollars can only be called by a user with "Underwriting Supervisor" role.

The following table is a summary of various application security concerns in each layer of the application architecture.

Table 1. Security Concerns in Various Application Layers

| Layer				|   Security																	|
|-------------------|-------------------------------------------------------------------------------|
| Client/Controller | Authentication, Web Page (URL) Level Authorization							|
| Facade			| Role based authorization														|
| Domain			| Domain instance level authorization, ACL										|
| Database			| DB object level authorization (Stored procedures, Stored functions, Triggers) |


## Business Rules
Business rules are an important part of the business domain. They define data validation and other constraints that need to be applied on domain objects in specific business process scenarios. Business rules typically fall into the following categories:

- Data validation
- Data transformation
- Business decision-making
- Process routing (work-flow logic)

The context is very important in DDD world. Context specificity dictates the domain object collaboration as well as other run-time factors like what business rules to apply etc. Validation and other business rules are always processed in a specific business context. This means the same domain object, in a different business context, will have to process different set of business rules. For example, some attributes of a loan domain object (such as loan amount and interest rate) cannot be changed after the loan has been through the Underwriting step in the loan approval process. But the same attributes can be changed when the loan is just registered and locked for a specific interest rate.
Even though all the domain specific business rules should be encapsulated in the domain layer, some application designs put the rules in facade classes, which leads to domain classes becoming "anemic" in terms of business rules logic. This may be an acceptable solution in small size applications, but it is not recommended for mid-size to large enterprise applications that contain complex business rules. A better design option is to put the rules where they belong, inside the domain objects. If a business rule logic spans two or more Entity objects, then it should become part of a Service class.

## Design
From a design stand-point, the domain layer should have a well defined boundary to avoid the corruption of the layer from non-core domain layer concerns such as vendor-specific translations, data filtering, transformations, etc. Domain elements should be designed to hold the domain state and behavior correctly. Different domain elements are structured differently based on state and behavior. Table 2 below shows the domain elements and what they contain.

Table 2. Domain elements with state and behavior

| Domain Element				  | State/Behavior	   |
|---------------------------------|--------------------|
| Entity, Value Object, Aggregate | State and Behavior |
| Data Transfer Object			  | State only		   |
| Service, Repository			  | Behavior only	   |

Entities, Value Objects, and Aggregates which contain both state (data) and behavior (operations), should have clearly defined state and behavior. At the same time, this behavior should not extend beyond the limits of the object's boundaries. Entities should do most of the work in the use case acting on their local state. But they shouldn't know about too many unrelated concepts.

Good design practice is to only include the getters/setters for the attributes that are required to encapsulate the state of domain objects. When designing the domain objects, only provide setter methods for those fields that can change. Also, the public constructors should contain only the required fields instead of a constructor with all the fields in the domain class.

In most of the use cases, we don't really have to be able to change the state of an object directly. So, instead of changing the internal state, create a new object with the changed state and return the new object. This is sufficient in these use cases and it also reduces design complexity.

Aggregate classes hide the usage of collaborating classes from callers. They can be used for encapsulating complex, intrusive, and state-dependent requirements in the domain classes.

## Design Patterns that Support DDD

There are several design patterns that help in domain driven design and development. Following is a list of these design patterns:

- Domain Object (DO)
- Data Transfer Object (DTO)
- DTO Assembler
- Repository: The Repository contains domain-centric methods and uses the DAO to interact with the database.
- Generic DAO's
- Temporal Patterns: These patterns add time dimension to rich domain models. Bitemporal framework, which is based on Martin Fowler's Temporal Patterns, provides a design approach to dealing with bitemporal issues in the domain models. The core domain objects and their bitemporal properties can be persisted using an ORM product such as Hibernate.

Other design patterns that are used in DDD include Strategy, Facade, and Factory. Jimmy Nilsson discussed Factory as one of the domain patterns in his book.

## DDD Anti-Patterns

On the flip side of the best practices and design patterns, there are some DDD smells that architects and developers should watch out for when implementing the domain model. As a result of these anti-patterns, domain layer becomes the least important part in application architecture and facade classes assume a more important role in the model. Following are some of these anti-patterns:

- Anemic domain objects
- Repetitive DAO's
- Fat Service Layer: This is where service classes will end up having all the business logic.
- Feature Envy: This is one of the classic smells mentioned in Martin Fowler's book on Refactoring where the methods in a class are far too interested in data belonging to other classes

## Data Access Objects

DAO's and Repositories are also important in domain driven design. DAO is the contract between relational database and the application. It encapsulates the details of database CRUD operations from the web application. On the other hand, a Repository is a separate abstraction that interacts with the DAOs and provides "business interfaces" to the domain model.

Repositories speak the Ubiquitous Language of the domain, work with all necessary DAOs and provide data access services to the domain model in a language the domain understands.

DAO methods are fine-grained and closer to the database while the Repository methods are more coarse-grained and closer to the domain. Also one Repository class may have multiple DAO's injected. Repositories and DAO's keep the domain model decoupled from dealing with the data access and persistence details.

The domain objects should depend only on Repository interfaces. This is the reason why injecting the Repository instead of a DAO results in a much cleaner domain model. DAO classes should never be called directly from the client (Services and other consumer classes). The clients should always call the domain objects which in turn should call the DAO's for persisting the data to the data store.

Managing the dependencies between domain objects (for example, the dependency between an Entity and its Repository) is a classic problem that developers often run into. The usual design solution to this problem is to have the Service or Facade class call a Repository directly and when invoked the Repository would return the Entity object to the client. This design eventually leads to the afore-mentioned Anemic Domain Model where facade classes start accumulating more business logic and domain objects become mere data carriers. A good design is to inject Repositories and Services into domain objects using DI & AOP techniques.

## Caching
When we talk about the state (data) of the domain layer, we have to talk about the aspect of caching. Frequently accessed domain data (such as products and rates in a mortgage loan processing application) are good candidates for caching. Caching speeds up the performance and reduces the load on the database server. Service layer is ideal for caching the domain state.

## Transaction Management

Transaction management is important to keep the data integrity and to commit or rollback the UOW as a whole. There has always been a debate about where the transactions should be managed in the application architecture layers. There are also the cross-entity transactions (that span multiple domain objects in the same UOW) that affect the design decision of where the transactions should be managed.

Some developers prefer managing the transactions in the DAO classes which is a poor design. This results in too fine-grained transaction control which doesn't give the flexibility of managing the use cases where the transactions span multiple domain objects. Service classes should handle transactions; this way even if the transaction spans multiple domain objects, the service class can manage the transaction since in most of the use cases the Service class handles the control flow.

FundingServiceImpl class in the sample application manages transactions for the funding request and executes multiple database operations by calling the Repositories and commits or rolls back all database changes in a single transactions.

## Data Transfer Objects

DTO's are also an important part of the design in an SOA environment where the Domain object model structurally is not compatible with the messages that are received and sent from a business service. The messages are typically defined and maintained in as XML Schema Definition documents (XSD's) and it's a common practice to write (or code generate) DTO objects from the XSD's and use them for data (message) transfer purposes between domain and SOA service layers. Mapping the data from one or more domain objects to a DTO will become a necessary evil in distributed applications where sending the domain objects across the wire may not be practical from a performance and a security stand-point.

From a DDD perspective, DTO's also help maintain the separation between Service and UI layers where DO's are used in the domain and service layers and DTO's are used in the presentation layer.

## Development

A model is no good without the actual implementation. The implementation phase should include automating the development tasks as much as possible. To see what tasks can be automated, let's look at a typical use case involving the domain model. Following is the list of steps in the use case:

**Request In**:

- Client calls a Facade class sending data as an XML document (which is XSD compliant); Facade class initiates a new transaction for the UOW.
- Run validations on the incoming data. These validations include the primary (basic/data type/field level checks) and business validations. If there are any validation errors, raise appropriate exceptions.
- Translate the descriptions to codes (to be domain friendly).
- Make the data formatting changes to be domain model friendly.
- Make any separation of attributes (like splitting a customer name into first and last name attributes in a Customer Entity object).
- Disassemble the DTO data into one or more domain objects.
- Persist the state of domain objects.

**Response Out:**

- Get the state of domain object(s) from datastore.
- Cache the state if necessary.
- Assemble the domain object(s) into application friendly data objects (DTO).
- Make any merge or separation of data elements (such as combining first and last names into single customer name attribute).
- Translate the codes into descriptions.
- Make the data formatting changes necessary to address the client data usage requirements.
- Cache the DTO state if necessary
- Transaction commits (or rolls back if there was an error) as the control flow exits.



# Conclusion

DDD is a powerful concept that will change the way modelers, architects, developers, and testers will look at the software once the team is trained in DDD and start to apply "domain first and infrastructure second" philosophy. As different stakeholders (from IT and business units) with different backgrounds and areas of expertise are involved in the domain modeling, design and implementation effort, to quote Eric Evans, "it's important not to blur the lines between the philosophy of design (DDD) and the technical tool box that helps us fulfill it (OOP, DI, and AOP)".

## Advancing Frontiers

This section covers some of the emerging approaches that impact the DDD design and development. Some of these concepts are still evolving and it will be interesting to see how they will affect DDD.

Architecture rules and Design by Contract enforcement plays an important role in the governance and policy enforcement of domain model standards and implementation best practices. Ramnivas talked about using the Aspects to enforce the rule of creating a Repository object only through Factories; this is an easy to violate design rule in domain layer.

Domain Specific Languages (DSL) and Business Natural Languages (BNL) are gaining more attention in the recent years. One can use these languages to represent business logic in the domain classes. BNL's are powerful in the sense that they can be used to capture business specifications, documenting the business rules, and as executable code as well. They can also be used to create test cases to verify the system works as expected.

# References

https://www.infoq.com/articles/ddd-in-practice/

Domain-Driven Design, Tackling Complexity in the Heart of Software, Eric Evans, Addison Wesley

Applying Domain-Driven Design and Patterns, Jimmy Nilsson, Addison Wesley

Refactoring to Patterns, Joshua Kerievsky, Addison Wesley

Can DDD be Adequately Implemented Without DI and AOP?