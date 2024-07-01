using Microsoft.Extensions.Options;
using Storage.Application.Features;
using Storage.Application.Models;
using Storage.CoreInfrastructure.CoreFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Storage.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileStorageSettings>(configuration.GetSection(nameof(FileStorageSettings)));

        services.AddSingleton<IStorageService>(sp =>
        {
            var storageSettings = sp.GetRequiredService<IOptions<FileStorageSettings>>();
            if (storageSettings.Value.DefaultStorage == nameof(StorageType.Local))
            {
                return new LocalStorageService(storageSettings);
            }
            else if (storageSettings.Value.DefaultStorage == nameof(StorageType.AzureBlob))
            {
                return new AzureBlobStorageService(storageSettings);
            }
            else if (storageSettings.Value.DefaultStorage == nameof(StorageType.AWS))
            {
                return new AWSStorageService();
            }
            else
            {
                throw new NotSupportedException("Storage provider not supported.");
            }
        });

        services.AddScoped<IFileManagerService, FileManagerService>();

        #region   Jwt

        var jwtSettingsSection = configuration.GetSection(nameof(JwtSettings));
        var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
            };
        });
        services.AddAuthorization();

        #endregion
        return services;
    }
}