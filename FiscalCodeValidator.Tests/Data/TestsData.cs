using FiscalCodeValidator.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FiscalCodeValidator.Tests.Data
{
    public class ValidDatesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "90D30", new DateTime(1990, 4, 30) };
            yield return new object[] { "V0D30", new DateTime(1990, 4, 30) };
            yield return new object[] { "VLD30", new DateTime(1990, 4, 30) };
            yield return new object[] { "90DP0", new DateTime(1990, 4, 30) };
            yield return new object[] { "90DPL", new DateTime(1990, 4, 30) };
            yield return new object[] { "VLDPL", new DateTime(1990, 4, 30) };
            yield return new object[] { "90D30", new DateTime(1990, 4, 30, 12, 00, 00) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InvalidDatesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "90D30", new DateTime(2090, 4, 30) };
            yield return new object[] { "90D30", new DateTime(2000, 4, 30) };
            yield return new object[] { "90D30", new DateTime(1990, 5, 30) };
            yield return new object[] { "90D30", new DateTime(1990, 4, 29) };
            yield return new object[] { "90D30", new DateTime(0001, 1, 1) };
            yield return new object[] { "V0D30", new DateTime(1990, 4, 29) };
            yield return new object[] { "VLD30", new DateTime(1990, 3, 30) };
            yield return new object[] { "90DP0", new DateTime(1989, 4, 30) };
            yield return new object[] { "90D30", null };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ValidPlacesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "L378", Utility.GetPlace("L378") };
            yield return new object[] { "Z907", Utility.GetPlace("Z907") };
            yield return new object[] { "A001", Utility.GetPlace("A001") };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InvalidPlacesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "L378", Utility.GetPlace("A001") };
            yield return new object[] { "Z907", Utility.GetPlace("Z999") };
            yield return new object[] { "A001", Utility.GetPlace("") };
            yield return new object[] { "A001", null };
            yield return new object[] { "++++", null };
            yield return new object[] { "    ", null };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InvalidGendersData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "RSSLRA90D70L378H", Gender.Male };
            yield return new object[] { "RSSLRA90D30L378H", Gender.Female };
            yield return new object[] { "RSSLRA90D70L378H", null };
            yield return new object[] { "RSSLRA90D30L378H", null };
            yield return new object[] { "RSSLRA90D35L378H", Gender.Male };
            yield return new object[] { "RSSLRA90D35L378H", Gender.Female };
            yield return new object[] { "RSSLRA90D90L378H", Gender.Male };
            yield return new object[] { "RSSLRA90D90L378H", Gender.Female };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ValidGendersData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "RSSLRA90D70L378H", Gender.Female };
            yield return new object[] { "RSSLRA90D30L378H", Gender.Male };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ValidCodesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "THDSDA95P08Z330H", "Ait Hadda", "Saad", Gender.Male, new DateTime(1995, 9, 8), Utility.GetPlace("Z330") };
            yield return new object[] { "BLSLGS84C22Z145O", "Belousovs", "Olegs", Gender.Male, new DateTime(1984, 3, 22), Utility.GetPlace("Z145") };
            yield return new object[] { "BRNGNN71B26L219T", "Bruno", "Giovanni", Gender.Male, new DateTime(1971, 2, 26), Utility.GetPlace("L219") };
            yield return new object[] { "CCCFBA85D03L219P", "Caccamo", "Fabio", Gender.Male, new DateTime(1985, 4, 3), Utility.GetPlace("L219") };
            yield return new object[] { "GMBLSN84A05G674H", "Gomba", "Alessandro", Gender.Male, new DateTime(1984, 1, 5), Utility.GetPlace("G674") };
            yield return new object[] { "MRTMRA83T56A269B", "Martini", "Maria", Gender.Female, new DateTime(1983, 12, 16), Utility.GetPlace("A269") };
            yield return new object[] { "PNLMHL79R27I158P", "Panella", "Michele", Gender.Male, new DateTime(1979, 10, 27), Utility.GetPlace("I158") };
            yield return new object[] { "CHRVGN94P64H282H", "Chiarelli", "Virginia", Gender.Female, new DateTime(1994, 9, 24), Utility.GetPlace("H282") };
            yield return new object[] { "CHRVGN94PS4H2U2F", "Chiarelli", "Virginia", Gender.Female, new DateTime(1994, 9, 24), Utility.GetPlace("H282") };
            yield return new object[] { "CHRVGN94PS4H2U2F", "Chiarelli", null, null, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, "Virginia", null, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, Gender.Female, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, null, new DateTime(1994, 9, 24), null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, null, null, Utility.GetPlace("H282") };
            yield return new object[] { "CHRVGN94PS4H2U2F", "Chiarelli", "Virginia", null, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, null, null, null };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InvalidCodesData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "THDSDA95P08Z330H", "Hadda Ait", "Sarad", Gender.Female, new DateTime(1993, 9, 8), Utility.GetPlace("Z333") };
            yield return new object[] { "BLSLGS84C22Z145O", "Brelousovs", "Otlegs", Gender.Female, new DateTime(1984, 4, 22), Utility.GetPlace("Z146") };
            yield return new object[] { "BRNGNN71B26L219T", "Biruno", "Grovanni", Gender.Female, new DateTime(1971, 2, 27), Utility.GetPlace("Y219") };
            yield return new object[] { "CCCFBA85D03L219P", "Raccamo", "Frabio", Gender.Female, new DateTime(2085, 4, 3), Utility.GetPlace("Y218") };
            yield return new object[] { "GMBLSN84A05G674H", "Combare", "Allex", Gender.Female, new DateTime(1, 1, 5), Utility.GetPlace("G647") };
            yield return new object[] { "MRTMRA83T56A269B", "Mywaria", "Muartini", Gender.Male, new DateTime(1983, 12, 1), Utility.GetPlace("A26 ") };
            yield return new object[] { "PNLMHL79R27I158P", "Pontella", "Micele", Gender.Female, new DateTime(1980, 10, 27), Utility.GetPlace("Z158") };
            yield return new object[] { "CHRVGN94P64H282H", "Ciarelli", "Vartinia", Gender.Male, new DateTime(1994, 10, 24), Utility.GetPlace("H283") };
            yield return new object[] { "CHRVGN94PS4H2U2F", "Ciarelli", "Tirginia", Gender.Male, new DateTime(1994, 1, 24), Utility.GetPlace("F282") };
            yield return new object[] { "CHRVGN94PS4H2U2F", "Ciarelli", null, null, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, "Tirginia", null, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, Gender.Male, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, null, new DateTime(1994, 1, 24), null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, null, null, Utility.GetPlace("F282") };
            yield return new object[] { "CHRVGN94PS4H2U2F", "Ciarelli", "Virginia", null, null, null };
            yield return new object[] { "CHRVGN94PS4H2U2F", null, null, Gender.Male, new DateTime(1994, 1, 24), null };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
