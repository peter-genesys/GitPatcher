create or replace view patches_unpromoted_v as
select *    
from patches_dependency_v 
where patch_name not in (select patch_name from patches_dependency_v@PATCH_ADMIN_FORWARD_DBLINK);
 