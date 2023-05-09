using Entities.Models;
using System.Reflection;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Repositories.EFCore.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
            books.Where(book=> 
            (book.Price >= minPrice) && 
            (book.Price <= maxPrice));

        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if(string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return books.Where(b=> b.Title.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.Id);

            // Query olusturan logic islemleri yoneten metot
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            if (orderQuery is null)
                return books.OrderBy(b => b.Id);

            //query stringimiz son halini almis oldu orn="title desc,id,price desc" sondaki virgul yerine bosluk atilmis oldu
            return books.OrderBy(orderQuery);
        }
    }
}
