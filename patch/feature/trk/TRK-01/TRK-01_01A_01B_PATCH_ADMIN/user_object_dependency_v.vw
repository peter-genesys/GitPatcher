create or replace view user_object_dependency_v as 
select max(level)  max_level
     , object_id 
     , owner
     , object_name     
     , object_type     
from 
(select b.d_obj# object_id
      , b.p_obj# referenced_object_id
      , a.owner
      , a.object_name
      , a.object_type
 from dba_objects       a
     ,sys.dependency$ b
 where a.object_id = b.d_obj# (+)
 and   a.owner = USER 
   and a.object_type in (
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
connect by object_id = prior referenced_object_id
group by object_id 
     , owner
     , object_name     
     , object_type
order by 1 DESC
        ,DECODE(object_type,'PACKAGE',1,'TYPE',1,'PACKAGE BODY',2,'TYPE BODY',2, 'SYNONYM',3,0);
