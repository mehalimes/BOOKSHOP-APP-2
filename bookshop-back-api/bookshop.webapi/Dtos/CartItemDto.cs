﻿namespace bookshop.webapi.Dtos
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public BookDto Book { get; set; }
        public int Quantity { get; set; }
    }
}
