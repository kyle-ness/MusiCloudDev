using LinqToTwitter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TweetSharp;

namespace MusiCloud.Controllers
{
    public class TweetsController : Controller
    {
        private readonly IConfiguration _config;
        private readonly TwitterContext _twitterContext;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly string _accessToken;
        private readonly string _accessTokenSecret;

        public TweetsController(IConfiguration config)
        {
            _config = config;
            this._consumerKey = _config.GetValue<string>("ConsumerKey");
            this._consumerSecret = _config.GetValue<string>("ConsumerSecret");
            this._accessToken = _config.GetValue<string>("AccessToken");
            this._accessTokenSecret = _config.GetValue<string>("AccessTokenSecret");
            this._twitterContext = new TwitterContext(new MvcAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore()
                {
                    ConsumerKey = _consumerKey,
                    ConsumerSecret = _consumerSecret,
                    AccessToken = _accessToken,
                    AccessTokenSecret = _accessTokenSecret
                }
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Tweet(string albumName)
        {
            var message = "#MusiCloudWebApp A new album has just added to our library - " + albumName + "!";

            TwitterService service = new TwitterService(_consumerKey, _consumerSecret);
            service.AuthenticateWith(_accessToken, _accessTokenSecret);

            service.SendTweet(new SendTweetOptions() { Status = message });

            return View("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<JsonResult> GetTweets()
        {
            var searchWord = "#MusiCloudWebApp";

            var searchResponse = await
                (from search in _twitterContext.Search
                 where search.Type == SearchType.Search &&
                       search.Query == searchWord
                 select search).FirstOrDefaultAsync();

            var tweets = (from tweet in searchResponse.Statuses
                          select new Tweets
                          {
                              Nickname = tweet.User.ScreenNameResponse,
                              Comment = tweet.Text
                          }).ToList().Take(10);
            return Json(tweets);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        public class Tweets
        {
            public string Nickname { get; set; }
            public string Comment { get; set; }
        }
    }
}