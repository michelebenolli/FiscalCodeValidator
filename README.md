# FiscalCodeValidator

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/808426d051784a5f9e24ed601ae89c9a)](https://app.codacy.com/gh/michelebenolli/FiscalCodeValidator?utm_source=github.com&utm_medium=referral&utm_content=michelebenolli/FiscalCodeValidator&utm_campaign=Badge_Grade_Settings)

Fiscal Code Validator can be used to perform a syntactic validation of [Italian fiscal codes][1].

## FiscalCode

The `FiscalCode` class can be initialized by passing the complete *fiscal code* in the constructor. 
The code is decomposed into the constituent units (*first name*, *last name*, *gender*, *birth date*, and *birth place*). 
Each piece of information is individually extracted from the string and the class properties are populated. 
If the code provided is incorrect, some of the properties may be left `null`.

```csharp
FiscalCode fiscalCode = new FiscalCode("RSSLRA90D70L378H");
```

```csharp
string lastName = fiscalCode.LastName; // RSS
string firstName = fiscalCode.FirstName; // LRA
DateTime? date = fiscalCode.Date; // 1990-04-30
Gender? gender = fiscalCode.Gender; // Gender.Female
Place place = fiscalCode.Place; // Trento
```

## Match

The `FiscalCode` class provides some methods to check if the fiscal code data partially corresponds to the owner’s personal information. 
It should be noted that these functions do not provide information on the overall validity of the code. 

```csharp
bool result = fiscalCode.matchLastName("Rossi"); // true
bool result = fiscalCode.matchFirstName("Laura"); // true
bool result = fiscalCode.matchGender(Gender.Male); // false
bool result = fiscalCode.matchDate(new DateTime(1990, 5, 30)); // false
bool result = fiscalCode.matchPlace(Utility.GetPlace("L378")); // true
```
## Validity

The `isValid()` method performs a syntactic validation of each section of the code, individually. 
If no parameters are provided, the method checks if all the properties of the `FiscalCode` instance have been populated correctly 
and then verifies the validity of the check digit.

```csharp
bool result = fiscalCode.isValid(); // true
```

If one or more parameters are provided, the method performs the former validation and then checks whether the data provided matches the code information.

```csharp
bool result = fiscalCode.isValid(firstName: "Laura", gender: Gender.Female); // true
bool result = fiscalCode.isValid(gender: Gender.Male); // false
```

### Notes
1. The validation method considers the [omocodia][2] problem
2. To verify the *existence* of a fiscal code you can use [this][3] service

[1]: <https://en.wikipedia.org/wiki/Italian_fiscal_code> "Italian fiscal codes"
[2]: <https://it.wikipedia.org/wiki/Omocodia> "omocodia"
[3]: <https://telematici.agenziaentrate.gov.it/VerificaCF/Scegli.do?parameter=verificaCf> "VerificaCf"
