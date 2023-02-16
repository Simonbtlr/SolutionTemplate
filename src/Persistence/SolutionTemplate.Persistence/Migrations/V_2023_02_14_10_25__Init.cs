using FluentMigrator;
using Migration = SolutionTemplate.Persistence.Extensions.Migration;

namespace SolutionTemplate.Persistence.Migrations;

[Migration(2023_02_14_10_25)]
public sealed class Init : Migration
{
    protected override string Sql => @"
CREATE TABLE IF NOT EXISTS point(
    id          BIGSERIAL PRIMARY KEY,
    order_id    BIGINT NOT NULL
);

CREATE TABLE IF NOT EXISTS orders(
    id BIGSERIAL PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS item(
    id          BIGSERIAL PRIMARY KEY,
    order_id    BIGINT NOT NULL
);
";
}
