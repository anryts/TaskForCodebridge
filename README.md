# TaskForCodebridge

# Provide into appsettings.Development.json\DefaultConnection your conneciton to DB.

In this API, two software patterns were utilized:

1. The Repository pattern was implemented to facilitate the mocking of dependencies in tests.
2. The CQRS pattern with Mediator was employed to separate Commands (such as creating dogs) from Queries (such as retrieving dogs).

To ensure request validation, the FluentValidation library was used.

To handle the issue of multiple dogs with the same name, it is not ideal to use the dog's name as the primary key. Instead, a better approach would be to use an ID field, such as a GUID or an integer, as the primary key.

Alternatively, we can create a composite key using multiple properties, such as the name and color of the dog. This approach allows us to uniquely identify dogs based on a combination of attributes, avoiding conflicts when multiple dogs share the same name.

All business logic was covered by unit tests.
