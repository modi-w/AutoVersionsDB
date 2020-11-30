SELECT 
    DB_NAME(dbid) as DBName, 
    COUNT(dbid) as NumberOfConnections,
    loginame as LoginName
FROM
    sys.sysprocesses
WHERE 
    dbid > 0
    AND  DB_NAME(dbid) = '{dbName}'
GROUP BY 
    dbid, loginame