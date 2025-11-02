using Sentinel.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddSentinel();

var app = builder.Build();
app.UseSentinel();

await app.RunAsync();
