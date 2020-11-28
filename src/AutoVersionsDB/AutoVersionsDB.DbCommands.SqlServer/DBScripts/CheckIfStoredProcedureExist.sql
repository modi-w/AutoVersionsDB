SELECT QUOTENAME(tbSchemas.name) AS SchemaName, QUOTENAME(tbObjects.name) AS ObjectName 
FROM sys.objects AS tbObjects 
    JOIN sys.schemas tbSchemas ON tbSchemas.schema_id = tbObjects.schema_id 
WHERE tbSchemas.name = '{schemaName}'
    AND tbObjects.name='{spName}'
    AND TYPE='P'