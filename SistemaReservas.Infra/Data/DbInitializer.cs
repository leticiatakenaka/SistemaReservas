using Microsoft.AspNetCore.Identity;
using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Enums;
using SistemaReservas.Infrastructure.Context;

namespace SistemaReservas.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            var roles = new[] { "Host", "Guest" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }

            var maria = await CreateUser(userManager, "Maria", "Santos", "maria@host.com", UserType.Host);

            var roberto = await CreateUser(userManager, "Roberto", "Almeida", "roberto@host.com", UserType.Host);

            var joao = await CreateUser(userManager, "João", "Silva", "joao@guest.com", UserType.Guest);


            if (!context.Properties.Any())
            {
                var listaPropriedades = new List<Property>
                {
                    new Property
                    {
                        Id = Guid.NewGuid(),
                        HostId = maria.Id,
                        Title = "Sítio Recanto da Paz",
                        Description = "Lugar incrível com cachoeira e churrasqueira. Perfeito para programadores cansados.",
                        PricePerNight = 450.00m,
                        MaxGuests = 10,
                        City = "Brumadinho",
                        State = "MG",
                        Address = "Estrada Real, km 50",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },

                    new Property
                    {
                        Id = Guid.NewGuid(),
                        HostId = roberto.Id,
                        Title = "Cabana Alpina na Montanha",
                        Description = "Chalé de madeira com lareira, vista para as araucárias e muita privacidade.",
                        PricePerNight = 850.00m,
                        MaxGuests = 2,
                        City = "Monte Verde",
                        State = "MG",
                        Address = "Rua das Araucárias, 120",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },

                    new Property
                    {
                        Id = Guid.NewGuid(),
                        HostId = maria.Id,
                        Title = "Casa Pé na Areia - Búzios",
                        Description = "Casa ampla com 4 suítes, piscina e saída direto para o mar.",
                        PricePerNight = 1200.00m,
                        MaxGuests = 8,
                        City = "Armação dos Búzios",
                        State = "RJ",
                        Address = "Praia de Geribá, Lote 4",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },

                    new Property
                    {
                        Id = Guid.NewGuid(),
                        HostId = roberto.Id,
                        Title = "Chácara Lazer Completo",
                        Description = "Campo de futebol, piscina aquecida e área gourmet. Ideal para famílias.",
                        PricePerNight = 600.00m,
                        MaxGuests = 12,
                        City = "Atibaia",
                        State = "SP",
                        Address = "Rodovia Fernão Dias, km 40",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    },

                    new Property
                    {
                        Id = Guid.NewGuid(),
                        HostId = maria.Id,
                        Title = "Casarão Colonial Histórico",
                        Description = "Hospede-se no coração do centro histórico com todo luxo e conforto.",
                        PricePerNight = 350.00m,
                        MaxGuests = 4,
                        City = "Ouro Preto",
                        State = "MG",
                        Address = "Rua Direita, 33",
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    }
                };

                context.Properties.AddRange(listaPropriedades);
                await context.SaveChangesAsync();
            }

            var sitioPrincipal = context.Properties.FirstOrDefault(p => p.Title == "Sítio Recanto da Paz");

            if (sitioPrincipal != null && !context.Bookings.Any())
            {
                var bookings = new List<Booking>
                {
                    new Booking
                    {
                        Id = Guid.NewGuid(), PropertyId = sitioPrincipal.Id, GuestId = joao.Id,
                        CheckInDate = DateTime.UtcNow.AddMonths(-2), CheckOutDate = DateTime.UtcNow.AddMonths(-2).AddDays(5),
                        TotalPrice = 2250.00m, Status = BookingStatus.Completed, CreatedAt = DateTime.UtcNow.AddMonths(-3)
                    },
                    new Booking
                    {
                        Id = Guid.NewGuid(), PropertyId = sitioPrincipal.Id, GuestId = joao.Id,
                        CheckInDate = DateTime.UtcNow.AddDays(10), CheckOutDate = DateTime.UtcNow.AddDays(15),
                        TotalPrice = 2250.00m, Status = BookingStatus.Confirmed, CreatedAt = DateTime.UtcNow.AddDays(-2)
                    }
                };
                context.Bookings.AddRange(bookings);
                await context.SaveChangesAsync();
            }
        }

        private static async Task<ApplicationUser> CreateUser(
            UserManager<ApplicationUser> userManager,
            string nome, string sobrenome, string email, UserType tipo)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser(nome, sobrenome, email, tipo);
                await userManager.CreateAsync(user, "Dev@1234");
                await userManager.AddToRoleAsync(user, tipo.ToString());
            }
            return user;
        }
    }
}