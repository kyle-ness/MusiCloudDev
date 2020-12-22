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
            string[] userDisplayNames = { "Kyle1994", "Admin", "Yotam"  };

            string[] userEmails = { "Kyle1994@gmail.com", "admin@musicloud.com", "yotam@gmail.com" };

            string[] userPasswords = { "Aa123456!!", "Aa123456!!", "Aa123456!!" };

            string[] userConfirmPasswords = { "Aa123456!!", "Aa123456!!", "Aa123456!!" };

            string[] userTypes = { "Admin", "Admin", "User" };

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

            int[] concertArtistIds = { 17, 18 };

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

            string[] albumNames = { "Mylo Xyloto", "A Night at the Opera", "American Idiot",
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


            int[] albumArtistIds = { 17, 18, 19, 20, 21 };

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
                                    "Clocks", "FixYou", "SpeedOfSound", "Talk", "Trouble", "Yellow",
            "AnotherOneBitestheDust", "Don'tStopMeNow", "IWantToBreakFree", "KillerQueen", "RadioGaGa",
            "SomebodyToLove", "TheShowMustGoOn", "UnderPressure", "WeAreTheChampions", "AmericanIdiot",
            "BoulevardOfBrokenDreams", "CleaninOutMyCloset", "TheRealSlimShady", "Halo", "RunTheWorld", };

            int[] songCountersPlay = { 1, 3, 8, 20, 15, 4, 4, 6, 7, 8, 9, 1, 23, 4, 5, 6, 14, 13, 14, 11, 16, 18,
            19, 20};

            string[] songLinksToPlays = { "/songs/Coldplay/MyloXyloto/Coldplay-Paradise(OfficialVideo).mp3",
                                          "/songs/Queen/ANightAtTheOpera/Queen-BohemianRhapsody(1975Video).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-CharlieBrown(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Clocks(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-FixYou(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-SpeedOfSound(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Talk(OfficialVideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Trouble(Officialvideo).mp3",
                                          "/songs/Coldplay/MyloXyloto/Coldplay-Yellow(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-AnotherOneBitestheDust(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-Don'tStopMeNow(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-IWantToBreakFree(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-KillerQueen(TopOfThePops,1974).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-RadioGaGa(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-SomebodyToLove(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-TheShowMustGoOn(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-UnderPressure(OfficialVideo).mp3",
                                "/songs/Queen/ANightAtTheOpera/Queen-WeAreTheChampions(OfficialVideo).mp3",
                                "/songs/GreenDay/GreenDay-AmericanIdiot[OFFICIALVIDEO].mp3",
                                "/songs/GreenDay/GreenDayBoulevardOfBrokenDreams-[OfficialVideo].mp3",
                                "/songs/Eminem/Eminem-Cleanin&#39OutMyCloset(OfficialVideo).mp3",
                                "/songs/Eminem/Eminem-TheRealSlimShady(OfficialVideo-CleanVersion).mp3",
                                "/songs/Beyonce/Beyoncé-Halo.mp3",
                                "/songs/Beyonce/Beyoncé-RunTheWorld(Girls)[Lyrics]HD.mp3",};

            int[] songAlbumIds = { 15, 16, 15, 15, 15, 15, 15, 15, 15, 16, 16, 16, 16, 16, 16, 16, 16, 16,
                17, 17, 18, 18, 19, 19  };

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
           


