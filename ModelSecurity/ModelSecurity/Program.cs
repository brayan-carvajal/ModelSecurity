using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelSecurity.Data;
using ModelSecurity.Interfaces;
using ModelSecurity.Repositories;
using ModelSecurity.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "ClavePorDefectoInsegura");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<IFormModuleRepository, FormModuleRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IRolUserRepository, RolUserRepository>();
builder.Services.AddScoped<IRolFormPermitRepository, RolFormPermitRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<IFormModuleService, FormModuleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IRolUserService, RolUserService>();
builder.Services.AddScoped<IRolFormPermitService, RolFormPermitService>();

// Generador de tokens
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activar autenticación y autorización con JWT
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ======================================================
// 🔹 Crear datos por defecto (usuario Administrador)
// ======================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        // 1️⃣ Crear Person si no existe
        if (!context.Person.Any())
        {
            var person = new ModelSecurity.Models.Person
            {
                FirstName = "Admin",
                LastName = "System",
                Document_type = "CC",
                Document = "999999999",
                DateBorn = DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc),
                PhoneNumber = "3000000000",
                Gender = "M",
                PersonExter = "N/A",
                EpsId = "SURA",
                SecondLastName = "Root",
                MiddleName = "Main",
                Active = true,
                CityId = 1
            };
            context.Person.Add(person);
            context.SaveChanges();
        }

        var personAdmin = context.Person.First();

        // 2️⃣ Crear User si no existe
        if (!context.User.Any(u => u.Email == "admin@system.com"))
        {
            var user = new ModelSecurity.Models.User
            {
                Email = "admin@system.com",
                Password = "Admin123!", // ⚠️ Solo para desarrollo. En prod, usa hash (BCrypt)
                Active = true,
                RegistrationDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                IsDeleted = false,
                PersonId = personAdmin.Id
            };
            context.User.Add(user);
            context.SaveChanges();
        }

        var userAdmin = context.User.First(u => u.Email == "admin@system.com");

        // 3️⃣ Crear Rol Administrador si no existe
        if (!context.Rol.Any(r => r.Name == "Administrador"))
        {
            var rol = new ModelSecurity.Models.Rol
            {
                Name = "Administrador",
                Description = "Rol con acceso completo al sistema",
                UserId = userAdmin.Id,
                IsDeleted = false
            };
            context.Rol.Add(rol);
            context.SaveChanges();
        }

        var rolAdmin = context.Rol.First(r => r.Name == "Administrador");

        // 4️⃣ Asociar el rol al usuario (RolUser)
        if (!context.RolUser.Any(ru => ru.UserId == userAdmin.Id && ru.RolId == rolAdmin.Id))
        {
            var rolUser = new ModelSecurity.Models.RolUser
            {
                UserId = userAdmin.Id,
                RolId = rolAdmin.Id,
                IsDeleted = false
            };
            context.RolUser.Add(rolUser);
            context.SaveChanges();
        }

        Console.WriteLine("✅ Usuario administrador creado o verificado correctamente.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error creando usuario administrador: {ex.Message}");
    }
}


app.Run();
