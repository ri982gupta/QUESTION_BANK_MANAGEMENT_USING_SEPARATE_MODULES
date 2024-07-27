using System;
using System.Collections;
using System.IO;

namespace QB.Model
{
    public class Option
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public bool IsCorrect { get; set; } = false;
    }
}