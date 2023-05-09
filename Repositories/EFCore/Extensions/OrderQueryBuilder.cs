using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Extensions
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            // bosluklari atip "," ile ayiriyoruz (id, title)=> orderParams[0] = "id" orderParams[1]="title"
            var orderParams = orderByQueryString.Trim().Split(',');

            // Book modelinin propertyleri alindi Title, Content gibi (Reflection ornegi)
            var properyInfos = typeof(Book)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                // Gelen query id desc,title,content desc gibi olabilir. Her bir virgul ile ayrilmis yapi icinde bosluk var ise yakaliyoruz (id desc)=>  propertyFromQueryName[0] = id propertyFromQueryName[1] = desc ifadeleri var ve id alani alindi.
                var propertyFromQueryName = param.Split(' ')[0];

                // model bilgilerinin uyusan bir alani var mi diye bakildi
                var objectProperty = properyInfos
                    .FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty is null)
                    continue;

                // Parametre " desc" ile bitiryorsa azalan bitmiyorsa artan ifadesi 
                var direction = param.EndsWith(" desc") ? "descending" : "ascending";

                // model alani ve artan mi azalan mi olarak uc uca eklendi orderQueryBuilder= "id desc,title,price desc," gibi ifadeleri tutacak
                orderQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }

            //sonundaki virgulden kurtuluyoruz
            var orderQuery = orderByQueryString.ToString().TrimEnd(',', ' ');

            return orderQuery;
        }
    }
}
