using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.Infrastructure.Repository
{
    public static class DbInitializer
    {
        public static void Initialize(UserDbContext dbContext)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.Database.EnsureCreated();
                    if (dbContext.Department.Any())
                    {
                        return;
                    }
                    else
                    {
                        var dep = new List<Department>
                        {
                            new Department{Id = 1 , Name="Software"},
                            new Department{Id = 2 , Name="IT"},
                            new Department{Id = 3 , Name="QA"},
                            new Department{Id = 4 , Name="Flutter"}
                        };
                        dbContext.Department.AddRange(dep);
                    }
                    if (dbContext.City.Any())
                    {
                        return;
                    }
                    else
                    {
                        var cities = new List<City>
                        {
                            new City { Id = 1, Name = "Kathmandu", Code = "KTM" ,CountryId=1},
                            new City { Id = 2, Name = "Biratnagar", Code = "BIR",CountryId=1 },
                            new City { Id = 3, Name = "Pokhara", Code = "POK" , CountryId = 1},
                            new City { Id = 4, Name = "Lalitpur", Code = "LAL"  ,CountryId=1 },
                            new City { Id = 5, Name = "Bharatpur", Code = "BHA",CountryId=1 },
                            new City { Id = 6, Name = "Birgunj", Code = "BIRG" , CountryId = 1},
                            new City { Id = 7, Name = "Hetauda", Code = "HET" ,CountryId=1},
                            new City { Id = 8, Name = "Dhangadhi", Code = "DHA" ,CountryId=1},
                            new City { Id = 9, Name = "Janakpur", Code = "JAN" ,CountryId=1},
                            new City { Id = 10, Name = "Nepalgunj", Code = "NEP",CountryId=1 }
                        };
                        dbContext.City.AddRange(cities);
                    }
                    if (dbContext.Country.Any())
                    {
                        return;
                    }
                    else
                    {
                        var countries = new List<Country>
                      {
                            new Country { Id = 1, Name = "Nepal", Code = "NP" },
                            new Country { Id = 2, Name = "United States", Code = "US" },
                            new Country { Id = 3, Name = "India", Code = "IN" },
                            new Country { Id = 4, Name = "United Kingdom", Code = "GB" },
                            new Country { Id = 5, Name = "Canada", Code = "CA" },
                            new Country { Id = 6, Name = "Australia", Code = "AU" },
                            new Country { Id = 7, Name = "Germany", Code = "DE" },
                            new Country { Id = 8, Name = "France", Code = "FR" },
                            new Country { Id = 9, Name = "Japan", Code = "JP" },
                            new Country { Id = 10, Name = "China", Code = "CN" }
                        };
                        dbContext.Country.AddRange(countries);
                    }
                    dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Handle or log the exception
                }
            }
        }
    }
}
