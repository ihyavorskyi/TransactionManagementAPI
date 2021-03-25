using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactionManagementAPI.Migrations
{
    public partial class ChangeColumnIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "Transactions",
            //    newName: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "TransactionId",
            //    table: "Transactions",
            //    newName: "Id");
        }
    }
}