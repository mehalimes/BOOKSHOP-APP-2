﻿namespace bookshop.webapi.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int BookId { get; set; }
        public string Content { get; set; }
    }
}
