using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Cavu.Api.Services;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersionedApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });
builder.Services.AddApiVersioning(options =>
{
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IPricingService, PricingService>();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddTransient<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICarParkRepository, CarParkRepository>();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("api/Health");


app.UseSwagger();
app.UseSwaggerUI(swaggerUiOptions =>
{
    swaggerUiOptions.DisplayOperationId();

    var provider = app.Services.GetService<IApiVersionDescriptionProvider>();

    foreach (var description in provider.ApiVersionDescriptions
                 .OrderByDescending(x => x.ApiVersion.MajorVersion)
                 .ThenByDescending(x => x.ApiVersion.MinorVersion))
    {
        swaggerUiOptions.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"API {description.GroupName.ToUpperInvariant()}");
    }
});

app.Run();
