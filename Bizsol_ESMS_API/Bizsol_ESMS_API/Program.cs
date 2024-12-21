using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//JSON Serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options =>
options.SerializerSettings.ContractResolver = new DefaultContractResolver());

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    DateTimeZoneHandling = DateTimeZoneHandling.Local
};

builder.Services.AddControllers();
builder.Services.AddTransient<IUOM, UOMService>();
builder.Services.AddTransient<IDropDown, DropDownService>();
builder.Services.AddTransient<ILocationMaster, LocationMasterService>();
builder.Services.AddTransient<IGroupMaster, GroupMasterService>();
builder.Services.AddTransient<ISubGroupMaster, SubGroupMasterService>();
builder.Services.AddTransient<ICategory, CategoryService>();
builder.Services.AddTransient<IBrandMaster, BrandMasterService>();
builder.Services.AddTransient<IWarehouse, WarehouseService>();
builder.Services.AddTransient<IItemMaster, ItemMasterService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
