using HomeworkWebServices.Models;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkWebServices
{
    [TestFixture]
    public class Tests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:3000");
            _client.DefaultRequestHeaders.Add("G-Token", "ROM831ESV");
        }

        [Test]
        [Order(1)]
        public async Task GetHouseholds()
        {      

            var response = await _client.GetAsync("/households");
            
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        [Order(2)]
        public async Task CreateHousehold()
        {         

            var requestHousehold = new Household() {Name = "Varna" };
            var request = new HttpRequestMessage(HttpMethod.Post, "/households");
            request.Content = new StringContent(requestHousehold.ToJson(), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var resString = await response.Content.ReadAsStringAsync();

            var household = Household.FromJson(resString);
          
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual("Varna", household.Name);
        }

        [Test] 
        [Order(3)]
        public async Task CreateUser()
        {          

            var requestUser = new User() { Email = "test3@test.com", FirstName = "Ivan", LastName = "Ivanov", HouseholdId = 2};
            var request = new HttpRequestMessage(HttpMethod.Post, "/users");
            request.Content = new StringContent(requestUser.ToJson(), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var resString = await response.Content.ReadAsStringAsync();

            var user = User.FromJson(resString);
           
            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Ivan", user.FirstName);
            Assert.AreEqual("Ivanov", user.LastName);
        }


        [Test]
        [Order (4)]
        public async Task GetBooks()
        {
            var response = await _client.GetAsync("/books");
         
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        [Order(5)]
        public async Task AddBook()
        {

            var requestBook = new Book() { Id = 10, Title = "Harry Poter 3", Author = "Joan Rowling", PublicationDate = "2003-11-18", Isbn = "3699665222"};
            var request = new HttpRequestMessage(HttpMethod.Post, "/books");
            request.Content = new StringContent(requestBook.ToJson(), Encoding.UTF8, "application/json");

            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var resString = await response.Content.ReadAsStringAsync();

            var book = Book.FromJson(resString);                           

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.AreEqual("Harry Poter 3", book.Title);            
        }

        [Test]
        [Order(6)]
        public async Task AddBookWishlist()
        {                        
            var request = new HttpRequestMessage(HttpMethod.Post, "/wishlists/1/books/1");
           
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
         
            Assert.IsTrue(response.IsSuccessStatusCode);         
        }

        [Test]
        [Order(7)]
        public async Task GetHouseholdBooks()
        {
            var response = await _client.GetAsync("/wishlists/1/books");
          
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
};