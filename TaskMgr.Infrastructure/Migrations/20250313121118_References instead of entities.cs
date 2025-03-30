using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMgr.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Referencesinsteadofentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Targets_RelatedTargetId",
                table: "Routines");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Routines_RelatedRoutineId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Targets_RelatedTargetId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "RelatedTargetId",
                table: "Tasks",
                newName: "TargetEntityId");

            migrationBuilder.RenameColumn(
                name: "RelatedRoutineId",
                table: "Tasks",
                newName: "RoutineEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_RelatedTargetId",
                table: "Tasks",
                newName: "IX_Tasks_TargetEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_RelatedRoutineId",
                table: "Tasks",
                newName: "IX_Tasks_RoutineEntityId");

            migrationBuilder.RenameColumn(
                name: "RelatedTargetId",
                table: "Routines",
                newName: "TargetEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Routines_RelatedTargetId",
                table: "Routines",
                newName: "IX_Routines_TargetEntityId");

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedRoutine",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedTarget",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedTarget",
                table: "Routines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Targets_TargetEntityId",
                table: "Routines",
                column: "TargetEntityId",
                principalTable: "Targets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Routines_RoutineEntityId",
                table: "Tasks",
                column: "RoutineEntityId",
                principalTable: "Routines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Targets_TargetEntityId",
                table: "Tasks",
                column: "TargetEntityId",
                principalTable: "Targets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Targets_TargetEntityId",
                table: "Routines");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Routines_RoutineEntityId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Targets_TargetEntityId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RelatedRoutine",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RelatedTarget",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RelatedTarget",
                table: "Routines");

            migrationBuilder.RenameColumn(
                name: "TargetEntityId",
                table: "Tasks",
                newName: "RelatedTargetId");

            migrationBuilder.RenameColumn(
                name: "RoutineEntityId",
                table: "Tasks",
                newName: "RelatedRoutineId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TargetEntityId",
                table: "Tasks",
                newName: "IX_Tasks_RelatedTargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_RoutineEntityId",
                table: "Tasks",
                newName: "IX_Tasks_RelatedRoutineId");

            migrationBuilder.RenameColumn(
                name: "TargetEntityId",
                table: "Routines",
                newName: "RelatedTargetId");

            migrationBuilder.RenameIndex(
                name: "IX_Routines_TargetEntityId",
                table: "Routines",
                newName: "IX_Routines_RelatedTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Targets_RelatedTargetId",
                table: "Routines",
                column: "RelatedTargetId",
                principalTable: "Targets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Routines_RelatedRoutineId",
                table: "Tasks",
                column: "RelatedRoutineId",
                principalTable: "Routines",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Targets_RelatedTargetId",
                table: "Tasks",
                column: "RelatedTargetId",
                principalTable: "Targets",
                principalColumn: "Id");
        }
    }
}
