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
            int[] userIds = { 1 };
            
            string[] userDisplayNames = { "Kyle1994" };

            string[] userEmails = { "Kyle1994@gmail.com" };

            string[] userPasswords = { "Aa123456!!" };

            string[] userConfirmPasswords = { "Aa123456!!" };

            string[] userTypes = { "User" };

            if (!_context.User.Any())
            {
                for (int i = 0; i <= 0; i++)
                {
                    User user = new User()
                    {
                        DisplayName = userDisplayNames[i],
                        Email = userEmails[i],
                        Password = userPasswords[i],
                        ConfirmPassword = userConfirmPasswords[i],
                        UserType = userTypes[i]
                    };

                    _context.Add(user);
                    _context.SaveChanges();

                }
            }

            // --------------------------------------------------------------------------------------


            // --------------------------Artist Table-----------------------------------------------

            string[] artistNames = { "Coldplay", "Queen" };

            string[] artistGenres = { "Rock", "Rock" };

            string[] artistImageLinks = { "bla", "bla" };

            string[] artistArtistLinks = { "bla", "bla" };

            if (!_context.Artist.Any())
            {
                for (int i = 0; i <= 1; i++)
                {
                    Artist artist = new Artist()
                    {
                        Name = artistNames[i],
                        Genre = artistGenres[i],
                        ImageLink = artistImageLinks[i],
                        AristLink = artistArtistLinks[i],
                    };

                    _context.Add(artist);
                    _context.SaveChanges();

                }
            }

            // ------------------------------------------------------------------------------------

            // --------------------------Concert Table-----------------------------------------------

            string[] concertNames = { "20 Years to Coldplay", "Mercury Rises" };

            int[] concertArtistIds = { 1009, 1010 };

            double[] concertLats = { 32.06133337842293, 31.319971773900196 };

            double[] concertLongs = { 34.7913075741064, 35.36288038430481 };

            string[] concertCountries = { "Israel", "Israel" };

            string[] concertCities = { "Tel-Aviv", "Mesada" };

            string[] concertAddressNames = { "Menora Mivtachim", "Mesada" };

            string[] concertDescriptions = { "First time is Israel", "Night lights show" };


            if (!_context.Concert.Any())
            {
                for (int i = 0; i <= 1; i++)
                {
                    Concert concert = new Concert()
                    {
                        Name = concertNames[i],
                        ArtistId = concertArtistIds[i],
                        Date = DateTime.Now,
                        Lat = concertLats[i],
                        Long = concertLongs[i],
                        Country = concertCountries[i],
                        City = concertCities[i],
                        AddressName = concertAddressNames[i],
                        Description = concertDescriptions[i],
                    };

                    _context.Add(concert);
                    _context.SaveChanges();

                }
            }

            // -------------------------------------------------------------------------------------


            // --------------------------Album Table-----------------------------------------------

            string[] albumNames = { "Mylo Xyloto", "A Night at the Opera" };

            string[] albumGenres = { "Rock", "Rock" };

            string[] albumImageLinks = { "bla", "bla" };

            string[] albumAlbumLinks = { "bla", "bla" };

            int[] albumArtistIds = { 1009, 1010 };

            if (!_context.Album.Any())
            {
                for (int i = 0; i <= 1; i++)
                {
                    Album album = new Album()
                    {
                        Name = albumNames[i],
                        Release_Date = DateTime.Now,   
                        Genre = albumGenres[i],
                        ImageLink = albumImageLinks[i],
                        AlbumLink = albumAlbumLinks[i],
                        ArtistId = albumArtistIds[i],
                    };

                    _context.Add(album);
                    _context.SaveChanges();

                }
            }

            // -----------------------------------------------------------------------------------------


            // --------------------------Song Table-----------------------------------------------

            string[] songNames = { "Paradise", "Bohemian Rhaphsody" };

            int[] songCountersPlay = { 1, 3 };

            string[] songLinksToPlays = { "bla", "bla" };

            int[] songAlbumIds = { 1009, 1010 };

            if (!_context.Song.Any())
            {
                for (int i = 0; i <= 1; i++)
                {
                    Song song = new Song()
                    {
                        Name = songNames[i],
                        CounterPlayed = songCountersPlay[i],
                        LinkToPlay = songLinksToPlays[i],
                        AlbumId = songAlbumIds[i],
                    };

                    _context.Add(song);
                    _context.SaveChanges();

                }
            }

            // -----------------------------------------------------------------------------------------
        }
    }
}
           


