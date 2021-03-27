using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Features.Query.WorkWithCsv
{
    /// <summary>
    /// Class is designed to work with Csv files
    /// </summary>
    public class CsvHelper
    {
        /// <summary>
        /// Method of parsing transactions from a Csv file
        /// </summary>
        /// <param name="fileName"> File name for parsing </param>
        /// <returns> Transaction list </returns>
        public static List<Transaction> ParseCsvTransaction(string fileName)
        {
            var transactions = new List<Transaction>();

            // Parsing a file with the fileName
            using (TextFieldParser tfp = new TextFieldParser(fileName + ".csv"))
            {
                // Setting delimiter options
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                // Adding rows from file to list
                var rows = new List<List<string>>();
                while (!tfp.EndOfData)
                {
                    rows.Add(tfp.ReadFields().ToList());
                }
                rows.RemoveAt(0);

                // Parsing rows from list and add them to transaction list
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