using WebHookPoc.WebHook;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IEgress, Egress>();
builder.Services.AddHttpClient("GalaxyAPI", client =>
{
    client.BaseAddress = new System.Uri( builder.Configuration["GalaxyAPI:EndPoint"]);
    client.DefaultRequestHeaders.Add("Accept", "*/*");
    client.DefaultRequestHeaders.Add("ContentType", "application/json");
    client.DefaultRequestHeaders.Add("Api-Key", builder.Configuration["GalaxyAPI:APIkey"]);

});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Urls.Add(builder.Configuration["Server:OnlineEndPoint"]);
app.Urls.Add(builder.Configuration["Server:LocalEndPoint"]);
//app.Urls.Add(builder.Configuration["Server:LocalHttpsEndPoint"]);

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
Console.WriteLine($"App is running @ {builder.Configuration["Server:OnlineEndPoint"]} and {builder.Configuration["Server:LocalEndPoint"]}");
app.Run();
