using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Security.Cryptography;
using Narzedzia.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Narzedzia.Data
{
    public class NarzedziaSeeder
    {
        public static Random r = new Random();
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))

                if (dbContext.Database.CanConnect())
                {
                    SeedRoles(dbContext);
                    SeedStanowisko(dbContext);
                    SeedWydzial(dbContext);
                    SeedKategoria(dbContext);
                    SeedProducent(dbContext);
                    SeedUsers(dbContext);
                    SeedNarzedzie(dbContext);
                    SeedAwaria(dbContext);
                }
        }

        private static void SeedAwaria(ApplicationDbContext dbContext)
        {
            // 1
            if (!dbContext.Awarie.Any(a => a.IdAwaria == 1))
            {
                var awaria1 = new Awaria
                {
                    NarzedzieId = 1, 
                    DescriptionAwaria = "Przykładowy opis awarii, który musi mieć co najmniej 20 znaków.",
                    NumberAwaria = "123456789",
                    DataPrzyjecia = DateTime.Today,
                    UzytkownikId = LosowyUser(dbContext), 
                    Status = StatusAwaria.nowe, 
                    UzytkownikRealizujacyId = null, 
                    StanowiskoId = 1, 
                    WydzialId = 1 
                };

                dbContext.Awarie.Add(awaria1);
                dbContext.SaveChanges();
            }
        }

        private static void SeedRoles(ApplicationDbContext dbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            if (!dbContext.Roles.Any(r => r.Name == "admin"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                }).Wait();
            }
            if (!dbContext.Roles.Any(r => r.Name == "nadzor"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "nadzor",
                    NormalizedName = "nadzor"
                }).Wait();
            }
            if (!dbContext.Roles.Any(r => r.Name == "pracownik"))
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "pracownik",
                    NormalizedName = "pracownik"
                }).Wait();
            }
        }

        private static void SeedUsers(ApplicationDbContext dbContext)
        {
            //admin
            if (!dbContext.Users.Any(u => u.UserName == "konrad@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "konrad@narzedzia.pl",
                    NormalizedUserName = "konrad@narzedzia.pl",
                    Email = "konrad@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Konrad",
                    Nazwisko = "Wiankowski",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 3,
                    StanowiskoId = 3,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "admin").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "kamil@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "kamil@narzedzia.pl",
                    NormalizedUserName = "kamil@narzedzia.pl",
                    Email = "kamil@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Kamil",
                    Nazwisko = "Racki",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 1,
                    StanowiskoId = 1,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "admin").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "damian@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "damian@narzedzia.pl",
                    NormalizedUserName = "damian@narzedzia.pl",
                    Email = "damian@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Damian",
                    Nazwisko = "Krysiewicz",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 1,
                    StanowiskoId = 1,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "admin").Wait();
                dbContext.SaveChanges();
            }

            //nadzór
            if (!dbContext.Users.Any(u => u.UserName == "tomek.wilary@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "tomek.wilary@narzedzia.pl",
                    NormalizedUserName = "tomek.wilary@narzedzia.pl",
                    Email = "tomek.wilary@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Tomek",
                    Nazwisko = "Wilary",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 1,
                    StanowiskoId = 1,
                };

                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "nadzor").Wait();
                dbContext.SaveChanges();

            }

            if (!dbContext.Users.Any(u => u.UserName == "marcin.wisniewski@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "marcin.wisniewski@narzedzia.pl",
                    NormalizedUserName = "marcin.wisniewski@narzedzia.pl",
                    Email = "marcin.wisniewski@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Marcin",
                    Nazwisko = "Wiśniewski",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 2,
                    StanowiskoId = 2,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "nadzor").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "mariusz.klekowiecki@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "mariusz.klekowiecki@narzedzia.pl",
                    NormalizedUserName = "mariusz.klekowiecki@narzedzia.pl",
                    Email = "mariusz.klekowiecki@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Mariusz",
                    Nazwisko = "Klekowiecki",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 2,
                    StanowiskoId = 2,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "nadzor").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "lukasz.kobierzycki@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "lukasz.kobierzycki@narzedzia.pl",
                    NormalizedUserName = "lukasz.kobierzycki@narzedzia.pl",
                    Email = "lukasz.kobierzycki@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Łukasz",
                    Nazwisko = "Kobierzycki",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 2,
                    StanowiskoId = 2,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "nadzor").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "pawel.kolas@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "pawel.kolas@narzedzia.pl",
                    NormalizedUserName = "pawel.kolas@narzedzia.pl",
                    Email = "pawel.kolas@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Paweł",
                    Nazwisko = "Kolas",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 2,
                    StanowiskoId = 2,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "nadzor").Wait();
                dbContext.SaveChanges();
            }

            //pracownik
            if (!dbContext.Users.Any(u => u.UserName == "marek.goc@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "marek.goc@narzedzia.pl",
                    NormalizedUserName = "marek.goc@narzedzia.pl",
                    Email = "marek.goc@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Marek",
                    Nazwisko = "Goc",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 1,
                    StanowiskoId = 1,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "Narzedzia1!");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "pracownik").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "mariusz.konieczny@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "mariusz.konieczny@narzedzia.pl",
                    NormalizedUserName = "mariusz.konieczny@narzedzia.pl",
                    Email = "mariusz.konieczny@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Mariusz",
                    Nazwisko = "Konieczny",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 1,
                    StanowiskoId = 2,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "1qaz@WSX");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "pracownik").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "artur.drozd@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "artur.drozd@narzedzia.pl",
                    NormalizedUserName = "artur.drozd@narzedzia.pl",
                    Email = "artur.drozd@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Artur",
                    Nazwisko = "Drozd",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 1,
                    StanowiskoId = 3,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "1qaz@WSX");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "pracownik").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "patryk.jaszczak@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "patryk.jaszczak@narzedzia.pl",
                    NormalizedUserName = "patryk.jaszczak@narzedzia.pl",
                    Email = "patryk.jaszczak@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Patryk",
                    Nazwisko = "Jaszczak",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 1,
                    StanowiskoId = 4,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "1qaz@WSX");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "pracownik").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "radoslaw.milczarek@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "radoslaw.milczarek@narzedzia.pl",
                    NormalizedUserName = "radoslaw.milczarek@narzedzia.pl",
                    Email = "radoslaw.milczarek@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Radosław",
                    Nazwisko = "Milczarek",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 2,
                    StanowiskoId = 5,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "1qaz@WSX");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "pracownik").Wait();
                dbContext.SaveChanges();
            }

            if (!dbContext.Users.Any(u => u.UserName == "andrzej.bombalicki@narzedzia.pl"))
            {
                var uzytkownik = new Uzytkownik
                {
                    UserName = "andrzej.bombalicki@narzedzia.pl",
                    NormalizedUserName = "andrzej.bombalicki@narzedzia.pl",
                    Email = "andrzej.bombalicki@narzedzia.pl",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    Imie = "Andrzej",
                    Nazwisko = "Bombalicki",
                    NrKontrolny = r.Next(10000, 40000),
                    WydzialId = 2,
                    StanowiskoId = 6,
                };
                var password = new PasswordHasher<Uzytkownik>();
                var hashed = password.HashPassword(uzytkownik, "1qaz@WSX");
                uzytkownik.PasswordHash = hashed;
                var userStore = new UserStore<Uzytkownik>(dbContext);
                userStore.CreateAsync(uzytkownik).Wait();
                userStore.AddToRoleAsync(uzytkownik, "pracownik").Wait();
                dbContext.SaveChanges();
            }
        }

        private static void SeedKategoria(ApplicationDbContext dbContext)
        {
            if (!dbContext.Kategorie.Any())
            {
                var kat = new List<Kategoria>
                {
                    new Kategoria {NazwaKategorii = "Frezarka", Active = true},                     // 1
                    new Kategoria {NazwaKategorii = "Stacja prób", Active = true},                  // 2
                    new Kategoria {NazwaKategorii = "Klucz elektryczny", Active = true},            // 3
                    new Kategoria {NazwaKategorii = "Komputer", Active = true},                     // 4
                    new Kategoria {NazwaKategorii = "Klucz akumulatorowy", Active = true},          // 5
                    new Kategoria {NazwaKategorii = "Miara", Active = true},                        // 6
                    new Kategoria {NazwaKategorii = "Miernik", Active = true},                      // 7
                    new Kategoria {NazwaKategorii = "Szlifierka elektryczna", Active = true},       // 8
                    new Kategoria {NazwaKategorii = "Szlifierka akumulatorowa", Active = true},     // 9
                    new Kategoria {NazwaKategorii = "Poziomica", Active = true},                    // 10
                    new Kategoria {NazwaKategorii = "Klucz dynamometryczny", Active = true},        // 11
                    new Kategoria {NazwaKategorii = "Suwmiarka", Active = true},                    // 12
                    new Kategoria {NazwaKategorii = "Suwnica", Active = true},                      // 13
                    new Kategoria {NazwaKategorii = "Wózek", Active = true},                        // 14
                    new Kategoria {NazwaKategorii = "Żuraw", Active = true},                        // 15
                    new Kategoria {NazwaKategorii = "Nitownica", Active = true},                    // 16
                    new Kategoria {NazwaKategorii = "Wózek narzędziowy", Active = true},            // 17
                    new Kategoria {NazwaKategorii = "Klucz udarowy pneumat", Active = true},        // 18
                    new Kategoria {NazwaKategorii = "Klucz hydroimpulsowy pneumat", Active = true}, // 19
                    new Kategoria {NazwaKategorii = "Wózek montażowy", Active = true},              // 20
                    new Kategoria {NazwaKategorii = "Wózek logistyczny", Active = true},            // 21
                    new Kategoria {NazwaKategorii = "Przyrząd technologiczny", Active = true},      // 22
                    new Kategoria {NazwaKategorii = "Nalewak", Active = true},                      // 23
                    new Kategoria {NazwaKategorii = "Pompa", Active = true},                        // 24
                    new Kategoria {NazwaKategorii = "Zawiesie", Active = true},                     // 25
                    new Kategoria {NazwaKategorii = "Podest", Active = true},                       // 26
                    new Kategoria {NazwaKategorii = "Drabina", Active = true},                      // 27
                    new Kategoria {NazwaKategorii = "Szafa", Active = true},                        // 28
                    new Kategoria {NazwaKategorii = "Numerator", Active = true},                    // 29
                    new Kategoria {NazwaKategorii = "Pas", Active = true},                          // 30
                    new Kategoria {NazwaKategorii = "Latarka", Active = true},                      // 31
                    new Kategoria {NazwaKategorii = "Wkrętarka", Active = true},                    // 32
                    new Kategoria {NazwaKategorii = "Skaner", Active = true},                       // 33
                    new Kategoria {NazwaKategorii = "Drukarka", Active = true},                     // 34

                };

                dbContext.AddRange(kat);
                dbContext.SaveChanges();
            }
        }

        private static void SeedProducent(ApplicationDbContext dbContext)
        {
            if (!dbContext.Producenci.Any())
            {
                var kat = new List<Producent>
                {
                    new Producent {NazwaProducenta = "Einhell", Active = true},         // 1
                    new Producent {NazwaProducenta = "Flexicos", Active = true},        // 2
                    new Producent {NazwaProducenta = "Stanley", Active = true},         // 3
                    new Producent {NazwaProducenta = "HP", Active = true},              // 4
                    new Producent {NazwaProducenta = "Panasonic", Active = true},       // 5
                    new Producent {NazwaProducenta = "Milwaukee", Active = true},       // 6
                    new Producent {NazwaProducenta = "Bosch", Active = true},           // 7
                    new Producent {NazwaProducenta = "Gesipa", Active = true},          // 8
                    new Producent {NazwaProducenta = "Pop", Active = true},             // 9
                    new Producent {NazwaProducenta = "Stahlwille", Active = true},      // 10
                    new Producent {NazwaProducenta = "Saweron", Active = true},         // 11
                    new Producent {NazwaProducenta = "Rialex", Active = true},          // 12
                    new Producent {NazwaProducenta = "Jungheinrich", Active = true},    // 13
                    new Producent {NazwaProducenta = "Yato", Active = true},            // 14
                    new Producent {NazwaProducenta = "Uryu", Active = true},            // 15
                    new Producent {NazwaProducenta = "Mipromet", Active = true},        // 16
                    new Producent {NazwaProducenta = "Produkcja CNHind", Active = true},// 17
                    new Producent {NazwaProducenta = "Krauze", Active = true},          // 18

                };

                dbContext.AddRange(kat);
                dbContext.SaveChanges();
            }
        }

        private static void SeedNarzedzie(ApplicationDbContext dbContext)
        {
            // 1
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805254))
            {
                var narzedzie1 = new Narzedzie
                {
                    ProducentId = 1,
                    KategoriaId = 1,
                    DataPrzyjecia = DateTime.Today,
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805254,
                    Nazwa = "Frezarka przedmontaż gardzieli",
                    Status = Status.używane,
                    ZdjecieFileName = "frezarka.jpg"
                };

                dbContext.Narzedzia.Add(narzedzie1);
                dbContext.SaveChanges();
            }
            // 2
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 801214))
            {
                var narzedzie2 = new Narzedzie
                {
                    ProducentId = 2,
                    KategoriaId = 2,
                    DataPrzyjecia = new DateTime(2018, 5, 15),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 801214,
                    Nazwa = "Kabina Flexicos - Hamownia",
                    Status = Status.używane,
                    ZdjecieFileName = "flexicos.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie2);
                dbContext.SaveChanges();
            }
            // 3
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 801317))
            {
                var narzedzie3 = new Narzedzie
                {
                    ProducentId = 3,
                    KategoriaId = 3,
                    DataPrzyjecia = new DateTime(2022, 8, 11),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 801317,
                    Nazwa = "Klucz elektryczny, stanowisko 2",
                    Status = Status.używane,
                    ZdjecieFileName = "Yato2.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie3);
                dbContext.SaveChanges();
            }
            // 4
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 801618))
            {
                var narzedzie4 = new Narzedzie
                {
                    ProducentId = 4,
                    KategoriaId = 4,
                    DataPrzyjecia = new DateTime(2023, 5, 15),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 801618,
                    Nazwa = "Komputer testy",
                    Status = Status.używane,
                    ZdjecieFileName = "Komputer.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie4);
                dbContext.SaveChanges();
            }
            // 5
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 801519))
            {
                var narzedzie5 = new Narzedzie
                {
                    ProducentId = 4,
                    KategoriaId = 4,
                    DataPrzyjecia = new DateTime(2019, 2, 25),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 801519,
                    Nazwa = "Komputer Dacos",
                    Status = Status.używane,
                    ZdjecieFileName = "dacos.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie5);
                dbContext.SaveChanges();
            }
            // 6
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 802123))
            {
                var narzedzie6 = new Narzedzie
                {
                    ProducentId = 4,
                    KategoriaId = 4,
                    DataPrzyjecia = new DateTime(2021, 11, 17),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 802123,
                    Nazwa = "Komputer przedmontaż silnika",
                    Status = Status.używane,
                    ZdjecieFileName = "Komputer.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie6);
                dbContext.SaveChanges();
            }
            // 7
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 802426))
            {
                var narzedzie7 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 5,
                    DataPrzyjecia = new DateTime(2021, 11, 17),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 802426,
                    Nazwa = "Klucz aku. M18, stanowisko 2",
                    Status = Status.używane,
                    ZdjecieFileName = "M18.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie7);
                dbContext.SaveChanges();
            }
            // 8
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 802729))
            {
                var narzedzie8 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 5,
                    DataPrzyjecia = new DateTime(2020, 11, 17),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 802729,
                    Nazwa = "Klucz aku. M18, stanowisko 5",
                    Status = Status.używane,
                    ZdjecieFileName = "M18a.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie8);
                dbContext.SaveChanges();
            }
            //9
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 802528))
            {
                var narzedzie9 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 5,
                    DataPrzyjecia = new DateTime(2022, 12, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 802528,
                    Nazwa = "Klucz aku. M18, stanowisko 9",
                    Status = Status.używane,
                    ZdjecieFileName = "M18b.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie9);
                dbContext.SaveChanges();
            }
            //10
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 803132))
            {
                var narzedzie10 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 5,
                    DataPrzyjecia = new DateTime(2022, 12, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 803132,
                    Nazwa = "Klucz aku. M18, stanowisko 12",
                    Status = Status.używane,
                    ZdjecieFileName = "M18a.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie10);
                dbContext.SaveChanges();
            }
            //11
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 803435))
            {
                var narzedzie11 = new Narzedzie
                {
                    ProducentId = 7,
                    KategoriaId = 6,
                    DataPrzyjecia = new DateTime(2022, 12, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 803435,
                    Nazwa = "Miernik elektroniczny - belka tył",
                    Status = Status.używane,
                    ZdjecieFileName = "Sta2.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie11);
                dbContext.SaveChanges();
            }
            //12
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 803637))
            {
                var narzedzie12 = new Narzedzie
                {
                    ProducentId = 3,
                    KategoriaId = 6,
                    DataPrzyjecia = new DateTime(2018, 5, 8),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 803637,
                    Nazwa = "Miara, stanowisko 4",
                    Status = Status.używane,
                    ZdjecieFileName = "Miara_2.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie12);
                dbContext.SaveChanges();
            }
            //13
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 803839))
            {
                var narzedzie13 = new Narzedzie
                {
                    ProducentId = 7,
                    KategoriaId = 7,
                    DataPrzyjecia = new DateTime(2021, 1, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 803839,
                    Nazwa = "Miernik testy DRI",
                    Status = Status.używane,
                    ZdjecieFileName = "Miernik2.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie13);
                dbContext.SaveChanges();
            }
            //14
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804142))
            {
                var narzedzie14 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 8,
                    DataPrzyjecia = new DateTime(2022, 6, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804142,
                    Nazwa = "Szlifierka elek. wylot",
                    Status = Status.używane,
                    ZdjecieFileName = "szlifierka.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie14);
                dbContext.SaveChanges();
            }
            //15
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804243))
            {
                var narzedzie15 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 9,
                    DataPrzyjecia = new DateTime(2021, 9, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804243,
                    Nazwa = "Szlifierka aku. młocarnia",
                    Status = Status.używane,
                    ZdjecieFileName = "szlifierka_aku.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie15);
                dbContext.SaveChanges();
            }
            //16
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804344))
            {
                var narzedzie16 = new Narzedzie
                {
                    ProducentId = 8,
                    KategoriaId = 16,
                    DataPrzyjecia = new DateTime(2023, 9, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804344,
                    Nazwa = "Nitownica przedmontaż silnika",
                    Status = Status.używane,
                    ZdjecieFileName = "NitownicaPOP.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie16);
                dbContext.SaveChanges();
            }
            //17
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804445))
            {
                var narzedzie17 = new Narzedzie
                {
                    ProducentId = 9,
                    KategoriaId = 16,
                    DataPrzyjecia = new DateTime(2019, 11, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804445,
                    Nazwa = "Nitownica zdawanie",
                    Status = Status.używane,
                    ZdjecieFileName = "NitownicaGESIPA2.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie17);
                dbContext.SaveChanges();
            }
            //18
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804546))
            {
                var narzedzie18 = new Narzedzie
                {
                    ProducentId = 7,
                    KategoriaId = 10,
                    DataPrzyjecia = new DateTime(2018, 11, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804546,
                    Nazwa = "Poziomica rama dolna",
                    Status = Status.używane,
                    ZdjecieFileName = "Poziomnica.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie18);
                dbContext.SaveChanges();
            }
            //19
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804647))
            {
                var narzedzie19 = new Narzedzie
                {
                    ProducentId = 11,
                    KategoriaId = 2,
                    DataPrzyjecia = new DateTime(2018, 11, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804647,
                    Nazwa = "Próby młocarnia",
                    Status = Status.używane,
                    ZdjecieFileName = "flexicos.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie19);
                dbContext.SaveChanges();
            }
            //20
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804648))
            {
                var narzedzie20 = new Narzedzie
                {
                    ProducentId = 11,
                    KategoriaId = 2,
                    DataPrzyjecia = new DateTime(2018, 11, 20),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804648,
                    Nazwa = "Próby rama dolna",
                    Status = Status.używane,
                    ZdjecieFileName = "zuraw.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie20);
                dbContext.SaveChanges();
            }
            //21
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 804849))
            {
                var narzedzie21 = new Narzedzie
                {
                    ProducentId = 10,
                    KategoriaId = 11,
                    DataPrzyjecia = new DateTime(2022, 3, 8),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 804849,
                    Nazwa = "Klucz dynamometryczny 40-400Nm",
                    Status = Status.używane,
                    ZdjecieFileName = "klucz_dynamometryczny_2.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie21);
                dbContext.SaveChanges();
            }
            //22
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805152))
            {
                var narzedzie22 = new Narzedzie
                {
                    ProducentId = 10,
                    KategoriaId = 11,
                    DataPrzyjecia = new DateTime(2021, 4, 16),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805152,
                    Nazwa = "Klucz dynamometryczny 20-200Nm",
                    Status = Status.używane,
                    ZdjecieFileName = "klucz_dynamometryczny_3.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie22);
                dbContext.SaveChanges();
            }
            //23
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805253))
            {
                var narzedzie23 = new Narzedzie
                {
                    ProducentId = 10,
                    KategoriaId = 11,
                    DataPrzyjecia = new DateTime(2021, 4, 16),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805253,
                    Nazwa = "Klucz dynamometryczny 2-40Nm",
                    Status = Status.używane,
                    ZdjecieFileName = "klucz_dynamometryczny_4.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie23);
                dbContext.SaveChanges();
            }
            //24
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805354))
            {
                var narzedzie24 = new Narzedzie
                {
                    ProducentId = 11,
                    KategoriaId = 12,
                    DataPrzyjecia = new DateTime(2023, 4, 16),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805354,
                    Nazwa = "Suwmiarka nabijanie",
                    Status = Status.używane,
                    ZdjecieFileName = "Suwmiarka2.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie24);
                dbContext.SaveChanges();
            }
            //25
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805455))
            {
                var narzedzie25 = new Narzedzie
                {
                    ProducentId = 12,
                    KategoriaId = 13,
                    DataPrzyjecia = new DateTime(2018, 8, 1),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805455,
                    Nazwa = "Suwnica 2T - stanowisko 1",
                    Status = Status.używane,
                    ZdjecieFileName = "suwnica.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie25);
                dbContext.SaveChanges();
            }
            //26
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805556))
            {
                var narzedzie26 = new Narzedzie
                {
                    ProducentId = 12,
                    KategoriaId = 13,
                    DataPrzyjecia = new DateTime(2018, 8, 1),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805556,
                    Nazwa = "Suwnica 3,5T - stanowisko 2",
                    Status = Status.używane,
                    ZdjecieFileName = "suwnica.jpg"


                };

                dbContext.Narzedzia.Add(narzedzie26);
                dbContext.SaveChanges();
            }
            //27
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805657))
            {
                var narzedzie27 = new Narzedzie
                {
                    ProducentId = 12,
                    KategoriaId = 13,
                    DataPrzyjecia = new DateTime(2018, 8, 1),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805657,
                    Nazwa = "Suwnica 1,5T - stanowisko 3",
                    Status = Status.używane,
                    ZdjecieFileName = "suwnica.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie27);
                dbContext.SaveChanges();
            }
            //28
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 805758))
            {
                var narzedzie28 = new Narzedzie
                {
                    ProducentId = 5,
                    KategoriaId = 4,
                    DataPrzyjecia = new DateTime(2022, 8, 1),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 805758,
                    Nazwa = "Tablet testy DRI",
                    Status = Status.używane,
                    ZdjecieFileName = "tablet.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie28);
                dbContext.SaveChanges();
            }
            //29
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806162))
            {
                var narzedzie29 = new Narzedzie
                {
                    ProducentId = 13,
                    KategoriaId = 12,
                    DataPrzyjecia = new DateTime(2018, 8, 1),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806162,
                    Nazwa = "Wózek orderpicker CP1A",
                    Status = Status.używane,
                    ZdjecieFileName = "order_picker.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie29);
                dbContext.SaveChanges();
            }
            //30
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806263))
            {
                var narzedzie30 = new Narzedzie
                {
                    ProducentId = 13,
                    KategoriaId = 14,
                    DataPrzyjecia = new DateTime(2018, 8, 1),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806263,
                    Nazwa = "Wózek orderpicker CP1B",
                    Status = Status.używane,
                    ZdjecieFileName = "order_picker.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie30);
                dbContext.SaveChanges();
            }
            //31
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806364))
            {
                var narzedzie31 = new Narzedzie
                {
                    ProducentId = 13,
                    KategoriaId = 14,
                    DataPrzyjecia = new DateTime(2018, 8, 1),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806364,
                    Nazwa = "Wózek tugger TB",
                    Status = Status.używane,
                    ZdjecieFileName = "tugger.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie31);
                dbContext.SaveChanges();
            }
            //32
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806465))
            {
                var narzedzie32 = new Narzedzie
                {
                    ProducentId = 14,
                    KategoriaId = 17,
                    DataPrzyjecia = new DateTime(2022, 11, 3),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806465,
                    Nazwa = "Wózek narzędziowy zdawanie",
                    Status = Status.używane,
                    ZdjecieFileName = "wozek_narzedziowy_2.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie32);
                dbContext.SaveChanges();
            }
            //33
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806566))
            {
                var narzedzie33 = new Narzedzie
                {
                    ProducentId = 14,
                    KategoriaId = 18,
                    DataPrzyjecia = new DateTime(2022, 11, 3),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806566,
                    Nazwa = "Klucz udarowy rama dolna",
                    Status = Status.używane,
                    ZdjecieFileName = "ult-50.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie33);
                dbContext.SaveChanges();
            }
            //34
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806667))
            {
                var narzedzie34 = new Narzedzie
                {
                    ProducentId = 14,
                    KategoriaId = 18,
                    DataPrzyjecia = new DateTime(2021, 5, 6),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806667,
                    Nazwa = "Klucz udarowy gardziel",
                    Status = Status.używane,
                    ZdjecieFileName = "uat-90.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie34);
                dbContext.SaveChanges();
            }
            //35
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806768))
            {
                var narzedzie35 = new Narzedzie
                {
                    ProducentId = 15,
                    KategoriaId = 19,
                    DataPrzyjecia = new DateTime(2022, 5, 6),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806768,
                    Nazwa = "Klucz UAT-100, stanowisko 6",
                    Status = Status.używane,
                    ZdjecieFileName = "ult-100.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie35);
                dbContext.SaveChanges();
            }
            //36
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 806869))
            {
                var narzedzie36 = new Narzedzie
                {
                    ProducentId = 15,
                    KategoriaId = 19,
                    DataPrzyjecia = new DateTime(2022, 5, 6),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 806869,
                    Nazwa = "Klucz UAT-50, stanowisko 12",
                    Status = Status.używane,
                    ZdjecieFileName = "ult-50.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie36);
                dbContext.SaveChanges();
            }
            //37
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807172))
            {
                var narzedzie37 = new Narzedzie
                {
                    ProducentId = 15,
                    KategoriaId = 19,
                    DataPrzyjecia = new DateTime(2021, 7, 15),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807172,
                    Nazwa = "Klucz zapadkowy, gardziel",
                    Status = Status.używane,
                    ZdjecieFileName = "Zapadkowy.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie37);
                dbContext.SaveChanges();
            }
            //38
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807273))
            {
                var narzedzie38 = new Narzedzie
                {
                    ProducentId = 16,
                    KategoriaId = 15,
                    DataPrzyjecia = new DateTime(2021, 7, 15),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807273,
                    Nazwa = "Żuraw 2T, TB",
                    Status = Status.używane,
                    ZdjecieFileName = "zuraw.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie38);
                dbContext.SaveChanges();
            }
            //39
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807374))
            {
                var narzedzie39 = new Narzedzie
                {
                    ProducentId = 16,
                    KategoriaId = 15,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807374,
                    Nazwa = "Żuraw 1T,stanowisko 5",
                    Status = Status.używane,
                    ZdjecieFileName = "zuraw.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie39);
                dbContext.SaveChanges();
            }
            //40
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807475))
            {
                var narzedzie40 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 20,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807475,
                    Nazwa = "Wózek linia główna nr.1",
                    Status = Status.używane,
                    ZdjecieFileName = "Wózek ETV.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie40);
                dbContext.SaveChanges();
            }
            //41
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807576))
            {
                var narzedzie41 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 20,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807576,
                    Nazwa = "Wózek linia główna nr.2",
                    Status = Status.używane,
                    ZdjecieFileName = "Wózek ETV.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie41);
                dbContext.SaveChanges();
            }
            //42
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807677))
            {
                var narzedzie42 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 21,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807677,
                    Nazwa = "Przedmontaż wylotu",
                    Status = Status.używane,
                    ZdjecieFileName = "flexicos.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie42);
                dbContext.SaveChanges();
            }
            //43
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807777))
            {
                var narzedzie43 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 22,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807777,
                    Nazwa = "Przyrząd do HillSide",
                    Status = Status.używane,
                    ZdjecieFileName = "KluczEle.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie43);
                dbContext.SaveChanges();
            }
            //44
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807877))
            {
                var narzedzie44 = new Narzedzie
                {
                    ProducentId = 3,
                    KategoriaId = 23,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807877,
                    Nazwa = "Nalewak olej hydrauliczny",
                    Status = Status.używane,
                    ZdjecieFileName = "nalewak_olej.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie44);
                dbContext.SaveChanges();
            }
            //45
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807879))
            {
                var narzedzie45 = new Narzedzie
                {
                    ProducentId = 3,
                    KategoriaId = 24,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807879,
                    Nazwa = "Pompa płyn chłodzący",
                    Status = Status.używane,
                    ZdjecieFileName = "pompa_plyn.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie45);
                dbContext.SaveChanges();
            }
            //46
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 807979))
            {
                var narzedzie46 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 25,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 807979,
                    Nazwa = "Zawiesie do klawiszy",
                    Status = Status.używane,
                    ZdjecieFileName = "zawiesie_klawisze.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie46);
                dbContext.SaveChanges();
            }
            //47
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808000))
            {
                var narzedzie47 = new Narzedzie
                {
                    ProducentId = 18,
                    KategoriaId = 26,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808000,
                    Nazwa = "Podest 6 stopni, stanowisko 4",
                    Status = Status.używane,
                    ZdjecieFileName = "podest_6stopni.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie47);
                dbContext.SaveChanges();
            }
            //48
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808001))
            {
                var narzedzie48 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 27,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808001,
                    Nazwa = "Drabina 3 stopnie, stanowisko 11",
                    Status = Status.używane,
                    ZdjecieFileName = "drabina_3stopnie.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie48);
                dbContext.SaveChanges();
            }
            //49
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808003))
            {
                var narzedzie49 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 28,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808003,
                    Nazwa = "Numer 23145",
                    Status = Status.używane,
                    ZdjecieFileName = "klucz_hydroimpuls.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie49);
                dbContext.SaveChanges();
            }
            //50
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808008))
            {
                var narzedzie50 = new Narzedzie
                {
                    ProducentId = 2,
                    KategoriaId = 29,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808008,
                    Nazwa = "Numerator tabliczki",
                    Status = Status.używane,
                    ZdjecieFileName = "numerator_tabliczki.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie50);
                dbContext.SaveChanges();
            }
            //51
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808012))
            {
                var narzedzie51 = new Narzedzie
                {
                    ProducentId = 17,
                    KategoriaId = 30,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808012,
                    Nazwa = "Pas rura wysypowa",
                    Status = Status.używane,
                    ZdjecieFileName = "rura_wysypowa.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie51);
                dbContext.SaveChanges();
            }
            //52
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808022))
            {
                var narzedzie52 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 31,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808022,
                    Nazwa = "Czołowa",
                    Status = Status.używane,
                    ZdjecieFileName = "rura_czolowa.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie52);
                dbContext.SaveChanges();
            }
            //53
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808028))
            {
                var narzedzie53 = new Narzedzie
                {
                    ProducentId = 6,
                    KategoriaId = 32,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808028,
                    Nazwa = "Wkrętarka aku. stanowisko 11",
                    Status = Status.używane,
                    ZdjecieFileName = "M18b.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie53);
                dbContext.SaveChanges();
            }
            //54
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808033))
            {
                var narzedzie54 = new Narzedzie
                {
                    ProducentId = 5,
                    KategoriaId = 33,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808033,
                    Nazwa = "Skaner etykier CP1A",
                    Status = Status.używane,
                    ZdjecieFileName = "skaner_etykiet.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie54);
                dbContext.SaveChanges();
            }
            //55
            if (!dbContext.Narzedzia.Any(n => n.NumerNarzedzia == 808034))
            {
                var narzedzie55 = new Narzedzie
                {
                    ProducentId = 5,
                    KategoriaId = 34,
                    DataPrzyjecia = new DateTime(2018, 9, 5),
                    UzytkownikId = LosowyUser(dbContext),
                    NumerNarzedzia = 808034,
                    Nazwa = "Drukarka etykiet CP1A",
                    Status = Status.używane,
                    ZdjecieFileName = "drukarka_etykiet.jpg"

                };

                dbContext.Narzedzia.Add(narzedzie55);
                dbContext.SaveChanges();
            }
        }
        private static void SeedWydzial(ApplicationDbContext dbContext)
        {
            if (!dbContext.Wydzialy.Any())
            {
                var kat = new List<Wydzial>
                {
                    new Wydzial {NazwaWydzialu = "P5", Active = true},
                    new Wydzial {NazwaWydzialu = "P6", Active = true},
                    new Wydzial {NazwaWydzialu = "P7", Active = true},
                };

                dbContext.AddRange(kat);
                dbContext.SaveChanges();
            }
        }

        private static void SeedStanowisko(ApplicationDbContext dbContext)
        {
            if (!dbContext.Stanowiska.Any())
            {
                var kat = new List<Stanowisko>
                {
                    new Stanowisko {NazwaStanowiska = "A", Active = true},
                    new Stanowisko {NazwaStanowiska = "B", Active = true},
                    new Stanowisko {NazwaStanowiska = "C", Active = true},
                    new Stanowisko {NazwaStanowiska = "D", Active = true},
                    new Stanowisko {NazwaStanowiska = "E", Active = true},
                    new Stanowisko {NazwaStanowiska = "F", Active = true},
                    new Stanowisko {NazwaStanowiska = "G", Active = true},
                    new Stanowisko {NazwaStanowiska = "H", Active = true},
                    new Stanowisko {NazwaStanowiska = "I", Active = true},
                    new Stanowisko {NazwaStanowiska = "J", Active = true},
                    new Stanowisko {NazwaStanowiska = "K", Active = true},
                    new Stanowisko {NazwaStanowiska = "L", Active = true},
                    new Stanowisko {NazwaStanowiska = "M", Active = true},
                    new Stanowisko {NazwaStanowiska = "N", Active = true},
                    new Stanowisko {NazwaStanowiska = "O", Active = true},
                    new Stanowisko {NazwaStanowiska = "P", Active = true},
                    new Stanowisko {NazwaStanowiska = "R", Active = true},
                    new Stanowisko {NazwaStanowiska = "S", Active = true},
                    new Stanowisko {NazwaStanowiska = "T", Active = true},
                    new Stanowisko {NazwaStanowiska = "U", Active = true},
                    new Stanowisko {NazwaStanowiska = "W", Active = true},
                    new Stanowisko {NazwaStanowiska = "X", Active = true},
                };

                dbContext.AddRange(kat);
                dbContext.SaveChanges();
            }
        }

        public static string LosowyUser(ApplicationDbContext dbContext)
        {
            var user = dbContext.Uzytkownicy.OrderBy(o => Guid.NewGuid()).First();
            return user.Id.ToString();

        }
    }
}
