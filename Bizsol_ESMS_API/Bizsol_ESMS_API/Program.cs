using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Service;
using Microsoft.Graph;
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

builder.Services.AddHttpContextAccessor();
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
builder.Services.AddTransient<IUserMaster, UserMasterService>();
builder.Services.AddTransient<IDesignationMaster, DesignationMasterService>();
builder.Services.AddTransient<IUserGroupMaster, UserGroupMasterService>();
builder.Services.AddTransient<IConfigItemMaster, ConfigItemMasterService>();
builder.Services.AddTransient<ICity, CityMasterService>();
builder.Services.AddTransient<IStateMaster,StateMasterServices>();
builder.Services.AddTransient<ICustomerType,CustomerTypeService>();
builder.Services.AddTransient<ICurrentDate,CurrentDateService>();
builder.Services.AddTransient<IOrder,OrderService>();
builder.Services.AddTransient<IMRNMaster,MRNMasterService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
