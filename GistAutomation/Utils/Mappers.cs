using ExcelDataReader;
using GistAutomation.Records;

namespace GistAutomation.Utils;

public static class Mappers
{
    public static Admin1CodesAscii MapAdmin1Codes(this IExcelDataReader reader)
    {
        var excelData = new Admin1CodesAscii();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            switch (i)
            {
                case 0:
                    excelData.Code = reader.GetString(i) ?? string.Empty;
                    break;
                case 1:
                    excelData.Name = reader.GetString(i) ?? string.Empty;
                    break;
                case 2:
                    excelData.NameAscii = reader.GetString(i) ?? string.Empty;
                    break;
                case 3:
                    if (int.TryParse(reader.GetString(i), out var tempInt))
                    {
                        excelData.GeonameId = tempInt;
                    }
                    break;
            }

        }
        return excelData;
    }
    public static CountryInfo MapCountryInfo(this IExcelDataReader reader)
    {
        var excelData = new CountryInfo();
        for (int i = 0; i < reader.FieldCount; i++)
        {
            switch (i)
            {
                case 0:
                    excelData.ISOAlpha2 = reader.GetString(i) ?? string.Empty;
                    break;
                case 1:
                    excelData.ISOAlpha3 = reader.GetString(i) ?? string.Empty;
                    break;
                case 2:
                    if (int.TryParse(reader.GetString(i), out var iSONumeric))
                    {
                        excelData.ISONumeric = iSONumeric;
                    }
                    break;
                case 3:
                    excelData.FipsCode = reader.GetString(i) ?? string.Empty;
                    break;
                case 4:
                    excelData.Name = reader.GetString(i) ?? string.Empty;
                    break;
                case 5:
                    excelData.Capital = reader.GetString(i) ?? string.Empty;
                    break;
                case 6:
                    if (double.TryParse(reader.GetString(i), out var areaInSqKM))
                    {
                        excelData.AreaInSqKM = areaInSqKM;
                    }
                    break;
                case 7:
                    if (int.TryParse(reader.GetString(i), out var population))
                    {
                        excelData.Population = population;
                    }
                    break;

                case 8:
                    excelData.Continent = reader.GetString(i) ?? string.Empty;
                    break;

                case 9:
                    excelData.TLD = reader.GetString(i) ?? string.Empty;
                    break;
                case 10:
                    excelData.CurrencyCode = reader.GetString(i) ?? string.Empty;
                    break;
                case 11:
                    excelData.CurrencyName = reader.GetString(i) ?? string.Empty;
                    break;
                case 12:
                    excelData.Phone = reader.GetString(i) ?? string.Empty;
                    break;
                case 13:
                    excelData.PostalCode = reader.GetString(i) ?? string.Empty;
                    break;
                case 14:
                    excelData.PostalCodeRegex = reader.GetString(i) ?? string.Empty;
                    break;
                case 15:
                    excelData.Languages = reader.GetString(i) ?? string.Empty;
                    break;
                case 16:
                    if (int.TryParse(reader.GetString(i), out var id))
                    {
                        excelData.GeonameId = id;
                    }
                    break;
                case 17:
                    excelData.Neighbors = reader.GetString(i) ?? string.Empty;
                    break;
                case 18:
                    excelData.EquivFIPSCode = reader.GetString(i) ?? string.Empty;
                    break;
            }

        }
        return excelData;
    }
}