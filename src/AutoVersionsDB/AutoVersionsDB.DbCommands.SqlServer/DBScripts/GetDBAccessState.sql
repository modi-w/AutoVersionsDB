SELECT 1 as IsDBInSigleUserMode
FROM sys.databases 
WHERE name = '{dbName}'
	AND (user_access_desc='SINGLE_USER' OR state_desc='RESTORING')