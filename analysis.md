The CI log shows errors while restoring Administration.Application.UnitTest:

```
/home/runner/.../Administration.Application.UnitTest.csproj : error NU1202: Package FluentAssertions 1.3.0.1 is not compatible with net9.0 (.NETCoreApp,Version=v9.0).
```

This indicates that the project references packages that only support very old frameworks. Because the repo targets `net9.0` (see `<TargetFramework>net9.0</TargetFramework>` in `src/Core/Services/Administration/tests/Administration.Application.UnitTest/Administration.Application.UnitTest.csproj`), NuGet tries to resolve compatible versions. If a package doesn't specify a lower bound, restore may select the earliest version, such as `FluentAssertions 1.3.0.1` and `xunit 1.7.0`, which fail to support .NET 9.

Additionally the CI tried to restore projects from deleted paths:

```
Skipping project ".../src/Core/Services/Administration/Core/test/Administration.Domain/Administration.Domain.csproj" because it was not found.
```

This suggests stale references in the solution or build script.

To fix the tests, ensure the `.csproj` files point to existing projects and update all package references to versions that support `net9.0`. For example `FluentAssertions` >= 8.x and `xunit` >= 2.4.
