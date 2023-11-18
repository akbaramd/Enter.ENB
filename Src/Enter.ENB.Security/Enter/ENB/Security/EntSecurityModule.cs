using System.Text;
using Enter.ENB.Modularity;
using Enter.ENB.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enter.ENB.Security;

public class EntSecurityModule : EntModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        if (context.Services.IsAdded<IConfiguration>())
        {
            var configuration = context.Services.GetConfiguration();
            context.Services.Configure<EntStringEncryptionOptions>(options =>
            {
                var keySize = configuration["StringEncryption:KeySize"];
                if (!keySize.IsNullOrWhiteSpace())
                {
                    if (int.TryParse(keySize, out var intValue))
                    {
                        options.Keysize = intValue;
                    }
                }

                var defaultPassPhrase = configuration["StringEncryption:DefaultPassPhrase"];
                if (!defaultPassPhrase.IsNullOrWhiteSpace())
                {
                    options.DefaultPassPhrase = defaultPassPhrase!;
                }

                var initVectorBytes = configuration["StringEncryption:InitVectorBytes"];
                if (!initVectorBytes.IsNullOrWhiteSpace())
                {
                    options.InitVectorBytes = Encoding.ASCII.GetBytes(initVectorBytes!);
                }

                var defaultSalt = configuration["StringEncryption:DefaultSalt"];
                if (!defaultSalt.IsNullOrWhiteSpace())
                {
                    options.DefaultSalt = Encoding.ASCII.GetBytes(defaultSalt!);
                }
            });
        }
        
    }
}