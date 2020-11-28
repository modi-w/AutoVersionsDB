SELECT request_session_id AS SessionID
FROM sys.dm_tran_locks 
WHERE resource_database_id = DB_ID('{dbName}')
	AND request_session_id>50