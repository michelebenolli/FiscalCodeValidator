using FiscalCodeValidator.Models;
using FiscalCodeValidator.Tests.Data;
using System;
using Xunit;

namespace FiscalCodeValidator.Tests
{
    public class FiscalCodeTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NullOrEmpty_Parameter_Throws_ArgumentNullException(string input)
        {
            Assert.Throws<ArgumentException>(() => new FiscalCode(input));
        }

        [Theory]
        [JsonData("Data/Invalid/lastNameCodes.json")]
        public void LastName_Null_With_Invalid_LastName_Code(string input)
        {
            var fiscalCode = new FiscalCode($"{input}LRA90D70L378H");
            Assert.Null(fiscalCode.LastName);
        }

        [Theory]
        [JsonData("Data/Valid/lastNameCodes.json")]
        public void LastName_Not_Null_With_Valid_LastName_Code(string input)
        {
            var fiscalCode = new FiscalCode($"{input}LRA90D70L378H");
            Assert.NotNull(fiscalCode.LastName);
        }

        [Theory]
        [JsonData("Data/Invalid/firstNameCodes.json")]
        public void FirstName_Null_With_Invalid_FirstName_Code(string input)
        {
            var fiscalCode = new FiscalCode($"RSS{input}90D70L378H");
            Assert.Null(fiscalCode.FirstName);
        }

        [Theory]
        [JsonData("Data/Valid/firstNameCodes.json")]
        public void FirstName_Not_Null_With_Valid_FirstName_Code(string input)
        {
            var fiscalCode = new FiscalCode($"RSS{input}90D70L378H");
            Assert.NotNull(fiscalCode.FirstName);
        }

        [Theory]
        [JsonData("Data/Invalid/dateCodes.json")]
        public void Date_Null_With_Invalid_Date_Code(string input)
        {
            var fiscalCode = new FiscalCode($"RSSLRA{input}L378H");
            Assert.Null(fiscalCode.Date);
        }

        [Theory]
        [JsonData("Data/Valid/dateCodes.json")]
        public void Date_Not_Null_With_Valid_Date_Code(string input)
        {
            var fiscalCode = new FiscalCode($"RSSLRA{input}L378H");
            Assert.NotNull(fiscalCode.Date);
        }

        [Theory]
        [JsonData("Data/Invalid/placeCodes.json")]
        public void Place_Null_With_Invalid_Place_Code(string input)
        {
            var fiscalCode = new FiscalCode($"RSSLRA90D70{input}H");
            Assert.Null(fiscalCode.Place);
        }

        [Theory]
        [JsonData("Data/Valid/placeCodes.json")]
        public void Place_Not_Null_With_Valid_Place_Code(string input)
        {
            var fiscalCode = new FiscalCode($"RSSLRA90D70{input}H");
            Assert.NotNull(fiscalCode.Place);
        }

        [Theory]
        [JsonData("Data/Invalid/lastNames.json")]
        public void Invalid_LastName_Do_Not_Match(string code, string lastName)
        {
            var fiscalCode = new FiscalCode(code);
            var match = fiscalCode.MatchLastName(lastName);
            Assert.False(match);
        }

        [Theory]
        [JsonData("Data/Valid/lastNames.json")]
        public void Valid_LastName_Matches(string code, string lastName)
        {
            var fiscalCode = new FiscalCode(code);
            var match = fiscalCode.MatchLastName(lastName);
            Assert.True(match);
        }

        [Theory]
        [JsonData("Data/Invalid/firstNames.json")]
        public void Invalid_FirstName_Do_Not_Match(string code, string firstName)
        {
            var fiscalCode = new FiscalCode(code);
            var match = fiscalCode.MatchFirstName(firstName);
            Assert.False(match);
        }

        [Theory]
        [JsonData("Data/Valid/firstNames.json")]
        public void Valid_FirstName_Matches(string code, string firstName)
        {
            var fiscalCode = new FiscalCode(code);
            var match = fiscalCode.MatchFirstName(firstName);
            Assert.True(match);
        }

        [Theory]
        [ClassData(typeof(InvalidGendersData))]
        public void Invalid_Gender_Do_Not_Match(string code, Gender? gender)
        {
            var fiscalCode = new FiscalCode(code);
            var match = fiscalCode.MatchGender(gender);
            Assert.False(match);
        }

        [Theory]
        [ClassData(typeof(ValidGendersData))]
        public void Valid_Gender_Matches(string code, Gender? gender)
        {
            var fiscalCode = new FiscalCode(code);
            var match = fiscalCode.MatchGender(gender);
            Assert.True(match);
        }

        [Theory]
        [ClassData(typeof(InvalidDatesData))]
        public void Invalid_Date_Do_Not_Match(string dateCode, DateTime? date)
        {
            var fiscalCode = new FiscalCode($"RSSLRA{dateCode}L378H");
            var match = fiscalCode.MatchDate(date);
            Assert.False(match);
        }

        [Theory]
        [ClassData(typeof(ValidDatesData))]
        public void Valid_Date_Matches(string dateCode, DateTime? date)
        {
            var fiscalCode = new FiscalCode($"RSSLRA{dateCode}L378H");
            var match = fiscalCode.MatchDate(date);
            Assert.True(match);
        }

        [Theory]
        [ClassData(typeof(InvalidPlacesData))]
        public void Invalid_Place_Do_Not_Match(string placeCode, Place place)
        {
            var fiscalCode = new FiscalCode($"RSSLRA90D70{placeCode}H");
            var match = fiscalCode.MatchPlace(place);
            Assert.False(match);
        }

        [Theory]
        [ClassData(typeof(ValidPlacesData))]
        public void Valid_Place_Matches(string placeCode, Place place)
        {
            var fiscalCode = new FiscalCode($"RSSLRA90D70{placeCode}H");
            var match = fiscalCode.MatchPlace(place);
            Assert.True(match);
        }

        [Theory]
        [JsonData("Data/Valid/codes.json")]
        public void Valid_Code_Returns_True(string input)
        {
            var fiscalCode = new FiscalCode(input);
            var result = fiscalCode.IsValid();
            Assert.True(result);
        }

        [Theory]
        [JsonData("Data/Invalid/codes.json")]
        public void Invalid_Code_Returns_False(string input)
        {
            var fiscalCode = new FiscalCode(input);
            var result = fiscalCode.IsValid();
            Assert.False(result);
        }

        [Theory]
        [ClassData(typeof(ValidCodesData))]
        public void Valid_Code_With_Valid_Data_Returns_True(
            string code,
            string lastName,
            string firstName,
            Gender? gender,
            DateTime? date,
            Place place)
        {
            var fiscalCode = new FiscalCode(code);
            var result = fiscalCode.IsValid(
                firstName: firstName,
                lastName: lastName,
                gender: gender,
                date: date,
                place: place);

            Assert.True(result);
        }

        [Theory]
        [ClassData(typeof(InvalidCodesData))]
        public void Valid_Code_With_Invalid_Data_Returns_False(
            string code,
            string lastName,
            string firstName,
            Gender? gender,
            DateTime? date,
            Place place)
        {
            var fiscalCode = new FiscalCode(code);
            var result = fiscalCode.IsValid(
                firstName: firstName,
                lastName: lastName,
                gender: gender,
                date: date,
                place: place);

            Assert.False(result);
        }
    }
}
