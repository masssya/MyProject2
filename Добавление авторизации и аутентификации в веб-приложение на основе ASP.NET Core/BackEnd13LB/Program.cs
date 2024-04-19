using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

var PeopleBD = new List<Person>
{
    new Person("�����", "123123", false),
    new Person("����", "654321", false),
    new Person("�������� �����", "����������", true)
};

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // �������������� 
    .AddJwtBearer(
        options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = TocenOptions.ISSUER,
                ValidateAudience = true,
                ValidAudience = TocenOptions.AUDIENCE,
                ValidateLifetime = false,
                IssuerSigningKey = TocenOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };
        }

    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapPost("/login", (Person Now_person) =>
{
    Person? person = PeopleBD.FirstOrDefault(p => p.Name == Now_person.Name && p.Password == Now_person.Password);

    if (person is null) return Results.Unauthorized(); // ���� ���� ������������� 0 - ������ (�� ������)

    var claims = new List<Claim> // ������� ������ �������
    {
        new Claim(ClaimTypes.Name, person.Name),
        person.IsAdmin == true ? new Claim(ClaimTypes.Role, "admin") : new Claim(ClaimTypes.Role, "user") // ��������, �������� �� ������������ �������, ���� �� - ��������� ���� ������
    };

    var jwt = new JwtSecurityToken( // �������� ������
        issuer: TocenOptions.ISSUER,
        audience: TocenOptions.AUDIENCE,
        claims: claims,
        signingCredentials: new SigningCredentials(TocenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    var response = new
    {
        access_token = encodedJwt,
        prava = person.IsAdmin
    };

    return Results.Json(response); // � ���� json ���������� ����� � ������� �������
});

app.Map("/all", () => "Home Page"); // � ���� ����� ���� ������ � ���� �������������

app.Map("/data", [Authorize] () => "����� ����������, �� ������������!"); //��� �������������� ������������

app.Map("/admin", [Authorize(Roles = "admin")] () => "�� �������������, ������� ���, ��� ��������"); // ����� ����� � �������� ������


//app.MapControllerRoute( // �������� �������� �� ���������
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}");

//app.MapControllerRoute(
//    name: "id",
//    pattern: "{controller}/{action}/{id:int}"); // �������� ����������� � �������������� �������

//app.MapControllerRoute( // ����� ����� ���������� ����������� ����� �������� constraints
//    name: "id_constraints",
//    pattern: "{controller=Home}/{action=Index}/{id?}",
//    constraints: new { id = new IntRouteConstraint() });

app.Run();


public class Person
{
    public Person(string name, string password, bool isAdmin)
    {
        Name = name;
        Password = password;
        IsAdmin = isAdmin;
    }

    public string Name {  get; set; }
    public string Password {  get; set; }
    public bool IsAdmin {  get; set; }
}

public class TocenOptions
{
    public const string ISSUER = "lubaya"; 
    public const string AUDIENCE = "stroka"; 
    const string KEY = "hello_allinthis_world_thatsnotallbaybe!@55652"; // ���� ����������
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}