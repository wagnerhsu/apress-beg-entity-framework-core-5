﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCOre5WebApp.DAL.Migrations
{
    public partial class AddStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            IF OBJECT_ID('GetPersonsByState', 'P') IS NOT NULL
            DROP PROC UpdateProfilesCountry
            GO
 
            CREATE PROCEDURE [dbo].[GetPersonsByState]
                @State varchar(255)
            AS
            BEGIN
                SELECT p.*
                FROM Persons p
                INNER JOIN Addresses a on p.Id = a.PersonId
                WHERE a.State =  @State
            END";

            sql += @" 
            IF OBJECT_ID('AddLookUpItem', 'P') IS NOT NULL
            DROP PROC AddLookUpItem
            GO
 
            CREATE PROCEDURE [dbo].[AddLookUpItem]
                @Code varchar(255),
                @Description varchar(255),
                @LookUpTypeId int
            AS
            BEGIN
                INSERT INTO LookUps (Code, Description, LookUpType) VALUES (@Code, @Description, @LookUpTypeId)
            END"; 

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC GetPersonsByState; DROP PROC AddLookUpItem;");
        }
    }
}
