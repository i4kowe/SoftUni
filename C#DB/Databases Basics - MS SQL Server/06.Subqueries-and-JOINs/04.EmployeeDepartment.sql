SELECT TOP 5
		e.EmployeeID,
		e.Firstname,
		e.Salary,
		d.Name AS [DepartmentName]
FROM Employees AS e
INNER JOIN Departments AS d
ON d.DepartmentID = e.DepartmentID
WHERE e.Salary > 15000
ORDER BY d.DepartmentID