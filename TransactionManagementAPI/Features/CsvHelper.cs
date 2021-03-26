using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Features.Query.WorkWithCsv
{
    public class CsvHelper
    {
        public static List<Transaction> ParseCsvTransaction(string fileName)
        {
            var transactions = new List<Transaction>();
            using (TextFieldParser tfp = new TextFieldParser(fileName + ".csv"))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                var rows = new List<List<string>>();

                while (!tfp.EndOfData)
                {
                    rows.Add(tfp.ReadFields().ToList());
                }
                rows.RemoveAt(0);

                rows.ForEach(item =>
                {
                    transactions.Add(new Transaction
                    {
                        Id = int.Parse(item[0]),
                        Status = (TransactionStatus)Enum.Parse(typeof(TransactionStatus), item[1]),
                        Type = (TransactionType)Enum.Parse(typeof(TransactionType), item[2]),
                        ClientName = item[3],
                        Amount = float.Parse(item[4].Trim('$'), CultureInfo.InvariantCulture.NumberFormat),
                    });
                });
            }
            return transactions;
        }
    }
}