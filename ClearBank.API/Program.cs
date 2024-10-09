using Autofac;
using Autofac.Extensions.DependencyInjection;
using ClearBank.Application.Config;
using ClearBank.Application.Interfaces;
using ClearBank.Application.PaymentValidators;
using ClearBank.Application.Resolvers;
using ClearBank.Application.Services;
using ClearBank.Common.Enums;
using ClearBank.Infrastructure.Factories;
using ClearBank.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Ideally would move this to a service collection extension method so that this class doesn't get too crowded

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DataStoreSettings>(builder.Configuration.GetSection("DataStoreConfig"));

//Validators
builder.Services.AddScoped<IPaymentValidator, BacsPaymentValidator>();
builder.Services.AddScoped<IPaymentValidator, ChapsPaymentValidator>();
builder.Services.AddScoped<IPaymentValidator, FasterPaymentsValidator>();

//Resolvers
builder.Services.AddSingleton<IPaymentValidatorResolver, PaymentValidatorResolver>();

//Services
builder.Services.AddScoped<IPaymentService, PaymentService>();

//Factories
builder.Services.AddSingleton<IDataStoreFactory, DataStoreFactory>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<AccountDataStore>().Keyed<IDataStore>(DataStoreEnums.Account);
    containerBuilder.RegisterType<BackupAccountDataStore>().Keyed<IDataStore>(DataStoreEnums.Backup);
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
