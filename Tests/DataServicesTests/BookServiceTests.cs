using BooksApp.Contracts.Models;
using BooksApp.Services.DataServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Tests.DataServicesTests
{
    [TestFixture]
    class BookServiceTests
    {
        private BookService service;

        [OneTimeSetUp] // Initialisierung 2
        public void Init()
        {
            service = new BookService();
        }

        [Test]
        [TestCase("xamarin")]
        public void GetBooksAsyncTest_FoundMoreThanZeroBooks(string SearchText)
        {
            BookQuery result = service.GetBooksAsync(SearchText).Result;

            Assert.Greater(result.Count, 0);
        }

        [Test]
        [TestCase("einbuchdasdefinitivnichtexistiert")]
        public void GetBooksAsyncTest_FoundZeroBooks(string SearchText)
        {
            BookQuery result = service.GetBooksAsync(SearchText).Result;

            Assert.Zero(result.Count);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void GetBooksAsyncTest_ExceptionWhenInvalidSearchText(string SearchText)
        {
            AsyncTestDelegate del = new AsyncTestDelegate( async () => 
            {
                BookQuery result = await service.GetBooksAsync(SearchText);
            } );

            Assert.ThrowsAsync<ArgumentException>(del);
        }
    }
}
