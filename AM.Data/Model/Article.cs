﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AM.Data.Model
{
    public class ArticleE
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Article { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
