create or replace view user_object_dependency_v as 
select max(level)   max_level
     , owner
     , object_name     
     , object_type     
from 
(select a.owner
      , a.object_name
      , a.object_type
      , b.referenced_name
      , b.referenced_type
 from dba_objects_v       a
     ,all_dependencies b
 where a.object_name = b.name (+)
 and   a.object_type = b.type (+)
 and   a.owner       = b.owner (+)
 and   a.owner LIKE user 
 and   a.object_name <> 'PATCH_INSTALLER'
 and   a.object_type in (
     'FUNCTION'
   , 'PROCEDURE'
   , 'PACKAGE'
   , 'PACKAGE BODY'
   , 'TYPE'
   , 'TYPE BODY'
   , 'VIEW'
   , 'MATERIALIZED VIEW'
   , 'TRIGGER'
   , 'SYNONYM') 
 and a.status='INVALID')
 connect by object_name = prior referenced_name
      and   object_type = prior referenced_type
 group by  owner
      , object_name     
      , object_type
 order by 1 DESC
         ,DECODE(object_type,'PACKAGE',1,'TYPE',1,'PACKAGE BODY',2,'TYPE BODY',2, 'SYNONYM',3,0);
