using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Migrator.Framework;

namespace PgTodoExample.Migrations
{
    [Migration(1)]
    public class CreateTodoTable : Migrator.Framework.Migration
    {
        public override void Up()
        {
           Database.AddTable("Todo", new [] {
               new Column("Id", System.Data.DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
               new Column("Text", System.Data.DbType.String),
               new Column("Done", System.Data.DbType.Boolean, ColumnProperty.NotNull, false),
           });
        }

        public override void Down()
        {
            Database.RemoveTable("Todo");
        }
    }
}