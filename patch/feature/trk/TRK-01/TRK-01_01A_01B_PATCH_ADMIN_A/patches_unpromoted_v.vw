create or replace view patches_unpromoted_v as
select *    
from patches_dependency_v 
where patch_name not in (select patch_name from patches_dependency_v@PATCH_ADMIN_FORWARD_DBLINK)
and patch_name not in (select supersedes_patch from patch_supersedes@PATCH_ADMIN_FORWARD_DBLINK)
and patch_name not in (select patch_name from patches@PATCH_ADMIN_FORWARD_DBLINK where retired_yn = 'Y');
 