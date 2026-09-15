using System.Text; 
using Azure.Identity; 
using dotnetapp.Data; 
using dotnetapp.Models; 
using dotnetapp.Services; 
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.IdentityModel.Tokens;
var b=WebApplication.CreateBuilder(args); 
var kv=b.Configuration["KeyVaultConfiguration:URL"];
 if(!string.IsNullOrWhiteSpace(kv))b.Configuration.AddAzureKeyVault(new Uri(kv),new DefaultAzureCredential());
var cs=b.Configuration.GetConnectionString("DefaultConnection")??b.Configuration["sqlconnectionstring"]??throw new InvalidOperationException("SQL connection string missing");
b.Services.AddDbContext<ApplicationDbContext>(o=>o.UseSqlServer(cs));
b.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
b.Services.AddScoped<IAuthService,AuthService>();b.Services.AddScoped<ICakeService,CakeService>();
b.Services.AddControllers();b.Services.AddEndpointsApiExplorer();b.Services.AddSwaggerGen();
b.Services.AddCors(o=>o.AddDefaultPolicy(p=>p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var key=Encoding.UTF8.GetBytes(b.Configuration["JWT:Secret"]??throw new InvalidOperationException("JWT secret missing"));b.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o=>{o.TokenValidationParameters=new(){ValidateIssuer=true,ValidateAudience=true,ValidateLifetime=true,ValidateIssuerSigningKey=true,ValidIssuer=b.Configuration["JWT:ValidIssuer"],ValidAudience=b.Configuration["JWT:ValidAudience"],IssuerSigningKey=new SymmetricSecurityKey(key)};});
var app=b.Build();
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();