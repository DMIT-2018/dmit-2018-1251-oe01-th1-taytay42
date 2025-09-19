<Query Kind="Statements">
  <Connection>
    <ID>1f02f3c6-d763-4494-849b-66409a24af55</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>TAYLORSPC\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <AlwaysPrefixWithSchemaName>true</AlwaysPrefixWithSchemaName>
    <Database>StartTed-2025-Sept</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

// Question 1
// Report Includes:
// Club activities scheduled on or after Jan. 1, 2025 (done)
// Omit Campus Venue labelled "Scheduled Room" and Name "BTech Club Meeting" (done)

// Each event must list:
// start date, venue name, hosting club's name, activity title (done)
// Present all entries in ascending order by start date (done)

ClubActivities
	.Where(x => x.StartDate.Value >= new DateTime(2025, 1, 1)
	&& x.CampusVenue.Location != "Scheduled Room"
	&& x.Name != "BTech Club Meeting")
	.Select(x => new
	{
		StartDate = x.StartDate,
		Location = x.CampusVenue.Location,
		Club = x.Club.ClubName,
		Activity = x.Name
	})
	.OrderBy(x => x.StartDate)
	.Dump();

// Question 2

// Must Map SchoolCode to its full school name (done?)
// Include program name (done)
// Count required courses & optional courses (done)
// Filter to only ones with required course count equal or greater than 22 (done)
// Order final list by program name (done)

Programs
	.Select(x => new
	{
		School = x.Schools.SchoolName,
		Program = x.ProgramName,
		RequiredCourseCount = x.ProgramCourses.Count(x => x.Required),
		OptionalCourseCount = x.ProgramCourses.Count(x => !x.Required)
	})
	.Where(x => x.RequiredCourseCount >= 22)
	.OrderBy(x => x.Program)
	.Dump();

// Question 3
// Filter Students to those with 0 entries in StudentPayments (done)
// + Country is not from Canada (done)
// Order by last name (done)
// for each student, include:
// Student Number, Full Country Name, Full Name, Club Membership Count (diusplays "none" if no clubs or memberships" (done)

Students
	.Where(x => x.StudentPayments.Count() == 0 
	&& x.CountryCode != "CA")
	.OrderBy(x => x.LastName)
	.Select(x => new
	{
		StudentNumber = x.StudentNumber,
		CountryName = x.Countries.CountryName,
		FullName = x.FirstName + " " + x.LastName,
		ClubMembershipCount = x.ClubMembers.Count() == 0 ? "None" : x.ClubMembers.Count().ToString(),
	})
	.Dump();

// Question 4
// Select employees with: 
// position "instructor", release date null, have taught at least 1 class in ClassOfferings (done)
// Order by descending number of class offering THEN by LastName (done)
// Include program name, full name, Workload Label (Over 24 offerings = "High", over 8 = "Medium", otherwise = "Low") (done)

Employees
	.Where(x => x.PositionID == 4
	&& x.ReleaseDate == null
	&& x.ClassOfferings.Count() > 0)
	.OrderByDescending(x => x.ClassOfferings.Count())
	.ThenBy(x => x.LastName)
		.Select(x => new
		{
			ProgramName = x.Program.ProgramName,
			FullName = x.FirstName + " " + x.LastName,
			WorkLoad = x.ClassOfferings.Count() > 24 ? "High" : x.ClassOfferings.Count() > 8 ? "Medium" : "Low"
		})
	.Dump();

// Question 5
// Contains: 
// Supervisor ("Unknown if Employee is null, otherwise full name) (done)
// Club Name (done)
// Member Count (# of entries in the ClubMembers table) (done)
// Activities (display "None Schedule" if Clubactivities.Count() == 0 (done)
// Order by member count in descending order (done)

Clubs
	.Select(x => new
	{
		Supervisor = x.Employee != null ? x.Employee.FirstName + " " + x.Employee.LastName : "Unknown",
		Club = x.ClubName,
		MemberCount = x.ClubMembers.Count(),
		Activities = x.ClubActivities.Count() > 0 ? x.ClubActivities.Count().ToString() : "None Schedule",
	})
	.OrderByDescending(x => x.MemberCount)
	.Dump();

