CREATE OR REPLACE EDITIONABLE VIEW "USER_OBJECT_DEPENDENCY_V"(
  "MAX_LEVEL"
  ,"OBJECT_ID"
  ,"OWNER"
  ,"OBJECT_NAME"
  ,"OBJECT_TYPE"
)AS
  SELECT MAX(level)max_level
         ,object_id
         ,owner
         ,object_name
         ,object_type
  FROM(SELECT b.d_obj#   object_id
              ,b.p_obj#   referenced_object_id
              ,a.owner
              ,a.object_name
              ,a.object_type
       FROM dba_objects a
            ,sys.dependency$ b
       WHERE a.object_id = b.d_obj#(+)
             AND a.owner = user
             AND a.object_type IN(
         'FUNCTION'
         ,'PROCEDURE'
         ,'PACKAGE'
         ,'PACKAGE BODY'
         ,'TYPE'
         ,'TYPE BODY'
         ,'VIEW'
         ,'MATERIALIZED VIEW'
         ,'TRIGGER'
         ,'SYNONYM'
       )
             AND a.status = 'INVALID'
      )CONNECT BY
    object_id = PRIOR referenced_object_id
  GROUP BY object_id
           ,owner
           ,object_name
           ,object_type
  ORDER BY 1 DESC
           ,DECODE(object_type,'PACKAGE',1,'TYPE',1,'PACKAGE BODY',2,'TYPE BODY',2,'SYNONYM',3,0);


--GRANTS


--SYNONYMS
