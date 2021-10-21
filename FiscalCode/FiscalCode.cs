using FiscalCodeValidator.Models;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FiscalCodeValidator
{
    public class FiscalCode
    {
        private readonly string _code;
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public Gender? Gender { get; private set; }
        public DateTime? Date { get; private set; }
        public Place Place { get; private set; }
        public string Code { get; private set; }

        public FiscalCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException(null, nameof(code));
            }

            if (!Regex.Match(code, "[a-zA-Z0-9]{16}").Success)
            {
                return;
            }

            Code = Utility.Clean(code);
            _code = Normalize(Code);

            TrySetLastName();
            TrySetFirstName();
            TrySetGender();
            TrySetDate();
            TrySetPlace();
        }

        /// <summary>
        /// Check if the code data match the person last name
        /// </summary>
        /// <param name="lastName">The person last name</param>
        public bool MatchLastName(string lastName)
        {
            return LastName != null && lastName != null && LastName == GetLastNameCode(lastName);
        }

        /// <summary>
        /// Check if the code data match the person first name
        /// </summary>
        /// <param name="firstName">The person first name</param>
        public bool MatchFirstName(string firstName)
        {
            return FirstName != null && firstName != null && FirstName == GetFirstNameCode(firstName);
        }

        /// <summary>
        /// Check if the code data match the person gender
        /// </summary>
        /// <param name="gender">The person gender</param>
        public bool MatchGender(Gender? gender)
        {
            return Gender != null && Gender == gender;
        }

        /// <summary>
        /// Check if the code data match the person birth date
        /// </summary>
        /// <param name="date">The person birth date</param>
        public bool MatchDate(DateTime? date)
        {
            return Date != null && date != null && Date.Value.Date == date.Value.Date;
        }

        /// <summary>
        /// Check if the code data match the person birth place
        /// </summary>
        /// <param name="place">The person birth place</param>
        public bool MatchPlace(Place place)
        {
            return Place != null && Place.Code == place?.Code;
        }

        /// <summary>
        /// Check if the fiscal code is valid
        /// </summary>
        /// <param name="lastName">The person last name</param>
        /// <param name="firstName">The person first name</param>
        /// <param name="date">The person birth date</param>
        /// <param name="gender">The person gender</param>
        /// <param name="place">The person birth place</param>
        public bool IsValid(
            string lastName = null,
            string firstName = null,
            DateTime? date = null,
            Gender? gender = null,
            Place place = null)
        {
            if (this.HasNullProperty())
            {
                return false;
            }

            if ((lastName != null && !MatchLastName(lastName)) ||
                (firstName != null && !MatchFirstName(firstName)) ||
                (gender != null && !MatchGender(gender)) ||
                (date != null && !MatchDate(date)) ||
                (place != null && !MatchPlace(place)))
            {
                return false;
            }

            var checkCode = GetCheckCode(Code);
            return Code.EndsWith(checkCode);
        }

        private static string Normalize(string code)
        {
            var result = new StringBuilder(code);

            foreach (var i in new [] { 6, 7, 9, 10, 12, 13, 14 })
            {
                if (Constants.OmocodeMap.ContainsKey(code[i]))
                {
                    result[i] = Constants.OmocodeMap[code[i]];
                }
            }

            result[15] = GetCheckCode(result.ToString());
            return result.ToString();
        }

        private void TrySetLastName()
        {
            var name = _code.Substring(0, 3);
            if (IsValidNameCode(name))
            {
                LastName = name;
            }
        }

        private void TrySetFirstName()
        {
            var name = _code.Substring(3, 3);
            if (IsValidNameCode(name))
            {
                FirstName = name;
            }
        }

        private void TrySetGender()
        {
            var result = int.TryParse(_code.Substring(9, 2), out int day);
            if (result)
            {
                if (day > 0 && day < 32)
                {
                    Gender = Models.Gender.Male;
                }

                if (day > 40 && day < 72)
                {
                    Gender = Models.Gender.Female;
                }
            }
        }

        private void TrySetDate()
        {
            var yearResult = int.TryParse(_code.Substring(6, 2), out int year);
            var dayResult = int.TryParse(_code.Substring(9, 2), out int day);
            var month = "ABCDEHLMPRST".IndexOf(_code[8]) + 1;

            if (yearResult && dayResult && month > 0)
            {
                day -= day > 40 ? 40 : 0;
                year += year + 2000 >= DateTime.Now.Year ? 1900 : 2000;

                if (DateTime.TryParseExact($"{year}/{month}/{day}", "yyyy/M/d",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    Date = date;
                }
            }
        }

        private void TrySetPlace()
        {
            Place = Utility.GetPlace(_code.Substring(11, 4));
        }

        private static bool IsValidNameCode(string code)
        {
            var match = Regex.Match(code, "[B-DF-HJ-NP-TV-Z]{0,3}[AEIOU]{0,3}[X]{0,3}");
            return match.Success && match.Value.Length == 3;
        }

        private static string GetLastNameCode(string name)
        {
            name = Utility.Clean(name);
            return $"{Utility.GetConsonants(name)}{Utility.GetVowels(name)}XXX".Substring(0, 3);
        }

        private static string GetFirstNameCode(string name)
        {
            name = Utility.Clean(name);
            var consonants = Utility.GetConsonants(name);
            return consonants.Length >= 4 ?
                $"{consonants[0]}{consonants[2]}{consonants[3]}" :
                $"{consonants}{Utility.GetVowels(name)}XXX".Substring(0, 3);
        }

        private static char GetCheckCode(string code)
        {
            var value = 0;
            for (var i = 0; i < 15; i++)
            {
                var c = code[i];
                value += i % 2 != 0 ? Constants.EvenMap[c] : Constants.OddMap[c];
            }
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[value % 26];
        }
    }
}
