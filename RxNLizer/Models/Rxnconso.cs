using System;

namespace RxNLizer
{
    /// <summary>
    /// Maps to RxNorm RxCui and STR fields
    /// </summary>
    public class Rxnconso
    {
        public string Rxcui { get; set; }
        public string Str { get; set; }
        public int ElementCount { get; set; }
        public int LengthDifference { get; set; }

    }
}
