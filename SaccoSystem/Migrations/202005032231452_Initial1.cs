namespace SaccoSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberDetails", "Country", c => c.String());
            AddColumn("dbo.MemberDetails", "Branch", c => c.String());
            AddColumn("dbo.MemberDetails", "LoanRefID", c => c.String());
            AddColumn("dbo.MemberDetails", "ClientRefID", c => c.String());
            AddColumn("dbo.MemberDetails", "Surname", c => c.String());
            AddColumn("dbo.MemberDetails", "FirstName", c => c.String());
            AddColumn("dbo.MemberDetails", "IDNumber", c => c.String());
            AddColumn("dbo.MemberDetails", "EmployeeNo", c => c.String());
            AddColumn("dbo.MemberDetails", "PhoneNo", c => c.String());
            AddColumn("dbo.MemberDetails", "EmployerGroup", c => c.String());
            AddColumn("dbo.MemberDetails", "PaymentMethod", c => c.String());
            AddColumn("dbo.MemberDetails", "Product", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberDetails", "Product");
            DropColumn("dbo.MemberDetails", "PaymentMethod");
            DropColumn("dbo.MemberDetails", "EmployerGroup");
            DropColumn("dbo.MemberDetails", "PhoneNo");
            DropColumn("dbo.MemberDetails", "EmployeeNo");
            DropColumn("dbo.MemberDetails", "IDNumber");
            DropColumn("dbo.MemberDetails", "FirstName");
            DropColumn("dbo.MemberDetails", "Surname");
            DropColumn("dbo.MemberDetails", "ClientRefID");
            DropColumn("dbo.MemberDetails", "LoanRefID");
            DropColumn("dbo.MemberDetails", "Branch");
            DropColumn("dbo.MemberDetails", "Country");
        }
    }
}
