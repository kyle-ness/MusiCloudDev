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
            string[] userDisplayNames = { "Kyle1994", "Admin" };

            string[] userEmails = { "Kyle1994@gmail.com", "admin@musicloud.com" };

            string[] userPasswords = { "Aa123456!!", "Aa123456!!" };

            string[] userConfirmPasswords = { "Aa123456!!", "Aa123456!!" };

            string[] userTypes = { "Admin", "Admin" };

            if (!_context.User.Any())
            {
                for (int i = 0; i < userEmails.Length; i++)
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

            string[] artistNames = { "Coldplay", "Queen", "GreenDay", "Eminem", "Beyonce" }; ;

            string[] artistGenres = { "Rock", "Rock", "Rock", "Hip-Hop", "Pop" };

            string[] artistImageLinks = { "/images/artists/Coldplay/Coldplay.jpg",
                                          "/images/artists/Queen/Queen.jpg",
                                          "/images/artists/GreenDay/GreenDay.jpg",
                                          "/images/artists/Eminem/Eminem.jpg",
                                          "/images/artists/Beyonce/Beyonce.jpg" };

            string[] artistArtistLinks = { "We are Coldplay", "Freddie is the KING",
                                           "We are Idiots", "I'm not afraid", "I love JZ" };

            if (!_context.Artist.Any())
            {
                for (int i = 0; i < artistNames.Length; i++)
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

            int[] concertArtistIds = { 5, 6 };

            double[] concertLats = { 32.06133337842293, 31.319971773900196 };

            double[] concertLongs = { 34.7913075741064, 35.36288038430481 };

            string[] concertCountries = { "Israel", "Israel" };

            string[] concertCities = { "Tel-Aviv", "Mesada" };

            string[] concertAddressNames = { "Menora Mivtachim", "Mesada" };

            string[] concertDescriptions = { "First time in Israel", "Night lights show" };


            if (!_context.Concert.Any())
            {
                for (int i = 0; i < concertNames.Length; i++)
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

            string[] albumNames = { "Mylo Xyloto", "A Night at the Opera", "AmericanIdiot",
                                    "Recovery", "Lemonade" }; 

            string[] albumGenres = { "Rock", "Rock", "Rock", "Hip-Hop", "Pop" };

            string[] albumImageLinks = { "/images/albums/MyloXyloto/MyloXyloto.jpg",
                                         "/images/albums/ANightAtTheOpera/Opera.jpg",
                                         "/images/albums/AmericanIdiot/AmericanIdiot.jpg",
                                         "/images/albums/Recovery/Recovery.jpg",
                                         "/images/albums/Lemonade/Lemonade.jpg" };

            string[] albumAlbumLinks = { "Coldplay rocks Euroupe",
                                         "Sssshhhh... Connect to your soul.",
                                         "Oh Yeah we are Idiots",
                                         "I'm white and I like it",
                                         "Halo everybody" };


            int[] albumArtistIds = { 5, 6, 7, 8, 9 };

            if (!_context.Album.Any())
            {
                for (int i = 0; i < albumNames.Length; i++)
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

            string[] songNames = {  "Paradise", "BohemianRhapsody", "CharlieBrown",
                                    "Clocks", "FixYou", "SpeedOfSound", "Talk", "Trouble", "Yellow" };

            int[] songCountersPlay = { 1, 3, 0, 20, 15 };

            string[] songLinksToPlays = { "/songs/Coldplay/MyloXyloto/Coldplay-Paradise(OfficialVideo).mp3",
                                          "/songs/Queen/ANightAtTheOpera/Queen-BohemianRhapsody(1975Video).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-CharlieBrown(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Clocks(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-FixYou(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-SpeedOfSound(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Talk(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Trouble(Officialvideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Yellow(OfficialVideo).mp3"};

            int[] songAlbumIds = { 5, 6, 7, 8, 9 };

            if (!_context.Song.Any())
            {
                for (int i = 0; i < songNames.Length; i++)
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
           


