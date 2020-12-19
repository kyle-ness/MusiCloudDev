using MusiCloud.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Localization.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusiCloud.Data
{
    public static class DbSeed
    {

        public static void Seed(MusiCloudContext _context)
        {

            // -------------------------- User Table-----------------------------------------------
            string[] userNames = { "Kyle30", "Noa30", "Yotam30", "Simba", "Zazo", "Saka", "Tom", "Men", "Women" };

            if (!_context.User.Any())
            {
                for (int i = 0; i <= 8; i++)
                {
                    User user = new User()
                    {
                        DisplayName = userNames[i]
                    };

                    _context.Add(user);
                    _context.SaveChanges();

                }
            }
        }
    }
}
            // -----------------------------------------------------------------------------------------

