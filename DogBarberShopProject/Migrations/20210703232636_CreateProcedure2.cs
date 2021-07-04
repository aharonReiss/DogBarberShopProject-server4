using Microsoft.EntityFrameworkCore.Migrations;

namespace DogBarberShopProject.Migrations
{
    public partial class CreateProcedure2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE Addappointment
                                        @id int, 
                                        @queueTime datetime2
                                        
                                    AS
                                    BEGIN

                                    INSERT INTO  [DogBarberShopDB].[dbo].[queues]
                                        VALUES(@id, SYSDATETIME ( ) , @queueTime)
                                    END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
