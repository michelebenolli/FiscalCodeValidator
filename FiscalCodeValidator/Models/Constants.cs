﻿using System.Collections.Generic;

namespace FiscalCodeValidator.Models
{
    public static class Constants
    {
        public static readonly Dictionary<char, int> OddMap = new Dictionary<char, int>()
        {
            { '0', 1 },
            { '1', 0 },
            { '2', 5 },
            { '3', 7 },
            { '4', 9 },
            { '5', 13 },
            { '6', 15 },
            { '7', 17 },
            { '8', 19 },
            { '9', 21 },
            { 'A', 1 },
            { 'B', 0 },
            { 'C', 5 },
            { 'D', 7 },
            { 'E', 9 },
            { 'F', 13 },
            { 'G', 15 },
            { 'H', 17 },
            { 'I', 19 },
            { 'J', 21 },
            { 'K', 2 },
            { 'L', 4 },
            { 'M', 18 },
            { 'N', 20 },
            { 'O', 11 },
            { 'P', 3 },
            { 'Q', 6 },
            { 'R', 8 },
            { 'S', 12 },
            { 'T', 14 },
            { 'U', 16 },
            { 'V', 10 },
            { 'W', 22 },
            { 'X', 25 },
            { 'Y', 24 },
            { 'Z', 23 }
        };

        public static readonly Dictionary<char, int> EvenMap = new Dictionary<char, int>()
        {
            { '0', 0 },
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'A', 0 },
            { 'B', 1 },
            { 'C', 2 },
            { 'D', 3 },
            { 'E', 4 },
            { 'F', 5 },
            { 'G', 6 },
            { 'H', 7 },
            { 'I', 8 },
            { 'J', 9 },
            { 'K', 10 },
            { 'L', 11 },
            { 'M', 12 },
            { 'N', 13 },
            { 'O', 14 },
            { 'P', 15 },
            { 'Q', 16 },
            { 'R', 17 },
            { 'S', 18 },
            { 'T', 19 },
            { 'U', 20 },
            { 'V', 21 },
            { 'W', 22 },
            { 'X', 23 },
            { 'Y', 24 },
            { 'Z', 25 }
        };

        public static readonly Dictionary<char, char> OmocodeMap = new Dictionary<char, char>()
        {
            { 'L', '0' },
            { 'M', '1' },
            { 'N', '2' },
            { 'P', '3' },
            { 'Q', '4' },
            { 'R', '5' },
            { 'S', '6' },
            { 'T', '7' },
            { 'U', '8' },
            { 'V', '9' }
        };
    }
}