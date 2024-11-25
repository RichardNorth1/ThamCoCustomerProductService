﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThamCoCustomerProductService.Data.Migrations
{
    /// <inheritdoc />
    public partial class companyProductsStockLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockLevel",
                table: "CompanyProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockLevel",
                table: "CompanyProducts");
        }
    }
}
