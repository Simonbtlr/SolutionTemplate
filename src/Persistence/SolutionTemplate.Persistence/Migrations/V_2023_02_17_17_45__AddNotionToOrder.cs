using FluentMigrator;
using Migration = SolutionTemplate.Persistence.Extensions.Migration;

namespace SolutionTemplate.Persistence.Migrations;

[Migration(2023_02_17_17_45)]
public sealed class AddNoteToOrder : Migration
{
    protected override string Sql => @"
ALTER TABLE IF EXISTS orders 
    ADD COLUMN IF NOT EXISTS note TEXT;
ALTER TABLE IF EXISTS point 
    ADD COLUMN IF NOT EXISTS note TEXT;
ALTER TABLE IF EXISTS item
    ADD COLUMN IF NOT EXISTS note TEXT;
";
}
