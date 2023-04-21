using API.Models;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
    j => j.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<OvoshebazaContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
RSACryptoServiceProvider RsaKey = new RSACryptoServiceProvider();
string publickey = RsaKey.ToXmlString(false); //получим открытый ключ
string privatekey = RsaKey.ToXmlString(true); //получим закрытый ключ
//"<RSAKeyValue><Modulus>qLSks6XFKN/iEPcvwWTJ4ghf/9gNLx7hCw7D2Y3j0ARNGmLqiBULAVnDTJ2iZhzzebsD1kaaMp2GRAROPHH/OwwD2C3x8rQQCl1VOKzzOQ1h+rNuAgezkPHVaXCu1OCuwURnTpqs09L3xVQitD1ZByxOxgZ0OzRKjUqpdwXXMfk=</Modulus><Exponent>AQAB</Exponent><P>0kkXifAB65p9o6Bf5F21Vs7jNcYa7s9WL3Acsu3rN0tA3nie3dWndEdPTEWUW+tsLyqFoFQcJzM5sK4DSpPwjw==</P><Q>zWGHUnMGxvmEWG2pzULSI53JvMuzV585VMzABSFEkGyYtXnpHVznrJK3DXrXD3kOnF0ZWnyT4MJuGfoxgqBo9w==</Q><DP>C8NO78ZfNSC1Onv0IUAkrrBwAUgNpaIvfgPVdyTb7YHmJQu2R052SYjbpLaXr/ShXpoQU4Gg+YhiB8IUKQ3RfQ==</DP><DQ>YXvQYmcsqVcX5W0v8riry7ICZnV9i7KM4N5KqmSvCaoyFblm18QYRwZgkqpi1/pK4BckiJmnC0DeR8BErc774w==</DQ><InverseQ>jt1UUisSEWNhqak5RO3vCOgt3k++QF7LBYZ5UELmfUqTk9sAqCfaziRddi9o601mWYfXLMr9hQ0naKbo70x2Aw==</InverseQ><D>nrZraFLgvAZ78EgMFl3Si6IjZlcEeDsNrlBysf4Jv038l4FNcT6Svu+Ki06VVImSCQiGoJSFRm7pvJ1sWPNKD+S9v+3ZjI5e+KdDx5BLCKHfwwlYYBxH5gMMC/84uaJVzjw0cBPifDQxRdTal/Vlopb9ZN9POYQ4TjxxcrJDiW0=</D></RSAKeyValue>"
RSACryptoServiceProvider RsaKey2 = new RSACryptoServiceProvider();
RsaKey2.FromXmlString(publickey);
//privatekey = RsaKey2.ToXmlString(true); //получим закрытый ключ
byte[] EncryptedData;
byte[] data = new byte[1024];
data = Encoding.UTF8.GetBytes("Hello");
EncryptedData = RsaKey2.Encrypt(data, false);

byte[] DecryptedData;
DecryptedData = RsaKey.Decrypt(EncryptedData, false);
string a = Encoding.UTF8.GetString(DecryptedData);


app.Run();


