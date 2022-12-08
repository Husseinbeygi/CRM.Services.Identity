﻿namespace CRM.Services.Identity.Infrastructure;

public abstract class BasePageModelWithDatabaseContext : BasePageModel
{
	public BasePageModelWithDatabaseContext
		(Data.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	protected Data.DatabaseContext DatabaseContext { get; }

	//protected readonly Data.DatabaseContext DatabaseContext;

	protected async
		Task DisposeDatabaseContextAsync()
	{
		if (DatabaseContext != null)
		{
			await DatabaseContext.DisposeAsync();

			//DatabaseContext = null;
		}
	}
}
