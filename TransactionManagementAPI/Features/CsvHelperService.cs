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
    public class CsvHelperService
    {
        private readonly AppDbContext _context;

        public CsvHelperService(AppDbContext context)
        {
            _context = context;
        }

        public void ExportTransactionFromCsv()
        {
            var transactions = ParseCsvTransaction("data");
            MergeTransactionsToDB(transactions);
        }

        /// <summary>
        /// Method of parsing transactions from a Csv file
        /// </summary>
        /// <param name="fileName"> File name for parsing </param>
        /// <returns> Transaction list </returns>
        private List<Transaction> ParseCsvTransaction(string fileName)
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

        /// <summary>
        /// Method that checks for a transaction and updates its status, or creates a new one
        /// </summary>
        /// <param name="transactions"> List transactions </param>
        private void MergeTransactionsToDB(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                var transactionExist = _context.Transactions.Find(transaction.Id);

                // If transaction exist update her status,
                // else create new transaction in DataBase
                if (transactionExist != null)
                {
                    transactionExist.Status = transaction.Status;
                }
                else
                {
                    transaction.Id = 0;
                    _context.Transactions.AddAsync(transaction);
                }
                _context.SaveChanges();
            }
        }
    }
}