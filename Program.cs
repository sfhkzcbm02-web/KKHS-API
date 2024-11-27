using AutoMapper;
using KKHS.Service.UserApp.Service;
using KKHS_API;
using KKHS_API.EFcore;
using KKHS_API.helper.JWT;
using KKHS_API.LayoutApp.Interface;
using KKHS_API.LayoutApp.Service;
using KKHS_API.OrderApp.Interface;
using KKHS_API.OrderApp.Service;
using KKHS_API.ShoppingCartApp.Interface;
using KKHS_API.ShoppingCartApp.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApidataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtDatabase")));
//builder.Services.AddDbContext<ApidataContext>(options =>
//options.UseSqlServer(builder.Configuration.GetValue<string>("DATABASE_CONNECTION_STRING"))
//);

//相關服務DI註冊
builder.Services.AddScoped<KKHS.Service.UserApp.IUserService, UserService>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(option => { option.AddProfile<AutomapProfile>();});
//JWT配置,授權
builder.Services.Configure<JWToption>(builder.Configuration.GetSection("JWToption"));
builder.Services.AddTransient<IJWTservice, JWTservice>();
JWToption _JWToption = new JWToption();
builder.Configuration.Bind("JWToption", _JWToption);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option =>
{
     option.TokenValidationParameters = new TokenValidationParameters()
   {
       ValidateIssuer = true,
       ValidateAudience = true,
       ValidateLifetime = true,
       ValidateIssuerSigningKey = true,
       ValidAudience = _JWToption.Audience,
       ValidIssuer = _JWToption.Issuer,
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWToption.SecurityKey!))
    };

});
//http 上下文註冊
builder.Services.AddHttpContextAccessor();

//配置swagger 傳token



//跨域
builder.Services.AddCors(option =>
{
    option.AddPolicy("any", opt => opt
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("any");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
