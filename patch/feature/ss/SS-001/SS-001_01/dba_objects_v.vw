create or replace view dba_objects_v as 
select 
  user  as OWNER
 ,uo.*	
from user_objects uo;

