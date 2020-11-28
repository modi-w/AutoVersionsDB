SELECT QUOTENAME(tbSchemas.name) AS SchemaName, QUOTENAME(tbObjects.name) AS ObjectName
FROM sys.objects AS tbObjects
    JOIN sys.schemas tbSchemas ON tbSchemas.schema_id = tbObjects.schema_id 
WHERE tbSchemas.name <> '{dbSchemaName}'
    AND (TYPE='U' OR TYPE='TF' OR TYPE='SF' OR TYPE='AF' OR TYPE='P')