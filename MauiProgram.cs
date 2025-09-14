using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ManagerProduct
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});
			
			builder.Services.AddDbContext<AppDbContext>( option =>
			{
				option.UseSqlite($"Filename={System.IO.Path.Combine(FileSystem.AppDataDirectory, "products.db")}");
            }
				);
			builder.Services.AddTransient<MainPage>();


#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}


	}
}
