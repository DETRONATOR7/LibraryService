using LibraryService.BL.Interfaces;
using LibraryService.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryService.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
        {
            services.AddSingleton<IBookCrudService, BookCrudService>();
            services.AddSingleton<IMemberCrudService, MemberCrudService>();
            services.AddSingleton<IBorrowBookService, BorrowBookService>();

            return services;
        }
    }
}
