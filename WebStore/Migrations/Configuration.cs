namespace WebStore.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebStore.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebStore.Models.WebStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebStore.Models.WebStoreDbContext context)
        {
            // add users and roles
            AddUsersAndRoles(context);

            // use add or update to avoid having duplicate data
            // add test categories. Upsert based on category name.
            GetCategories().ForEach(category => context.Categories.AddOrUpdate(c => c.CategoryName, category));
            context.SaveChanges();

            // add test products
            GetProducts().ForEach(p => context.Products.AddOrUpdate(p));
        }

        private static List<Category> GetCategories()
        {
            var categories = new List<Category> {
                new Category
                {
                    CategoryName = "Automatic Weather Stations",
                    Description = "Automatic weather stations for gathering data on ground."
                },
                new Category
                {
                    CategoryName = "Data Loggers",
                    Description = "Data loggers for measuring specific meteorological variable."
                },
                new Category
                {
                    CategoryName = "Weather Radars",
                    Description = "Weather radars for detecting percipitation."
                },
                new Category
                {
                    CategoryName = "Road Weather Systems",
                    Description = "Weather systems for monitoring road conditions."
                },
                new Category
                {
                    CategoryName = "Aviation Weather Systems",
                    Description = "Weather systems for monitoring at the airports."
                },
            };

            return categories;
        }

        private static List<Product> GetProducts()
        {
            var products = new List<Product> {
                new Product
                {
                    ProductName = "MAWS50",
                    ShortDescription = "AWS for collecting temperature, humidity and precipitation data.",
                    FullDescription = "The Vaisala MAWS50 offers a flexible and low-power platform for remote, unmanned data collection applications. All units come with a standard 2 year warranty. It is extremely flexible, supporting basic operations as well as downstream scope for expansion with SDI-12, 20 analog channels, 8 digital I/Os, multiple serial ports and built-in TCP/IP connectivity. Vaisala provides full-service tailoring options or training services that allow end-users more autonomy to refine their systems and networks. MAWS50 is only available in the US.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/MAWS50-210x170.jpg",
                    UnitPrice = 115.5M,
                    CategoryID = 1
               },
                new Product 
                {
                    ProductName = "MAWS100",
                    ShortDescription = "AWS for collecting wind speed, wind direction data.",
                    FullDescription = "The Vaisala HydroMet™ System MAWS100 extends the field-proven quality and reliability of the existing Vaisala HydroMet™ Systems to new applications. The MAWS100 is a compact system for hydrometeorological monitoring when a small number of sensors is required. The MAWS100 is well suited for Vaisala Weather Transmitter WXT520 and cellular telemetry. MAWS100 uses the same field-proven and high accuracy data logger and advanced software as other Vaisala HydroMet™ Systems. Data retrieval can be done by direct link to PC via serial or TCP/IP connection, via PSTN modem or using wireless cellular telemetry links. Flexible sensor interfacing, advanced statistical calculations, extensive data logging on a compact flash memory card and versatile data reporting functions allow the MAWS100 to be customized for a variety of applications.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/MAWS100_210x170.jpg",
                    UnitPrice = 130.95M,
                     CategoryID = 1
               },
                new Product
                {
                    ProductName = "MAWS110",
                    ShortDescription = "AWS for collecting humidity data.",
                    FullDescription = "The Vaisala HydroMet™ System MAWS110 is a compact, robust and easy to use system which provides quality controlled data for both  meteorological and hydrological applications. The system is especially designed for unattended operations requiring high reliability and accuracy at sites with the mains power and with battery back-up. The MAWS110 uses a field proven and high accuracy data logger and advanced software. Every system can be individually equipped with the sensor and telemetry devices of your choice to provide the most economical and optimized turnkey solution for your every application.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/MAWS110_210x170.jpg",
                    UnitPrice = 156.99M,
                    CategoryID = 1
                },
                new Product
                {
                    ProductName = "MAWS201",
                    ShortDescription = "AWS for collecting wind speed, wind direction data.",
                    FullDescription = "The Vaisala HydroMet™ Automatic Weather Station MAWS201 is a portable AWS for temporary installations, featuring the same design as the Vaisala TacMet Tactical Meteorological Observation System MAWS201M for demanding tactical meteorological applications. Its lightweight aluminium tripod and easy-to-use connectors make it fast to set up. Each leg is adjustable for use on uneven terrain. The MAWS201 is field-proven in a wide range of applications.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/MAWS201_210x170.jpg",
                    UnitPrice = 356.24M,
                    CategoryID = 1
                },
                new Product
                {
                    ProductName = "MAWS201M",
                    ShortDescription = "AWS for collecting wind speed, wind direction data.",
                    FullDescription = "The Vaisala TacMet® Tactical Meteorological Observation System MAWS201M is a field-deployable, compact weather station for various field operations. Offering broad sensor capability, the portable MAWS201M system can be easily and rapidly deployed. Moreover, it is a Commercial-Off-the-Shelf (COTS) product and incorporates efficient Built-In-Test (BIT) diagnostics.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/MAWS201M_210x170.jpg",
                    UnitPrice = 501.11M,
                    CategoryID = 1
                },
                new Product
                {
                    ProductName = "DL 1000/1400",
                    ShortDescription = "Data logger for collecting temperature data.",
                    FullDescription = "​Vaisala DL1000/1400 temperature data loggers are perfect for monitoring, alarming, and recording environmental conditions in FDA/GxP-regulated environments like pharmaceutical warehouses, research and labs, hospitals, and ultra-low temperature freezers, fridges, cold rooms.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/vaisala-veriteq-1000-data-logger-210x170.jpg",
                    UnitPrice = 35.00M,
                    CategoryID = 2
                },
                new Product
                {
                    ProductName = "SP 1000/1400",
                    ShortDescription = "Data logger for collecting temperature data.",
                    FullDescription = "Vaisala Veriteq SP-1000/1400 temperature data loggers are perfect for monitoring, alarming, and recording environmental conditions in Pharmaceutical Warehouses, Research and Development labs, Hospital & Clinical Environments and Ultra-low Freezers, fridges, and cold rooms. All 1000/1400 data loggers have on-board power and memory for autonomous recording, making your temperature data immune to any network or power failure. They are compact, easily deployable, and have multiple inputs.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/vaisala-veriteq-1000-data-logger-210x170.jpg",
                    UnitPrice = 55.38M,
                    CategoryID = 2
                },
                new Product
                {
                    ProductName = "VL 1016/1416",
                    ShortDescription = "Data logger for collecting temperature data.",
                    FullDescription = "The Vaisala Veriteq Multi-application data logger can simultaneously monitor different temperature ranges in up to four chambers— -80°C ultra-low temperature freezers, freezer/refrigerator units and incubators. It eliminates the need to purchase and install additional hardware — loggers and network access points — otherwise required for measuring different temperature ranges.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/vaisala-veriteq-1000-data-logger-210x170.jpg",
                    UnitPrice = 67.22M,
                    CategoryID = 2
                },
                new Product
                {
                    ProductName = "SP 1016/1416",
                    ShortDescription = "Data logger for collecting temperature data.",
                    FullDescription = "The Vaisala Veriteq Multi-application data logger can simultaneously monitor different temperature ranges in up to four chambers — -80°C ultra-low temperature freezers, freezer/refrigerator units and incubators. This eliminates the need to purchase and install additional hardware — loggers and network access points — otherwise required for measuring different temperature ranges.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/vaisala-veriteq-1000-data-logger-210x170.jpg",
                    UnitPrice = 88.77M,
                    CategoryID = 2
                },
                new Product
                {
                    ProductName = "WRM200",
                    ShortDescription = "Radar for detecting precipitation.",
                    FullDescription = "The WRM200 is a dual polarization Doppler weather radar with already proven new applications like HydroClass™ - the real time operational hydrometeor classification software. This software is the first COTS product that uses real-time polarization measurement for classifying targets into categories such as hail, snow pellets, snow or rain. The WRM200 provides superior data quality, accurate measurement, easy maintenance access, low maintenance costs, semi-yoke style antenna/pedestal with a low momentum of inertia and weight.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/WRM200_210x170.jpg",
                    UnitPrice = 2500000.00M,
                    CategoryID = 3
                },
                new Product
                {
                    ProductName = "WRM100",
                    ShortDescription = "Radar for detecting precipitation.",
                    FullDescription = "WRM100 is a single polarization Doppler weather radar with upgrade possibility to dual polarization. The WRM100 provides superior data quality, accurate measurement, easy maintenance access, low maintenance costs, semi-yoke style antenna/pedestal with a low momentum of inertia and weight.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/WRM100_210x170.JPG",
                    UnitPrice = 1700000.00M,
                    CategoryID = 3
                },
                new Product
                {
                    ProductName = "WRK200",
                    ShortDescription = "Radar for detecting precipitation.",
                    FullDescription = "For customers who require coherent transmitter, Vaisala Dual Polarization Doppler Weather Radar WRK200 is equipped with klystron transmitter technology. WRK200 offers exceptional spectral purity and phase noise performance. Vaisala dual polarization technology supports already proven applications like HydroClass™ - real time operational hydrometeor classification software. This software is the first COTS product that uses real time polarization measurement for classifying targets into categories such as hail, snow pellets, snow or rain. It can also be used to identify and filter out non-meteorological targets.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/WRK200.jpg",
                    UnitPrice = 1500000.00M,
                    CategoryID = 3
                },
                new Product
                {
                    ProductName = "Vaisala Guardian",
                    ShortDescription = "Information system for road.",
                    FullDescription = "Vaisala Guardian is an innovative, non-invasive Road Weather Information System (RWIS) that cost-effectively provides you with the most accurate pavement-specific weather information available.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/Guardian_210x170.jpg",
                    UnitPrice = 256000.00M,
                    CategoryID = 4
                },
                new Product
                {
                    ProductName = "Vaisala Road Weather Observer",
                    ShortDescription = "Information system for road.",
                    FullDescription = "The Vaisala RoadDSS Observer is a hosted web service for viewing real-time road weather data. The application displays data collected by the Vaisala Global Data Center which handles data collection from the road weather networks around the world. The application consists of a set of dynamic web pages that can be viewed over an Internet connection.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/RoadDSS-Observer-210x170.jpg",
                    UnitPrice = 145000.00M,
                    CategoryID = 4
                },
                new Product
                {
                    ProductName = "Vaisala AviMet Weather Display",
                    ShortDescription = "Information system for aviation.",
                    FullDescription = "Vaisala AviMet Displays deliver realtime data and history for use by airport staff. Vaisala AviMet Displays present data in comprehensive and easy-to-read formats, providing critical weather and wind information necessary for operational decision-making.",
                    ImageURL = "https://srcwebstore.blob.core.windows.net/images/AviMet-displays_210x170.jpg",
                    UnitPrice = 1678.00M,
                    CategoryID = 5
                }
            };

            return products;
        }

        /// <summary>
        /// Add a user that can only browse products in store, a user that can also edit products and
        /// categories in store and a user that can also edit other users.
        /// </summary>
        /// <param name="context"></param>
        private static bool AddUsersAndRoles(WebStore.Models.WebStoreDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));

            // we have multiple roles
            ir = rm.Create(new IdentityRole("canEditProducts"));
            ir = rm.Create(new IdentityRole("canEditCategories"));
            ir = rm.Create(new IdentityRole("canEditUsers"));

            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            // three test users, one can only view products, one that can edit products and categories
            // and one that can also edit other users
            var users = new List<ApplicationUser> {
                new ApplicationUser
                {
                    Email = "janez.novak@gmail.com",
                    UserName = "janez.novak@gmail.com"
                },
                new ApplicationUser
                {
                    Email = "marijan.lovec@webstore.si",
                    UserName = "marijan.lovec@webstore.si"
                },
                new ApplicationUser
                {
                    Email = "admin@webstore.si",
                    UserName = "admin@webstore.si"
                }
            };

            // add all users. All of them have the same password.
            foreach (ApplicationUser user in users)
            {
                ir = um.Create(user, "P_assw0rd1");

                if (ir.Succeeded == false)
                    return ir.Succeeded;
            }

            // add roles to users. The second user has the ability to edit products and categories
            um.AddToRole(users[1].Id, "canEditProducts");
            um.AddToRole(users[1].Id, "canEditCategories");

            // the third user can edit all
            um.AddToRole(users[2].Id, "canEditProducts");
            um.AddToRole(users[2].Id, "canEditCategories");
            um.AddToRole(users[2].Id, "canEditUsers");

            return true;
        }
    }
}
