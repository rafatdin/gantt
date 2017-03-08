namespace Gantt.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        resource_id = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.resource_id);
            
            CreateTable(
                "dbo.ResourceInTasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        resource_id = c.Int(nullable: false),
                        task_id = c.Int(nullable: false),
                        date_from = c.DateTime(nullable: false),
                        date_to = c.DateTime(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Resources", t => t.resource_id, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.task_id, cascadeDelete: true)
                .Index(t => t.resource_id)
                .Index(t => t.task_id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        task_id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                        created = c.DateTime(nullable: false),
                        updated = c.DateTime(),
                    })
                .PrimaryKey(t => t.task_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResourceInTasks", "task_id", "dbo.Tasks");
            DropForeignKey("dbo.ResourceInTasks", "resource_id", "dbo.Resources");
            DropIndex("dbo.ResourceInTasks", new[] { "task_id" });
            DropIndex("dbo.ResourceInTasks", new[] { "resource_id" });
            DropTable("dbo.Tasks");
            DropTable("dbo.ResourceInTasks");
            DropTable("dbo.Resources");
        }
    }
}
