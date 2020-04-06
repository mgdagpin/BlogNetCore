﻿using System;

namespace Canary.Domain.Entities
{
    public class BlogPost : AuditedBaseEntity
    {
        public int AuthorID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }


        public Author Author { get; set; }
    }
}
