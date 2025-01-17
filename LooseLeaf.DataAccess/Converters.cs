﻿using LooseLeaf.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooseLeaf.DataAccess
{
    public static class Converters
    {
        public static IBook ConvertToIBook(this Book book) => new Business.Models.Book(book.Id, book.Title, book.Author, book.Isbn, book.Genres.Select(g => g.GenreName).ToList(), book.Thumbnail);

        public static IAddress ConvertToIAddress(this Address address) => new Business.Models.Address(address.Address1, address.Address2, address.City, address.State, address.Country, address.Zipcode);
    }
}