using Akgunler.Models;
using Akgunler.Models.Core;
using Akgunler.Models.Jobs;
using Akgunler.Models.Staffs;
using Akgunler.Models.Vehicles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Data
{
    public class DataSeeder
    {
        public static void Init(IServiceProvider serviceProvider)
        {

            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AkgunlerContext>();
            context.Database.EnsureCreated();

            SeedTypes(context);
            SeedUsers(context);
        }
        private static void SeedUsers(AkgunlerContext context)
        {
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Username = "admin",
                    Password = "0okm0okm",
                    FirstName = "Yakup",
                    LastName = "Kocaman",
                    IsActive = true
                };
                context.Add(user);
                context.SaveChanges();

                var userRole = new UserRole { UserId = user.Id, RoleId = 3 };
                context.Add(userRole);
                context.SaveChanges();
            }
        }

        private static void SeedTypes(AkgunlerContext context)
        {
            if (!context.Groups.Any())
            {
                var items = new List<Group>
                {
                    new Group { Name = "Bireysel" },
                    new Group { Name = "Kurumsal" },
                };
                context.AddRange(items);
            }

            if (!context.Makes.Any())
            {
                var items = new List<Make>
                {
                    new Make { Name = "BMC" },
                    new Make { Name = "DAF" },
                    new Make { Name = "Lincoln" },
                    new Make { Name = "Fiat" },
                    new Make { Name = "Ford" },
                    new Make { Name = "Iveco" },
                    new Make { Name = "MAN" },
                    new Make { Name = "Mercedes - Benz" },
                    new Make { Name = "Renault" },
                    new Make { Name = "Scania" },
                    new Make { Name = "Volvo" },
                };
                context.AddRange(items);
            }

            if (!context.Transmissions.Any())
            {
                var items = new List<Transmission>
                {
                    new Transmission { Name = "Manuel" },
                    new Transmission { Name = "Otomatik" },
                    new Transmission { Name = "Yarı Otomatik" },
                };
                context.AddRange(items);
            }

            if (!context.Fuels.Any())
            {
                var items = new List<Fuel>
                {
                    new Fuel { Name = "Benzin" },
                    new Fuel { Name = "Dizel" },
                };
                context.AddRange(items);
            }

            if (!context.VehicleTypes.Any())
            {
                var items = new List<VehicleGroup>
                {
                    new VehicleGroup { Name = "Çekici" },
                    new VehicleGroup { Name = "Dorse" },
                };
                context.AddRange(items);
            }

            if (!context.VehicleTypes.Any())
            {
                var items = new List<VehicleType>
                {
                    new VehicleType { VehicleGroupId = 1, Name = "Frigo" },
                    new VehicleType { VehicleGroupId = 1, Name = "Standart" },
                    new VehicleType { VehicleGroupId = 1, Name = "Mega" },
                    new VehicleType { VehicleGroupId = 1, Name = "Sal" },
                    new VehicleType { VehicleGroupId = 1, Name = "Uzatmalı Sal" },
                    new VehicleType { VehicleGroupId = 1, Name = "Römorklu" },
                    new VehicleType { VehicleGroupId = 1, Name = "Box" },
                    new VehicleType { VehicleGroupId = 1, Name = "Lowbed" },
                };
                context.AddRange(items);
            }

            if (!context.Colors.Any())
            {
                var items = new List<Color>
                {
                    new Color { Name = "Bej" },
                    new Color { Name = "Beyaz" },
                    new Color { Name = "Bordo" },
                    new Color { Name = "Füme" },
                    new Color { Name = "Gri" },
                    new Color { Name = "Gümüş Gri" },
                    new Color { Name = "Kahverengi" },
                    new Color { Name = "Kırmızı" },
                    new Color { Name = "Lacivert" },
                    new Color { Name = "Mavi" },
                    new Color { Name = "Mor" },
                    new Color { Name = "Pembe" },
                    new Color { Name = "Sarı" },
                    new Color { Name = "Siyah" },
                    new Color { Name = "Turkuaz" },
                    new Color { Name = "Turuncu" },
                    new Color { Name = "Yeşil" }
                };
                context.AddRange(items);
            }

            if (!context.Languages.Any())
            {
                var items = new List<Language>
                {
                    new Language { Code = "en", Name = "İngilizce" },
                    new Language { Code = "de", Name = "Almanca" },
                    new Language { Code = "fr", Name = "Fransızca" },
                    new Language { Code = "ru", Name = "Rusça" },
                    new Language { Code = "ar", Name = "Arapça" },
                };
                context.AddRange(items);
            }

            if (!context.AddressTypes.Any())
            {
                var items = new List<AddressType>
                {
                    new AddressType { AddressGroup = AddressGroup.Customer, Name = "Fatura Adresi" },
                    new AddressType { AddressGroup = AddressGroup.Customer, Name = "İletişim Adresi" },

                    new AddressType { AddressGroup = AddressGroup.Staff, Name = "Varsayılan" },

                    new AddressType { AddressGroup = AddressGroup.Job, Name = "Limanlar" },

                };
                context.AddRange(items);
            }

            if (!context.Currencies.Any())
            {
                var items = new List<Currency>
                {
                    new Currency { Name = "Türk Lirası", ShortName = "TRY", Sign = "₺" },
                    new Currency { Name = "Amerikan Doları", ShortName = "USD", Sign = "$" },
                    new Currency { Name = "İngiliz Sterlini", ShortName = "GBP", Sign = "£" },
                    new Currency { Name = "Euro", ShortName = "EUR", Sign = "€" },
                };
                context.AddRange(items);
            }

            if (!context.Departments.Any())
            {
                var items = new List<Department>
                {
                    new Department { Name = "Bilgi İşlem" },
                    new Department { Name = "Kadrolu Sürücüler" },
                    new Department { Name = "Operasyon" },
                    new Department { Name = "Yönetim" }
                };
                context.AddRange(items);
            }

            if (!context.DriversLicenseTypes.Any())
            {
                var items = new List<DriversLicenseType>
                {
                    new DriversLicenseType { Name = "Yok" },
                    new DriversLicenseType { Name = "A" },
                    new DriversLicenseType { Name = "A1" },
                    new DriversLicenseType { Name = "B" },
                    new DriversLicenseType { Name = "BE" },
                    new DriversLicenseType { Name = "C" },
                    new DriversLicenseType { Name = "C1" },
                    new DriversLicenseType { Name = "C1E" },
                    new DriversLicenseType { Name = "CE" },
                    new DriversLicenseType { Name = "D" },
                    new DriversLicenseType { Name = "D1" },
                    new DriversLicenseType { Name = "D1E" },
                    new DriversLicenseType { Name = "DE" }
                };
                context.AddRange(items);
            }

            if (!context.Roles.Any())
            {
                var items = new List<Role>
                {
                    new Role { Name = "admin", Title = "Yönetici"},
                    new Role { Name = "editor", Title = "Editor" },
                    new Role { Name = "user", Title = "Kullanıcı" }
                };
                context.AddRange(items);
            }

            if (!context.DocumentTypes.Any())
            {
                var items = new List<DocumentType>
                {
                    new DocumentType { DocumentGroup = DocumentGroup.Customer, Name = "Ceza Makbuzu" },
                    new DocumentType { DocumentGroup = DocumentGroup.Customer, Name = "Ehliyet" },
                    new DocumentType { DocumentGroup = DocumentGroup.Customer, Name = "Kimlik" },
                    new DocumentType { DocumentGroup = DocumentGroup.Customer, Name = "Pasaport" },
                    new DocumentType { DocumentGroup = DocumentGroup.Customer, Name = "Sözleşme" },

                    new DocumentType { DocumentGroup = DocumentGroup.Vehicle, Name = "Bakım Faturası" },
                    new DocumentType { DocumentGroup = DocumentGroup.Vehicle, Name = "Benzin Fişi" },
                    new DocumentType { DocumentGroup = DocumentGroup.Vehicle, Name = "Ceza Makbuzu" },
                    new DocumentType { DocumentGroup = DocumentGroup.Vehicle, Name = "Ruhsat" },

                    new DocumentType { DocumentGroup = DocumentGroup.Staff, Name = "Ceza Makbuzu" },
                    new DocumentType { DocumentGroup = DocumentGroup.Staff, Name = "Ehliyet" },
                    new DocumentType { DocumentGroup = DocumentGroup.Staff, Name = "Kimlik" },
                    new DocumentType { DocumentGroup = DocumentGroup.Staff, Name = "Pasaport" }
                };
                context.AddRange(items);
            }

            if (!context.AccountTypes.Any())
            {
                var items = new List<AccountType>
                {
                    new AccountType { Name = "Havale - EFT" },
                    new AccountType { Name = "Kredi Kartı" },
                    new AccountType { Name = "Nakit" }
                };
                context.AddRange(items);
            }

            if (!context.JobTypes.Any())
            {
                var items = new List<JobType>
                {
                    new JobType { Name = "Sefer" },
                };
                context.AddRange(items);
            }


            if (!context.Ships.Any())
            {
                var items = new List<Ship>
                {
                    new Ship { Name = "Via Mare" },
                    new Ship { Name = "Via Famagusta" },
                };
                context.AddRange(items);
            }

            if (!context.Ports.Any())
            {
                var items = new List<Port>
                {
                    new Port { Name = "Girne Limanı", DistrictId = 1141 },
                    new Port { Name = "Mağusa Limanı", DistrictId = 995 },
                    new Port { Name = "Taşucu Limanı", DistrictId = 717 },
                    new Port { Name = "Mersin Limanı", DistrictId = 708},
                };
                context.AddRange(items);
            }

            if (!context.Customs.Any())
            {
                var items = new List<Customs>
                {
                    new Customs { Name = "Erenköy", DistrictId = 450 },
                    new Customs { Name = "Halkalı", DistrictId = 473 },
                    new Customs { Name = "Mersin", DistrictId = 708 },
                    new Customs { Name = "İzmir", DistrictId = 1126 },
                    new Customs { Name = "Ankara", DistrictId = 72 },
                    new Customs { Name = "Antalya", DistrictId = 104 },
                    new Customs { Name = "Bursa", DistrictId = 229 },
                    new Customs { Name = "Gebze", DistrictId = 617 },
                    new Customs { Name = "Derince", DistrictId = 615 }
                };
                context.AddRange(items);
            }

            context.SaveChanges();
        }
    }
}
