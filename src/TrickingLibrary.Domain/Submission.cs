﻿namespace TrickingLibrary.Domain
{
    public class Submission : BaseEntity
    {
        public int TrickId { get; set; }
        public string Video { get; set; }
        public string Description { get; set; }
    }
}