﻿namespace TrickingLibrary.Domain
{
    public class Submission : BaseEntity<int>
    {
        public string TrickId { get; set; }
        public string Video { get; set; }
        public string Description { get; set; }
    }
}