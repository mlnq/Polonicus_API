using Polonicus_API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polonicus_API
{
    public class PolonicusSeeder
    {
        private readonly PolonicusDbContext dbContext;

        public PolonicusSeeder(PolonicusDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Seed()
        {
            if(dbContext.Database.CanConnect())
            {
                if(!dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    dbContext.SaveChanges();
                }

                if(!dbContext.Outposts.Any())
                {
                    var outposts = GetOutposts();
                    dbContext.Outposts.AddRange(outposts);
                    dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role(){ Name="User"},
                new Role(){ Name="Manager"},
                new Role(){ Name="Admin"},
            };

            return roles;
        }

        private IEnumerable<Outpost> GetOutposts()
        {
            var outposts = new List<Outpost>()
            {
                new Outpost()
                {
                    Name="PlacowkaKresy",
                    Description="Placówka integrująca polaków na terenach kresowych",
                    ContactEmail="kresyplacowka@wp.pl",

                    Chronicles = new List<Chronicle>()
                    {
                        new Chronicle()
                        {
                            Name="Wrzesniowy poranek",
                            Description="Lorem ipsum...",
                            PublicationDate=new DateTime(2021,12,23)
                        },
                        new Chronicle()
                        {
                            Name="Kamienny dzien",
                            Description="Lorem ipsum...",
                            PublicationDate= new DateTime(2021, 1, 3)
                        }
                    },
                     Address = new Address()
                    {
                        City="Kowno",
                        Street="Kaunas 13",
                        PostalCode="33-222"
                    }
                },
                new Outpost()
                {
                    Name="Grodno Wsparcie",
                    Description="Placówka integrująca polaków na terenach kresowych",
                    ContactEmail="belarus@wp.ru",

                    Chronicles = new List<Chronicle>()
                    {
                        new Chronicle()
                        {
                            Name="Wielkie księstwo litewskie",
                            Description="Lorem ipsum...",
                            PublicationDate=new DateTime(2021,12,23)
                        },
                        new Chronicle()
                        {
                            Name="Powstanie urzedu..",
                            Description="Lorem ipsum...",
                            PublicationDate= new DateTime(2021, 1, 3)
                        }
                    }

                    ,
                    Address = new Address()
                    {
                        City="Grodno",
                        Street="Musnikov 13",
                        PostalCode="70-012"
                    }
                }


            };

            return outposts;
        }
    }
}
