namespace SolutionTemplate.Persistence.Extensions;

public abstract class Migration : FluentMigrator.Migration
{
    protected abstract string Sql { get; }

    public override void Up()
    {
        Execute.Sql(Sql);
    }

    public override void Down()
    {
        // Empty
    }
}
