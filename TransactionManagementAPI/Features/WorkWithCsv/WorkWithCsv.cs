using MediatR;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TransactionManagementAPI.Data;
using TransactionManagementAPI.Data.Enums;

namespace TransactionManagementAPI.Features.Query.WorkWithCsv
{
    public class WorkWithCsv
    {
        public string FileName { get; set; }

        public WorkWithCsv(string fileName)
        {
            FileName = fileName;
        }

        public List<Transaction> Read()
        {
            var transactions = new List<Transaction>();
            using (TextFieldParser tfp = new TextFieldParser(FileName + ".csv"))
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
                        Amount = item[4],
                    });
                });
            }
            return transactions;
        }
    }
}