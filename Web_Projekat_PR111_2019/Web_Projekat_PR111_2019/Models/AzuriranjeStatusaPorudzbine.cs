using Web_Projekat_PR111_2019.Data;

namespace Web_Projekat_PR111_2019.Models
{
    public class AzuriranjeStatusaPorudzbine: BackgroundService
    {
        private readonly IServiceProvider services;
        private readonly ILogger<AzuriranjeStatusaPorudzbine> logger;

        public AzuriranjeStatusaPorudzbine(IServiceProvider services, ILogger<AzuriranjeStatusaPorudzbine> logger)
        {
            this.services = services;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Pokrenuto azuriranje porudzbina");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = services.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();


                        var porudzbineZaAzuriranje = dbContext.Porudzbine
                            .Where(p => p.StatusPorudzbine == StatusPorudzbine.UObradi && p.VrijemeIsporuke <= DateTime.Now)
                            .ToList();


                        foreach (var porudzbina in porudzbineZaAzuriranje)
                        {
                            porudzbina.StatusPorudzbine = StatusPorudzbine.Prihvacena;
                        }

                        await dbContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Greška prilikom ažuriranja .");
                }


                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }

            logger.LogInformation("Kraj azuriranja");
        }
    }
}
